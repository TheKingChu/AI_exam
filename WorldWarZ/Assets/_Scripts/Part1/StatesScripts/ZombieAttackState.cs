/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class ZombieAttackState : State
{
    float rotationSpeed = 2f;

    public ZombieAttackState(GameObject _zombie, NavMeshAgent _agent, Transform _human, GameObject _canvas, int _health) : base(_zombie, _agent, _human, _canvas, _health)
    {
        state = States.Attack;
    }

    public override void Enter()
    {
        agent.isStopped = true;
        base.Enter();
    }

    public override void Update()
    {
        var killsHuman = GameObject.FindGameObjectWithTag("Human");
        Vector3 direction = human.position - zombie.transform.position;
        float angle = Vector3.Angle(direction, zombie.transform.forward);

        direction.y = 0;
        zombie.transform.rotation = Quaternion.Slerp(zombie.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

        if (killsHuman)
        {
            human.transform.position = new Vector3(1000, 1, 0);
            canvas.SetActive(true);
        }

        if (!CanZombieAttackHuman())
        {
            nextState = new ZombieIdleState(zombie, agent, human, canvas, health);
            stage = Event.Exit;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
