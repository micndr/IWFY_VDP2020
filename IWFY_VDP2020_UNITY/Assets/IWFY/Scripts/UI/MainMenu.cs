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
        AudioManager am = FindObjectOfType<AudioManager>();
        am.StopOstForSceneChange();
        StartCoroutine(startGame());
    }

    public void LoadGame() {
        AudioManager am = FindObjectOfType<AudioManager>();
        am.StopOstForSceneChange();
        StartCoroutine(startGame());
    }

    IEnumerator startGame() {
        yield return new WaitForSeconds(0.25f);
        GlobalState globalState = FindObjectOfType<GlobalState>();
        globalState.Load();
    }

    public void QuitGame() {
        Debug.Log("Close game");
        Application.Quit();
    }
}
