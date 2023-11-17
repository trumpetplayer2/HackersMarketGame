using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void SwitchScenes(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void exit()
    {
        Application.Quit();
    }
}
