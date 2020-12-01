using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxThunderTrigger : MonoBehaviour {

    FxThunderstorm thunderstorm;

    public void Strike() {
        if (!thunderstorm) {
            GameObject thunderstormObj = GameObject.Find("FxThunderFactory");
            if (thunderstormObj) {
                thunderstorm = thunderstormObj.GetComponent<FxThunderstorm>();
                thunderstorm.Strike(transform.position, transform.position.normalized);
            } 
        }
    }
}
