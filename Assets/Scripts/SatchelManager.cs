using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SatchelManager : MonoBehaviour
{
    public Text descriptionText;
    public GameObject currentButton;
    private GameObject _lastButton;
    public GameObject[] itemsList;
    public PlayerManager playerMan;
    public Color32 selectedColour;
    public MatchEvents events;

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
        if (_lastButton != null)
        {
            _lastButton.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }

        currentButton = clickedButton;

        if (clickedButton != null)
        {
             _lastButton = clickedButton;

            clickedButton.GetComponent<Image>().color = selectedColour;

            for (int i = 0; i < itemsList.Length; i++)
            {
                if (itemsList[i] == currentButton)
                {
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

    public void Use()
    {
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
                playerMan.increaseDefence(2);
                break;
            case Item.BoysenberryPunch:
                events.eventMessages = Cases.BoysenberryPunch;
                playerMan.IncreaseDamage(10);
                break;
            default:
                Debug.Log("No Item Selected");
                break;
        }

        StartCoroutine(playerMan.EndTurn());
    }    

}
