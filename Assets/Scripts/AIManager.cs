using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : PlayerManager
{
    //Different States of the AI.
   public enum State
    {
        FullHP,
        Damaged,
        LowHP,
        Dead
    }

    public static State currentState;

    private PlayerManager _pMan;

    //The scenario int will help determine whos turn it is and display the correct match event.
    [Header("Scenario for Match Events")]
    public int scenario;

    protected override void Start()
    {
        //Performs the Start actions from the inherited class.
        base.Start();

        //Gets the PlayerManager.
        _pMan = GetComponent<PlayerManager>();
    }

    public override void TakeTurn()
    {
        //If AI health is less than or equal to 0. 
        if (_health <= 0f)
        {
            //Change the AI state to the dead state.
            currentState = State.Dead;
            //Change the event message. 
            matchEvent.TextChange();
        }

        //Change the scenario int to 1, this is used for the MatchEvents script.
        scenario = 1;

        //Depending on the currentState of the AI
        switch (currentState)
        {
            //If the AI has FullHP (100%).
            case State.FullHP:
                //Run the FullHPState method.
                FullHPState();
                //Start the TurnEnd Coroutine.
                StartCoroutine(TurnEnd());
                break;
            //If the AI has LowHP (<40%).
            case State.LowHP:
                //Run the LowHPState method.
                LowHPState();
                //Start the TurnEnd Coroutine.
                StartCoroutine(TurnEnd());
                break;
            //If the AI is Damaged (<99%).
            case State.Damaged:
                //Run the DamagedState method.
                DamagedState();
                //Start the TurnEnd Coroutine.
                StartCoroutine(TurnEnd());
                break;
            //If the AI is Dead (0%).
            case State.Dead:
                Dead();
                break;
        }
    }

    IEnumerator TurnEnd()
    {
        //Change the event message. 
        matchEvent.TextChange();
        //Pause the coroutine for 2 seconds. 
        yield return new WaitForSeconds(2f);
        //Run the DefenceReturn method.
        DefenceReturn();
        //Change the scenario int to 0.
        scenario = 0;
        //Run the DefenceReturn method for the player.
        _pMan.DefenceReturn();
        //Run the ReturnDamage method for the player.
        _pMan.ReturnDamage();
        //Run the TakeTurn method for the player.
        _pMan.TakeTurn();
       
    }

    void FullHPState()
    {
        //If AI health is less than 100
        if(_health < 100f)
        {
            //Change the AI state to Damaged. 
            currentState = State.Damaged;
            //Run the DamagedState method.
            DamagedState();
            return;
        }

        //The randomAttack int will be used to determine what attack the AI will use. 
        int randomAttack = Random.Range(0, 10);
        Debug.Log("int: " + randomAttack);

        //Switch between the cases for randomAttack.
        switch (randomAttack)
        {
            //If randomAttack is greater than or equal to 0 and less than or equal to 2..
            case int i when i >= 0 && i <= 2:
                //Call the Crunch method.
                Crunch();
                break;
            //If randomAttack is greater than or equal to 3 and less than or equal to 5..
            case int i when i >= 3 && i <= 5:
                //Call the GnarlAttack method.
                GnarlAttack();
                break;
            //If randomAttack is greater than or equal to 6 and less than or equal to 9..
            case int i when i >= 6 && i <= 9:
                //Call the ThrowBerry method.
                ThrowBerry();
                break;

        }
    }

    void DamagedState()
    {
        //The randomAttack int will be used to determine what attack the AI will use. 
        int randomAttack = Random.Range(0, 10);
        Debug.Log("int: " + randomAttack);

        //Switch between the cases for randomAttack.
        switch (randomAttack)
        {
            //If randomAttack is greater than or equal to 0 and less than or equal to 2..
            case int i when i >= 0 && i <= 2:
                //Call the ThrowBerry method.
                ThrowBerry();
                break;
            //If randomAttack is greater than or equal to 3 and less than or equal to 4..
            case int i when i >= 3 && i <= 4:
                //Call the StealLife method.
                StealLife();
                break;
            //If randomAttack is greater than or equal to 4 and less than or equal to 7..
            case int i when i >= 4 && i <= 7:
                //Call the GnarlAttack method.
                GnarlAttack();
                break;
            //If randomAttack is greater than or equal to 8 and less than or equal to 9..
            case int i when i >= 8 && i <= 9:
                //Call the Crunch method.
                Crunch();
                break;

        }

        //If AI health is equal to 100.
        if (_health == 100f)
        {
            //Change the AI state to FullHP.
            currentState = State.FullHP;
            //Run the FullHPState method.
            FullHPState();
            return;
        }
        //If AI health is less than 40.
        else if (_health < 40f)
        {
            //Change the AI state to LowHP.
            currentState = State.LowHP;
            //Run the LowHPState.
            LowHPState();
            return;
        }

    }

    void LowHPState()
    {
        //The randomAttack int will be used to determine what attack the AI will use. 
        int randomAttack = Random.Range(0, 10);
        Debug.Log("int: " + randomAttack);

        //Switch between the cases for randomAttack.
        switch (randomAttack)
        {
            //If randomAttack is greater than or equal to 0 and less than or equal to 3..
            case int i when i >= 0 && i <= 3:
                //Call the ThrowBerry method.
                ThrowBerry();
                break;
            //If randomAttack is greater than or equal to 4 and less than or equal to 6..
            case int i when i >= 4 && i <= 6:
                //Call the StealLife method.
                StealLife();
                break;
            //If randomAttack is greater than or equal to 7 and less than or equal to 8..
            case int i when i >= 7 && i <= 8:
                //Call the GnarlAttack method.
                GnarlAttack();
                break;
            //If randomAttack is equal to 9..
            case int i when i == 9:
                //Call the Crunch method.
                Crunch();
                break;

        }
        //If the AI health is greater than 40.
        if (_health > 40f)
        {
            //Change the AI state to its Damaged behaviour.
            currentState = State.Damaged;
            //Activate the DamagedState method.
            DamagedState();
            return;
        }

    }

    void Dead()
    {
        Debug.Log("Brungleberry defeated!");
        //Change the event message. 
        matchEvent.eventMessages = Cases.BermonFainted;
        //Change the AI animation to the dead state. 
        characterAnim.SetBool("IsDead", true);
    }

    public void StealLife()
    {
        //The heal float will determine how much health is stolen from the player and will range from 10-24 as the last value of an int is exclusive. 
        float heal = Random.Range(10, 25);
        //Call the Heal method and heal AI health by the heal float. 
        Heal(heal);
        //Call the DealDamage method and take away the player's health.
        _pMan.DealDamage(heal);
        //Change the event message. 
        matchEvent.eventMessages = Cases.LifeSteal;
        Debug.Log("StealLife " + heal);

    }

    public void ThrowBerry()
    {
        //The critDamage float will determine if there is extra damage done to the player. 
        float critDamage = Random.Range(0, 5);
        //Change the event message. 
        matchEvent.eventMessages = Cases.BerryThrow;
        //Call the DealDamage method and add base damage plus critDamage.
        _pMan.DealDamage(15f + critDamage);
        Debug.Log(10 + critDamage + " | AI");

       
    }

    public void Crunch()
    {
        //Call the DealDamage method for the player 
        _pMan.DealDamage(25f);
        //Change the event message.
        matchEvent.eventMessages = Cases.BerryCrunch;
        Debug.Log("Crunch was used on player");
        

    }

    public void GnarlAttack()
    {
        //A chance ranging from 0-4 that will determine if the attack hits.
        int chance = Random.Range(0, 5);

        //If chance is greater than 1 and defence does not equal 0, lower the defence of the player.
        if (chance >= 1 && _defence != 0)
        {
            //Lower the players defence.
            _pMan.LowerDefence(1);
            //Change the event message.
            matchEvent.eventMessages = Cases.Gnarl;
            Debug.Log("Lowered defence of player");
        }
        else
        {
            //If the conditions aren't met, change the condition to show that the attack had missed. 
            matchEvent.eventMessages = Cases.MissedAttack;
            Debug.Log("The attack missed!");
        }
       

    }

 
}
