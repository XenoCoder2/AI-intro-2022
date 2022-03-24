using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseManager : MonoBehaviour
{
    #region Variables
    //Protected is basically private, but inherited classes also have access to it
    [Header("Health Values")]
    [SerializeField] protected float _health = 100;

    [SerializeField] protected float _maxHealth = 100;

    [Header("Defence Value")]
    [SerializeField] protected int _defence = 5;

    [Header("Health Display Text")]
    [SerializeField] protected Text _healthText;

    [Header("Status Effect Display")]
    [SerializeField] protected Image[] statusDisplay = new Image[3];

    [Header("Turns Until Defence Is Normal")]
    [SerializeField] protected int turnsTillNormalDefence = 6;
    #endregion

    #region Start Method
    //Virtual allows the function to be "overridden" by child classes
    //override replaces parent class' function (must be marked virtual)
    protected virtual void Start()
    {
        UpdateHealthText();
    }
    #endregion

    #region Update Health Text Method
    public void UpdateHealthText()
    {
        if (_healthText != null)
        {
            _healthText.text = _health.ToString("0");
        }
       
    }
    #endregion

    #region Abstract Take Turn Method
    //Abstract classes cannot be used, only children of abstract classes
    //Abstract function (inside an abstract class) has to be implemented by child classes
    public abstract void TakeTurn();
    #endregion

    #region Heal and Damage Methods
    public void Heal(float heal)
    {
        //If _health + heal is less than _maxHealth, increase health..
        _health = Mathf.Min(_health + heal, _maxHealth);
        //Update the health text. 
        UpdateHealthText();
    }

    public void DealDamage(float damage)
    {
        //If _health - damage + _defence is greater than 0, take away health from the character.
        _health = Mathf.Max(_health - damage + _defence, 0);
        
        //If health is less than or equal to 0.
        if (_health <= 0)
        {
            //Change _health to 0 in case it is a negative number. 
            _health = 0; 
            Debug.Log("Is Dead");
        }

        //Update the health text. 
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
    #endregion

    #region Defence Methods
    public void LowerDefence(int defence)
    {
        //If _defence - defence is greater than 0, lower defence.
        _defence = Mathf.Max(_defence - defence, 0);
        //Display the status effect.
        statusDisplay[2].gameObject.SetActive(true);

    }

    public void NormaliseDefence()
    {
        //If _defence is greater than 5. 
        if (_defence > 5)
        {
            //Display the status effect.
            statusDisplay[1].gameObject.SetActive(true);
        }
        else
        {
            //Hide the status effect.
            statusDisplay[2].gameObject.SetActive(false);
        }
        //Set _defence back to 5. 
        _defence = 5;
        Debug.Log("Defence has reverted to normal");

    }

    public void DefenceReturn()
    {
        //If _defence is less than or equal to 4..
        if (_defence <= 4)
        {
            //Take away 1 from turnsTillNormalDefence.
            turnsTillNormalDefence--;
        }

        //If turnsTillNormalDefence is less than or equal to 0..
        if (turnsTillNormalDefence <= 0)
        {
            if (_defence <= 4 || _defence == 5)
            {
                //Disable the defence down status effect.
                statusDisplay[2].gameObject.SetActive(false);
            }
            else if (_defence >= 6 || _defence == 5)
            {
                //Disable the defence up status effect.
                statusDisplay[1].gameObject.SetActive(false);
            }
            //Reset _defence back to 5. 
            _defence = 5;
            //Reset the turnsTillNormalDefence value.
            turnsTillNormalDefence = 3;
        }
        
        //If _defence is greater than 5.
        if (_defence > 5)
        {
            //Take away 1 from turnsTillNormalDefence. 
            turnsTillNormalDefence--;
        }
    }

    public void IncreaseDefence(int defenceIncrease)
    {
        //Display the status effect.
        statusDisplay[1].gameObject.SetActive(true);
        //If _defence + defenceIncrease is less than 10, increase defence. 
        _defence = Mathf.Min(_defence + defenceIncrease, 10);
    }
    #endregion
}
