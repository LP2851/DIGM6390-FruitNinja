using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controller for the store UI elements.
/// </summary>
public class StoreMenuController : MonoBehaviour
{
    public Scrollbar scrollBar;
    private float scrollPosition = 0;
    private float[] buttonPositions;

    public float speed = 0.1f;
    public float smallScale = 0.8f;

    private StoreItem[] storeItems;
    public GameObject storeItemButtonPrefab;
    private Vector3 spawnPosition;

    [SerializeField] private PlayerDataImporter playerDataImporter;

    [SerializeField] private Text playerMoney;
    
    void Awake()
    {
        // Gets all of the store item buttons added to the store
        storeItems = StoreData.instance.storeItems;
        foreach (StoreItem item in storeItems)
        {
            if (playerDataImporter.IsOwned(item)) item.playerHasBought = true;
            
            GameObject itemButton = Instantiate(storeItemButtonPrefab, spawnPosition, Quaternion.identity);
            itemButton.transform.SetParent(transform);
            StoreItemButton storeItemButton = itemButton.GetComponent<StoreItemButton>();
            storeItemButton.SetData(item);
            itemButton.transform.localScale = storeItemButtonPrefab.transform.localScale;
        }
    }
    
    void Update()
    {
        // Updating the text showing the amount of money the player has
        playerMoney.text = "" + PlayerDataImporter.instance.amountOfMoney;
        
        // Positions of all the buttons (between 0 and 1).
        buttonPositions = new float[transform.childCount];
        
        // Distance between each button (when in scale between 0 and 1).
        float distance = 1f / (buttonPositions.Length - 1);
        
        // Calculating where (between 0 and 1) each button is.
        // Done like this because the scroll bar works with values from 0 to 1.
        for (int i = 0; i < buttonPositions.Length; i++)
        {
            buttonPositions[i] = distance * i;
        }
        
        // When LMB is pressed
        if (Input.GetMouseButton(0))
            scrollPosition = scrollBar.value;
        else
        {
            // Snapping to current button
            for (int i = 0; i < buttonPositions.Length; i++)
            {
                // Checks if the button is in range to be snapped to
                if (scrollPosition < buttonPositions[i] + (distance / 2) &&
                    scrollPosition > buttonPositions[i] - (distance / 2))
                {
                    // Linearly interpolates between a and b by t. 
                    // Moves the scroll bar towards the button we are snapping to
                    scrollBar.value = Mathf.Lerp(scrollBar.value, buttonPositions[i], speed);
                }
            }
        }
        
        // For resizing the buttons
        for (int i = 0; i < buttonPositions.Length; i++)
        {
            // Checks if button is being snapped to
            if (scrollPosition < buttonPositions[i] + (distance / 2) &&
                scrollPosition > buttonPositions[i] - (distance / 2))
            {
                // Gets the button, resizes it to full size (smooth resize- animation like) 
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), speed);
                
                // Sets the button to be the one in focus 
                transform.GetChild(i).GetComponent<StoreItemButton>().IsFocused(true);
                
                // All of the other buttons (not being snapped to) are resized to be smaller than the focused button.
                for (int j = 0; j < buttonPositions.Length; j++)
                {
                    if (j != i)
                    {
                        // Resizing
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(smallScale, smallScale), speed);
                        // Setting the button to not focused.
                        transform.GetChild(j).GetComponent<StoreItemButton>().IsFocused(false);
                    }
                }
                
            }
        } 
    }
}
