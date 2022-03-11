using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchEvents : MonoBehaviour
{
    public Text eventText;
    public PlayerManager playMan;
    public AIManager aiMan;

    // Start is called before the first frame update
    void Start()
    {
        eventText.text = "A wild Blungerberry has appeared!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
