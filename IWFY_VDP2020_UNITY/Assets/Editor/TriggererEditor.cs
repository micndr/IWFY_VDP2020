using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CustomEditor(typeof(Triggerer))]
[CanEditMultipleObjects]
public class LookAtPointEditor : Editor {
    List<SerializedProperty> other = new List<SerializedProperty>();
    List<SerializedProperty> components = new List<SerializedProperty>();
    SerializedProperty findComps;

    bool showComp = false;
    bool showOth = false;

    void OnEnable() {
        components.Add(serializedObject.FindProperty("qlock"));
        components.Add(serializedObject.FindProperty("pickup"));
        components.Add(serializedObject.FindProperty("dialogue"));
        components.Add(serializedObject.FindProperty("triggerer"));
        components.Add(serializedObject.FindProperty("animator"));
        components.Add(serializedObject.FindProperty("mirrorcont"));
        components.Add(serializedObject.FindProperty("ropeActivator"));
        components.Add(serializedObject.FindProperty("audioSource"));
        components.Add(serializedObject.FindProperty("thunder"));
        components.Add(serializedObject.FindProperty("link"));
        components.Add(serializedObject.FindProperty("video"));
        components.Add(serializedObject.FindProperty("openNote"));
        findComps = serializedObject.FindProperty("findComponents");
        other.Add(serializedObject.FindProperty("autoTrigger"));
        other.Add(serializedObject.FindProperty("autoTriggerDelay"));
        other.Add(serializedObject.FindProperty("destroyAfterTrigger"));
        other.Add(serializedObject.FindProperty("disableAfterTrigger"));
        other.Add(serializedObject.FindProperty("lockPlayer"));
        other.Add(serializedObject.FindProperty("unlockPlayer"));
        other.Add(serializedObject.FindProperty("findComponents"));
        other.Add(serializedObject.FindProperty("playAmbientSound"));
    }

    public override void OnInspectorGUI() {
        serializedObject.Update();
        if (showOth) {
            foreach (SerializedProperty prop in other) {
                EditorGUILayout.PropertyField(prop);
            }
            if (GUILayout.Button("Hide Attributes")) {
                showOth = !showOth;
            }
        } else {
            if (GUILayout.Button("Show Attributes")) {
                showOth = !showOth;
            }
        }
        if (showComp) {
            foreach (SerializedProperty prop in components) {
                EditorGUILayout.PropertyField(prop);
            }
            if (GUILayout.Button("Hide Components")) {
                showComp = !showComp;
            }
        } else {
            if (GUILayout.Button("Show Components")) {
                showComp = !showComp;
            }
        }
        serializedObject.ApplyModifiedProperties();
    }
}