using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullscreenToggle : MonoBehaviour {

    Toggle toggle;

    private void Awake() {
        toggle = GetComponent<Toggle>();
    }

    public void onValueChanged() {
        Screen.SetResolution(Screen.width, Screen.height, toggle.isOn);
    }
}
