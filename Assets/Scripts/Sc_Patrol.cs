using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sc_Patrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float patrolSpeed = 3f;
    public float chaseSpeed = 5f;
    public GameObject goalCheck;
    public float distanceToTargertPoint;
    float waitingTime = 6000f;
    public Vector3 playerLastPosition;

    public bool isPatrolling;
    public bool isWaiting;
    public bool isChasing;
    public bool isSearching;

    public GameObject chaseCheck;
    public GameObject deathCheck;
    public GameObject playerController;


    // Start is called before the first frame update
    void Start()
    {
        targetPoint = 0;
        isPatrolling = true;
    }


    // Update is called once per frame
    void Update()
    {
        if(isPatrolling == true);
        {
            distanceToTargertPoint = Vector3.Distance(patrolPoints[targetPoint].position, goalCheck.transform.position); //On calcule la distance entre le goal et l'ennemi
            //Debug.Log("Distance to target Point : " + distanceToTargertPoint);

            if (distanceToTargertPoint < 0.09f) //Si l'ennemi est suffisament proche, on lance le script
            {
                StartCoroutine("waitingAtGoal");
                increaseTargetInt();
            }

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, patrolSpeed * Time.deltaTime); //L'ennemi se d�place vers le Goal en prenant (sa position, la position de on but, sa vitesse)  
        }
        


        if(chaseCheck.GetComponent<Sc_FieldOfView>().canSeePlayer == true)
        {
            Debug.Log("AHHHHHHHHHHH Je te poursuis");
            isPatrolling = false;
            isChasing = true;
        }

        if(isChasing == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerController.transform.position, chaseSpeed * Time.deltaTime);

            if(chaseCheck.GetComponent<Sc_FieldOfView>().canSeePlayer == false)
            {
                isChasing = false;
                isSearching = true;
            }
        } 

        if(isSearching == true)
        {
            Debug.Log("Je t'ai perdu de vue");
            Destroy(this.gameObject);
            playerLastPosition = playerController.GetComponent<GameObject>().transform.position;
        }
    }


    void increaseTargetInt()
    {
        targetPoint++; //Loop qui passe a la valeur "suivante"

        if (targetPoint >= patrolPoints.Length) //Si on atteint la fin (Length est la "longueur" de l'array) on retourne au d�but
        {
            targetPoint = 0;
        }
    }

    private IEnumerator waitingAtGoal()
    {
        WaitForSeconds wait = new WaitForSeconds(waitingTime);

        while (true)
        {
            yield return wait;

            increaseTargetInt();
        }
    }


    void chasePlayer()
    {
        
    }


}


