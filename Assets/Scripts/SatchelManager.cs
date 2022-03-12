using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatchelManager : MonoBehaviour
{
    #region Variables
    public Text descriptionText;
    public GameObject currentButton;
    private GameObject _lastButton;
    public GameObject[] itemsList;
    public PlayerManager playerMan;
    public Color32 selectedColour;
    public MatchEvents events;
    #endregion

    public enum Item
    {
        GoldenBerry = 0,
        CranberryJuice = 1,
        BartholomewsBerryMix = 2,
        BoysenberryPunch = 3
    }

    public Item satchelItem; 

    public void ChangeItem(GameObject clickedButton)
    {
        //If the last button variable is not null..
        if (_lastButton != null)
        {
            //Get the Image component and change the colour to 255,255,255,255 (white)
            _lastButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        //Change the currentButton variable to the clickedButton GameObject
        currentButton = clickedButton;

        if (clickedButton != null)
        {
            //Set _lastButton to the clickedButton, this will be used to revert the colours back to white after another button is clicked
             _lastButton = clickedButton;

            //Change the current button to the selected button colour
            clickedButton.GetComponent<Image>().color = selectedColour;

            //Find the right item to display from the itemsList array
            for (int i = 0; i < itemsList.Length; i++)
            {
                if (itemsList[i] == currentButton)
                {
                    //Change the satchelItem to the appropriate item
                    satchelItem = (Item)i;
                }
            }

            switch (satchelItem)
            {
                case Item.GoldenBerry:
                    StartCoroutine(GBerry());
                    break;

                case Item.CranberryJuice:
                    StartCoroutine(CJuice());
                    break;

                case Item.BartholomewsBerryMix:
                    StartCoroutine(BBM());
                    break;

                case Item.BoysenberryPunch:
                    StartCoroutine(BP());
                    break;

                default:
                    Debug.Log("No Item Found");
                    break;
            }

            currentButton = null;
        }

        

    }
    #region IEnumerator Methods
    //The IEnumerators will change the description text to the appropriate item description
    IEnumerator GBerry()
    {
        descriptionText.text = "The Golden Berry is a legendary one-of-a-kind fruit that only grows once every 5000 years. It will restore the HP of any Bermon by 50% of their original value.";
        
        yield return null;

    }

    IEnumerator CJuice()
    {
        descriptionText.text = "Ultra-powerful Cranberry Juice squeezed from the mountains beyond where an ancient berry dragon lives. It will restore the BP of any Bermon by 25 BP.";
       
        yield return null;
    }

    IEnumerator BBM()
    {
        descriptionText.text = "A berry bonanza capable of temporarily increasing the defence of any Bermon for Two Turns.";
       
        yield return null;
    }

    IEnumerator BP()
    {
        descriptionText.text = "A powerful Boysenberry Punch that increases a Bermons attack power by 10% of their attack for Two Turns.";
       
        yield return null; 
    }
    #endregion

    public void Use()
    {
        //Depending on what item was clicked, switch between the use cases
        switch (satchelItem)
        {
            case Item.GoldenBerry:
                events.eventMessages = Cases.GoldenBerry;
                playerMan.Heal(100 / 2);
                break;
            case Item.CranberryJuice:
                events.eventMessages = Cases.CranberryJuice;
                playerMan.RecoverBP(25);
                break;
            case Item.BartholomewsBerryMix:
                events.eventMessages = Cases.BartholomewsBerryMix;
                playerMan.IncreaseDefence(2);
                break;
            case Item.BoysenberryPunch:
                events.eventMessages = Cases.BoysenberryPunch;
                playerMan.IncreaseDamage(10);
                break;
            default:
                Debug.Log("No Item Selected");
                break;
        }

        //End the turn
        StartCoroutine(playerMan.EndTurn());
    }    

}
