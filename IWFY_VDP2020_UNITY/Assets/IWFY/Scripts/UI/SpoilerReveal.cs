using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpoilerReveal : MonoBehaviour {

    public void Start() {
        transform.Find("Text").gameObject.SetActive(true);
        transform.Find("Spoiler").gameObject.SetActive(false);
    }

    public void OnClick () {
        // transform.Find("Text").gameObject.SetActive(false);
        transform.Find("Spoiler").gameObject.SetActive(true);
    }
}
