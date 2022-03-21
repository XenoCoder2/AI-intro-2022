using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    #region Variables
    //An array of GameObjects 
    [Tooltip("The positions the square will go to")]
    public List<Transform> waypoints;
    public Transform player;
    public GameObject waypointPrefab; 
    [Header("Colours")]
    public Color aiColour;
    public Color aiColour2; 
    [Header("Sprite Renderer")]
    public SpriteRenderer sRender; 
    [Header("Values")]
    public int waypointIndex = 0;
    //Create the variable and place data in the variable, use the variable. :)
    //public GameObject position0, position1, player;
    public float speed = 1.5f;
    public float minGoalDistance = 0.1f;
    private float _closestWaypoint = Mathf.Infinity;
    public float chaseDistance = 3.5f;
    public bool chased = false;
    public static bool defending; 
    private float _timer = 1.5f;
    #endregion

    #region Update Method
    void Update()
    {
        //If the defending bool is true.
        if (defending)
        {
            //Take away Time.deltaTime from the timer float.
            _timer -= Time.deltaTime;
            //If the timer is less than or equal to 0.
            if (_timer <= 0)
            {
                //Run the NewWaypoint method.
                NewWaypoint();
                //Set the timer to 1.5f.
                _timer = 1.5f; 
            }
        }

        /*
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
        */
    }
    #endregion

    #region Spawn New Waypoint Method
    public void NewWaypoint()
    {
        //Instantiate a new Berry Prefab.
        GameObject newBerry = Instantiate(waypointPrefab, new Vector2(Random.Range(-5f, 5f), Random.Range(-5f, 5f)), Quaternion.identity);
        //Add the newBerry gameObject to the waypoints list.
        waypoints.Add(newBerry.transform);
    }
    #endregion

    #region AI Movement Behaviour Methods
    public void AIMove(Transform goal)
    {
        //Set the vector of aiTransform to the transform.position of the AI.
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
            //Set the directionToGoal Vector2 from a calculation of goal.position minus transform.position.
            Vector2 directionToGoal = goal.position - transform.position;
            //Normalise the directionToGoal Vector2.
            directionToGoal.Normalize();
            //Add 1 * Time.deltaTime * directionToGoal to the transform.position of the AI.
            transform.position += 1 * Time.deltaTime * (Vector3)directionToGoal;

        }

    }

    public void AIDefence (Transform moveAway)
    {
        //If the distance between the transform.position and moveAway.position is greater than or equal to 3.5f.
        if (Vector2.Distance(transform.position, moveAway.position) <= 3.5f) 
        {
            //Initiliase the defenceMove variable.
            Vector2 defenceMove = moveAway.position - transform.position;
            //Normalise the defenceMove variable.
            defenceMove.Normalize();
            //Make the AI move away from the player when close.
            transform.position -= 3 * Time.deltaTime * (Vector3)defenceMove; 
        }
    }

    public void AIRun (Transform runAway)
    {
        //Initialise the runMove variable.
        Vector2 runMove = runAway.position - transform.position;
        //Normalise the runMove variable.
        runMove.Normalize();
        //Move the AI in a direction away from the player.
        transform.position -= 2.5f * Time.deltaTime * (Vector3)runMove; 

    }
    #endregion

    #region Waypoint Update Method
    //array.Length = List.Count
    public void WaypointUpdate()
    {
        //Set aiTransform to the transform.position of the AI.
        Vector2 aiTransform = transform.position;

        //If the chased bool is true.
        if (chased == true)
        {
            //Set the closestWaypoint to an infinite value, this will be changed in the for loop to determine the closest waypoint.
            _closestWaypoint = Mathf.Infinity;
            for (int i = 0; i < waypoints.Count; i++)
            {
                //Initialise the float dist to the distance between aiTransform and the current waypoint.
                float dist = Vector2.Distance(aiTransform, waypoints[i].position);
                //If dist is less than closestWaypoint.
                if (dist < _closestWaypoint)
                {
                    //Set the waypointIndex to the current waypoint (closest).
                    waypointIndex = i;
                    //Change the closestWaypoint variable to dist.
                    _closestWaypoint = dist;
                    //Change chased to false.
                    chased = false;
                }
            }
          
        }

        //If we are near the goal
        if (Vector2.Distance(aiTransform, waypoints[waypointIndex].position) < minGoalDistance)
        {
           //Destroy the waypoint gameobject.
            Destroy(waypoints[waypointIndex].gameObject);
            //Remove the waypoint from the list.
            waypoints.RemoveAt(waypointIndex);
            //Increase waypointIndex by 1.
            waypointIndex++;

            //If waypointIndex is greater than or equal to waypoints.Count.
            if (waypointIndex >= waypoints.Count)
            {
                //Set waypointIndex to 0.
                waypointIndex = 0;
            }

            

        }
    }
    #endregion
}
