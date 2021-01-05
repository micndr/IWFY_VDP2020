using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

// bababa ba ba ba, the bird is the word
// bababa ba ba ba, the bird is the word
// bababa ba ba ba, the bird is the word
// bababa ba ba ba, the bird is the word

// game starts when activated, ends by activating a triggerer.
public class BirdGameMain : MonoBehaviour {

    int selseed;
    GameObject hudseed; /* seed in the player's hand */
    public MeshRenderer hudseedmat; /* seed in the player's hand */

    public List<int> seq = new List<int>(); // the correct sequence
    List<int> seqcurrent = new List<int>(); // the inputted seq.

    public AnimController birbAnim;
    public Animator birbgothere;
    public Animator ding;


    //AudioSource audioSource;
    Text debugText;
    float playingTimer;
    float endtimer = float.MaxValue;
    bool completed = false;

    AudioManager audioManager;

    void Awake() {
        // init and cache
        selseed = -1;
        hudseed = GameObject.Find("BirdGameHudSeed");
        if (!Application.isEditor) {
            GameObject.Find("BirdCanvas").SetActive(false);
        } else {
            debugText = GameObject.Find("BirdCanvas").transform.Find("BirdText").GetComponent<Text>();
        }
        playingTimer = 0;

        birbgothere = GameObject.Find("BirbGoThere").GetComponent<Animator>();
        birbAnim = GameObject.Find("BirbAnim").GetComponent<AnimController>();

        audioManager = FindObjectOfType<AudioManager>();
    }


    public void GiveSeed () {
        if (completed) return;
        if (selseed == -1) {
            FailFeedback();
            return;
        }
        seqcurrent.Add(selseed);
        selseed = -1;

        if (seqcurrent.Count >= seq.Count) {
            // the sequence is complete, check if it's correct
            if (CheckCorrectSeq()) {
                SuccessFeedback(-1);
                seqcurrent.Clear();
            } else {
                FailFeedback();
                seqcurrent.Clear();
            }
        } else {
            // the sequence is not complete, if correct play the part
            // otherwise play the whole 4 parts and restart.
            if (CheckCorrectSeq()) {
                SuccessFeedback(seqcurrent.Count);
            } else {
                FailFeedback();
                seqcurrent.Clear();
            }
        }

        if (debugText) {
            debugText.text = "(";
            for (int i = 0; i < seqcurrent.Count; i++) {
                debugText.text += seqcurrent[i].ToString();
                if (i < seqcurrent.Count - 1) debugText.text += ", ";
            }
            debugText.text += ")";
        }
    }

    public void EndKiteAnimationCallback () {
        GetComponent<Triggerer>().Trigger();
        completed = true;
    }

    public void SuccessFeedback(int part) {
        if (part == -1) {
            Destroy(GameObject.Find("Kite").gameObject, 5.5f);
            birbgothere.SetTrigger("getkite");
            PlayClip(10);
            endtimer = Time.time + 11f;
            return;
        }
        PlayClip(2+ part - 1);

        ding.SetTrigger("trigger");
        birbAnim.OneShot(4);
    }

    public void FailFeedback () {
        PlayClip(9);
        //Debug.Log("bababa ba baba bird");

        birbAnim.OneShot(2);
    }

    public bool CheckCorrectSeq () {
        // alphabetic equality check
        for(int i=0; i<seqcurrent.Count; i++) {
            if (seq[i] != seqcurrent[i]) return false;
        }
        return true;
    }

    public void PlayClip (int clip) {
        playingTimer = Time.time + 2;
        audioManager.PlayAmbient(clip);
    }

    public void PickupSeed (BirdGameSeedbag sb) {
        selseed = sb.seedtype;
        hudseedmat.material = sb.mat;
    }

    void Update() {
        // deactivate hudseed if no seed is selected
        if (selseed == -1) {
            hudseed.SetActive(false);
        } else { hudseed.SetActive(true); }

        if (endtimer < Time.time) {
            EndKiteAnimationCallback();
        }
    }

}
