/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieIdleState_Part3 : State
{
    public ZombieIdleState_Part3(GameObject _zombie, NavMeshAgent _agent, Transform _human, GameObject _canvas, int _health) : base(_zombie, _agent, _human, _canvas, _health)
    {
        state = States.Idle;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        if (CanZombieSeeHuman())
        {
            nextState = new ZombieChaseState_Part3(zombie, agent, human, canvas, health);
            stage = Event.Exit;
        }
        else if (Random.Range(0f, 100f) < 10)
        {
            nextState = new ZombieWalkState_Part3(zombie, agent, human, canvas, health);
            stage = Event.Exit;
        }
    }

    public override void Exit()
    {

        base.Exit();
    }
}
