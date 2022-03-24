using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Switch : MonoBehaviour
{
    #region Variables
    //This switch script is used in reference with other scripts to set the AI into a Run-Away state.
    public Animator switchAnim;
    public GameObject interactText;
    public static bool active;
    #endregion

    #region Awake Method
    // Start is called before the first frame update
    void Awake()
    {
        //Get the Animator component from the switch.
        switchAnim = GetComponent<Animator>();
    }
    #endregion
}
