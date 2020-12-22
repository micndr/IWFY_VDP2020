﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Rendering.PostProcessing;

[SerializeField]
public class SaveBin {
    public List<string> quests = new List<string>();
    public List<int> inventoryIDs = new List<int>();
    public List<int> inventoryQuantity = new List<int>();

    public Vector3 playerPos;
    public Vector3 playerRot;
    public Vector3 playerCamRot;

    public string currentLevel;
}

public class GlobalState : MonoBehaviour {
    public static GlobalState Instance;

    public List<string> completedQuests = new List<string>();
    public float globalVolume = 1;
    public int graphicsLevel = 2;
    public bool vsync = true;

    public ItemObject[] inventoryItems;
    public List<InventorySlot> inventoryBackup = new List<InventorySlot>();

    SaveBin loadbin;

    void Awake() {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += this.OnLoadCallback;

        inventoryItems = Resources.LoadAll<ItemObject>("Items");
    }

    public void AddQuest(QuestMain qm) {
        if (!completedQuests.Contains(qm.questName)) {
            print("added completed quest name " + qm.name);
            completedQuests.Add(qm.questName);
        }
    }

    public void SpawnpointPlayer() {
        if (SceneManager.GetActiveScene().name == "WorldHub") {
            if (completedQuests.Contains("Tutorial")) {
                Transform player = GameObject.FindGameObjectWithTag("Player").transform;
                Transform spawnpoint = GameObject.Find("PlayerPortalSpawnpoint").transform;
                player.position = spawnpoint.position;
                player.rotation = spawnpoint.rotation;
            }
        }
    }

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode) {
        if (loadbin != null) {
            ApplyBin(loadbin);
        } else {
            loadbin = null;
            SpawnpointPlayer();
        }
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

        BackupInventory();

        UpdateAudioVideo();
        UpdateGraphicLevel();
    }

    private void BackupInventory() {
        InventoryManager inventory = FindObjectOfType<InventoryManager>();
        inventoryBackup.Clear();
        inventoryBackup.AddRange(inventory.inventory.itemList);
    }

    private void UpdateAudioVideo() {
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        VideoPlayer[] videos = FindObjectsOfType<VideoPlayer>();

        for (int i = 0; i < audios.Length; i++) {
            audios[i].volume = globalVolume;
        }

        for (int i = 0; i < videos.Length; i++) {
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
        if (vsync) { QualitySettings.vSyncCount = 1; } else { QualitySettings.vSyncCount = 0; }
    }

    public void OnVolumeChanged(float volume) {
        globalVolume = volume;
        UpdateAudioVideo();
    }

    public void OnGraphicsChanged(int value) {
        graphicsLevel = value;
        UpdateGraphicLevel();
    }

    public void OnVsyncChanged(bool value) {
        vsync = value;
        UpdateVsync();
    }

    public SaveBin GetBin() {
        SaveBin bin = new SaveBin();
        bin.currentLevel = SceneManager.GetActiveScene().name;
        bin.quests.AddRange(completedQuests);

        // saves the items that were in the inventory on level load
        foreach (InventorySlot item in inventoryBackup) {
            bin.inventoryIDs.Add(item.itemID);
            bin.inventoryQuantity.Add(item.amount);
        }

        MoveFlat player = FindObjectOfType<MoveFlat>();
        bin.playerPos = player.transform.position;
        bin.playerRot = player.transform.rotation.eulerAngles;
        bin.playerCamRot = player.cam.rotation.eulerAngles;
        return bin;
    }

    public void Save() {
        SaveBin bin = GetBin();
        string raw = JsonUtility.ToJson(bin);
        File.WriteAllText(Application.persistentDataPath + "/save.json", raw);
    }

    public void Load() {
        string raw = File.ReadAllText(Application.persistentDataPath + "/save.json");
        SaveBin bin = JsonUtility.FromJson<SaveBin>(raw);
        loadbin = bin;
        SceneManager.LoadScene(bin.currentLevel);
    }

    public void ApplyBin(SaveBin bin) {
        completedQuests.Clear();
        completedQuests.AddRange(bin.quests);
        InventoryManager inventory = FindObjectOfType<InventoryManager>();
        inventory.inventory.itemList.Clear();
        for (int i = 0; i < bin.inventoryIDs.Count; i++) {
            ItemObject found = null;
            foreach (ItemObject item in inventoryItems) {
                if (item.itemID == bin.inventoryIDs[i]) {
                    found = item; break;
                }
            }
            if (found) {
                inventory.inventory.AddItem(found, bin.inventoryQuantity[i], bin.inventoryIDs[i]);
            } else {
                Debug.LogWarning("While loading, item id " + bin.inventoryIDs[i] + " was not found.");
            }
        }
        MoveFlat player = FindObjectOfType<MoveFlat>();
        player.characterController.enabled = false;
        player.transform.position = bin.playerPos;
        player.characterController.enabled = true;
        player.transform.rotation = Quaternion.Euler(bin.playerRot);
        player.cam.rotation = Quaternion.Euler(bin.playerCamRot);
    }

    public void EraseSavedData() {
        SaveBin bin = new SaveBin();
        bin.currentLevel = "WorldHub";
        string raw = JsonUtility.ToJson(bin);
        File.WriteAllText(Application.persistentDataPath + "/save.json", raw);
    }
}