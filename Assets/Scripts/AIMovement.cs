using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    //An array of GameObjects 
    [Tooltip("The positions the square will go to")]
    public Transform[] waypoints;
    public Transform player;
    [Header("Colours")]
    public Color aiColour;
    public Color aiColour2; 
    [Header("Sprite Renderer")]
    public SpriteRenderer sRender; 
    [Header("Values")]
    public int waypointIndex0 = 0;
    //Create the variable and place data in the variable, use the variable. :)
    //public GameObject position0, position1, player;
    public float speed = 1.5f;
    public float minGoalDistance = 0.1f;
    private float closestWaypoint = Mathf.Infinity;
    public float chaseDistance = 3.5f;
    public bool chased = false;

    void Update()
    {
      
        //Are we within the player chase distance
        if (Vector2.Distance(transform.position, player.position) < chaseDistance)
        {
            AIMove(player);
            chased = true;
        }

        else
        {
            //The number is called the index
            WaypointUpdate();
            AIMove(waypoints[waypointIndex0]);
        }

        if (chased)
        {
            sRender.color = aiColour;
        }
        else
        {
            sRender.color = aiColour2;
        }
        #region Definitions
        // < less than 
        // <= less than or equal
        // > greater than
        // >= greater than or equal 
        // == equals
        // != not equal
        #endregion
    }

    private void AIMove(Transform goal)
    {
        Vector2 aiTransform = transform.position;
        #region commented out code!!
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
        #endregion
        //If we are near the goal, stop!
        if (Vector2.Distance(aiTransform, goal.position) > minGoalDistance)
        {
            Vector2 directionToGoal = goal.transform.position - transform.position;
            directionToGoal.Normalize();
            transform.position += (Vector3)directionToGoal * 1 * Time.deltaTime;

        }

    }

    private void WaypointUpdate()
    {
        Vector2 aiTransform = transform.position;

        
        if (chased == true)
        {
           
            closestWaypoint = Mathf.Infinity;
            for (int i = 0; i < waypoints.Length; i++)
            {
                
                float dist = Vector2.Distance(aiTransform, waypoints[i].position);
                if (dist < closestWaypoint)
                {
                    waypointIndex0 = i;
                    closestWaypoint = dist;
                    chased = false;
                }
            }
           
        }

        //If we are near the goal
        if (Vector2.Distance(aiTransform, waypoints[waypointIndex0].position) < minGoalDistance)
        {
            waypointIndex0++;

            if (waypointIndex0 >= waypoints.Length)
            {
                waypointIndex0 = 0;
            }

            
        }
    }
}
