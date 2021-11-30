using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controller for the MainMenu scene
/// </summary>
public class MainMenuController : MonoBehaviour
{
    /// <summary>
    /// All of the state the main menu scene can be in
    /// </summary>
    [Serializable]
    public enum MainMenuState
    {
        MAIN_MENU, PLAY, STORE, OPTIONS, CREDITS, QUIT
    }
    
    private MainMenuState currentState = MainMenuState.MAIN_MENU;

    public Animator mainMenuButtons;
    public GameObject background;

    public GameObject store, options, credits;
    
    void Awake()
    {
        // Buttons fly in
        mainMenuButtons.SetBool("isDisplay", true);
        Debug.Log("[MainMenuController] :: The current state is " + currentState);
    }
    
    /// <summary>
    /// Used by buttons to change the next state. (Since buttons don't like IEnumerator methods).
    /// </summary>
    /// <param name="nextState">MAIN_MENU=0, PLAY=1, STORE=2, OPTIONS=3, CREDITS=4, QUIT=5</param>
    public void ChangeStateTo(int nextState)
    {
        MainMenuState next = (MainMenuState)nextState;
        StartCoroutine(nameof(ChangeState), next);
    }

    /// <summary>
    /// Coroutine for changing the state of the scene. 
    /// </summary>
    /// <param name="nextState">The next state of the scene</param>
    /// <returns></returns>
    private IEnumerator ChangeState(MainMenuState nextState)
    {
        // Moving from old state
        switch (currentState)
        {
            case MainMenuState.MAIN_MENU:
                mainMenuButtons.SetBool("isDisplay", false);
                background.SetActive(false);
                //yield return new WaitUntil(() => (mainMenuButtons.GetCurrentAnimatorStateInfo(0).normalizedTime > 1));
                yield return new WaitForSeconds(0.8f);
                break;
            // case MainMenuState.PLAY:
            //     break;
            case MainMenuState.STORE:
                store.SetActive(false);
                yield return new WaitForSeconds(0.1f);
                break;
            case MainMenuState.CREDITS:
                credits.SetActive(false);
                yield return new WaitForSeconds(0.1f);
                break;
            case MainMenuState.OPTIONS: 
                options.SetActive(false);
                yield return new WaitForSeconds(0.1f);
                break;
            // case MainMenuState.QUIT:
            //     break;
        }
        
        // Change current state to the next state.
        currentState = nextState;
        Debug.Log("[MainMenuController] :: The current state is " + currentState);
        
        
        // Changing to new state
        switch (currentState)
        {
            case MainMenuState.MAIN_MENU:
                background.SetActive(true);
                mainMenuButtons.SetBool("isDisplay", true);
                yield return new WaitUntil(() => (mainMenuButtons.GetCurrentAnimatorStateInfo(0).normalizedTime > 1));
                break;
            case MainMenuState.PLAY:
                SceneManager.LoadScene("NinjaGame");
                break;
            case MainMenuState.STORE:
                store.SetActive(true);
                break;
            case MainMenuState.CREDITS:
                credits.SetActive(true);
                break;
            case MainMenuState.OPTIONS: 
                options.SetActive(true);
                break;
            case MainMenuState.QUIT:
#if !UNITY_EDITOR
                Application.Quit();
#else 
                Debug.Log("[MainMenuController] :: The game would have ended.");
#endif
                break;
        }

    }
    
}
