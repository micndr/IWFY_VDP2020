using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableKeyArray : MonoBehaviour {

    public Image[] imgs;

    GlobalState gs;
    void Update() {
        if (!gs) gs = FindObjectOfType<GlobalState>();
        for (int i = 0; i < gs.keys.Length; i++) {
            if (gs.keys[i] == 1) imgs[i].gameObject.SetActive(true);
            else imgs[i].gameObject.SetActive(false);
        }
    }
}
