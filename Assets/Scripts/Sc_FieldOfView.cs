using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_FieldOfView : MonoBehaviour
{
    public float fovRadius;
    public float fovAngle;

    public GameObject playerRef;

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool canSeePlayer;



    // Start is called before the first frame update
    void Start()
    {
        playerRef = GameObject.FindGameObjectWithTag("Player"); 
        StartCoroutine(FOVRoutine());
    }



    private IEnumerator FOVRoutine()
    {
        float fovDelay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(fovDelay);

        while (true)
        {
            yield return wait;
            FOVCheck();
        }
    }


    private void FOVCheck()
    {
        Collider[] rangeCheck = Physics.OverlapSphere(transform.position, fovRadius, targetMask); //On sait qu'il n'y a qu'un seul "player" donc la valeur va rester a 0

        if (rangeCheck.Length != 0) // Si le tableau n'est pas vide : Si le joueur n'est pas dans la range de l'ennemi
        {
            Transform playerTarget = rangeCheck[0].transform; //On regarde le 1e objet de la liste et on regarde son transform
            Vector3 directionToTarget = (playerTarget.position - transform.position).normalized; //On soustrait la position du joueur et la position de l'objet pour connaitre la plus courte distance

            if (Vector3.Angle(transform.forward, directionToTarget) < fovAngle / 2) //Ca compare l'angle de la position de l'ennemi et de la target et v�rifie si la target dans le fov 
            {
                float distanceToTarget = Vector3.Distance(transform.position, playerTarget.position); // Envoie un float qui vérifie la distance centre l'ennemi et la targe

                if (Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask) == false) //Le "!" veut dire si on ne voit pas d'obstructionMask (mur) on voit le joueur
                {
                    canSeePlayer = true;
                    Debug.Log("On voit le joueur");
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
        else if (canSeePlayer == true) //Si le joueur est rentr�e dans la vue de l'ennemi et qu'il en est sorti
        {
            canSeePlayer = false;
        }

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
