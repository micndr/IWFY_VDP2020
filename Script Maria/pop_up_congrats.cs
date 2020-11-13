using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pop_up_congrats : MonoBehaviour
{
    public float sec = 14f;
    public GameObject nextObj;

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
        nextObj.SetActive(true);
        //Do Function here...
    }
}
