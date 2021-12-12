using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Controls the play menu
/// </summary>
public class PlayMenuController : MonoBehaviour
{
    [Serializable]
    enum MenuState
    {
        MAIN_MENU, CHOICE, TUTORIAL, ABILITY_MENU
    }

    [SerializeField] private MenuState currentState = MenuState.MAIN_MENU;
    [ColorUsage(true, false)] private Color activeAbilityColor, inactiveAbilityColor;

    public GameObject contentsChoice, contentsAbility;
    public Animator choiceAnimator, abilityAnimator;

    public GameObject mainMenuButton, playMenuButton;

    public MainMenuController controller;


    void Start()
    {
        contentsChoice.SetActive(false);
        contentsAbility.SetActive(false);
        ChangeState(1);
    }

    /// <summary>
    /// Called when a button is pressed
    /// </summary>
    /// <param name="nextState">The next state: 0 = MAIN_MENU, 1 = CHOICE, 2 = TUTORIAL, 3 = ABILITY_MENU</param>
    public void ChangeState(int nextState)
    {
        StartCoroutine(nameof(ChangeStateTo), nextState);
    }

    /// <summary>
    /// Changes to the next menu state
    /// </summary>
    /// <param name="nextState"></param>
    /// <returns></returns>
    private IEnumerator ChangeStateTo(int nextState)
    {
        MenuState next = (MenuState)nextState;

        switch (currentState)
        {
            case MenuState.CHOICE:
                choiceAnimator.SetBool("isDisplay", false);
                yield return new WaitForSeconds(0.25f);
                contentsChoice.SetActive(false);
                if (next == MenuState.ABILITY_MENU)
                {
                    mainMenuButton.SetActive(false);
                    playMenuButton.SetActive(true);
                }
                break;
            case MenuState.ABILITY_MENU:
                abilityAnimator.SetBool("isDisplay", false);
                yield return new WaitForSeconds(0.25f);
                contentsAbility.SetActive(false);
                if (next == MenuState.CHOICE)
                {
                    mainMenuButton.SetActive(true);
                    playMenuButton.SetActive(false);
                }
                break;
        }

        currentState = next;

        switch (currentState)
        {
            case MenuState.CHOICE:
                contentsChoice.SetActive(true);
                choiceAnimator.SetBool("isDisplay", true);
                break;
            case MenuState.ABILITY_MENU:
                contentsAbility.SetActive(true);
                abilityAnimator.SetBool("isDisplay", true);
                break;
            case MenuState.MAIN_MENU:
                controller.ChangeStateTo(0);
                break;
        }
    }

    /// <summary>
    /// Starts the game
    /// </summary>
    /// <param name="isTutorial">Is the game in tutorial mode</param>
    public void StartGame(bool isTutorial)
    {
        //if(isTutorial) SceneManager
        // else
        SceneManager.LoadScene("NinjaGame");
    }
}
