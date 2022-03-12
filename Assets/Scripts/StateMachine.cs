using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIMovement))]
public class StateMachine : MonoBehaviour
{
    //A comma separated list of identifiers
    public enum State
    {
        Attack,
        Defence,
        Run,
        BerryPicking
    }

    public State currentState;
    public AIMovement aiMovement; 
   
    private void Start()
    {
        aiMovement = GetComponent<AIMovement>();
        NextState();

        //Addition(Random.Range(0, 100), Random.Range(0, 100));
    }

    private void NextState()
    {
        //Runs one of the cases that matches the value (in this example the value is currentState)
        switch (currentState)
        {
            case State.Attack:
                StartCoroutine(AttackState());
                break;
            case State.Defence:
                StartCoroutine(DefenceState());
                break;
            case State.Run:
                StartCoroutine(RunState());
                break;
            case State.BerryPicking:
                StartCoroutine(BerryPickingState());
                break;
        }
    }

    /*
    private int Addition(int a, int b)
    {
        int answer = a + b;
        Debug.Log(answer);
        return answer;
       
    }
    */

    private void Update()
    {
        if (aiMovement.chased)
        {
            aiMovement.sRender.color = aiMovement.aiColour;
        }
        else
        {
            aiMovement.sRender.color =  aiMovement.aiColour2;
        }
    }

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
        while(currentState == State.Attack)
        {
            aiMovement.AIMove(aiMovement.player);
            aiMovement.chased = true;
            
            if (Vector2.Distance(transform.position, aiMovement.player.position) >= aiMovement.chaseDistance)
            {
                currentState = State.BerryPicking; 
            }

            if (Switch.active)
            {
                currentState = State.Run;
            }

            yield return null;

        }
        Debug.Log("Attack: Exit");

        NextState();
    }
    private IEnumerator DefenceState()
    {
        Debug.Log("Defence: Enter");
        AIMovement.defending = true; 
        while (currentState == State.Defence)
        {
            //The AI should move away from the player when the player is close. 
            aiMovement.AIDefence(aiMovement.player.transform);

            if (aiMovement.waypoints.Count == 5)
            {
                currentState = State.BerryPicking;
            }

            if (Switch.active)
            {
                currentState = State.Run;
            }

            yield return null;

        }
        AIMovement.defending = false; 
        Debug.Log("Defence: Exit");

        NextState();
    }
    private IEnumerator RunState()
    {
        Debug.Log("Run: Enter");
        while (currentState == State.Run)
        {
            if (!Switch.active)
            {
                if (aiMovement.waypoints.Count >= 1)
                {
                    currentState = State.BerryPicking;
                }
                else
                {
                    currentState = State.Defence;
                }
                
            }

            aiMovement.AIRun(aiMovement.player.transform);

            yield return null;

        }
        Debug.Log("Run: Exit");

        NextState();
    }
   
    private IEnumerator BerryPickingState()
    {
        Debug.Log("Berry Picking: Enter");
        //Once every frame, similar to update function
        while (currentState == State.BerryPicking)
        {
            aiMovement.WaypointUpdate();

            if (Switch.active)
            {
                currentState = State.Run;
            }

            if (aiMovement.waypoints.Count != 0)
            {
                aiMovement.AIMove(aiMovement.waypoints[aiMovement.waypointIndex0]);
            }
            else
            {
                currentState = State.Defence;
            }

            if (Vector2.Distance(transform.position, aiMovement.player.position) < aiMovement.chaseDistance)
            {
                currentState = State.Attack;
            }

            yield return null;

        }
        Debug.Log("Berry Picking: Exit");

        NextState();
    }
}
    