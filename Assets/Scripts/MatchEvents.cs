using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchEvents : MonoBehaviour
{
    public Text eventText;
    public PlayerManager playMan;
    public AIManager aiMan;

    public Cases eventMessages;

    // Start is called before the first frame update
    void Start()
    {
        //When starting the battle, set the event text
        eventText.text = "A wild Blungerberry has appeared!";
    }

    public void TextChange()
    {
        //Change the event text to the current case at the end of each turn
        switch (eventMessages)
        {
            case Cases.BermonIntroduction:
                eventText.text = "A wild Blungerberry has appeared!";
                break;
            case Cases.BerryThrow:
                if (aiMan.scenario == 0)
                {
                    eventText.text = "Bert used Berry Throw!";
                }
                else if (aiMan.scenario == 1)
                {
                    eventText.text = "Blungerberry used Berry Throw!";
                }
                break;
            case Cases.BerryCrunch:
                if (aiMan.scenario == 0)
                {
                    eventText.text = "Bert used Berry Crunch!";
                }
                else if (aiMan.scenario == 1)
                {
                    eventText.text = "Blungerberry used Berry Crunch!";
                }
                
                break;
            case Cases.LifeSteal:
                if (aiMan.scenario == 0)
                {
                    eventText.text = "Bert used Life Steal!";
                }
                else if (aiMan.scenario == 1)
                {
                    eventText.text = "Blungerberry used Life Steal!";
                }
               
                break;
            case Cases.Gnarl:
                if (aiMan.scenario == 0)
                {
                    eventText.text = "Bert used Gnarl!";
                }
                else if (aiMan.scenario == 1)
                {
                    eventText.text = "Blungerberry used Gnarl!";
                }
               
                break;
            case Cases.GoldenBerry:
                eventText.text = "Bert ate a Golden Berry!";
                break;
            case Cases.CranberryJuice:
                eventText.text = "Bert drank some Cranberry Juice!";
                break;
            case Cases.BartholomewsBerryMix:
                eventText.text = "Bert ate Bartholomew's Berry Mix!";
                break;
            case Cases.BoysenberryPunch:
                eventText.text = "Bert drank some Boysenberry Punch!";
                break;
            case Cases.AttackRevert:
                if (aiMan.scenario == 0)
                {
                    eventText.text = "Bert's attack reverted!";
                }
                else if (aiMan.scenario == 1)
                {
                    eventText.text = "Blungerberry's attack reverted!";
                }
                
                break;
            case Cases.DefenceRevert:
                if (aiMan.scenario == 0)
                {
                    eventText.text = "Bert's defence reverted!";
                }
                else if (aiMan.scenario == 1)
                {
                    eventText.text = "Blungerberry's defence reverted!";
                }
               
                break;
            case Cases.BermonFainted:
                eventText.text = "The Blungerberry Fainted!";
                break;
            case Cases.PlayerFainted:
                eventText.text = "Bert Fainted!";
                break;
            case Cases.MissedAttack:
                 if (aiMan.scenario == 0)
                {
                    eventText.text = "Bert's Attack Missed!";
                }
                else if (aiMan.scenario == 1)
                {
                    eventText.text = "Blungerberry's Attack Missed!";
                }
                break;
            case Cases.NoBP:
                eventText.text = "Not enough BP to perform this action!";
                break;
            default:
                break;
        }
    }



}
//By placing the enum outside of the script class it will be able to be accessed by other scripts without a reference to Match Events
public enum Cases
{
    BermonIntroduction,
    BerryThrow,
    BerryCrunch,
    LifeSteal,
    Gnarl,
    GoldenBerry,
    CranberryJuice,
    BartholomewsBerryMix,
    BoysenberryPunch,
    AttackRevert,
    DefenceRevert,
    BermonFainted,
    PlayerFainted,
    MissedAttack,
    NoBP
}