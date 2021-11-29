using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit2D : MonoBehaviour
{
    private bool canBeDead; //If we can destroy the object
    private Vector3 screen; //Position on the screen

    public GameObject splat;
    void Update ()
    {
        //Set screen position
        screen = Camera.main.WorldToScreenPoint(transform.position);
        //If we can die and are not on the screen
        if (canBeDead && screen.y < -20)
        {
            //Destroy
            GameObject.FindWithTag("Player").GetComponent<NinjaPlayer>().ResetStreak();
            Destroy(gameObject);
        }
        //If we cant die and are on the screen
        //for the fruit to appear on screen the first time
        else if (!canBeDead && screen.y > -10)
        {
            //We can die
            canBeDead = true;
        }
    }
    
    public void Hit()
    {
        //Destroy


        if (gameObject.tag == "Fruit")
        {
            GameObject g = Instantiate(splat, new Vector3(transform.position.x, transform.position.y, transform.position.z + 1), transform.rotation);
        }
        Destroy(gameObject);
        
    }
}
