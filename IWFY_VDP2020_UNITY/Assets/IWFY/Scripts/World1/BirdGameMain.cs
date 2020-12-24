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

    public List<int> seq = new List<int>(); // the correct sequence
    List<int> seqcurrent = new List<int>(); // the inputted seq.

    // sequence of sounds in the correct order
    public List<AudioClip> seqAudio = new List<AudioClip>();
    public AudioClip wholeClip; /* the complete theme */
    public AudioClip successClip; /* the success clip */

    public AudioMixer mixer;
    public float fadeinSpeed = 2;

    AudioSource audioSource;
    Text debugText;
    float playingTimer;
    float smoothvol = 1;
    float smoothvoltarget = 1;

    public void GiveSeed () {
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

    public void SuccessFeedback(int part) {
        if (part == -1) {
            Debug.Log("you win");
            PlayClip(successClip);
            return;
        }
        if (part == 1) Debug.Log("bababa");
        if (part == 2) Debug.Log("ba");
        if (part == 3) Debug.Log("baba");
        PlayClip(seqAudio[part - 1]);
    }

    public void FailFeedback () {
        PlayClip(wholeClip);
        Debug.Log("bababa ba baba bird");
    }

    public bool CheckCorrectSeq () {
        // alphabetic equality check
        for(int i=0; i<seqcurrent.Count; i++) {
            if (seq[i] != seqcurrent[i]) return false;
        }
        return true;
    }

    public void PlayClip (AudioClip clip) {
        playingTimer = Time.time + clip.length;
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PickupSeed (BirdGameSeedbag sb) {
        selseed = sb.seedtype;
        hudseed.GetComponent<MeshRenderer>().material = sb.mat;
    }

    void Awake() {
        // init and cache
        selseed = -1;
        hudseed = GameObject.Find("BirdGameHudSeed");
        audioSource = GetComponent<AudioSource>();
        if (!Application.isEditor) {
            GameObject.Find("BirdCanvas").SetActive(false);
        } else {
            debugText = GameObject.Find("BirdCanvas").transform.Find("BirdText").GetComponent<Text>();
        }
        playingTimer = 0;
    }

    void Update() {
        // deactivate hudseed if no seed is selected
        if (selseed == -1) {
            hudseed.SetActive(false);
        } else { hudseed.SetActive(true); }

        if (playingTimer > Time.time) {
            mixer.SetFloat("BirdVolume", 0);
            smoothvoltarget = 0.01f;
        } else {
            mixer.SetFloat("BirdVolume", -80);
            smoothvoltarget = 1;
        }

        smoothvol += (smoothvoltarget - smoothvol) * fadeinSpeed / 100f;
        mixer.SetFloat("NotBirdVolume", Mathf.Log10(smoothvol) * 20);
    }

}
