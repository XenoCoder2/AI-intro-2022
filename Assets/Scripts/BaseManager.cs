using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseManager : MonoBehaviour
{
    //Protected is basically private, but inherited classes also have access to it
    [Header("Health Values")]
    [SerializeField] protected float _health = 100;

    [SerializeField] protected float _maxHealth = 100;

    [Header("Defence Value")]
    [SerializeField] protected int _defence = 5;

    [Header("Health Display Text")]
    [SerializeField] protected Text _healthText;

    [Header("Turns Until Defence Is Normal")]
    [SerializeField] protected int turnsTillNormalDefence = 6;

    //Virtual allows the function to be "overridden" by child classes
    //override replaces parent class' function (must be marked virtual)
    protected virtual void Start()
    {
        UpdateHealthText();
    }

    public void UpdateHealthText()
    {
        if (_healthText != null)
        {
            _healthText.text = _health.ToString("0");
        }
       
    }

    //Abstract classes cannot be used, only children of abstract classes
    //Abstract function (inside an abstract class) has to be implemented by child classes
    public abstract void TakeTurn();

    public void Heal(float heal)
    {
        _health = Mathf.Min(_health + heal, _maxHealth);
        UpdateHealthText();
    }

    public void DealDamage(float damage)
    {
        _health = Mathf.Max(_health - damage + _defence, 0);
        
        if (_health <= 0)
        {
            _health = 0; 
            Debug.Log("Is Dead");
        }
        UpdateHealthText();
    }

    /*
    private IEnumerator HealOverTime (float waitTime)
    {
        for (int i = 0; i < 3; i++)
        {
            Heal(10f);
            yield return new WaitForSeconds(waitTime);
        }

    }
    */

    public void LowerDefence(int defence)
    {
        _defence = Mathf.Max(_defence - defence, 0);
        

    }

    public void normaliseDefence()
    {
        _defence = 5;
        Debug.Log("Defence has reverted to normal");

    }

    public void defenceReturn()
    {
        if (_defence <= 4)
        {
            turnsTillNormalDefence--;
        }

        if (turnsTillNormalDefence <= 0)
        {
            _defence = 5;
            turnsTillNormalDefence = 3;
        }

        if (_defence > 5)
        {
            turnsTillNormalDefence = 4;
            turnsTillNormalDefence--;
        }
    }

    public void increaseDefence(int defenceIncrease)
    {
        _defence = Mathf.Min(_defence + defenceIncrease, 10);
    }    
}
