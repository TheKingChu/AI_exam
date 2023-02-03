/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieFlockState_Part3 : State
{
    public ZombieFlockState_Part3(GameObject _zombie, NavMeshAgent _agent, Transform _human, GameObject _canvas, int _health) : base(_zombie, _agent, _human, _canvas, _health)
    {
        state = States.Flocking;
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
            if (CanZombieAttackHuman())
            {
                nextState = new ZombieAttackState_Part3(zombie, agent, human, canvas, health);
                stage = Event.Exit;
            }
            else if (!CanZombieSeeHuman())
            {
                nextState = new ZombieWalkState_Part3(zombie, agent, human, canvas, health);
                stage = Event.Exit;
            }

            if (health <= 0)
            {
                nextState = new ZombieDeathState_Part3(zombie, agent, human, canvas, health);
                stage = Event.Exit;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
