using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class load_scene : MonoBehaviour
{
    public string scene_name;
    public void OnMouseDown()
    {
        SceneManager.LoadScene(scene_name);
    }

}
