using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalState : MonoBehaviour { 
    public static GlobalState Instance;

    public Transform playerPos;
    public List<string> completedQuests = new List<string>();

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
        completedQuests.Add(qm.questName);
    }

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode) { 
        var qms = FindObjectsOfType<QuestMain>();
        foreach (QuestMain qm in qms) {
            if (completedQuests.Contains(qm.questName)) {
                // TODO: change quest completion
                qm.state = qm.stateNames.Length - 1;
                qm.completed = true;
            }
        }
    }
}