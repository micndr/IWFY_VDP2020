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
            if ((isPaused) && (!UI.activeSelf) && panelToBeOpened.activeSelf)
            {
                Debug.Log("Esc in preferences");
                panelToBeOpened.SetActive(false);
                UI.SetActive(true);
            }

            else if (isPaused)
            {
                ResumeGame();
            }
            else
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
        Debug.Log("pausa");
        isPaused = true;
        player.GetComponent<MoveFlat>().lockUserInput = true;
        player.GetComponent<MoveFlat>().freeCursor = true;
    }

    public void ResumeGame()
    {
        Debug.Log("riprendi");
        UI.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        player.GetComponent<MoveFlat>().lockUserInput = false;
        player.GetComponent<MoveFlat>().freeCursor = false;
    }

    public void SavingGame()
    {
        Debug.Log("metodo di salvataggio delle partite");
        GlobalState globalState = FindObjectOfType<GlobalState>();
        globalState.Save();
    }

    public void preferences()
    {
        Debug.Log("apertura modifica impostazioni");
        this.UI.SetActive(false);
        panelToBeOpened.SetActive(true);
    }

    public void QuitGame()
    {
        SavingGame();
        Debug.Log("metodo di chiusura delle partite");
        Application.Quit();
    }
}