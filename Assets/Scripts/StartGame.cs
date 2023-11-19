using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public string SceneName;

    public void LoadGame()
    {
        SceneManager.LoadScene(SceneName);
    }
}
