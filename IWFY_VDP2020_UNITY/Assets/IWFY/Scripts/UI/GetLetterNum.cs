using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetLetterNum : MonoBehaviour {

    TMPro.TextMeshProUGUI tmptext;
    GlobalState gs;

    void Update() {
        if (!gs) gs = FindObjectOfType<GlobalState>();
        if (!tmptext) tmptext = GetComponent<TMPro.TextMeshProUGUI>();
        tmptext.text = "x1";
        string lvl = SceneManager.GetActiveScene().name;
        if (lvl.Substring(0, 5) == "World") {
            if (lvl == "WorldHub") {
                // there are two letters in worldhub
                tmptext.text = "x2";
                if (gs.letters[0] == 1) tmptext.text = "x1";
                if (gs.letters[0] + gs.letters[6] == 2) tmptext.text = "DONE";
            } else {
                int index = int.Parse(lvl[5].ToString());
                if (gs.letters[index] == 1) tmptext.text = "DONE";
            }
        }
    }
}
