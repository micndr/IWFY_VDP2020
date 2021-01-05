using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResDropdown : MonoBehaviour {

    Dropdown drop;
    List<Resolution> ress = new List<Resolution>();

    void Awake() {
        // filter to 16:9
        float epsilon = 0.05f;
        foreach (Resolution res in Screen.resolutions) {
            float ratio = (float)res.width / res.height;
            if (res.width >= 800
                && ratio > 16.0f / 9.0f - epsilon
                && ratio < 16.0f / 9.0f + epsilon) {
                ress.Add(res);
            }
        }

        drop = GetComponent<Dropdown>();
        for (int i = 0; i < ress.Count; i++) {
            Resolution res = ress[i];
            drop.options.Add(new Dropdown.OptionData(res.ToString()));
            if (Screen.width == res.width && Screen.height == res.height) 
            {
                drop.value = i;
            }
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
