/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieChaseState_Part3 : State
{
    public ZombieChaseState_Part3(GameObject _zombie, NavMeshAgent _agent, Transform _human, GameObject _canvas, int _health) : base(_zombie, _agent, _human, _canvas, _health)
    {
        state = States.Chase;
        agent.speed = 10f;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        agent.SetDestination(human.position);

        if (agent.hasPath) //if its following the player
        {
            if (ZombiesBeFlocking())
            {
                nextState = new ZombieFlockState_Part3(zombie, agent, human, canvas, health);
                stage = Event.Exit;
            }
            else if (!CanZombieSeeHuman())
            {
                nextState = new ZombieWalkState_Part3(zombie, agent, human, canvas, health);
                stage = Event.Exit;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
