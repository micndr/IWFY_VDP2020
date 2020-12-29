using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResDropdown : MonoBehaviour {

    Dropdown drop;
    List<Resolution> ress = new List<Resolution>();

    void Awake() {
        // filter to 16:9
        foreach (Resolution res in Screen.resolutions) {
            if (res.width >= 800) {
                ress.Add(res);
            }
        }

        drop = GetComponent<Dropdown>();
        foreach (Resolution res in ress) {
            drop.options.Add(new Dropdown.OptionData(res.ToString()));
        }
    }

    public void OnValueChanged () {
        Resolution res = ress[drop.value];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
        }
    }
}
