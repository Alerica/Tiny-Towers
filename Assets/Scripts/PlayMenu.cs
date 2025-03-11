using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject levelUI; // Assign in Inspector

    public void ToggleLevelUI()
    {
        levelUI.SetActive(true); 
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneByIndex(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit"); 
    }
}
