using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseMenuDisappear : MonoBehaviour
{
    public Image image;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (image.enabled)
                image.enabled = false;
        }
    }
}
