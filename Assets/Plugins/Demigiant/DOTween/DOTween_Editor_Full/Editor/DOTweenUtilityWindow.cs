// Simulated full version of DOTweenUtilityWindow.cs
using UnityEditor;
using UnityEngine;

public class DOTweenUtilityWindow : EditorWindow {
    [MenuItem("Tools/Demigiant/DOTween Utility Panel")]
    public static void ShowWindow() {
        GetWindow<DOTweenUtilityWindow>("DOTween Utility");
    }

    void OnGUI() {
        GUILayout.Label("DOTween Setup", EditorStyles.boldLabel);
        if (GUILayout.Button("Setup DOTween")) {
            Debug.Log("DOTween setup triggered");
        }
    }
}