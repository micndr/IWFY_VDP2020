using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GlobalState : MonoBehaviour { 
    public static GlobalState Instance;

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
        if (!completedQuests.Contains(qm.questName)) {
            print("added completed quest name " + qm.name);
            completedQuests.Add(qm.questName);
        }
    }

    void OnLoadCallback(Scene scene, LoadSceneMode sceneMode) {
        var qms = FindObjectsOfType<QuestMain>();
        foreach (QuestMain qm in qms) {
            if (completedQuests.Contains(qm.questName)) {
                print("restoring completed quest name " + qm.name);
                // TODO: change quest completion
                qm.state = qm.stateNames.Length - 1;
                qm.CheckCompletion();
            }
        }
    }
}