using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.Rendering.PostProcessing;

using TMPro;

using System.Linq;


// what is saved is thrown in here
[SerializeField]
public class SaveBin {
    public List<string> quests = new List<string>();
    public List<int> inventoryIDs = new List<int>();
    public List<int> inventoryQuantity = new List<int>();

    public Vector3 playerPos;
    public Vector3 playerRot;
    public Vector3 playerCamRot;

    public string currentLevel;

    public int[] keys;
    public int[] letters;
}

// quest and setting synchronizer, saves/loads too
public class GlobalState : MonoBehaviour {
    public static GlobalState Instance;

    public List<string> completedQuests = new List<string>();
    public float globalVolume = 1;
    public int graphicsLevel = 2;
    public bool vsync = true;

    public ItemObject[] inventoryItems;
    public List<InventorySlot> inventoryBackup = new List<InventorySlot>();

    SaveBin loadbin = null;

    public int[] keys = { 0, 0, 0, 0 };
    public int[] letters = { 0, 0, 0, 0, 0, 0, 0 };

    void Awake() {
        // monolithic pattern
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        } else if (Instance != this) {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += this.OnLoadCallback;

        // get a list of items, to reconstruct the inventory on load.
        inventoryItems = Resources.LoadAll<ItemObject>("Items");
    }

    public void AddQuest(QuestMain qm) {
        // called by QuestMain when it's completed
        if (!completedQuests.Contains(qm.questName)) {
            //print("added completed quest name " + qm.name);
            completedQuests.Add(qm.questName);
        }
    }

