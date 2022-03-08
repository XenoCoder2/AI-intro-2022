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
        Application.targetFrameRate = 144;
    }

    // Update is called once per frame
    void Update()
    {
        Inputs();
    }

    private void Inputs()
    {
        Vector2 moveDirection = Vector2.zero;
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection.x += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection.x -= speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection.y += speed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection.y -= speed * Time.deltaTime;
        }
        if (inSwitch)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Switch.active = !Switch.active;

                if (Switch.active)
                {
                    switchControl.switchAnim.SetBool("Switch", true);
                }
                else
                {
                    switchControl.switchAnim.SetBool("Switch", false);
                }
            }



        }
        transform.position += (Vector3)moveDirection;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Switch"))
        {
            inSwitch = true;
            switchControl.interactText.SetActive(true);

           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Switch"))
        {
            inSwitch = false; 
            switchControl.interactText.SetActive(false);
        }
    }
}
