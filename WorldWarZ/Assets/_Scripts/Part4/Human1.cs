using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human1 : MonoBehaviour
{
    public bool isH1;
    public bool killable = true;
     bool isDed;
    public HumanController_Part3 hc;
    public Material ded;
    MeshRenderer mesh;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
    }
    void Update()
    {
        if (isH1 == true)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hit))
            {
                hc.humanAgent.SetDestination(hit.point);
            }

            if (hc.humanAgent.remainingDistance < 1)
            {
                hc.humanAgent.SetDestination(hc.safeZone);
            }

        }
    }

    public void UseMe()
    {
        isDed = true;
    }
}
