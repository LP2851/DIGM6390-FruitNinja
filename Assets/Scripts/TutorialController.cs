using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class TutorialController : MonoBehaviour
{
    [Header("Tutorial Game Objects")]
    public GameObject tutorialMoveAroundText;
    public GameObject slicingBasicText, slicingAdvancedText, bombsAndHealthText, abilitiesText;
    
    [Header("Other Game Objects")]
    public GameObject doneScreen;
    public GameObject health, score, chargeBar;
    
    // Spawning
    [Header("Spawning Parameters")]
    public float upForce = 750; //Up force
    public float leftRightForce = 200; //Left and right force
    public float maxX = -7; //Max x spawn position
    public float minX = 7; //Min x spawn position
    [Header("Spawning Fruits")]
    public GameObject basicFruit;
    public GameObject strongerFruit, bomb;

    [Serializable]
    enum TutorialState {
        MOVE_AROUND, SLICING_BASIC, SLICING_STRONGER, BOMBS_AND_HEALTH, ABILITIES, DONE
    }

    [SerializeField] private TutorialState currentState = TutorialState.MOVE_AROUND;
    private Fruit2D currentFruit;
    private bool hasSpawnedFruit = false;
    private bool hasActivatedAbility = false;

    private Vector3 previousPosition;
    private float distance = 0f;

    public AbilityDetails shockwaveAbility;


    void Update()
    {
        switch (currentState)
        {
            case TutorialState.MOVE_AROUND:
                StateMoveAround();
                break;
            case TutorialState.SLICING_BASIC:
                StateSlicingBasic();
                break;
            case TutorialState.SLICING_STRONGER:
                StateSlicingAdvanced();
                break;
            case TutorialState.BOMBS_AND_HEALTH:
                StateBombsAndHealth();
                break;
            case TutorialState.ABILITIES:
                StateAbilities();
                break;
        }

        if (currentFruit != null && currentFruit.transform.position.y >= 2f)
        {
            // Freeze the fruit
            currentFruit.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
            currentFruit.GetComponent<Rigidbody2D>().isKinematic = true;
        } 
        
    }


    void StateMoveAround()
    {
        
        Vector3 playerPosition = NinjaPlayer.instance.transform.position;
        distance += Vector3.Distance(playerPosition, previousPosition);
        previousPosition = playerPosition;

        if (distance >= 50f)
        {
            Debug.Log("COMPLETED TUTORIAL: " + currentState);
            currentState = TutorialState.SLICING_BASIC;
            tutorialMoveAroundText.SetActive(false);
            slicingBasicText.SetActive(true);
        }
    }

    void StateSlicingBasic()
    {
        if (!hasSpawnedFruit)
        {
            SpawnItem(basicFruit);
            hasSpawnedFruit = true;
            return;
        }

        if (currentFruit == null)
        {
            Debug.Log("COMPLETED TUTORIAL: " + currentState);
            currentState = TutorialState.SLICING_STRONGER;
            slicingBasicText.SetActive(false);
            slicingAdvancedText.SetActive(true);
            // currentState = TutorialState.SLICING_ADVANCED;
            hasSpawnedFruit = false;

        }
    }
    
    void StateSlicingAdvanced()
    {
        if (!hasSpawnedFruit)
        {
            SpawnItem(strongerFruit);
            hasSpawnedFruit = true;
            return;
        }

        if (currentFruit == null)
        {
            Debug.Log("COMPLETED TUTORIAL: " + currentState);
            currentState = TutorialState.BOMBS_AND_HEALTH;
            slicingAdvancedText.SetActive(false);
            bombsAndHealthText.SetActive(true);
            // currentState = TutorialState.SLICING_ADVANCED;
            hasSpawnedFruit = false;

        }
    }
    
    void StateBombsAndHealth()
    {
        if (!hasSpawnedFruit)
        {
            SpawnItem(bomb);
            hasSpawnedFruit = true;
            return;
        }

        if (currentFruit == null)
        {
            Debug.Log("COMPLETED TUTORIAL: " + currentState);
            currentState = TutorialState.ABILITIES;
            bombsAndHealthText.SetActive(false);
            hasSpawnedFruit = false;
            
            abilitiesText.SetActive(true);
            health.transform.localScale = new Vector3(0, 0, 0);
            score.transform.localScale = new Vector3(0, 0, 0);
            chargeBar.SetActive(true);
            PlayerDataImporter.instance.SetAbilityTo(shockwaveAbility);
            NinjaPlayer.instance.HandleGettingAbility();
            NinjaPlayer.instance.ability.countdown = 5f;
        }
    }

    void StateAbilities()
    {
        if (!hasActivatedAbility)
        {
            hasActivatedAbility = NinjaPlayer.instance.ability.isActive;
            return;
        }

        if (!NinjaPlayer.instance.ability.isActive)
        {
            Debug.Log("COMPLETED TUTORIAL: " + currentState);
            currentState = TutorialState.DONE;
            chargeBar.transform.localScale = new Vector3(0, 0, 0);
            abilitiesText.SetActive(false);
            doneScreen.SetActive(true);
        }
    }



    /// <summary>
    /// Copied from SpawnItem script
    /// </summary>
    /// <param name="prefab"></param>
    void SpawnItem(GameObject prefab)
    {
        GameObject go = Instantiate(prefab, new Vector3(Random.Range(minX
            , maxX + 1), transform.position.y, 0f), Quaternion.Euler(0, 0, Random.Range(-
            90F, 90F)));
        
        //If x position is over 0 go left
        if (go.transform.position.x > 0)
        {
            go.GetComponent<Rigidbody2D>().AddForce(new Vector2(-
                leftRightForce,upForce));
        }
        //Else go right
        else
        {
            go.GetComponent<Rigidbody2D>().AddForce(new Vector2(leftRightForce,upForce));
        }

        currentFruit = go.GetComponent<Fruit2D>();
    }


    public void Restart()
    {
        SceneManager.LoadScene("TutorialGame");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
