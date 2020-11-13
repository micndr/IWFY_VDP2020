using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class wait_canvas : MonoBehaviour
{
    public float sec = 14f;
    void Start()
    {
        if (gameObject.activeInHierarchy == false)
            gameObject.SetActive(true);

        StartCoroutine(LateCall());
    }

    IEnumerator LateCall()
    {

        yield return new WaitForSeconds(sec);

        gameObject.SetActive(false);
        //Do Function here...
    }
}
