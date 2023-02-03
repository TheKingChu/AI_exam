using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class newFSM : MonoBehaviour
{
    //the different states the zombies has access to
    public enum States
    {
        None,
        Walk,
        Chase,
        Attack,
        Death
    }

    //state variables
    BaseState currentState;

    //get and set
    public BaseState CurrentState { get { return currentState; }  set { currentState = value; } }

    public List<Transform> humanTransforms = new List<Transform>();
    int currentHuman = 0;

    //zombie variables
    private NavMeshAgent zombieAgent;
    public Transform zombieTransform;

    private void Start()
    {
        zombieAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        zombieAgent.destination= humanTransforms[currentHuman].position;
    }
}
