using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BirdGameBird : MonoBehaviour {
    BirdGameMain main;

    void Awake() {
        main = FindObjectOfType<BirdGameMain>();
    }

    public virtual void OnPointerClick() {
        main.GiveSeed();
    }
}
