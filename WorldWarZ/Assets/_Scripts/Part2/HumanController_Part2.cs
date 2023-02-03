using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanController_Part2 : MonoBehaviour
{
    public GameObject canvas;
    NavMeshAgent humanAgent;
    Vector3 safeZone;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(Random.Range(48, -48), 1, -47);
        humanAgent = GetComponent<NavMeshAgent>();
        safeZone = new Vector3(0, 1, 48);

        humanAgent.SetDestination(safeZone);
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hit))
        {
            humanAgent.SetDestination(hit.point);
        }

        if(humanAgent.remainingDistance < 1)
        {
            humanAgent.SetDestination(safeZone);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SafeZone")
        {
            canvas.SetActive(true);
        }
    }
}
