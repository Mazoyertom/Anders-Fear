using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_LineOfSight : MonoBehaviour
{

    public LayerMask targetMask;

    public GameObject playerTarget;
    public GameObject monster;

    public bool canSeePlayer;
    public bool enterTriggerZone;


    // Start is called before the first frame update
    void Start()
    {
        canSeePlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(enterTriggerZone == true)
        {
            RaycastHit hit;
            if (Physics.Raycast(monster.transform.position, ( playerTarget.transform.position - monster.transform.position), out hit, 15f) == true)
            {
                Debug.Log(hit.transform.gameObject.name + " hit");
                Debug.DrawLine (monster.transform.position, hit.point,Color.red);
                if(hit.transform.gameObject.tag == "Player")
                {
                    canSeePlayer = true;
                }
                else
                {
                    canSeePlayer = false;
                }
            }
            else
            {
                canSeePlayer = false;
            }
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Vector3 directionToTarget = (playerTarget.position - transform.position).normalized; //On soustrait la position du joueur et la position de l'objet pour connaitre la plus courte distance
            //float distanceToTarget = Vector3.Distance(transform.position, playerTarget.position);

            enterTriggerZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            enterTriggerZone = false;
            canSeePlayer = false;
        }
    }



}
