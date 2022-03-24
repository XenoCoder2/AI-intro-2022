using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryChaser : MonoBehaviour
{
    #region Variables
    public Transform spawnWaypoint;
    public Transform chaseTarget;
    #endregion

    #region Update
    private void Update()
    {
        //If the switch is active.
        if(Switch.active)
        {
            //Run the Chase method.
            Chase();
        }
        else
        {
            //Run the GoBackToSpawn method.
            GoBackToSpawn();
        }
    }
    #endregion

    #region Movement Methods
    public void Chase()
    {
        //Create a new Vector2 variable called directionToAi that will direct the BerryChaser to the AI.
        Vector2 directionToAI = chaseTarget.position - transform.position;
        //Normalise the directionToAI variable.
        directionToAI.Normalize();
        //Apply the directionToAI variable to the transform.position of this GameObject and multiply it by deltaTime.
        transform.position += 1 * Time.deltaTime * (Vector3)directionToAI;

    }

    public void GoBackToSpawn()
    {
        //Create a new Vector2 variable called moveToSpawn that will direct the berryChaser to its spawnpoint.
        Vector2 moveToSpawn = spawnWaypoint.position - transform.position;
        //Normalise the moveToSpawn variable.
        moveToSpawn.Normalize();
        //Apply the moveToSpawn variable to the transform.position of this GameObject and multiply it by deltaTime.
        transform.position += 1 * Time.deltaTime * (Vector3)moveToSpawn;

        //If the transform.position of this GameObject is equal to the spawnWaypoint.position.
        if (transform.position == spawnWaypoint.position)
        {
            //Disable the GameObject.
            gameObject.SetActive(false);
        }
    }
    #endregion

}
