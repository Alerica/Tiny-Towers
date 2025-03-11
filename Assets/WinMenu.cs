using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class WinMenu : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }
}
