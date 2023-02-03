/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Zombie FSM for part 1
/// </summary>
public class FSM : ZombieBaseState
{
    //the different states the zombies has access to
    public enum States
    {
        Walk,
        Chase,
        Attack,
        Death
    }

    //way for us to make the zombies change to corresponding states ^
    public States currentState;

    //zombie variables
    public float speed = 30;
    public float distance = 10;
    private Vector3 waypoint; //where the zombie is going
    public Transform zombieTransform;

    public List<Transform> humanTransforms = new List<Transform>();
    public List<float> distances = new List<float>(); //list of different distances between zombie and human
    public Transform[] humans; //transform of humans in scene
    //int currentWaypoint = 0;

    public GameManager_P1 manager;

    //When the game starts the zombies enter the walk state immediately
    private void Awake()
    {
        humanTransforms.AddRange(humans);
        manager.HumanDistance();
        currentState = States.Walk;
    }

    private void Update()
    {
        UpdateState();
    }

    protected override void UpdateState()
    {
        switch(currentState)
        {
            case States.Walk:
                UpdateWalkState();
                break;
            case States.Chase:
                UpdateChaseState();
                break;
            case States.Attack:
                UpdatedAttackState();
                break; 
            case States.Death:
                UpdateDeathState();
                break;
        }
    }


    protected void UpdateWalkState()
    {
        //TODO get waypoints, detect player
        if (waypoint == null)
        {
            FindNextWaypoint();
        }

        zombieTransform.position += transform.forward * speed * Time.deltaTime;

        if (Vector3.Distance(zombieTransform.position, waypoint) <= 10)
        {
            FindNextWaypoint();
            manager.HumanDistance();
        }
    }

    protected void UpdateChaseState()
    {
        /*TODO change the waypoint to human transform
         * if in range change to attack state*/
        zombieTransform.position += transform.forward * speed * Time.deltaTime;
        Debug.Log("i am chasing");
    }

    protected void UpdatedAttackState()
    {
        /*TODO dmg human unless human out of range
         * if out of range change to chase state
         * if human dead go to walk state*/
    }

    protected void UpdateDeathState() 
    {
        //? DEATH
    }

    public void FindNextWaypoint()
    {
        int xPos = Random.Range(-48, 48);
        int zPos = Random.Range(-48, 41);
        Vector3 newWaypoint = new Vector3(xPos, 1, zPos);

        //while (currentWaypoint < humanTransforms.Count)
        //{
        //    float distanceToWayPoint = Vector3.Distance(transform.position, humanTransforms[currentWaypoint].transform.position);
        //    transform.position = Vector3.MoveTowards(transform.position, humanTransforms[currentWaypoint].transform.position, Time.deltaTime * speed);

        //    if (distanceToWayPoint <= 10f)
        //    {
        //        currentWaypoint++;
        //        if (currentWaypoint >= humanTransforms.Count)
        //        {
        //            currentWaypoint = 0;
        //        }
        //    }
        //}

        Debug.Log(waypoint);
    }

    protected override void ExitState()
    {
        
    }
}
