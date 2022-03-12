using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Switch : MonoBehaviour
{
    //This switch script is used in reference with other scripts to set the AI into a Run-Away state
    public Animator switchAnim;
    public GameObject interactText;
    public static bool active; 

    // Start is called before the first frame update
    void Awake()
    {
        switchAnim = GetComponent<Animator>();
    }

   
  
}
