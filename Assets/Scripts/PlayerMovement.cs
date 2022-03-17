using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5f;
    public Switch switchControl;
    public bool inSwitch; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Run the Inputs method.
        Inputs();
    }

    private void Inputs()
    {
        //Initialise moveDirection and set it to 0,0,0.
        Vector2 moveDirection = Vector2.zero;
        //If the D key is pressed.
        if (Input.GetKey(KeyCode.D))
        {
            //Increase the x of moveDirection.x by speed, multiplied by Time.DeltaTime.
            moveDirection.x += speed * Time.deltaTime;
        }
        //If the A key is pressed.
        if (Input.GetKey(KeyCode.A))
        {
            //Decrease the x of moveDirection.x by speed, multiplied by Time.DeltaTime.
            moveDirection.x -= speed * Time.deltaTime;
        }
        //If the W key is pressed.
        if (Input.GetKey(KeyCode.W))
        {
            //Increase the y of moveDirection.y by speed, multiplied by Time.DeltaTime.
            moveDirection.y += speed * Time.deltaTime;
        }
        //If the S key is pressed.
        if (Input.GetKey(KeyCode.S))
        {
            //Decrease the y of moveDirection.y by speed, multiplied by Time.DeltaTime.
            moveDirection.y -= speed * Time.deltaTime;
        }
        //If the bool inSwitch is true.
        if (inSwitch)
        {
            //If the F key is pressed down.
            if (Input.GetKeyDown(KeyCode.F))
            {
                //Change the switch value to opposite of what it is currently.
                Switch.active = !Switch.active;

                //If the switch is active.
                if (Switch.active)
                {
                    //Change the animation bool of "Switch" to true.
                    switchControl.switchAnim.SetBool("Switch", true);
                }
                else
                {
                    //Change the animation bool of "Switch" to false.
                    switchControl.switchAnim.SetBool("Switch", false);
                }
            }



        }
        //Add the moveDirection to transform.position.
        transform.position += (Vector3)moveDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the tag of the trigger is equal to "Switch".
        if (collision.CompareTag("Switch"))
        {
            //Change the inSwitch bool to true.
            inSwitch = true;
            //Enable the interactText.
            switchControl.interactText.SetActive(true);

           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //If the tag of the trigger is equal to "Switch".
        if (collision.CompareTag("Switch"))
        {
            //Change the inSwitch bool to false.
            inSwitch = false;
            //Disable the interactText.
            switchControl.interactText.SetActive(false);
        }
    }
}
