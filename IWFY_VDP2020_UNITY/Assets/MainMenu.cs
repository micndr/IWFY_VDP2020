using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
        [SerializeField] private string _scene;
        
        public void PlayGame()
        {
                SceneManager.LoadScene(_scene);
        }

        public void QuitGame()
        {
            Debug.Log("Close game");
            Application.Quit();
        }
}
