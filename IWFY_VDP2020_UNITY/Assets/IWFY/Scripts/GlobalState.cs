using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

using UnityEngine.Rendering.PostProcessing;


public class GlobalState : MonoBehaviour {
    public static GlobalState Instance;

    public List<string> completedQuests = new List<string>();
    public float globalVolume = 1;
    public int graphicsLevel = 2;
    public bool vsync = true;

    void Awake() {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += this.OnLoadCallback;
    }

    public void AddQuest (QuestMain qm) {
        if (!completedQuests.Contains(qm.questName)) {
            print("added completed quest name " + qm.name);
            completedQuests.Add(qm.questName);
        }
    }

    public void SpawnpointPlayer() {
        if (SceneManager.GetActiveScene().name == "WorldHub") {
            if (completedQuests.Contains("Tutorial")) {
                Transform player = GameObject.Find("Player").transform;
                Transform spawnpoint = GameObject.Find("PlayerPortalSpawnpoint").transform;
                player.position = spawnpoint.position;
                player.rotation = spawnpoint.rotation;
            }
        }
    }

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode) {
        SpawnpointPlayer();
        var qms = FindObjectsOfType<QuestMain>();
        foreach (QuestMain qm in qms) {
            if (completedQuests.Contains(qm.questName)) {
                print("restoring completed quest name " + qm.name);
                // TODO: change quest completion
                qm.state = qm.stateNames.Length - 1;
                qm.completed = true;
                qm.CheckCompletion();
            }
        }

        UpdateAudioVideo();
        UpdateGraphicLevel();
    }

    private void UpdateAudioVideo()
    {
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        VideoPlayer[] videos = FindObjectsOfType<VideoPlayer>();
        
        for (int i = 0; i < audios.Length; i++)
        {
            audios[i].volume = globalVolume; 
        }
        
        for (int i = 0; i < videos.Length; i++)
        {
            videos[i].SetDirectAudioVolume(0, globalVolume); 
        }
    }

    public void UpdateGraphicLevel() {
        if (graphicsLevel < 2) {
            var ppvs = FindObjectsOfType<PostProcessVolume>();
            foreach (PostProcessVolume ppv in ppvs) { ppv.enabled = false; }
            var ppls = FindObjectsOfType<PostProcessLayer>();
            foreach (PostProcessLayer ppl in ppls) { ppl.enabled = false; }
        }
        if (graphicsLevel < 1) {
            QualitySettings.SetQualityLevel(0, true);
        } else {
            QualitySettings.SetQualityLevel(5, true);
        }
    }

    public void UpdateVsync() { 
        if (vsync) { QualitySettings.vSyncCount = 1; }
        else { QualitySettings.vSyncCount = 0; }
    }

    public void OnVolumeChanged(float volume)
    {
        globalVolume = volume;
        UpdateAudioVideo();
    }

    public void OnGraphicsChanged (int value) {
        graphicsLevel = value;
        UpdateGraphicLevel();
    }

    public void OnVsyncChanged (bool value) {
        vsync = value;
        UpdateVsync();
    }
}