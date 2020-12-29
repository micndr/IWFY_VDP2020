using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimController : MonoBehaviour {

    public List<Animation> anims = new List<Animation>();
    public float animTimer;
    public int current;
    public int oneshot = -1;
    public bool useoneshot = false;

    void Start() {
        anims[current].Play();
        animTimer = Time.time + anims[current].clip.length;
    }

    void Update() {
        if (animTimer < Time.time) {
            if (useoneshot) { useoneshot = false; } 
            else if (oneshot != -1) { current = oneshot; oneshot = -1; }
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

    public void OneShot (int anim) {
        oneshot = current;
        current = anim;
        animTimer = 0;
        useoneshot = true;
    }
}
