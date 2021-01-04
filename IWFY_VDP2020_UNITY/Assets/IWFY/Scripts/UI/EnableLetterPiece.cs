using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableLetterPiece : MonoBehaviour {

    public TMPro.TextMeshProUGUI[] tmptext;

    GlobalState gs;
    void Update() {
        if (!gs) gs = FindObjectOfType<GlobalState>();
        for (int i = 0; i < gs.letters.Length; i++) {
            if (gs.letters[i] == 1) tmptext[i].gameObject.SetActive(true);
            else tmptext[i].gameObject.SetActive(false);
        }
    }
}
