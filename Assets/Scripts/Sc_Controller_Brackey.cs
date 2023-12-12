using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class Sc_Controller_Brackey : MonoBehaviour
{

    public CharacterController controller;
    Rigidbody rb;
    public MouvementState state;


    [Header("Machine State")]
    public bool isGrounded;
    public bool isJumping;
    public bool isCrouching;
    public bool isTryingToUncrouching;


    [Header("Mouvement")]
    public float moveSpeed = 15f;
    public float walkingSpeed;
    float horizontalInput;
    float verticalInput;


    [Header("Ground check")]
    public Transform groundCheck;
    public float sphereSize = 0.05f;
    public LayerMask groundMask;
    

    [Header("Jump")]
    public float gravityForce = -9.81f;
    Vector3 velocity;
    public float JumpHeight = 3f;


    [Header("Light")]
    public bool playerLightOn;
    public float playerLightEnergy;


    [Header("Crouch")]
    public float crouchSpeed = 5f;
    public float currentYScale;
    public float crouchYScale;
    public float startYScale;
    private KeyCode crouchKey = KeyCode.E;
    public float crouchTransitionSpeed = 10000f;
    public LayerMask hidingZoneLayer;
    public float crouchDelta;
    public bool aboveCheck;



    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        startYScale = currentYScale = transform.localScale.y; 
        walkingSpeed = moveSpeed;

        isCrouching = false;
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
        StateHandler();


        // Get mouvement input
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Gravité
        velocity.y = velocity.y + gravityForce * Time.deltaTime; //Simulation de la gravité 
        controller.Move(velocity * Time.deltaTime); //Son application sur le controller



        crouchDelta = Time.deltaTime * crouchTransitionSpeed;
        currentYScale = Mathf.Lerp(currentYScale, crouchYScale, crouchDelta);

        //start crouch
        if (Input.GetKeyDown(crouchKey))
        {
            isCrouching = true;
        }

        if (isCrouching == true)
        {
            transform.localScale = new Vector3(transform.localScale.x, currentYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        //stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            isTryingToUncrouching = true;
        }

        if(isTryingToUncrouching == true)
        {
            if(!Physics.Raycast(transform.position, Vector3.up, out RaycastHit hit, 3f) == false)
            {
                isTryingToUncrouching = false;
                isCrouching = false;
            }
        }

        if (isCrouching == false)
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            moveSpeed = walkingSpeed;
            isTryingToUncrouching = false;
        }


        hidingZoneLayer = LayerMask.NameToLayer("Wall");
        hidingZoneLayer = ~hidingZoneLayer;
        //aboveCheck = Physics.Raycast(transform.position, transform.up, out RaycastHit hit, 3f);




        isGrounded = Physics.CheckSphere(groundCheck.position, sphereSize, groundMask);

        if (isGrounded && velocity.y < 0) //si on touche le sol, on reset la vélocité
        {
            velocity.y = -2f;
        }

        if (isGrounded && !isCrouching && !isTryingToUncrouching && Input.GetKeyDown(KeyCode.Space)) //on vérifie si le player est grounded quand il appuie sur l'input de saut
        {
            velocity.y = velocity.y + Mathf.Sqrt(JumpHeight * -2f * gravityForce); //on multiplie la hauteur du saut par (-2) et par la gravité
        }



        if(isJumping == true)
        {

            isGrounded = false;
        }
    


    }


}


    




    







        




    
