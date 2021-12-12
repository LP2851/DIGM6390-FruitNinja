using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenuController : MonoBehaviour
{
    public GameObject OptionsCanvas;
    public GameObject TutorialModeActive;
    public GameObject TutorialModeInactive;
    public GameObject EraseDataInactive;
    
    public void loadgame(){
    SceneManager.LoadScene("MainMenu");
    }

    public void ActivateTutorialMode(){
        TutorialModeInactive.SetActive(false);
        TutorialModeActive.SetActive(true);
    }

   public void DeactivateTutorialMode(){
        TutorialModeActive.SetActive(false);
        TutorialModeInactive.SetActive(true);
    }

    public void EraseData(){
        //calls delay action as a coroutine 
        StartCoroutine(DelayAction(3));

    }

    IEnumerator DelayAction(float delayTime){   
        //Change Text component on EraseDataInactive button to "ERASED"
        EraseDataInactive.GetComponentInChildren<Text>().text = "ERASED";
        //Wait for the specified delay time before continuing.
        yield return new WaitForSeconds(delayTime);
        //Change text back to "ERASE PLAYER DATA" after the delay
        EraseDataInactive.GetComponentInChildren<Text>().text = "ERASE PLAYER DATA";
    }

        // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
