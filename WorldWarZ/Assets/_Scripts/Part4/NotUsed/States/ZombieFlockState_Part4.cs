/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieFlockState_Part4 : State_Part4
{
    public ZombieFlockState_Part4(GameObject _zombie, NavMeshAgent _agent, Transform _human1, Transform _human2, Transform _human3, Transform _human4, GameObject _canvas, int _health) :
        base(_zombie, _agent, _human1, _human2, _human3, _human4, _canvas, _health)
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
        agent.SetDestination(human1.position);

        if (agent.hasPath) //if its following the player
        {
            if (CanZombieAttackHuman())
            {
                nextState = new ZombieAttackState_Part4(zombie, agent, human1,human2,human3,human4, canvas, health);
                stage = Event.Exit;
            }
            else if (!CanZombieSeeHuman())
            {
                nextState = new ZombieWalkState_Part4(zombie, agent, human1, human2,human3,human4, canvas, health);
                stage = Event.Exit;
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}

