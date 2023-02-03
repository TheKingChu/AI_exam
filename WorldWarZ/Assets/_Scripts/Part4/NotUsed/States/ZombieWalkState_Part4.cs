/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieWalkState_Part4 : State_Part4
{
    int currentIndex = -1;

    public ZombieWalkState_Part4(GameObject _zombie, NavMeshAgent _agent, Transform _human1, Transform _human2, Transform _human3, Transform _human4, GameObject _canvas, int _health) :
        base(_zombie, _agent, _human1, _human2, _human3, _human4, _canvas, _health)
    {
        state = States.Walk;
        agent.speed = 5;
        agent.isStopped = false;
    }

    //here we get our waypoints 
    public override void Enter()
    {
        float lastDistance = Mathf.Infinity;

        for (int i = 0; i < Path.Singleton.Waypoints.Count; i++)
        {
            GameObject thisWaypoint = Path.Singleton.Waypoints[i];
            float distance = Vector3.Distance(zombie.transform.position, thisWaypoint.transform.position);

            if (distance < lastDistance)
            {
                currentIndex = i - 1;
                lastDistance = distance;
            }
        }

        base.Enter();
    }

    //moving around through the waypoints set in the Path.Singleton method
    public override void Update()
    {
        if (agent.remainingDistance < 1)
        {
            if (currentIndex >= Path.Singleton.Waypoints.Count - 1)
            {
                currentIndex = 0;
            }
            else
            {
                currentIndex++;
            }

            agent.SetDestination(Path.Singleton.Waypoints[currentIndex].transform.position);
        }

        if (CanZombieSeeHuman())
        {
            nextState = new ZombieChaseState_Part4(zombie, agent, human1,human2,human3,human4, canvas, health);
            stage = Event.Exit;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

