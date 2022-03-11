using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : PlayerManager
{
   public enum State
    {
        FullHP,
        Damaged,
        LowHP,
        Dead
    }

    public State currentState;

    private PlayerManager _pMan;

    protected override void Start()
    {
        base.Start();

        _pMan = GetComponent<PlayerManager>();
    }

    public override void TakeTurn()
    {
        if (_health <= 0f)
        {
            currentState = State.Dead;
        }

        switch(currentState)
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
        yield return new WaitForSeconds(2f);
        defenceReturn();
        _pMan.defenceReturn();
        _pMan.returnDamage();
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

    }

    void LowHPState()
    {
        int randomAttack = Random.Range(0, 10);
        Debug.Log("int: " + randomAttack);

        switch (randomAttack)
        {
            case int i when i >= 0 && i <= 2:
                ThrowBerry();
                break;
            case int i when i >= 3 && i <= 6:
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
    }

    public void StealLife()
    {
        float heal = Random.Range(10, 25);
        _aiManager.Heal(heal);
        _pMan.DealDamage(heal);
        Debug.Log("StealLife " + heal);

    }

    public void ThrowBerry()
    {
        float critDamage = Random.Range(0, 5);
        _pMan.DealDamage(15f + critDamage);
        Debug.Log(10 + critDamage + " | AI");

       
    }

    public void Crunch()
    {
        _pMan.DealDamage(25f);
        Debug.Log("Crunch was used on player");
        

    }

    public void Gnarly()
    {
        int chance = Random.Range(0, 3);

        if (chance == 2 && _defence != 0)
        {
            _pMan.LowerDefence(1);
            Debug.Log("Lowered defence of player");
        }
        else
        {
            Debug.Log("The attack missed!");
        }
       

    }

 
}
