using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : BaseManager
{
    [Header("AI Manager")]
    protected AIManager _aiManager;
    protected int berryPoints = 50;
    [Header("Berry Points Text | Leave blank for AI Manager")]
    public Text _bpText;
    [Header("Canvas Group")]
    [SerializeField] protected CanvasGroup _canGroup;
    [Header("Attack Values")]
    private float attackDamage = 15f;
    private float defaultDamage = 15f;
    private int turnsUntilNormalDamage = 4;
    [Header("Animator")]
    public Animator characterAnim;
    [Header("Match Events for text box")]
    public MatchEvents matchEvent;

    protected override void Start()
    {
        base.Start();

        _aiManager = GetComponent<AIManager>();
        if (_aiManager == null)
        {
            Debug.LogError("No AIManager Found! :(");
        }
    }

    public override void TakeTurn()
    {
        if (_health > 0)
        {
            _canGroup.interactable = true;
           
        }
        else
        {
            DeadPlayer();
        }
        
    }

    public IEnumerator EndTurn()
    {
        _canGroup.interactable = false;
        matchEvent.TextChange();
        yield return new WaitForSeconds(2f);
        _aiManager.TakeTurn();
        DefenceReturn();
        ReturnDamage();
       
    }

    public void DeadPlayer()
    {
        characterAnim.SetBool("IsDead", true);

    }

    private void UpdateBP()
    {
        _bpText.text = berryPoints.ToString();

    }

    public void LifeSteal()
    {
        if (berryPoints >= 15)
        {
            float heal = Random.Range(10, 25);
            Heal(heal);
            _aiManager.DealDamage(heal);
            Debug.Log(heal);
            berryPoints -= 15;
            matchEvent.eventMessages = Cases.LifeSteal;
            UpdateBP();
            StartCoroutine(EndTurn());
        }
        else
        {
            Debug.Log("Not enough BP to perform this action!");
            matchEvent.eventMessages = Cases.NoBP;
            matchEvent.TextChange();
        }
        
    }

    public void BerryThrow()
    {
        if (berryPoints >= 5)
        {
            float critDamage = Random.Range(0, 5);
            _aiManager.DealDamage(attackDamage + critDamage);
            Debug.Log(10 + critDamage);
            berryPoints -= 5;
            matchEvent.eventMessages = Cases.BerryThrow;
            UpdateBP();
            StartCoroutine(EndTurn());
        }
        else
        {
            Debug.Log("Not enough BP to perform this action!");
            matchEvent.eventMessages = Cases.NoBP;
            matchEvent.TextChange();
        }
    }

    public void BerryCrunch()
    {
        if (berryPoints >= 10)
        {
            _aiManager.DealDamage(attackDamage + 10f);
            berryPoints -= 10;
            matchEvent.eventMessages = Cases.BerryCrunch;
            UpdateBP();
            StartCoroutine(EndTurn());
        }
        else
        {
            Debug.Log("Not enough BP to perform this action!");
            matchEvent.eventMessages = Cases.NoBP;
            matchEvent.TextChange();
        }

    }

    public void Gnarl()
    {
        if (berryPoints >= 5)
        {
            int chance = Random.Range(0, 3);

            if (chance == 2 && _defence != 0)
            {
                _aiManager.LowerDefence(1);
                matchEvent.eventMessages = Cases.Gnarl;
                Debug.Log("Defence was lowered to:" + _aiManager._defence);
            }
            else
            {
                matchEvent.eventMessages = Cases.MissedAttack;
                Debug.Log("The attack missed!");
            }
            berryPoints -= 5;
            UpdateBP();
            StartCoroutine(EndTurn());
        }
        else
        {
            Debug.Log("Not enough BP to perform this action!");
            matchEvent.eventMessages = Cases.NoBP;
            matchEvent.TextChange();
        }
      

    }

    public void RecoverBP(int bpAmount)
    {
        berryPoints = Mathf.Min(berryPoints + bpAmount, 50);
        UpdateBP();
    }

    public void IncreaseDamage(float damageIncrease)
    {
        attackDamage += (attackDamage / damageIncrease);

    }

    public void ReturnDamage()
    {
        if (attackDamage > 15f)
        {
            turnsUntilNormalDamage--; 
        }

        if (turnsUntilNormalDamage <= 0)
        {
            attackDamage = defaultDamage;
            Debug.Log("Attack was reverted to the default value");
        }

    }    

}
