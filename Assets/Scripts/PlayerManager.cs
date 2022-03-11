using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : BaseManager
{
    protected AIManager _aiManager;
    protected int berryPoints = 50;
    public Text _bpText;
    [SerializeField] protected CanvasGroup _canGroup;

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
        _canGroup.interactable = true;
    }

    public void EndTurn()
    {
        _canGroup.interactable = false;
        _aiManager.TakeTurn();

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
            UpdateBP();
            EndTurn();
        }
        else
        {
            Debug.Log("Not enough BP to perform this action!");
        }
        
    }

    public void BerryThrow()
    {
        if (berryPoints >= 5)
        {
            float critDamage = Random.Range(0, 5);
            _aiManager.DealDamage(15f + critDamage);
            Debug.Log(10 + critDamage);
            berryPoints -= 5;
            UpdateBP();
            EndTurn();
        }
        else
        {
            Debug.Log("Not enough BP to perform this action!");
        }
    }

    public void BerryCrunch()
    {
        if (berryPoints >= 10)
        {
            _aiManager.DealDamage(25f);
            berryPoints -= 10;
            UpdateBP();
            EndTurn();
        }
        else
        {
            Debug.Log("Not enough BP to perform this action!");
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
                Debug.Log("Defence was lowered to:" + _aiManager._defence);
            }
            else
            {
                Debug.Log("The attack missed!");
            }
            berryPoints -= 5;
            UpdateBP();
            EndTurn();
        }
        else
        {
            Debug.Log("Not enough BP to perform this action!");
        }
      

    }

    
}
