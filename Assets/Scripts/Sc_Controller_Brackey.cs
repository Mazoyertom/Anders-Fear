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
    public bool isTryingToStand;


    [Header("Mouvement")]
    public float currentSpeed;
    float walkingSpeed = 5f;
    float crouchSpeed = 1f;
    public float focusSpeed = 1f;
    float horizontalInput;
    float verticalInput;
    public GameObject playerLightRef;
    

    [Header("Ground check")]
    public Transform groundCheck;
    public float sphereSize = 0.05f;
    public LayerMask groundMask;
    

    [Header("Jump")]
    public float gravityForce = -9.81f;
    Vector3 velocity;
    public float JumpHeight = 3f;
    

    [Header("Crouch")]
    public float currentYScale;
    public float crouchYScale;
    public float startYScale;
    private KeyCode crouchKey = KeyCode.LeftShift;
    public float crouchTransitionSpeed = 10000000000000f;
    public LayerMask hidingZoneLayer;
    public float crouchDelta;
    public GameObject aboveCheck;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        startYScale = currentYScale = transform.localScale.y; 
        currentSpeed = 5f;

        isCrouching = false;
        
    }


    public enum MouvementState
    {
        grounded,
        jumping,
        walking,
        crouching,
        focus,
    }

    private void StateHandler()
    {
        //Mode - Mouvement
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            state = MouvementState.walking;
            Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput; //Pour les valeurs du Vector3 de direction on prends les inputs et on les associe a leurs directions
            controller.Move(moveDirection * currentSpeed * Time.deltaTime); //On multiple ces coordonnées par la vitesse de mouvement
        }

        //Mode - Crouching
        if(Input.GetKeyDown(crouchKey))
        {
            state = MouvementState.crouching;
            currentSpeed = crouchSpeed;
        }

        //Mode - Focus
        //if(GetComponent<Sc_Light>().lightFocus == true)
        if((Input.GetKey(KeyCode.Mouse0)))
        {
            Debug.Log("Y'a un truc au dessus");
            state = MouvementState.focus;
            currentSpeed = focusSpeed;
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
            aboveCheck.GetComponent<Sc_AboveCheck>().isThereSomethingAbove = false;
        }

        if (isCrouching == true)
        {
            transform.localScale = new Vector3(transform.localScale.x, currentYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        //stop crouch
        if (Input.GetKeyUp(crouchKey))
        {
            isTryingToStand = true;
        }

        if(isTryingToStand == true)
        {
            if(aboveCheck.GetComponent<Sc_AboveCheck>().isThereSomethingAbove == false)
            {
                isTryingToStand = false;
                isCrouching = false;
            }
            else
            {
                isTryingToStand = true;
                isCrouching = false;
                Debug.Log("Y'a un truc au dessus");
            }
        }

        if (isCrouching == false)
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
            currentSpeed = walkingSpeed;
            isTryingToStand = false;
            aboveCheck.GetComponent<Sc_AboveCheck>().isThereSomethingAbove = false;
        }


        hidingZoneLayer = LayerMask.NameToLayer("Wall");
        hidingZoneLayer = ~hidingZoneLayer;
        //aboveCheck = Physics.Raycast(transform.position, transform.up, out RaycastHit hit, 3f);




        isGrounded = Physics.CheckSphere(groundCheck.position, sphereSize, groundMask);

        if (isGrounded && velocity.y < 0) //si on touche le sol, on reset la vélocité
        {
            velocity.y = -2f;
        }

        if (isGrounded && !isCrouching && !isTryingToStand && Input.GetKeyDown(KeyCode.Space)) //on vérifie si le player est grounded quand il appuie sur l'input de saut
        {
            velocity.y = velocity.y + Mathf.Sqrt(JumpHeight * -2f * gravityForce); //on multiplie la hauteur du saut par (-2) et par la gravité
        }



        if(isJumping == true)
        {

            isGrounded = false;
        }
    


    }


}


    




    







        




    
