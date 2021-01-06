using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string _scene;
        
    public void PlayGame() {
        GlobalState globalState = FindObjectOfType<GlobalState>();
        globalState.EraseSavedData();
        globalState.Load();
    }

    public void LoadGame() {
        GlobalState globalState = FindObjectOfType<GlobalState>();
        globalState.Load();
    }

    public void QuitGame() {
        Debug.Log("Close game");
        Application.Quit();
    }
}
