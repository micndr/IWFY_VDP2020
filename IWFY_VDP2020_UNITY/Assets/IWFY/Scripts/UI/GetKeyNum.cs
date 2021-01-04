using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GetKeyNum : MonoBehaviour {

    TMPro.TextMeshProUGUI tmptext;
    GlobalState gs;

    void Update() {
        if (!gs) gs = FindObjectOfType<GlobalState>();
        if (!tmptext) tmptext = GetComponent<TMPro.TextMeshProUGUI>();
        tmptext.text = "x1";
        string lvl = SceneManager.GetActiveScene().name;
        if (lvl.Substring(0, 5) == "World" && lvl != "WorldHub") {
            int index = int.Parse(lvl[5].ToString()) - 1;
            if (index < 4) {
                if (gs.keys[index] == 1) tmptext.text = "DONE";
            }
        }
    }
}
