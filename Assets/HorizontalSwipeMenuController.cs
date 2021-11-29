using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;
using UnityEngine.UI;

public class HorizontalSwipeMenuController : MonoBehaviour
{
    public Scrollbar scrollBar;
    private float scrollPosition = 0;
    public float[] buttonPositions;

    public float speed = 0.1f;
    public float smallScale = 0.8f;

    public StoreItem[] storeItems;
    public GameObject storeItemButtonPrefab;
    public Vector3 spawnPosition;
    
    void Start()
    {
        foreach (StoreItem item in storeItems)
        {
            GameObject itemButton = Instantiate(storeItemButtonPrefab, spawnPosition, Quaternion.identity);
            itemButton.transform.SetParent(transform);
            itemButton.GetComponent<StoreItemButton>().SetData(item);
            itemButton.transform.localScale = storeItemButtonPrefab.transform.localScale;
        }
    }
    
    void Update()
    {
        buttonPositions = new float[transform.childCount];
        float distance = 1f / (buttonPositions.Length - 1);
        
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
                if (scrollPosition < buttonPositions[i] + (distance / 2) &&
                    scrollPosition > buttonPositions[i] - (distance / 2))
                {
                    // Linearly interpolates between a and b by t. 
                    scrollBar.value = Mathf.Lerp(scrollBar.value, buttonPositions[i], speed);
                }
            }
        }

        for (int i = 0; i < buttonPositions.Length; i++)
        {
            if (scrollPosition < buttonPositions[i] + (distance / 2) &&
                scrollPosition > buttonPositions[i] - (distance / 2))
            {
                transform.GetChild(i).localScale = Vector2.Lerp(transform.GetChild(i).localScale, new Vector2(1f, 1f), speed);

                transform.GetChild(i).GetComponent<StoreItemButton>().IsFocused(true);
                
                for (int j = 0; j < buttonPositions.Length; j++)
                {
                    if (j != i)
                    {
                        transform.GetChild(j).localScale = Vector2.Lerp(transform.GetChild(j).localScale, new Vector2(smallScale, smallScale), speed);
                        transform.GetChild(j).GetComponent<StoreItemButton>().IsFocused(false);
                    }
                }
                
            }
        } 
    }
}
