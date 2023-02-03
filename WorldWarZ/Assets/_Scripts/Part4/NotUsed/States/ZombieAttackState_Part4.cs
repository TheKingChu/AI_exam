/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;

public class ZombieAttackState_Part4 : State_Part4
{
    float rotationSpeed = 2f;

    public ZombieAttackState_Part4(GameObject _zombie, NavMeshAgent _agent, Transform _human1, Transform _human2, Transform _human3, Transform _human4, GameObject _canvas, int _health) : 
        base(_zombie, _agent, _human1, _human2, _human3, _human4, _canvas, _health)
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
        Vector3 direction = human1.position - zombie.transform.position;
        float angle = Vector3.Angle(direction, zombie.transform.forward);

        direction.y = 0;
        zombie.transform.rotation = Quaternion.Slerp(zombie.transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);

        if (killsHuman)
        {
            human1.transform.position = new Vector3(1000, 1, 0);
            canvas.SetActive(true);
            human1.transform.gameObject.CompareTag("dead");
        }

        if (!CanZombieAttackHuman())
        {
            nextState = new ZombieIdleState_Part4(zombie, agent, human1, human2,human3,human4, canvas, health);
            stage = Event.Exit;
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
