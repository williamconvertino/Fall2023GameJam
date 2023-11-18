using System.Collections.Generic;
using UnityEngine;
using Bounds = UnityEngine.Bounds;
using Mathf = UnityEngine.Mathf;
using Transform = UnityEngine.Transform;
using Vector3 = UnityEngine.Vector3;

//This code is taken from this stack overflow post:
//https://stackoverflow.com/questions/22015697/how-to-keep-2-objects-in-view-at-all-time-by-scaling-the-field-of-view-or-zy/22018593#22018593
public class DynamicCamera : MonoBehaviour
{
    public List<Transform> targets;
    public float padding = 30f; // amount to pad in world units from screen edge

    Camera _camera;
    private bool _initiated = false;

    private void Start()
    {
        _camera = gameObject.GetComponent<Camera>();
    }

    private void LateUpdate() // using LateUpdate() to ensure camera moves after everything else has
    {
        if (!_initiated)
        {
            return;
        }
        Bounds bounds = FindBounds();

        // Calculate distance to keep bounds visible. Calculations from:
        //     "The Size of the Frustum at a Given Distance from the Camera": https://docs.unity3d.com/Manual/FrustumSizeAtDistance.html
        //     note: Camera.fieldOfView is the *vertical* field of view: https://docs.unity3d.com/ScriptReference/Camera-fieldOfView.html
        float desiredFrustumWidth = bounds.size.x + 2 * padding;
        float desiredFrustumHeight = bounds.size.y + 2 * padding;

        float distanceToFitHeight = desiredFrustumHeight * 0.5f / Mathf.Tan(_camera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float distanceToFitWidth = desiredFrustumWidth * 0.5f / Mathf.Tan(_camera.fieldOfView * _camera.aspect * 0.5f * Mathf.Deg2Rad);

        float resultDistance = Mathf.Max(distanceToFitWidth, distanceToFitHeight);

        // Set camera to center of bounds at exact distance to ensure targets are visible and padded from edge of screen 
        _camera.transform.position = bounds.center + Vector3.back * resultDistance;
        _camera.transform.position = new Vector3(_camera.transform.position.x, _camera.transform.position.y, -10);
    }

    private Bounds FindBounds()
    {
        if (targets.Count == 0)
        {
            return new Bounds();
        }

        Bounds bounds = new Bounds(targets[0].position, Vector3.zero);
        foreach (Transform target in targets)
        {
            if (target.gameObject.activeSelf) // if target not active
            {
                bounds.Encapsulate(target.position);
            }
        }

        return bounds;
    }
}

