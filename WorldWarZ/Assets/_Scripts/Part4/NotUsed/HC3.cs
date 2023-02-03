///* Made by
// * Charlie Eikås &  Heimir Sindri Þorláksson
// */

//using System.Collections;
//using System.Collections.Generic;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.AI;
//using TMPro;

//public class HC3 : MonoBehaviour
//{
//    public Weapon_Part4 _weapon;
//    [SerializeField] ZombieController_Part4 _zombie;
//    NavMeshAgent humanAgent;
//    Vector3 safeZone;

//    public List<Transform> zombies = new List<Transform>();

//    //UI
//    public GameObject canvas;
//    public TextMeshProUGUI ammoUI, noAmmoUI, text;

//    //weapon variables
//    public GameObject weapon;
//    float shootingDist = 17f;
//    int maxAmmo;
//    public int currentAmmo;

//    public float dmgDistance;

//    // Start is called before the first frame update
//    void Start()
//    {
//        currentAmmo = 20;
//        maxAmmo = currentAmmo;

//        this.transform.position = new Vector3(Random.Range(48, -48), 1, -47);
//        humanAgent = GetComponent<NavMeshAgent>();
//        safeZone = new Vector3(0, 1, 48);

//        humanAgent.SetDestination(safeZone);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        ammoUI.text = currentAmmo.ToString();
//        if (currentAmmo == 0)
//        {
//            noAmmoUI.enabled = true;
//            text.enabled = false;
//        }
//        else
//        {
//            text.enabled = true;
//            noAmmoUI.enabled = false;
//        }

//        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//        if (Input.GetMouseButtonDown(0) && Physics.Raycast(ray, out RaycastHit hit))
//        {
//            humanAgent.SetDestination(hit.point);
//        }

//        if (humanAgent.remainingDistance < 1)
//        {
//            humanAgent.SetDestination(safeZone);
//        }

//        Shoot();
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.tag == "SafeZone")
//        {
//            canvas.SetActive(true);
//        }

//        if (other.tag == "Ammo")
//        {
//            GameObject.Destroy(other.gameObject);
//            currentAmmo = currentAmmo + 5;

//            if (currentAmmo > maxAmmo)
//            {
//                currentAmmo = maxAmmo;
//            }
//        }
//    }

//    public void Shoot()
//    {
//        CheckDist();
//        GetClosestEnemy(zombies);
//    }

//    //checks the distance from human to zombie so we can call this in the bullet script
//    public void CheckDist()
//    {
//        RaycastHit hit;
//        Ray forwardRay = new Ray(transform.position, Vector3.forward);

//        if (Physics.Raycast(forwardRay, out hit, 50))
//        {
//            if (hit.transform.CompareTag("Zombie"))
//            {
//                dmgDistance = Vector3.Distance(transform.position, hit.point);
//            }
//        }
//    }

//    public Transform GetClosestEnemy(List<Transform> zombies)
//    {
//        Transform bestTarget = null;
//        float closestDistanceSqr = Mathf.Infinity;
//        Vector3 currentPosition = transform.position;

//        for (int i = 0; i < zombies.Count; i++)
//        {
//            Vector3 directionToTarget = zombies[i].position - currentPosition;
//            float dSqrToTarget = directionToTarget.sqrMagnitude;
//            if (dSqrToTarget < closestDistanceSqr)
//            {
//                closestDistanceSqr = dSqrToTarget;
//                bestTarget = zombies[i];
//            }
//            float distance = Vector3.Distance(transform.position, bestTarget.transform.position);

//            if (shootingDist > distance)
//            {
//                Vector3 relativPos = bestTarget.transform.position - transform.position;
//                Quaternion rot = Quaternion.LookRotation(relativPos, Vector3.up);
//                transform.rotation = rot;

//                _weapon.WeaponHandler();
//            }
//        }

//        return bestTarget;
//    }

//    //void RemoveFromList()
//    //{
//    //    for (int i = _zombie.human.Count - 1; i >= 0; i--)
//    //    {
//    //        if (_zombie.human[i].tag == "dead")
//    //        {
//    //            _zombie.human.RemoveAt(i);
//    //            Destroy(this.gameObject);
//    //        }
//    //    }
//    //}
//}



