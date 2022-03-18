using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCombat : MonoBehaviour
{
    public GameObject combatPanel;

    public GameObject[] disableObjects;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.gameObject.name);

        AIMovement aiMove = collision.gameObject.GetComponent<AIMovement>();

        if (aiMove == null)
        {
            return;
        }

        Debug.Log("We have encountered a dangerous Blungerberry!");

        //Enter Combat.
        combatPanel.SetActive(true);
        //Disable the GameObjects from the disableObjects array.
        for (int i = 0; i < disableObjects.Length; i++)
        {
            disableObjects[i].SetActive(false);
        }
        
    }

}
