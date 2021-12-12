using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR;

[RequireComponent(typeof(PlayerDataImporter))]
public class NinjaPlayer : MonoBehaviour
{

    [Serializable]
    struct AbilityDictionary
    {
        public AbilityDetails data;
        public Ability abilityObject;
    }
    
    
    public static NinjaPlayer instance;
    public Ability ability;

    [SerializeField] private AbilityDictionary[] abilityDictionary;
    
    
    private Vector3 pos; //Position

    [SerializeField] private InGameMenuController menuController;
    [SerializeField] private GameObject spawner;

    private int score = 0;

    public int streak = 1;

    public int playerLives = 3;

    public Text text;
    void Start ()
    {
        instance = this;
        //Set screen orientation to landscape
        Screen.orientation = ScreenOrientation.Landscape;
        //Set sleep timeout to never
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        ChangeText();
        if (!SceneManager.GetActiveScene().name.Contains("Tutorial"))
        {
            HandleGettingAbility();
        }
        
        
    }

    public void HandleGettingAbility()
    {
        AbilityDetails a = PlayerDataImporter.instance.currentAbility;
        foreach (AbilityDictionary dictEntry in abilityDictionary)
        {

            if (dictEntry.data == a)
            {
                ability = dictEntry.abilityObject;
                dictEntry.abilityObject.gameObject.SetActive(true);
                break;
            }
        }
    }
    
    
    void Update () {
        if (playerLives <= 0)
        {
            menuController.ChangeState(false); 
            spawner.SetActive(false);
            ClearAllBombs();
        }
        
        //If the game is running on an iPhone device
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            //If we are hitting the screen
            if (Input.touchCount == 1)
            {
                //Find screen touch position, by
                 //transforming position from screen space into game world space.
                pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.GetTouch(0).position.x, Input.GetTouch(0).position.y, 1));
                //Set position of the player object
                transform.position = new Vector3(pos.x,pos.y,3);
                //Set collider to true
                GetComponent<Collider2D>().enabled = true;
                return;
            }
            //Set collider to false
            GetComponent<Collider2D>().enabled = false;
        }
        //If the game is not running on an iPhone device
        else
        {
            //Find mouse position
            pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            //Set position
            transform.position = new Vector3(pos.x,pos.y,3);
        }
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Fruit")
        {
            //write your code here
            other.GetComponent<Fruit2D>().Hit();
            Score(true);
        } else if (other.tag == "Enemy")
        {
            other.GetComponent<Fruit2D>().Hit();
            Score(false);
            //playerLives--;
            FindObjectOfType<HealthBarController>().UpdateHealthBar(true);
            if (playerLives == 0)
            {
                menuController.ChangeState(false);
                spawner.SetActive(false);
            }
                
        }
    }


    public void Score(bool isFruit)
    {
        if (isFruit)
        {
            score += streak * streak;
            streak++;
            ability.PlayerGotKill();
        }
        else
        {
            score -= 2;
            streak = 1;
        }
        
        //Debug.Log(score);
        ChangeText();
        
    }

    public void ResetStreak()
    {
        streak = 1;
        ChangeText();
    }

    private void ChangeText()
    {
        text.text = "Score: " + score + "\nStreak: " + streak;
    }

    public int GetScore()
    {
        return score;
    }


    /// <summary>
    /// Clears all of the bombs on the screen 
    /// </summary>
    void ClearAllBombs()
    {
        foreach (Fruit2D fruit in FindObjectsOfType<Fruit2D>())
        {
            fruit.ShockwaveHit();
        }
    }
}
