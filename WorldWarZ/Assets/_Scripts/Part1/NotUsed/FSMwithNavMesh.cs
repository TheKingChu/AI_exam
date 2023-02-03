using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FSMwithNavMesh : ZombieBaseState
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

    //way for us to make the zombies change to corresponding states ^
    public States currentState;
    public List<Transform> humanTransforms = new List<Transform>();
    int currentHuman = 0;
    public float remainingDist; //from the zombie to the waypoint
    public float distanceToHuman;
    public Transform zombieTransform;
    private NavMeshAgent zombieAgent;

    bool isInAttackRange = false;


    private void Start()
    {
        zombieAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        ChaseRange();
        FindNextHuman();
        UpdateState(); //the AI logic
        if (zombieAgent.pathPending)
        {
            return;
        }
        remainingDist = zombieAgent.remainingDistance;

    }

    protected override void UpdateState()
    {
        switch (currentState)
        {
            case States.None:
                StartStates();
                break;
            case States.Walk:
                UpdateWalkState();
                break;
            case States.Chase:
                UpdateChaseState();
                break;
            case States.Attack:
                if (!isInAttackRange)
                {
                    StartCoroutine(AttackRoutine());
                    isInAttackRange = true;
                    UpdateAttackState();
                }
                break;
            case States.Death:
                UpdateDeathState();
                break;
        }
    }

    public void StartStates()
    {
        Vector3 startDestination = new Vector3(this.transform.position.x + 1, 1, this.transform.position.z);
        zombieAgent.destination = startDestination;
        currentState = States.Walk;
    }

    public void UpdateWalkState()
    {
        //FindNextHuman();
        if (distanceToHuman < 10f)
        {
            currentState = States.Chase;
        }
        //currentState = States.Death;
    }

    private void FindNextHuman()
    {
        if (zombieAgent.remainingDistance < 1f)
        {
            CompareDistance(this.zombieTransform);
            zombieAgent.destination = humanTransforms[currentHuman].position;
        }
            
    }

    private void CompareDistance(Transform zombie) //sorting the list by distance A to B, closest being the first
    {
        this.humanTransforms.Sort(delegate (Transform t1, Transform t2) 
        {
            return
                Vector3.Distance(t1.position,zombie.position).CompareTo
                (Vector3.Distance(t2.position,zombie.position));
        });
    }

    private void ChaseRange()
    {
        distanceToHuman = Vector3.Distance(humanTransforms[currentHuman].position, this.zombieTransform.position);

    }

    public void UpdateChaseState()
    {
        if (isInAttackRange)
        {
            currentState = States.Attack;
            isInAttackRange = false;
        }
        //currentState = States.Death;
    }

    public void UpdateAttackState()
    {
        RaycastHit hit;
        var origin = transform.position;
        isInAttackRange = false;
        Debug.DrawRay(transform.position, origin, Color.red);

        if(Physics.Raycast(origin, Vector3.forward, out hit, 1f))
        {
            if (hit.collider.CompareTag("Human"))
            {
                Destroy(humanTransforms[currentHuman].gameObject);
                humanTransforms.Remove(humanTransforms[currentHuman]);

            }

        }

        //currentState = States.Walk;
            Debug.Log("HUMAN");
        //currentState = States.Death;
    }

    IEnumerator AttackRoutine()
    {
        zombieAgent.isStopped = true;
        yield return new WaitForSeconds(1f);
        zombieAgent.isStopped = false;
        currentState = States.Walk;
    }

    protected void UpdateDeathState()
    {

    }
}
