using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Controller_Brackey : MonoBehaviour
{

    public CharacterController controller;
    Rigidbody rb;
    public MouvementState state;


    [Header("Mouvement")]
    public float moveSpeed = 15f;
    public float walkingSpeed;
    float horizontalInput;
    float verticalInput;

    [Header("Ground check")]
    public Transform groundCheck;
    public float sphereSize = 0.05f;
    public LayerMask groundMask;
    public bool isGrounded;
    

    [Header("Jump")]
    public float gravityForce = -9.81f;
    Vector3 velocity;
    public float JumpHeight = 3f;
    public bool isJumping;


    [Header("Light")]
    public bool playerLightOn;
    public float playerLightEnergy;


    [Header("Crouch")]
    public float crouchSpeed = 5f;
    public float crouchYScale;
    public float startYScale;
    private KeyCode crouchKey = KeyCode.E;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        startYScale = transform.localScale.y; 
        walkingSpeed = moveSpeed;
    }



    public enum MouvementState
    {
        grounded,
        jumping,
        walking,
        crouching,
    }

    private void StateHandler()
    {
        //Mode - Mouvement
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            state = MouvementState.walking;
            Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput; //Pour les valeurs du Vector3 de direction on prends les inputs et on les associe a leurs directions
            controller.Move(moveDirection * moveSpeed * Time.deltaTime); //On multiple ces coordonnées par la vitesse de mouvement
        }

        //Mode - Crouching
        if(Input.GetKeyDown(crouchKey))
        {
            state = MouvementState.crouching;
            moveSpeed = crouchSpeed;
        }
    }


    // Update is called once per frame
    void Update()
    {
        // Get mouvement input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        StateHandler();

        // Gravité
        velocity.y = velocity.y + gravityForce * Time.deltaTime; //Simulation de la gravité 
        controller.Move(velocity * Time.deltaTime); //Son application sur le controller


        //start crouch
        if(Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        //stop crouch
        if(Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            moveSpeed = walkingSpeed;
        }


        isGrounded = Physics.CheckSphere(groundCheck.position, sphereSize, groundMask);

        if (isGrounded && velocity.y < 0) //si on touche le sol, on reset la vélocité
        {  
            velocity.y = -2f;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) //on vérifie si le player est grounded quand il appuie sur l'input de saut
        {
           velocity.y = velocity.y + Mathf.Sqrt(JumpHeight * -2f * gravityForce); //on multiplie la hauteur du saut par (-2) et par la gravité
        }
    

        while(playerLightOn == true)
        {
            playerLightEnergy -= Time.deltaTime;
        }


        if (playerLightEnergy < 0)
        {
            

        }


    }


}


    




    







        




    
