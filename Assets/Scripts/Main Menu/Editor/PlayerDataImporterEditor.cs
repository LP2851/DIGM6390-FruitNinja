using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Custom editor for the PlayerDataImporter class. Allows the developer the ability to add money to player
/// and reset all of the PlayerPref values. 
/// </summary>
[CustomEditor(typeof(PlayerDataImporter))]
public class PlayerDataImporterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Do normal stuff
        base.OnInspectorGUI();
        
        // The component this editor is working on
        PlayerDataImporter controller = (PlayerDataImporter)target;
        
        // Creates a button that when clicked added 1000 money units to player 
        if (GUILayout.Button("Add 1000 Money"))
        {
            controller.GiveMoney(1000);
        }

        // Creates a button that when clicked removes all saved data.
        if (GUILayout.Button("Reset Player Prefs"))
        {
            controller.ResetPlayerPrefsData();
            Debug.Log("[PlayerDataImporterEditor] :: Reset PlayerPrefs. You must restart the game!");
            // Reloads current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
}
