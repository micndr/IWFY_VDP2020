using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockSelect : MonoBehaviour {

    List<ClockSelectArtifact> objsSelected = new List<ClockSelectArtifact>();
    List<ClockSelectArtifact> objsShowcase = new List<ClockSelectArtifact>();
    public bool selecting;
    public int selectedValue = 0;

    ClockGameMain main;

    void Start() {
        main = FindObjectOfType<ClockGameMain>();

        foreach (Transform child in transform.parent) {
            ClockSelectArtifact art = child.GetComponent<ClockSelectArtifact>();
            if (art) {
                if (!art.OpenSelection) {
                    objsShowcase.Add(child.gameObject.GetComponent<ClockSelectArtifact>());
                }
            }
        }
        foreach (Transform child in transform) {
            if (child.GetComponent<ClockSelectArtifact>()) {
                objsSelected.Add(child.gameObject.GetComponent<ClockSelectArtifact>());
            }
        }
        Reset();
    }

    public void Open () {
        foreach (ClockSelectArtifact child in objsShowcase) {
            child.gameObject.SetActive(true);
        }
    }

    public void Select (GameObject obj) {
        if (obj.GetComponent<ClockSelectArtifact>().OpenSelection) {
            selecting = !selecting;
            if (selecting) {
                Open();
            } else {
                Reset();
            }
        } else {
            ChangeSelected(obj.GetComponent<ClockSelectArtifact>().value);
            main.OnSelection();
            Close();
        }
    }

    public void ChangeSelected (int value) {
        selectedValue = value;
        foreach (ClockSelectArtifact child in objsSelected) {
            if (child.value == selectedValue) {
                child.gameObject.SetActive(true);
            } else {
                child.gameObject.SetActive(false);
            }
        }
    }

    public void Close() {
        foreach (ClockSelectArtifact child in objsShowcase) {
            child.gameObject.SetActive(false);
            child.GetComponent<Outline>().enabled = false;
        }
        selecting = false;
    }

    public void Reset() {
        Close();
        selectedValue = 0;
        ChangeSelected(selectedValue);
    }
}
