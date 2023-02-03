/* Made by
 * Charlie Eikås &  Heimir Sindri Þorláksson
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State_Part4
{
    //what states the zombie can have
    public enum States
    {
        Idle,
        Walk,
        Chase,
        Attack,
        Flocking,
        Death
    }

    //where the zombie is in the particluar state
    public enum Event
    {
        Enter,
        Update,
        Exit
    }

    public States state;
    protected Event stage;
    protected GameObject zombie;
    protected Transform human1, human2, human3, human4;
    protected State_Part4 nextState;
    protected NavMeshAgent agent;
    protected GameObject canvas;
    protected int health;

    float visionDistance = 20f;
    float visionAngle = 60f;
    float attackDistance = 2f;
    float flockingDistance = 10f;

    public State_Part4(GameObject _zombie, NavMeshAgent _agent, Transform _human1, Transform _human2, Transform _human3, Transform _human4, GameObject _canvas, int _health)
    {
        zombie = _zombie;
        agent = _agent;
        stage = Event.Enter;
        human1 = _human1;
        human2 = _human2;
        human3 = _human3;
        human4 = _human4;
        canvas = _canvas;
        health = _health;
    }

    //when going into the event stage the stage will know whats next
    public virtual void Enter() { stage = Event.Update; }

    //when its first in update it wants to stay in update until it gets put into next stage
    public virtual void Update() { stage = Event.Update; }

    public virtual void Exit() { stage = Event.Exit; }

    //helps progress the different stages
    public State_Part4 Process()
    {
        if (stage == Event.Enter)
        {
            Enter();
        }
        if (stage == Event.Update)
        {
            Update();
        }
        //exit is different since it wants to clean itself up and get ready for the next state
        if (stage == Event.Exit)
        {
            Exit();
            return nextState;
        }
        return this;
    }

    //chasing / attacking the human
    public bool CanZombieSeeHuman()
    {
        //direction from the human to the zombie
        Vector3 dir = human1.position - zombie.transform.position;
        float angle = Vector3.Angle(dir, zombie.transform.forward);

        if (dir.magnitude < visionDistance && angle < visionAngle)
        {
            return true;
        }

        return false;
    }

    public bool CanZombieAttackHuman()
    {
        Vector3 dir = human1.position - zombie.transform.position;
        if (dir.magnitude < attackDistance)
        {
            return true;
        }

        return false;
    }

    public bool ZombiesBeFlocking()
    {
        var allZombies = GameObject.FindGameObjectsWithTag("Zombie");
        Vector3 dir = human1.position - zombie.transform.position;

        foreach (var zombie in allZombies)
        {
            if (dir.magnitude < flockingDistance)
            {
                agent.SetDestination(human1.position);
                return true;
            }
        }
        return false;
    }
}

