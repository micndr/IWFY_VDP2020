using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour {

    public GameObject arrow;
    public Transform target; // target is assigned by QuestMain


    void Update() {
        if (target) {
            // deactivate for the time being
            target = null;

            arrow.gameObject.SetActive(true);
            // calculate the look rotation to rotate the visualizer
            Vector3 toTarget = target.position - transform.position;
            arrow.transform.rotation = Quaternion.LookRotation(toTarget.normalized, transform.position.normalized);
            // then use only the y component to make it viable in a globe shaped earth
            arrow.transform.localEulerAngles = new Vector3(0, arrow.transform.localEulerAngles.y, 0);
        } else {
            // no target, no compass visualizer
            arrow.gameObject.SetActive(false);
        }
    }
}
