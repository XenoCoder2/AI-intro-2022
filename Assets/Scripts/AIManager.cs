using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : PlayerManager
{
    //Different States of the AI
   public enum State
    {
        FullHP,
        Damaged,
        LowHP,
        Dead
    }

    public static State currentState;

    private PlayerManager _pMan;

    //The scenario int will help determine whos turn it is and display the correct match event
    [Header("Scenario for Match Events")]
    public int scenario;

    protected override void Start()
    {
        //Performs the Start actions from the inherited class
        base.Start();

        //Gets the PlayerManager
        _pMan = GetComponent<PlayerManager>();
    }

    public override void TakeTurn()
    {
        if (_health <= 0f)
        {
            currentState = State.Dead;
            matchEvent.TextChange();
        }

        scenario = 1;

        //Depending on the currentState of the AI
        switch (currentState)
        {
            case State.FullHP:
                FullHPState();
                StartCoroutine(TurnEnd());
                break;
            case State.LowHP:
                LowHPState();
                StartCoroutine(TurnEnd());
                break;
            case State.Damaged:
                DamagedState();
                StartCoroutine(TurnEnd());
                break;
            case State.Dead:
                Dead();
                break;
        }
    }

    IEnumerator TurnEnd()
    {
        matchEvent.TextChange();
        yield return new WaitForSeconds(2f);
        DefenceReturn();
        scenario = 0;
        _pMan.DefenceReturn();
        _pMan.ReturnDamage();
        _pMan.TakeTurn();
       
    }
    void FullHPState()
    {
        if(_health < 100f)
        {
            currentState = State.Damaged;
            DamagedState();
            return;
        }
        

        int randomAttack = Random.Range(0, 10);
        Debug.Log("int: " + randomAttack);

        switch (randomAttack)
        {
            case int i when i >= 0 && i <= 2:
                Crunch();
                break;
            case int i when i >= 3 && i <= 5:
                Gnarly();
                break;
            case int i when i >= 6 && i <= 9:
                ThrowBerry();
                break;

        }
    }

    void DamagedState()
    {
        int randomAttack = Random.Range(0, 10);
        Debug.Log("int: " + randomAttack);

        switch (randomAttack)
        {
            case int i when i >= 0 && i <= 2:
                ThrowBerry();
                break;
            case int i when i >= 3 && i <= 4:
                StealLife();
                break;
            case int i when i >= 4 && i <= 7:
                Gnarly();
                break;
            case int i when i >= 8 && i <= 9:
                Crunch();
                break;

        }

        if (_health == 100f)
        {
            currentState = State.FullHP;
            FullHPState();
            return;
        }
        else if (_health < 40f)
        {
            currentState = State.LowHP;
            LowHPState();
            return;
        }

    }

    void LowHPState()
    {
        int randomAttack = Random.Range(0, 10);
        Debug.Log("int: " + randomAttack);

        switch (randomAttack)
        {
            case int i when i >= 0 && i <= 3:
                ThrowBerry();
                break;
            case int i when i >= 4 && i <= 6:
                StealLife();
                break;
            case int i when i >= 7 && i <= 8:
                Gnarly();
                break;
            case int i when i == 9:
                Crunch();
                break;

        }

        if (_health > 40f)
        {
            currentState = State.Damaged;
            DamagedState();
            return;
        }

    }

    void Dead()
    {
        Debug.Log("Brungleberry defeated!");
        matchEvent.eventMessages = Cases.BermonFainted;
        characterAnim.SetBool("IsDead", true);
    }

    public void StealLife()
    {
        float heal = Random.Range(10, 25);
        _aiManager.Heal(heal);
        _pMan.DealDamage(heal);
        matchEvent.eventMessages = Cases.LifeSteal;
        Debug.Log("StealLife " + heal);

    }

    public void ThrowBerry()
    {
        float critDamage = Random.Range(0, 5);
        matchEvent.eventMessages = Cases.BerryThrow;
        _pMan.DealDamage(15f + critDamage);
        Debug.Log(10 + critDamage + " | AI");

       
    }

    public void Crunch()
    {
        _pMan.DealDamage(25f);
        matchEvent.eventMessages = Cases.BerryCrunch;
        Debug.Log("Crunch was used on player");
        

    }

    public void Gnarly()
    {
        int chance = Random.Range(0, 5);

        if (chance >= 1 && _defence != 0)
        {
            _pMan.LowerDefence(1);
            matchEvent.eventMessages = Cases.Gnarl;
            Debug.Log("Lowered defence of player");
        }
        else
        {
            matchEvent.eventMessages = Cases.MissedAttack;
            Debug.Log("The attack missed!");
        }
       

    }

 
}
