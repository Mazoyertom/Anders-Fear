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

    public bool isPatrolling;
    public bool isWaiting;
    public bool isChasing;

    public GameObject chaseCheck;
    public GameObject playerController;


    // Start is called before the first frame update
    void Start()
    {
        targetPoint = 0;
    }


    // Update is called once per frame
    void Update()
    {
        distanceToTargertPoint = Vector3.Distance(patrolPoints[targetPoint].position, goalCheck.transform.position); //On calcule la distance entre le goal et l'ennemi
        Debug.Log("Distance to target Point : " + distanceToTargertPoint);

        if (distanceToTargertPoint < 0.1f) //Si l'ennemi est suffisament proche, on lance le script
        {
            StartCoroutine("waitingAtGoal");
            increaseTargetInt();
        }

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, patrolSpeed * Time.deltaTime); //L'ennemi se d�place vers le Goal en prenant (sa position, la position de on but, sa vitesse)   
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
        float waitingTime = 5f;
        WaitForSeconds wait = new WaitForSeconds(waitingTime);

        while (true)
        {
            yield return wait;

            increaseTargetInt();
        }
    }


    void chasePlayer()
    {
        if(chaseCheck.GetComponent<Sc_FieldOfView>().canSeePlayer == true)
        {
            isChasing = true;
        }

        if(isChasing == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerController.transform.position, chaseSpeed * Time.deltaTime);
        }



    }


}


