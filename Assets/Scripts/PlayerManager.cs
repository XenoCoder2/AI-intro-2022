using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : BaseManager
{
    private AIManager _aiManager;
    public int berryPoints = 50; 

    private void Start()
    {
        _aiManager = GetComponent<AIManager>();
        if (_aiManager == null)
        {
            Debug.LogError("No AIManager Found! :(");
        }
    }

    public void LifeSteal()
    {
        if (berryPoints >= 15)
        {
            float heal = Random.Range(10, 15);
            Heal(heal);
            _aiManager.DealDamage(heal);
            Debug.Log(heal);
            berryPoints -= 15;
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
            _aiManager.DealDamage(10f + critDamage);
            Debug.Log(10 + critDamage);
            berryPoints -= 5;
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
            _aiManager.DealDamage(15f);
            berryPoints -= 10;
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
        }
        else
        {
            Debug.Log("Not enough BP to perform this action!");
        }
      

    }
}
