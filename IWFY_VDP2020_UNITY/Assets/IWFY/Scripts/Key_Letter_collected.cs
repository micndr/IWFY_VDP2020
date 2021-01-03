using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key_Letter_collected : MonoBehaviour
{
    void CanvasOn()
    {
        GetComponent<Canvas>().enabled = true;
        StartCoroutine(CanvasOff());
    }

    private IEnumerator CanvasOff()
    {
        yield return new WaitForSeconds(3f);
        GetComponent<Canvas>().enabled = false;
    }
}
