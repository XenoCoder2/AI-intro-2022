using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    //Create the variable and place data in the variable, use the variable. :)
    public GameObject position0;
    public GameObject position1;
    

    
    void Update()
    {
        Vector2 aiTransform = transform.position;
        //transform.position = Vector2.MoveTowards(transform.position, position1.transform.position, Time.deltaTime);
        /*if (transform.position.x < position1.transform.position.x)
        {
            //Move right
            
            aiTransform.x += 1 * Time.deltaTime * 3;
            transform.position = aiTransform;

        }
        else 
        {
            aiTransform.x -= 1 * Time.deltaTime * 3;
            transform.position = aiTransform;
        }

        if (transform.position.y < position1.transform.position.y)
         {
             transform.position += Vector3.up * 1 * Time.deltaTime;
         }    
         else
         {
             transform.position -= Vector3.up * 1 * Time.deltaTime;
         }
        */

        Vector2 directionToPos1 = position1.transform.position - transform.position;
        directionToPos1.Normalize();
        transform.position += (Vector3) directionToPos1 * 1 * Time.deltaTime; 
    }
}
