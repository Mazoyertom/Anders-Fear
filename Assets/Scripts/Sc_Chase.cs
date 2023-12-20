using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Chase : MonoBehaviour
{
        [Header("Chase")]
    public float chaseSpeed = 5f;
    public float distanceToPlayerLastPosition;
    public Vector3 playerLastPosition;
    public GameObject chaseCheck;
    public GameObject deathCheck;
    public GameObject playerController;

    public bool isChasing;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isChasing == true)
        {
            transform.LookAt(playerController.transform.position);
        }

            if(chaseCheck.GetComponent<Sc_FOV2>().canSeePlayer == true)
            {
            Debug.Log("AHHHHHHHHHHH Je te poursuis");
            isChasing = true;
            }

        if(isChasing == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerController.transform.position, chaseSpeed * Time.deltaTime);

            if(chaseCheck.GetComponent<Sc_FOV2>().canSeePlayer == false)
            {
                playerLastPosition = playerController.GetComponent<Rigidbody>().transform.position;
                isChasing = false;
            }
        }
    }
}