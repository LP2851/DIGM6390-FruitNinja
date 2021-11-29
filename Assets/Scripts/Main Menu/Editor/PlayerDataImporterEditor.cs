using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerDataImporter))]
public class PlayerDataImporterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        PlayerDataImporter controller = (PlayerDataImporter)target;
        
        if (GUILayout.Button("Add 1000 Money"))
        {
            controller.GiveMoney(1000);
        }

        if (GUILayout.Button("Reset Player Prefs"))
        {
            controller.ResetPlayerPrefsData();
            Debug.LogError("[PlayerDataImporterEditor] :: Reset PlayerPrefs. You must restart the game!");
        }
    }
}
