using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_LineOfSight : MonoBehaviour
{

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public GameObject playerTarget;

    public bool canSeePlayer;
    public bool isThereObstruction;


    // Start is called before the first frame update
    void Start()
    {
        canSeePlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Vector3 directionToTarget = (playerTarget.position - transform.position).normalized; //On soustrait la position du joueur et la position de l'objet pour connaitre la plus courte distance
            //float distanceToTarget = Vector3.Distance(transform.position, playerTarget.position);

            if (Physics.Raycast(transform.position, playerTarget.transform.position, 15f, obstructionMask) == false)
            {
                canSeePlayer = true;
                isThereObstruction = false;
            }
            else
            {
                canSeePlayer = false;
                isThereObstruction = true;
            }
            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            canSeePlayer = false;
            isThereObstruction = false;
        }
    }



}
