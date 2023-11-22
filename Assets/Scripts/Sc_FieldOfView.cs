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
        Collider[] rangeCheck = Physique.OverlapSphere(transform.position, fovRadius, targetMask); //On sait qu'il n'y a qu'un seul "player" donc la valeur va rester a 0

        if (rangeCheck.Length != 0) 
        {
            Transform target = rangeChecks[0].transform; //On vérifie si on a dépassé la Length //(on a qu'une valeur) donc on ramene a 0 
            Vector3 directionToTarget = (target.position - transform.position).normalized; //On sosutrait la position du joueur et la position de l'objet pour connaitre la plus courte distance

            if (Vector3.Angle(transform.forward, directionToTarget) < fovAngle / 2) //On vérifie si l'angle du FOV devant l'ennemi avec la distance la plus courte ??????????
            {
                float distanceToTarget = Vector3.Distance(transform.position, targetMask.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) //Le "!" veut dire si on ne voit pas d'obstructionMask (mur) on voit le joueur
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
        else if (canSeePlayer == true) //Si le joueur est rentrée dans la vue de l'ennemi et tu en est sorti
        {
            canSeePlayer = false;
        }

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
