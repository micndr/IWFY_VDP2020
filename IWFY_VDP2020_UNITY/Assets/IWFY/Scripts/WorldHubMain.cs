using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is the script for the tutorial

public class WorldHubMain : MonoBehaviour {

    GameObject wallsHouse;
    PopupLogic popupJimmy;
    PopupLogic popupShovel;
    PopupLogic popupGarden;
    GameObject portal;

    void Start() {
        portal = GameObject.Find("portal");

        wallsHouse = GameObject.Find("HouseCaptivity");
        popupJimmy = GameObject.Find("PopupJimmy").GetComponent<PopupLogic>();
        popupShovel = GameObject.Find("PopupShovel").GetComponent<PopupLogic>();
        popupGarden = GameObject.Find("PopupGarden").GetComponent<PopupLogic>();

        portal.SetActive(false);
        popupShovel.gameObject.SetActive(false);
        popupGarden.gameObject.SetActive(false);
    }

    void Update() {
        if (popupJimmy.pressed) {
            popupJimmy.gameObject.SetActive(false);
            popupShovel.gameObject.SetActive(true);
            wallsHouse.SetActive(false);
        }
        if (popupShovel.pressed) {
            popupShovel.gameObject.SetActive(false);
            popupGarden.gameObject.SetActive(true);
        }
        if (popupGarden.pressed) {
            popupGarden.gameObject.SetActive(false);
            portal.SetActive(true);
        }
    }
}
