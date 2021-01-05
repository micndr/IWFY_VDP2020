using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class PauseMenuController : MonoBehaviour
{
    public static bool isPaused = false;

    [SerializeField] private GameObject UI;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject panelToBeOpened;


    private void Awake() {
        if (!player) player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // if ((isPaused) && (!UI.activeSelf) && panelToBeOpened.activeSelf)
            // {
            //     panelToBeOpened.SetActive(false);
            //     UI.SetActive(true);
            // }
            //
            // else if (isPaused)
            // {
            //     ResumeGame();
            // }
            // else
            {
                Paused();
            }
        }
    }

    private void Paused()
    {
        Cursor.lockState = CursorLockMode.None;
        UI.SetActive(true);
        Time.timeScale = 0;

        isPaused = true;
        player.GetComponent<MoveFlat>().lockUserInput = true;
        player.GetComponent<MoveFlat>().freeCursor = true;
    }

    public void ResumeGame()
    {
        UI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<MoveFlat>().lockUserInput = false;
        player.GetComponent<MoveFlat>().freeCursor = false;
    }

    public void SavingGame()
    {
        GlobalState globalState = FindObjectOfType<GlobalState>();
        //globalState.Save(); save only at level load (so at every portal)
    }

    public void preferences()
    {
        this.UI.SetActive(false);
        panelToBeOpened.SetActive(true);
        Debug.Log("vieni");
    }

    public void QuitGame()
    {
        SavingGame();
        Application.Quit();
    }
}