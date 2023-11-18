using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Windows;

//This design was heavily inspired by TaroDev, though redone for my own uses.
//https://github.com/Matthew-J-Spencer/Ultimate-2D-Controller
public class PlayerMovement : MonoBehaviour
{
    
    #region Core
    [Header("Interaction Masks")]
    [SerializeField] private LayerMask solidLayerMask;
    [SerializeField] private LayerMask headCollisionMask;
    
    private Rigidbody2D _rb2d;
    private BoxCollider2D _boxCollider;
    private PlayerMovementInput _playerMovementInput;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
        _playerMovementInput = GetComponentInChildren<PlayerMovementInput>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        InitializeAnimation();

    }

    private void Update()
    {
        
        GetInput();
        Move();
        UpdateSpriteAnimation();
    }
    #endregion




    #region Movement
    [Header("Movement")]
    [SerializeField] private float speedScale = 6.0f;
    [SerializeField] private float jumpScale = 25.0f;

    private Vector2 _velocity = Vector2.zero;
    public Vector2 GetVelocity => _velocity;
    private int _directionX = 1;
    public int GetDirectionX => _directionX;
    private PlayerMovementInput.MovementInput _input;
    
    //Moves the player according to physics and their input.
    private void Move()
    {
       
        _velocity.x = _input.DirX * speedScale;

        //sticky direction for the dart script 
        if (_input.DirX > 0)
        {
            _directionX = 1;
        }
        if (_input.DirX < 0)
        {
            _directionX = -1;
        }
     
        UpdateGravity();
        CheckGrounding();
        UpdateJump();

        _rb2d.velocity = new Vector2(_velocity.x, _velocity.y);
    }
    #endregion

    #region Animation

    public Sprite[] idleFrames;
    public Sprite[] upFrames;
    public Sprite[] runFrames; 
    public Sprite[] fallFrames; 
    private List<Sprite[]> allSprites;
    private bool doAnimation = true;

    public int currentAnimation;
    public int currentFrame;
    public float framesPerSecond = 10;
    private float frameTimer;

    private void InitializeAnimation()
    {
        allSprites = new List<Sprite[]>();
        allSprites.Add(idleFrames);
        allSprites.Add(runFrames); 
        allSprites.Add(upFrames);
        allSprites.Add(fallFrames);
        if (idleFrames.Length + runFrames.Length + upFrames.Length + fallFrames.Length == 0)
        {
            doAnimation = false;
        }
    }

    private void UpdateSpriteAnimation()
    {
        
        //flip the animation if it is going left in any way
       
        //if character is on the ground
        if(_directionX == -1)
        {
            _spriteRenderer.flipX = true;
        }
        else
        {
            _spriteRenderer.flipX = false; 
        }
        //if the character is in the air
        if (!_isGrounded)
        {
            if (_directionX == -1)
            {
                _spriteRenderer.flipX = true;
            }
            else
            {
                _spriteRenderer.flipX = false;
            }

            //if the cat is going up
            if (_rb2d.velocity.y > 0)
            {
                currentAnimation = 2;
            }
            //if the cat is going down 
            else if (_rb2d.velocity.y < 0)
            {
                currentAnimation = 3;
            }

        }

        //if the character is on the ground 
        else
        {
            //running animations 
            if(_input.DirX != 0)
            {
                currentAnimation = 1;

                //if it's going left, flip the animation 
                if(_input.DirX < 0)
                {
                    _spriteRenderer.flipX = true;
                }
                else
                {
                    _spriteRenderer.flipX = false;
                }
            }

            //idle animations 
            else
            {
                currentAnimation = 0; 
            }

        }

        if (!doAnimation) return;
        
        
        //the issue is that if the animation doesn't complete, then it doesnt go back to the idle animation 
        frameTimer -= Time.deltaTime;
        if (frameTimer <= 0)
        {
            _spriteRenderer.sprite = allSprites[currentAnimation][currentFrame];
            frameTimer = 1 / framesPerSecond;
            currentFrame++;
            if (currentFrame >= allSprites[currentAnimation].Length)
            {
                currentFrame = 0;
            }
        }
        //update with the currentSprite
        _spriteRenderer.sprite = allSprites[currentAnimation][currentFrame];


    }

    #endregion 

    #region Gravity
    [Header("Gravity")]
    [SerializeField] private float minGravity = 80f;
    [SerializeField] private float maxGravity = 120f;
    [SerializeField] private float maxFallSpeed = 20f;
    [SerializeField] private float jumpApexThreshold = 10f;

    //Much of this logic is from TaroDev
    //https://github.com/Matthew-J-Spencer/Ultimate-2D-Controller
    private void UpdateGravity()
    {
        if (_headCollision && !_wasHeadCollision)
        {
            _velocity.y = 0;
        }

        float apexPoint = Mathf.InverseLerp(jumpApexThreshold, 0, Mathf.Abs(_velocity.y));
        float fallSpeed = Mathf.Lerp(minGravity, maxGravity, apexPoint);
        _velocity.y -= fallSpeed * Time.deltaTime;

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = 0;
        }
        else
        {
            _velocity.y = Mathf.Max(_velocity.y, -maxFallSpeed);
        }
    }

    #endregion

    #region Grounding
    [Header("Grounding")]
    
    [SerializeField] private int numGroundingDetectors = 3;
    [SerializeField] private float groundingDetectorLength = 0.02f;

    private bool _isGrounded = false;
    private bool _wasGrounded = false;
    private bool _headCollision = false;
    private bool _wasHeadCollision = false;

    private void CheckGrounding()
    {
        _wasGrounded = _isGrounded;
        _wasHeadCollision = _headCollision;

        Bounds bounds = _boxCollider.bounds;
        float detectorStartX = bounds.min.x;
        float detectorEndX = bounds.max.x;
        float detectorYTop = bounds.max.y;
        float detectorYBottom = bounds.min.y;
        
        _isGrounded = GetDetectorPositions(detectorStartX, detectorEndX, detectorYBottom).Any(startingPoint => 
            Physics2D.Raycast(
                startingPoint, Vector2.down, groundingDetectorLength,  solidLayerMask
            ).collider != null
        );
        _headCollision = GetDetectorPositions(detectorStartX, detectorEndX, detectorYTop).Any(startingPoint => 
            Physics2D.Raycast(
                startingPoint, Vector2.up, groundingDetectorLength,  headCollisionMask
            ).collider != null
        );
    }

    private IEnumerable<Vector2> GetDetectorPositions(float detectorStartX, float detectorEndX, float detectorY)
    {
        float deltaT = (float)1 / (numGroundingDetectors - 1);
        float t = 0.0f;
        
        for (int i = 0; i < numGroundingDetectors - 1; i++)
        {
            yield return new Vector2(Mathf.Lerp(detectorStartX, detectorEndX, t), detectorY);
            t += deltaT;
        }
        yield return new Vector2(detectorEndX, detectorY);
    }

    #endregion
    
    #region Jump
    [Header("Coyote Jump")]
    [SerializeField] private float coyoteJumpTime = 0.15f;
    
    private float _coyoteJumpCurrentTimer = 0.0f;

    private bool _canJump = false;
    
    private void UpdateJump()
    {
        if (!_wasGrounded && _isGrounded)
        {
            _canJump = true;
        }

        if (_isGrounded)
        {
            _coyoteJumpCurrentTimer = coyoteJumpTime;
        }
        else
        {
            _coyoteJumpCurrentTimer -= Time.deltaTime;
        }
        
        if (_canJump && _input.DoJump && _coyoteJumpCurrentTimer >= 0 && _velocity.y <= 0)
        {
            _canJump = false;
            _velocity.y = jumpScale;
        }

    }
    #endregion
    
    #region Input
    
    //Gets the player's current input.
    private void GetInput()
    {
        _input = _playerMovementInput.GetMovementInput();
    }
    
    //A struct for storing the player's input.
    

    #endregion
    
    #region Gizmos
    private void OnDrawGizmos()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
        Bounds bounds = _boxCollider.bounds;
        float detectorStartX = bounds.min.x;
        float detectorEndX = bounds.max.x;
        float detectorYTop = bounds.max.y;
        float detectorYBottom = bounds.min.y;
        
        Gizmos.color = Color.blue;
        foreach (Vector2 startingPoint in GetDetectorPositions(detectorStartX, detectorEndX, detectorYBottom))
        {
            Gizmos.DrawRay(startingPoint, Vector2.down * groundingDetectorLength);    
        }
        Gizmos.color = Color.yellow;
        foreach (Vector2 startingPoint in GetDetectorPositions(detectorStartX, detectorEndX, detectorYTop))
        {
            Gizmos.DrawRay(startingPoint, Vector2.up * groundingDetectorLength);    
        }
    }
    #endregion
}