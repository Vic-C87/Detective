using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject credits; 
    int currentScene;
    int tapeSelectionSceneIndex;

    void Start() 
    {
        currentScene = SceneManager.GetActiveScene().buildIndex;
        tapeSelectionSceneIndex = currentScene + 1;
    }

    public void CreditsButton() 
    {
        credits.SetActive(true);
    }

    public void Play() 
    {
        SceneManager.LoadScene(tapeSelectionSceneIndex);
    }

    public void Quit() 
    {
        Application.Quit();
    }
}