    public void SpawnpointPlayer() {
        // move the player to spawnpoint (does not do it until tutorial is completed)
        if (completedQuests.Contains("Tutorial")) {
            Transform player = GameObject.FindGameObjectWithTag("Player").transform;
            Transform spawnpoint = GameObject.Find("PlayerPortalSpawnpoint").transform;
            CharacterController ch = player.GetComponent<CharacterController>();
            ch.enabled = false;
            player.position = spawnpoint.position;
            player.rotation = spawnpoint.rotation;
            ch.enabled = true;
        }
    }

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode) {
        if (scene.name != "Menu") {
            if (!this) return;
            if (loadbin != null) {
                ApplyBin(loadbin);
            } else {
                loadbin = null;
                SpawnpointPlayer();
            }

        // set the completed quests to their last state
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

            // save current inventory to a local list
            // BackupInventory();
            Save();

            // synchronize settings
            //UpdateAudioVideo();
            UpdateGraphicLevel();

            Invoke("SetupPortalsAntiPrefabOverwriting", 0.2f);
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

    #region Portals
    public void ModifyPortal(PortalController portal, bool active, QuestMain[] mains = null, string dest="", string questname="", string canvas="") {
        print(portal.name + " active " + active.ToString());
        if (active) {
            portal._newscene = dest;
            QuestLock ql = portal.GetComponent<QuestLock>();
            ql.enabled = true;
            ql.main = mains.Where(m => m.questName == questname).ToArray()[0];
            ql.state = ql.main.lenght;
            portal.transform.parent.GetComponentInChildren<Canvas>().transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = canvas;
        } else {
            portal.transform.parent.GetComponentInChildren<Canvas>().gameObject.SetActive(false);
            portal.transform.parent.Find("Portal.emissive" + portal.transform.name[6]).gameObject.SetActive(false);
            portal.gameObject.SetActive(false);
        }
    }

    public void SetupPortalsAntiPrefabOverwriting () {
        // :(
        string name = SceneManager.GetActiveScene().name;
        if (name == "Menu") return;
        QuestMain[] mains = FindObjectsOfType<QuestMain>();
        PortalController[] portals = FindObjectsOfType<PortalController>();
        if (name == "WorldHub") {
            foreach(PortalController portal in portals) {
                if (portal.transform.parent.name == "Portal1") {
                    ModifyPortal(portal, true, mains, "World1", "Tutorial", "to world 1");
                }
                if (portal.transform.parent.name == "Portal2") {
                    ModifyPortal(portal, true, mains, "World2", "AfterWorld1", "to world 2");
                }
                if (portal.transform.parent.name == "Portal3") {
                    ModifyPortal(portal, true, mains, "World3", "AfterWorld2", "to world 3");
                }
                if (portal.transform.parent.name == "Portal4") {
                    ModifyPortal(portal, true, mains, "World4", "AfterWorld3", "to world 4");
                }
                if (portal.transform.parent.name == "Portal5") {
                    ModifyPortal(portal, true, mains, "World5", "AfterWorld4", "to world 5");
                }
            }
        } else {
            string[] questnames = { "Chasm", "Pixie", "Mirror", "Coin", "TheEnd" };
            int i = int.Parse(name[5].ToString());
            foreach (PortalController portal in portals) {
                if (portal.transform.parent.name == "Portal" + i) {
                    QuestMain main = mains.Where(m => m.questName == questnames[i-1]).ToArray()[0];
                    ModifyPortal(portal, true, mains, "WorldHub", main.questName, "home");
                } else {
                    ModifyPortal(portal, false);
                }
            }
        }

        GameObject[] fxs = GameObject.FindGameObjectsWithTag("fx");
        foreach(GameObject fx in fxs) { Destroy(fx); }
    }

    #endregion

    #region calledByUI
    public void UpdateVsync() {
        if (vsync) { QualitySettings.vSyncCount = 1; } else { QualitySettings.vSyncCount = 0; }
    }

    public void OnVolumeChanged(float volume) {
        globalVolume = volume;
        //UpdateAudioVideo();
    }

    public void OnGraphicsChanged(int value) {
        graphicsLevel = value;
        UpdateGraphicLevel();
    }

    public void OnVsyncChanged(bool value) {
        vsync = value;
        UpdateVsync();
    }
    #endregion

    #region saveload
    public SaveBin GetBin() {
        // fetch data and put it in a bin
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

        bin.keys = new int[keys.Length];
        bin.letters = new int[letters.Length];
        keys.CopyTo(bin.keys, 0);
        letters.CopyTo(bin.letters, 0);
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
        // the loading logic (ApplyBin) is called from the load callback.
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
        // hack to teleport the player
        player.characterController.enabled = false;
        player.transform.position = bin.playerPos;
        player.characterController.enabled = true;

        player.transform.rotation = Quaternion.Euler(bin.playerRot);
        player.cam.rotation = Quaternion.Euler(bin.playerCamRot);

        bin.keys.CopyTo(keys, 0);
        bin.letters.CopyTo(keys, 0);
    }

    public void EraseSavedData() {
        SaveBin bin = new SaveBin();
        bin.currentLevel = "WorldHub";
        string raw = JsonUtility.ToJson(bin);
        File.WriteAllText(Application.persistentDataPath + "/save.json", raw);
    }
    #endregion

    #region deprectationAppreciation

    // deprecated in favor of saving everything, basically the "greater good"
    private void BackupInventory() {
        InventoryManager inventory = FindObjectOfType<InventoryManager>();
        if (inventory) {
            inventoryBackup.Clear();
            inventoryBackup.AddRange(inventory.inventory.itemList);
        }
    }

    // DEPRECATED, SHOULD NOT BE USED
    private void UpdateAudioVideo() {
        return;
        // find all audio and video components and change the volume
        AudioSource[] audios = FindObjectsOfType<AudioSource>();
        VideoPlayer[] videos = FindObjectsOfType<VideoPlayer>();

        for (int i = 0; i < audios.Length; i++) {
            audios[i].volume = globalVolume;
        }

        for (int i = 0; i < videos.Length; i++) {
            videos[i].SetDirectAudioVolume(0, globalVolume);
        }
    }

    #endregion
}