using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Script controlling buttons in the store. Used for each of the items that are bought in the store. 
/// </summary>
[RequireComponent(typeof(Button))]
public class StoreItemButton : MonoBehaviour
{
    public StoreItem data;
    
    public Text itemName, priceTag;
    public GameObject detailsDisplay;
    public GameObject fakePlayer;
    public Image background;
    private bool focused = false;

    [ColorUsage(true, false)] public Color genericButtonColor, inUseColor;
    
    private bool recentlyClicked = false;

    void Start()
    {
        // Closer the display tab on the button
        detailsDisplay.GetComponent<Animator>().SetBool("isDisplay", false);
    }

    void Update()
    {
        // Check the text and change color accordingly if item has been bought by the player
        if (data.playerHasBought)
        {
            background.color = (data.isActiveTrail) ? inUseColor: genericButtonColor;
            priceTag.text = (data.isActiveTrail) ? "ACTIVE" : "OWNED";
        }
    }

    /// <summary>
    /// If the item is focused, activates the details tab for the item button
    /// </summary>
    /// <param name="isFocused">If the button is focused or not.</param>
    public void IsFocused(bool isFocused)
    {
        // Does nothing if nothing has changed
        if (isFocused == focused)
            return;
        
        focused = isFocused;
        detailsDisplay.GetComponent<Animator>().SetBool("isDisplay", isFocused);

    }
    
    /// <summary>
    /// Sets the item being sold by the button as the active trail for the player.
    /// </summary>
    private void SetAsActiveTrail()
    {
        data.SetActive();
    }

    /// <summary>
    /// Setup for the button. Passed all of the data required to fill the buttons contents.
    /// </summary>
    /// <param name="itemData">The data for the item being sold</param>
    public void SetData(StoreItem itemData)
    {
        data = itemData;
        
        itemName.text = data.itemName;

        if (data.playerHasBought)
        {
            priceTag.text = (data.isActiveTrail) ? "ACTIVE" : "OWNED";
        }
        else
        {
            priceTag.text = "Price: $" + data.price;
        }
        
        // Changes the color of the button based on the status of the item (not bought, owned, active)
        if(data.isActiveTrail) background.color = inUseColor; 
        
        // Trail adding to fake player
        if (data.trailOrParticleSystem != null)
        {
            GameObject go = Instantiate(data.trailOrParticleSystem, transform.position, Quaternion.identity);
            go.transform.SetParent(fakePlayer.transform);
            go.transform.localPosition = Vector3.zero;
        }

    }

    /// <summary>
    /// Called whenever the button is clicked by the user.
    /// </summary>
    public void OnClick()
    {
        // If the button isnt focused, do nothing
        if (!focused) return;
        // If the button was not recently clicked and the item isn't owned then shows a message telling the user that
        //      A) They cannot afford the item
        //      B) They must click again to buy the item
        if (!recentlyClicked && !data.playerHasBought)
        {
            recentlyClicked = true;
            Invoke(nameof(TurnOffRecentlyClicked), 2f);
            string text = "Price: $" + data.price;
            
            // Sets message based on if player has enough money 
            if (PlayerDataImporter.instance.amountOfMoney <= data.price)
            {
                text += "\nCANNOT AFFORD THIS ITEM.";
            }
            else
            {
                text += "\nPRESS AGAIN TO BUY.";
            }
            priceTag.text = text;
        } 
        // If button was not recently clicked and the player owns the item then recently clicked is turned on
        else if (!recentlyClicked && data.playerHasBought)
        {
            recentlyClicked = true;
            Invoke(nameof(TurnOffRecentlyClicked), 2f);
        } 
        // If recently clicked and player doesn't own item, the item is bought (assuming player has enough money)
        else if (recentlyClicked && !data.playerHasBought)
        {
            // Resets recently clicked timer
            CancelInvoke(nameof(TurnOffRecentlyClicked));
            Invoke(nameof(TurnOffRecentlyClicked), 2f);
            
            // Checking player has enough money, if so the item is bought. 
            if (PlayerDataImporter.instance.amountOfMoney >= data.price)
            {
                data.Buy();
                priceTag.text = "OWNED";
            }
        } 
        // If recently clicked and the player owns the item, then the item is equipped.
        else if (recentlyClicked && data.playerHasBought)
        {
            // Resets recently clicked timer
            CancelInvoke(nameof(TurnOffRecentlyClicked));
            Invoke(nameof(TurnOffRecentlyClicked), 2f);
            
            SetAsActiveTrail();
            priceTag.text = "ACTIVE";
        }
        
    }
    
    /// <summary>
    /// Resets the recently clicked cooldown
    /// </summary>
    private void TurnOffRecentlyClicked()
    {
        // Called some time after a button was clicked.
        recentlyClicked = false;
        
        // Removes extra messages on items not purchased yet
        if (!data.playerHasBought)
            priceTag.text = "Price: $" + data.price;
    }
}
