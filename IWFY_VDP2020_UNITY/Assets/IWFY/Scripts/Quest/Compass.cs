using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour {

    public GameObject arrow;
    public Transform target;


    void Update() {
        if (target) {
            arrow.gameObject.SetActive(true);
            Vector3 toTarget = target.position - transform.position;
            arrow.transform.rotation = Quaternion.LookRotation(toTarget.normalized, transform.position.normalized);
            arrow.transform.localEulerAngles = new Vector3(0, arrow.transform.localEulerAngles.y, 0);
        } else {
            arrow.gameObject.SetActive(false);
        }
    }
}
