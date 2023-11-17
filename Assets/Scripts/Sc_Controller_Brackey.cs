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




    // Update is called once per frame
    void Update()
    {

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");


        Vector3 moveDirection = transform.right * horizontalInput + transform.forward * verticalInput; //Pour les valeurs du vector3 de direction on prends les inputs et on les associe a leurs directions
        controller.Move(moveDirection * moveSpeed * Time.deltaTime); //On multiple ces coordonnées par la vitesse de mouvement

        

        velocity.y = velocity.y + gravityForce * Time.deltaTime; //Simulation de la gravité 
        controller.Move(velocity * Time.deltaTime); //Son application sur le controller



        isGrounded = Physics.CheckSphere(groundCheck.position, sphereSize, groundMask);

        if (isGrounded && velocity.y < 0) //si on touche le sol
        {  
            velocity.y = -2f;

        }



        if (isGrounded && Input.GetKeyDown(KeyCode.Space)) //on vérifie si le player est grounded quand il appuie sur l'input de saut
        {

           velocity.y = velocity.y + Mathf.Sqrt(JumpHeight * -2f * gravityForce); //on multiplie la hauteur du saut par (-2) et par la gravité

        }
    


    }



}


    




    







        




    
