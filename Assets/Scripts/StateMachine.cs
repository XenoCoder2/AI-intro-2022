using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIMovement))]
public class StateMachine : MonoBehaviour
{
    #region States
    //A comma separated list of identifiers
    public enum State
    {
        Attack,
        Defence,
        Run,
        BerryPicking
    }

    public State currentState;
    #endregion

    #region Variables
    public AIMovement aiMovement;
    public StatePanel panelState;
    #endregion

    #region Start Method
    private void Start()
    {
        aiMovement = GetComponent<AIMovement>();
        //Runs the NextState method.
        NextState();

        //Addition(Random.Range(0, 100), Random.Range(0, 100));
    }
    #endregion

    #region Next State Method
    private void NextState()
    {
        //Runs one of the cases that matches the value (in this example the value is currentState).
        switch (currentState)
        {
            //If the currentState is equal to State.Attack.
            case State.Attack:
                panelState.UpdateStatusText("Attack");
                //Start the AttackState coroutine.
                StartCoroutine(AttackState());
                break;
            //If the currentState is equal to State.Defence.
            case State.Defence:
                panelState.UpdateStatusText("Defence");
                //Start the DefenceState coroutine.
                StartCoroutine(DefenceState());
                break;
            //If the currentState is equal to State.Run.
            case State.Run:
                panelState.UpdateStatusText("Run");
                //Start the RunState coroutine.
                StartCoroutine(RunState());
                break;
            //If the currentState is equal to State.BerryPicking.
            case State.BerryPicking:
                panelState.UpdateStatusText("Berry Picking");
                //Start the BerryPickingState coroutine.
                StartCoroutine(BerryPickingState());
                break;
        }
    }
    #endregion

    #region Example Int Method (Unused) 
    /*
    private int Addition(int a, int b)
    {
        int answer = a + b;
        Debug.Log(answer);
        return answer;
       
    }
    */
    #endregion

    #region Update Method
    private void Update()
    {
        //If chased is equal to true.
        if (aiMovement.chased)
        {
            //Change the AIs colour to aiColour.
            aiMovement.sRender.color = aiMovement.aiColour;
        }
        else
        {
            //Change the AIs colour to aiColour2.
            aiMovement.sRender.color =  aiMovement.aiColour2;
        }
    }
    #endregion

    #region AI States
    //Coroutine is a special method that can be paused and returned to later
    private IEnumerator AttackState()
    {
        /*
        Debug.Log("First log");
        Yield return pauses running of our coroutine
        yield return null; //Return to method on the very next frame 
        Debug.Log("Second log");
        */

        Debug.Log("Attack: Enter"); 
        //Whilst the currentState is equal to State.Attack.
        while(currentState == State.Attack)
        {
            //Change the AIs target to the player.
            aiMovement.AIMove(aiMovement.player);
            //Change the chased value to true.
            aiMovement.chased = true;
            
            //If the player's distance is further than the chase distance.
            if (Vector2.Distance(transform.position, aiMovement.player.position) >= aiMovement.chaseDistance)
            {
                //Change the currentState to State.BerryPicking. 
                currentState = State.BerryPicking; 
            }
            //If the Switch is active.
            if (Switch.active)
            {
                //Change the currentState to State.Run. 
                currentState = State.Run;
            }

            //Return a null value.
            yield return null;

        }
        Debug.Log("Attack: Exit");
        //Run the NextState method.
        NextState();
    }
    private IEnumerator DefenceState()
    {
        Debug.Log("Defence: Enter");
        //Change the defending bool to true.
        AIMovement.defending = true;
        //Whilst the currentState is equal to State.Defence.
        while (currentState == State.Defence)
        {
            //The AI should move away from the player when the player is close. 
            aiMovement.AIDefence(aiMovement.player.transform);
            //If the waypoints.Count is equal to 5.
            if (aiMovement.waypoints.Count == 5)
            {
                //Set the currentState to State.BerryPicking.
                currentState = State.BerryPicking;
            }

            //If the switch is active.
            if (Switch.active)
            {
                //Set the currentState to State.Run.
                currentState = State.Run;
            }

            //Return a null value.
            yield return null;

        }
        //Set the defending bool to false. 
        AIMovement.defending = false; 
        Debug.Log("Defence: Exit");

        //Run the NextState method.
        NextState();
    }
    private IEnumerator RunState()
    {
        Debug.Log("Run: Enter");
        //Whilst the currentState is equal to State.Run.
        while (currentState == State.Run)
        {
            //If the switch is not active.
            if (!Switch.active)
            {
                //If waypoints.Count is greater than or equal to 1.
                if (aiMovement.waypoints.Count >= 1)
                {
                    //Set the currentState to State.BerryPicking.
                    currentState = State.BerryPicking;
                }
                else
                {
                    //Set the currentState to State.Defence.
                    currentState = State.Defence;
                }
                
            }

            //Move the AI away from the player.
            aiMovement.AIRun(aiMovement.player.transform);

            //Return a null value.
            yield return null;

        }
        Debug.Log("Run: Exit");

        //Run the NextState method.
        NextState();
    }
   
    private IEnumerator BerryPickingState()
    {
        Debug.Log("Berry Picking: Enter");
        //Once every frame, similar to update function
        while (currentState == State.BerryPicking)
        {
            //Run the WaypointUpdate method.
            aiMovement.WaypointUpdate();

            //If the switch is active.
            if (Switch.active)
            {
                //Change the currentState to State.Run.
                currentState = State.Run;
            }

            //If waypoints.Count does not equal 0.
            if (aiMovement.waypoints.Count != 0)
            {
                //Run the AIMove method with the waypoints as the targets.
                aiMovement.AIMove(aiMovement.waypoints[aiMovement.waypointIndex]);
            }
            else
            {
                //Change the currentState to State.Defence.
                currentState = State.Defence;
            }

            //If the distance between the AI and the player is less than chaseDistance, change the currentState to State.Attack.
            if (Vector2.Distance(transform.position, aiMovement.player.position) < aiMovement.chaseDistance)
            {
                currentState = State.Attack;
            }

            //Return a null value.
            yield return null;

        }
        Debug.Log("Berry Picking: Exit");

        //Run the NextState method.
        NextState();
    }
    #endregion
}
