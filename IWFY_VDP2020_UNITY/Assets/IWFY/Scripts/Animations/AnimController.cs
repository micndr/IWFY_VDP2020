using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour {

    public List<Animation> anims = new List<Animation>();
    public float animTimer;
    public int current;

    void Start() {
        anims[current].Play();
        animTimer = Time.time + anims[current].clip.length;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.T)) {
            SetAnim(0);
            animTimer = 0;
        }
        if (Input.GetKeyDown(KeyCode.Y)) {
            SetAnim(1);
            animTimer = 0;
        }

        if (animTimer < Time.time) {
            anims[current].Play();
            animTimer = Time.time + anims[current].clip.length;
        }

        for (int i=0; i<anims.Count; i++) {
            if (i != current) {
                anims[i].gameObject.SetActive(false);
            } else {
                anims[i].gameObject.SetActive(true);
            }
        }
    }

    public void SetAnim (int anim) {
        current = anim;
    }
}
