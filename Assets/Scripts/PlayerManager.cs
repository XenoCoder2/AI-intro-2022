using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : BaseManager
{
    #region Variables
    [Header("AI Manager")]
    protected AIManager _aiManager;
    private int berryPoints = 50;
    private Text _bpText;
    [Header("Canvas Group")]
    [SerializeField] protected CanvasGroup _canGroup;
    [Header("Attack Values")]
    private float attackDamage = 15f;
    private float defaultDamage = 15f;
    private int turnsUntilNormalDamage = 4;
    [Header("Animator")]
    protected Animator _characterAnim;
    private int attackType = 0;
    [Header("Match Events for text box")]
    public MatchEvents matchEvent;
    [Header("StartCombat | Reload Scene")]
    public StartCombat startCom;
    #endregion

    #region Start Method
    protected override void Start()
    {
        //Performs the Start actions from the inherited class.
        base.Start();

        //Gets the AIManager. 
        _aiManager = GetComponent<AIManager>();

        _bpText = GameObject.FindGameObjectWithTag("BerryPoints").GetComponent<Text>();

        _characterAnim = GameObject.FindGameObjectWithTag("PlayerB").GetComponent<Animator>();
        //If no AIManager is found, log an error.
        if (_aiManager == null)
        {
            Debug.LogError("No AIManager Found!");
        }
    }
    #endregion

    #region Start and End Turn Methods
    public override void TakeTurn()
    {
        //If _health is greater than 0.
        if (_health > 0)
        {
            //Set the canvas group to interactable. 
            _canGroup.interactable = true;
           
        }
        else
        {
            //Run the DeadPlayer method. 
            StartCoroutine(DeadPlayer());
           
        }
        
    }

    public IEnumerator EndTurn()
    {
        //Make the canvas group no longer interactable.
        _canGroup.interactable = false;
        //Change the event message.
        matchEvent.TextChange();
        //Pause the coroutine for 2 seconds.
        yield return new WaitForSecondsRealtime(2f);
        //Run the TakeTurn method for the AIManager.
        _aiManager.TakeTurn();
        //Return to the Idle anim.
        ReturnToIdle();
        //Run the DefenceReturn method.
        DefenceReturn();
        //Run the ReturnDamage method. 
        ReturnDamage();
        //Change the state text.
        _aiManager.states.EndTurnUpdate();
       
    }
    #endregion

    #region Dead Player Coroutine
    public IEnumerator DeadPlayer()
    {
        //Change the event message.
        matchEvent.eventMessages = Cases.PlayerFainted;
        //Reload the event text.
        matchEvent.TextChange();
        //Change the AI animation to the dead state. 
        _characterAnim.SetBool("IsDead", true);
        //Wait for two seconds.
        yield return new WaitForSecondsRealtime(2f);
        //Reload the scene.
        startCom.ReloadScene();

    }
    #endregion

    #region Berry Points (BP) Methods
    private void UpdateBP()
    {
        //Change the Berry Points text to the value of the berryPoints variable.
        _bpText.text = berryPoints.ToString();

    }

    public void RecoverBP(int bpAmount)
    {
        //If berryPoints + bpAmount is less than 50, increase berryPoints.
        berryPoints = Mathf.Min(berryPoints + bpAmount, 50);
        //Run the UpdateBP method.
        UpdateBP();
    }
    #endregion

    #region Abilities
    public void LifeSteal()
    {
        //If berryPoitns is greater than or equal to 15.. 
        if (berryPoints >= 15)
        {
            //The heal float will determine how much health is stolen from the player and will range from 10-24 as the last value of an int is exclusive. 
            float heal = Random.Range(10, 25);
            //Call the Heal method and heal AI health by the heal float.
             Heal(heal);
            //Call the DealDamage method and take away the AI's health.
            _aiManager.DealDamage(heal);
            Debug.Log(heal);
            //Take away 15 berryPoints.
            berryPoints -= 15;
            //Set attackType to 3.
            attackType = 3;
            //Apply the value to the animator.
            _characterAnim.SetInteger("AttackType", attackType);
            //Change the event message.
            matchEvent.eventMessages = Cases.LifeSteal;
            //Run the UpdateBP method.
            UpdateBP();
            //Start the EndTurn coroutine.
            StartCoroutine(EndTurn());
        }
        else
        {
            Debug.Log("Not enough BP to perform this action!");
            //Change the event message. 
            matchEvent.eventMessages = Cases.NoBP;
            //Update the event text. 
            matchEvent.TextChange();
        }
        
    }

    public void BerryThrow()
    {
        //If berryPoints is greater than or equal to 5. 
        if (berryPoints >= 5)
        {
            //The critDamage float will determine if there is extra damage done to the AI. 
            float critDamage = Random.Range(0, 5);
            //Call the DealDamage method for the AIManager and add base damage plus critDamage.
            _aiManager.DealDamage(attackDamage + critDamage);
            Debug.Log(10 + critDamage);
            //Take away 5 berryPoints.
            berryPoints -= 5;
            //Change the event message.
            matchEvent.eventMessages = Cases.BerryThrow;
            //Set attackType to 1.
            attackType = 1;
            //Apply the value to the animator.
            _characterAnim.SetInteger("AttackType", attackType);
            //Run the UpdateBP method. 
            UpdateBP();
            //Start the EndTurn coroutine. 
            StartCoroutine(EndTurn());
        }
        else
        {
            Debug.Log("Not enough BP to perform this action!");
            //Change the event message.
            matchEvent.eventMessages = Cases.NoBP;
            //Update the event text.
            matchEvent.TextChange();
        }
    }

    public void BerryCrunch()
    {
        //If erryPoints is greater than or equal to 10.  
        if (berryPoints >= 10)
        {
            //Call the DealDamage method for the AIManager. 
            _aiManager.DealDamage(attackDamage + 10f);
            //Take away 10 berryPoints. 
            berryPoints -= 10;
            //Change the event message. 
            matchEvent.eventMessages = Cases.BerryCrunch;
            //Set attackType to 2.
            attackType = 2;
            //Apply the value to the animator.
            _characterAnim.SetInteger("AttackType", attackType);
            //Run the UpdateBP method. 
            UpdateBP();
            //Start the EndTurn coroutine. 
            StartCoroutine(EndTurn());
        }
        else
        {
            Debug.Log("Not enough BP to perform this action!");
            //Change the event message. 
            matchEvent.eventMessages = Cases.NoBP;
            //Update the event text.
            matchEvent.TextChange();
        }

    }

    public void Gnarl()
    {
        //If berryPoints is greater than or equal to 5.
        if (berryPoints >= 5)
        {
            //A chance ranging from 0-4 that will determine if the attack hits.
            int chance = Random.Range(0, 5);
            //Set attackType to 4.
            attackType = 4;
            //Apply the value to the animator.
            _characterAnim.SetInteger("AttackType", attackType);

            //If chance is greater than 1 and defence does not equal 0, lower the defence of the AI.
            if (chance >= 1 && _defence != 0)
            {
                //Lower the AIs defence.
                _aiManager.LowerDefence(1);
                //Update the event message.
                matchEvent.eventMessages = Cases.Gnarl;
                Debug.Log("Defence was lowered to:" + _aiManager._defence);
            }
            else
            {
                //Update the event message.
                matchEvent.eventMessages = Cases.MissedAttack;
                Debug.Log("The attack missed!");
            }
            //Take away 5 berryPoints.
            berryPoints -= 5;
            //Run the UpdateBP method. 
            UpdateBP();
            //Start the EndTurn coroutine. 
            StartCoroutine(EndTurn());
        }
        else
        {
            Debug.Log("Not enough BP to perform this action!");
            //Update the event message.
            matchEvent.eventMessages = Cases.NoBP;
            //Update the event text.
            matchEvent.TextChange();
        }
      

    }
    #endregion

    #region Damage Modifier Methods
    public void IncreaseDamage(float damageIncrease)
    {
        //Display the status effect.
        statusDisplay[0].gameObject.SetActive(true);
        //Increase attackDamage.
        attackDamage += (attackDamage / damageIncrease);
        

    }

    public void ReturnDamage()
    {
        //If attackDamage is greater than 15.
        if (attackDamage > 15f)
        {
            //Take away 1 from turnsUntilNormalDamage.
            turnsUntilNormalDamage--; 
        }

        //If turnsUntilNormalDamage isless than or equal to 0. 
        if (turnsUntilNormalDamage <= 0)
        {
            //Revert attackDamage to defaultDamage values.
            attackDamage = defaultDamage;
            Debug.Log("Attack was reverted to the default value");
            //Stop displaying the status effect.
            statusDisplay[0].gameObject.SetActive(false);
        }

    }
    #endregion

    #region Animator Methods
    public void ReturnToIdle()
    {
        //Change the attackType to 0 (idle state).
        attackType = 0;
        //Apply this value to the animator.
        _characterAnim.SetInteger("AttackType", attackType);
    }
    #endregion

}
