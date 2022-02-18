using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 5f; 

    // Start is called before the first frame update
    void Start()
    {
        
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

        transform.position += (Vector3)moveDirection;
    }
}
