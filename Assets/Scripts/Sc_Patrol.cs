using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sc_Patrol : MonoBehaviour
{
    public Transform[] patrolPoints;
    public int targetPoint;
    public float speed;
    public GameObject goalCheck;

    public bool isPatrolling;
    public bool isWaiting;
    public bool isChasing;


    // Start is called before the first frame update
    void Start()
    {
        targetPoint = 0;
    }


    // Update is called once per frame
    void Update()
    {
        float distanceToTargertPoint = Vector3.Distance(patrolPoints[targetPoint].position, goalCheck.transform.position); //On calcule la distance entre le goal et l'ennemi
        Debug.Log("Distance to target Point : " + distanceToTargertPoint);

        if (distanceToTargertPoint < 0.5f) //Si l'ennemi est suffisament proche, on lance le script
        {
            StartCoroutine("waitingAtGoal");
            increaseTargetInt();
        }

        transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, speed * Time.deltaTime); //L'ennemi se déplace vers le Goal en prenant (sa position, la position de on but, sa vitesse)   

    }


    void increaseTargetInt()
    {
        targetPoint++; //Loop qui passe a la valeur "suivante"

        if (targetPoint >= patrolPoints.Length) //Si on atteint la fin (Length est la "longueur" de l'array) on retourne au début
        {
            targetPoint = 0;
        }
    }

    private IEnumerator waitingAtGoal()
    {
        float waitingTime = 2f;
        WaitForSeconds wait = new WaitForSeconds(waitingTime);

        while (true)
        {
            yield return wait;

            increaseTargetInt();
        }
    }


}


