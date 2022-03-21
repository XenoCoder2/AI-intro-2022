using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartCombat : MonoBehaviour
{

    #region Variables
    public GameObject combatPanel;

    public GameObject[] disableObjects;

    public GameObject aiPrefab;

    public GameObject combatMan;

    public Animator screenFlash;

    public GameObject statePanel;
    #endregion

    #region On Collision Enter 2D Method
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.collider.gameObject.name);

        AIMovement aiMove = collision.gameObject.GetComponent<AIMovement>();

        if (aiMove == null)
        {
            return;
        }

        
        Debug.Log("We have encountered a dangerous Blungerberry!");
        //Start the StartBattle coroutine.
        StartCoroutine(StartBattle());

    }
    #endregion

    #region Start Battle Coroutine
    public IEnumerator StartBattle()
    {
        //Disable the statePanel.
        statePanel.SetActive(false);
        //Set the animation bool for initialising the transition animation to true.
        screenFlash.SetBool("Init", true);
        //Set the timeScale to 0 to freeze time.
        Time.timeScale = 0;
        //Wait 2.15 seconds before continuing the coroutine.
        yield return new WaitForSecondsRealtime(2.15f);
        
        //Enter Combat.
        combatPanel.SetActive(true);
        //Enable the Combat Manager.
        combatMan.SetActive(true);
        //Disable the GameObjects from the disableObjects array.
        for (int i = 0; i < disableObjects.Length; i++)
        {
            //Disable the object.
            disableObjects[i].SetActive(false);
        }
        //Wait 0.5 seconds.
        yield return new WaitForSecondsRealtime(0.5f);
        //Destroy the screenFlash GameObject.
        Destroy(screenFlash.gameObject);

    }
    #endregion

    #region Reload Scene Method
    public void ReloadScene()
    {
        //Set the timeScale to 1 to resume time.
        Time.timeScale = 1;
        //Reload the current scene.
        SceneManager.LoadScene(0);
    }
    #endregion
}
