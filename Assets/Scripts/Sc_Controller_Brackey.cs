using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Controller_Brackey : MonoBehaviour
{

    public float moveSpeed = 15f;

    public CharacterController controller;


    public float gravityForce = -9.81f;
    Vector3 velocity;
    public float JumpHeight = 3f;


    public Transform groundCheck;
    public float sphereSize = 0.05f;
    public LayerMask groundMask;
    public bool isGrounded;
    public bool isJumping;
    public bool isCrounched;

    public bool playerLightOn;
    public float playerLightEnergy;



    public float crouchSpeed = 5f;
    public float crouchYScale;
    public float startYScale;
    public KeyCode crouchKey = LeftControl;
    

    public MovementState state;




    private void Start()
    {
        startYScale = transform.localScale.y; 



    }



    public enum MovementState
    {
        idle,
        jumping,
        walking,
        crounching,
    }

    private void StateHandler()
    {
        //Mode - Mouvement
        if(isGrounded && Input.GetAxisRaw("Horizontal") || Input.GetAxisRaw("Vertical"))
        {
            state = MouvementState.walking;
        }



    }


    // Update is called once per frame
    void Update()
    {

        // Get mouvement input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // Mouvement
        if(MouvementState.walking == true);
        {
            Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput; //Pour les valeurs du vector3 de direction on prends les inputs et on les associe a leurs directions
            controller.Move(moveDirection * moveSpeed * Time.deltaTime); //On multiple ces coordonnées par la vitesse de mouvement
        }
        

        // Gravité
        velocity.y = velocity.y + gravityForce * Time.deltaTime; //Simulation de la gravité 
        controller.Move(velocity * Time.deltaTime); //Son application sur le controller


        //start crouch
        if(Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crounchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }

        //stop crouch
        if(Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }




        isGrounded = Physics.CheckSphere(groundCheck.position, sphereSize, groundMask);

        if (isGrounded && velocity.y < 0) //si on touche le sol
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


    




    







        




    
