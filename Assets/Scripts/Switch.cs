using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class Switch : MonoBehaviour
{
    public Animator switchAnim;
    public GameObject interactText;
    public static bool active; 

    // Start is called before the first frame update
    void Awake()
    {
        switchAnim = GetComponent<Animator>();
    }

   
  
}
