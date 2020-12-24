using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// changes a text based on a slider
public class SliderListenMap : MonoBehaviour {

    public string[] map;
    public Slider slider;

    TextMeshProUGUI text;

    void Start() {
        text = GetComponent<TextMeshProUGUI>();
    }

    void Update() {
        if ((int)slider.value >= 0 && (int)slider.value < map.Length) {
            text.text = map[(int)slider.value];
        }
    }
}
