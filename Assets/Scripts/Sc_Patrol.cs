using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Sc_Patrol : MonoBehaviour
{

    [Header("Patrol")]
    public Transform[] patrolPoints;
    public int targetPoint;
    public float patrolSpeed = 3f;
    public float distanceToTargertPoint;
    public GameObject goalCheck;

    [Header("Waiting")]
    private float waitCounter = 0f;
    private float waitingTime = 10f;

    [Header("Chase")]
    float chaseSpeed = 4.5f;
    public float distanceToPlayerLastPosition;
    public Vector3 playerLastPosition;
    public GameObject chaseCheck;
    public GameObject deathCheck;
    public GameObject playerController;
    [SerializeField] private Sc_LineOfSight SeeingPlayer;

    [Header("Blind")]
    private float blindSpeed = 0f;
    public float blindTime;
    public GameObject blindCheck;
    [SerializeField] private Sc_Blinded EnnemyBlind;


    [Header("State")]
    public bool isPatrolling;
    public bool isWaiting;
    public bool isChasing;
    public bool isSearching;
    public bool isBlinded;




    // Start is called before the first frame update
    void Start()
    {
        targetPoint = 0;
        isPatrolling = true;

        EnnemyBlind = blindCheck.GetComponent<Sc_Blinded>();
        SeeingPlayer = chaseCheck.GetComponent<Sc_LineOfSight>();

    }


    // Update is called once per frame
    void Update()
    {

       if (isPatrolling == true)
        {
            transform.LookAt(patrolPoints[targetPoint].position);
            transform.position = Vector3.MoveTowards(transform.position, patrolPoints[targetPoint].position, patrolSpeed * Time.deltaTime); //L'ennemi se d�place vers le Goal en prenant (sa position, la position de on but, sa vitesse) 

            distanceToTargertPoint = Vector3.Distance(patrolPoints[targetPoint].position, goalCheck.transform.position); //On calcule la distance entre le goal et l'ennemi

            if (distanceToTargertPoint < 0.09f) //Si l'ennemi est suffisament proche, on lance le script
            {
                increaseTargetInt();
            }
        }
        
        if(chaseCheck.GetComponent<Sc_LineOfSight>().canSeePlayer == true && !isBlinded)
        {
            //Debug.Log("Je te poursuis");
            isPatrolling = false;
            isChasing = true;
        }
             
        if(isChasing == true)
        {
            transform.LookAt(playerController.transform.position);
            transform.position = Vector3.MoveTowards(transform.position, playerController.transform.position, chaseSpeed * Time.deltaTime);

            if(chaseCheck.GetComponent<Sc_LineOfSight>().canSeePlayer == false)
            {
                playerLastPosition = playerController.transform.position;
                isChasing = false;
                isSearching = true;
            }
        } 

        if(isSearching == true)
        {
            //Debug.Log("Mais ou est tu ?");
            transform.LookAt(playerLastPosition);
            transform.position = Vector3.MoveTowards(transform.position, playerLastPosition, patrolSpeed * Time.deltaTime);

            distanceToPlayerLastPosition = Vector3.Distance(this.transform.position, playerLastPosition);
            //Debug.Log("Distance to Player last position : " + distanceToPlayerLastPosition);

            if (distanceToPlayerLastPosition < 0.09f)
            {
                isChasing = false;
                isSearching = false;
                isPatrolling = true;
            }

            if(chaseCheck.GetComponent<Sc_LineOfSight>().canSeePlayer == true)
            {
                isSearching = false;
                isChasing = true;
            }
        }





        if(EnnemyBlind.isBlindTrigger == true)
        {
            isChasing = false;
            isSearching = false;
            isPatrolling = false;
            isBlinded = true;
        }
        else 
        {
            isBlinded = false;
        }

        if (isBlinded == true)
        {
            isChasing = false;
            isSearching = false;
            isPatrolling = false;
            //SeeingPlayer.canSeePlayer = false;

            transform.position = Vector3.MoveTowards(transform.position, playerController.transform.position, blindSpeed * Time.deltaTime);
            blindTime += Time.deltaTime;

            if (blindTime >= 5f)
            {
                EnnemyBlind.isBlindTrigger = false;
                isBlinded = false;
                blindTime = 0f;
                isPatrolling = true;
            }


            
        }
        else if (isBlinded == false)
        {
            blindTime = 0f;
            //isPatrolling = true;
        }

        if (!isChasing && !isSearching && !isPatrolling && !isBlinded)
        {
            isPatrolling = true;
        }






        if (isWaiting == true)
        {
            isPatrolling = false;
            waitCounter += Time.deltaTime;

            Debug.Log("Time : " + waitCounter);

            if(waitCounter < waitingTime)
            {
                isWaiting = false;

                increaseTargetInt();
                
            }
            else
            {
                
            }
            

        }
    }


    void increaseTargetInt()
    {
        isPatrolling = true;
        waitCounter = 0f;

        targetPoint++; //Loop qui passe a la valeur "suivante"

        if (targetPoint >= patrolPoints.Length) //Si on atteint la fin (Length est la "longueur" de l'array) on retourne au d�but
        {
            targetPoint = 0;
        }
    }



    







}


