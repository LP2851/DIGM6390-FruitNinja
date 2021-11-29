using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
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
        mainMenuButtons.SetBool("isDisplay", true);
        Debug.Log("[MainMenuController] :: The current state is " + currentState);
    }
    
    public void ChangeStateTo(int nextState)
    {
        MainMenuState next = (MainMenuState)nextState;
        StartCoroutine(nameof(ChangeState), next);
    }

    private IEnumerator ChangeState(MainMenuState nextState)
    {
        // Moving from old state
        switch (currentState)
        {
            case MainMenuState.MAIN_MENU:
                mainMenuButtons.SetBool("isDisplay", false);
                yield return new WaitUntil(() => (mainMenuButtons.GetCurrentAnimatorStateInfo(0).normalizedTime > 1));
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
