/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieDeathState_Part3 : State
{
    public ZombieDeathState_Part3(GameObject _zombie, NavMeshAgent _agent, Transform _human, GameObject _canvas, int _health) : base(_zombie, _agent, _human, _canvas, _health)
    {
        state = States.Death;
    }

    public override void Enter()
    {
        agent.isStopped = true;
        base.Enter();
    }

    public override void Update()
    {
        //TODO random chance to drop ammo 
        GameObject.Destroy(this.zombie);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
