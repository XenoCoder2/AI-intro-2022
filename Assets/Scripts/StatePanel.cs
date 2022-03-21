using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatePanel : MonoBehaviour
{
    #region Variable
    public Text stateText;
    #endregion

    #region Update Text Methods
    public void UpdateStatusText(string status)
    {
        //Change the stateTexts text to status.
        stateText.text = status;
    }

    public void EndTurnUpdate()
    {
        //Switch between the Text depending on the currentState.
        switch (AIManager.currentState)
        {
            case AIManager.State.FullHP:
                stateText.text = "Full HP";
                break;
            case AIManager.State.Damaged:
                stateText.text = "Damaged";
                break;
            case AIManager.State.LowHP:
                stateText.text = "Low HP";
                break;
            case AIManager.State.Dead:
                stateText.text = "Dead";
                break;
            default:
                break;
        }
    }
    #endregion
}
