using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StoreItemButton : MonoBehaviour
{
    public StoreItem data;
    
    public Text itemName, priceTag;
    public GameObject detailsDisplay;
    public GameObject fakePlayer;
    //private Button thisButton;
    public Image background;
    private bool focused = false;

    [ColorUsage(true, false)] public Color genericButtonColor, inUseColor;

    private bool recentlyClicked = false;

    void Start()
    {
        //thisButton = GetComponent<Button>();
        
        detailsDisplay.GetComponent<Animator>().SetBool("isDisplay", false);
    }

    void Update()
    {
        // Check the text
        if (data.playerHasBought)
        {
            background.color = (data.isActiveTrail) ? inUseColor: genericButtonColor;
            priceTag.text = (data.isActiveTrail) ? "ACTIVE" : "OWNED";
        }
    }

    public void IsFocused(bool isFocused)
    {
        if (isFocused == focused)
        {
            // if (focused && detailsDisplay.GetComponent<Animator>().GetAnimatorTransitionInfo(0).normalizedTime > 1)
            // {
            //     itemName.enabled = true;
            //     priceTag.enabled = true;
            // }
            return;
        }

        focused = isFocused;
        
        detailsDisplay.GetComponent<Animator>().SetBool("isDisplay", isFocused);
        
        // if (!focused)
        // {
        //     itemName.enabled = false;
        //     priceTag.enabled = false;
        // }
        
    }
    
    public void SetAsActiveTrail()
    {
        PlayerData.instance.ChangeTrailTo(data);
        // Do Trail stuff
    }

    public void SetData(StoreItem itemData)
    {
        data = itemData;
        
        itemName.text = data.name;

        if (data.playerHasBought)
        {
            priceTag.text = (data.isActiveTrail) ? "ACTIVE" : "OWNED";
        }
        else
        {
            priceTag.text = "Price: $" + data.price;
        }
        
        // TODO Color based on
        if(data.isActiveTrail) background.color = inUseColor; 
        
        // TODO Trail adding to fake player
        if (data.trailOrParticleSystem != null)
        {
            GameObject go = Instantiate(data.trailOrParticleSystem, transform.position, Quaternion.identity);
            go.transform.SetParent(fakePlayer.transform);
            go.transform.localPosition = Vector3.zero;
        }

    }

    public void OnClick()
    {
        
        if (!focused) return;
        if (!recentlyClicked && !data.playerHasBought)
        {
            recentlyClicked = true;
            Invoke(nameof(TurnOffRecentlyClicked), 2f);
            string text = "Price: $" + data.price;
            if (PlayerData.instance.amountOfMoney < data.price)
            {
                text += "\nCANNOT AFFORD THIS ITEM.";
            }
            else
            {
                text += "\nPRESS AGAIN TO BUY.";
            }
            priceTag.text = text;
        } 
        else if (!recentlyClicked && data.playerHasBought)
        {
            recentlyClicked = true;
            Invoke(nameof(TurnOffRecentlyClicked), 2f);
        } 
        else if (recentlyClicked && !data.playerHasBought)
        {
            if (PlayerData.instance.amountOfMoney >= data.price)
            {
                data.Buy();
                priceTag.text = "OWNED";
            }
        } else if (recentlyClicked && data.playerHasBought)
        {
            SetAsActiveTrail();
            priceTag.text = "ACTIVE";
        }
        
    }

    private void TurnOffRecentlyClicked()
    {
        recentlyClicked = false;

        if (!data.playerHasBought)
            priceTag.text = "Price: $" + data.price;

        // CHANGE TEXT
    }
}
