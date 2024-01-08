using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float sphereSize = 0.2f;
    public float JumpForce = 3f;

    public LayerMask groundMask;

    public bool isGrounded;
    public bool IsJumping = false;
    public bool IsFalling = false;

    public Vector3 lastPosition;
    public Vector3 futurePosition;
    public Vector3 actualPosition;

    void Start()
    {

    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(this.transform.position, sphereSize, groundMask); // checkSphere

        if (isGrounded == true){  //si on touche le sol

            IsFalling = false; // on ne doit pas etre en train de tomber suite a un saut
            

            if (Input.GetKey(KeyCode.Space)) // input de saut
            {
                lastPosition = this.transform.position;   //on prend la position avant jump
                futurePosition = lastPosition + new Vector3(0, 5, 0); //on calcule la position apres jump
                this.transform.position += new Vector3(0, JumpForce * Time.deltaTime, 0);
                
                IsJumping = true; //on est en train de sauter
            }  
        }

        if(IsJumping == true){ //on est en train de sauter
            if (isGrounded == false && this.transform.position.y >= futurePosition.y) // si on touche pas le sol et qu'on est au plus haut du saut
            {
                IsFalling = true; // on tombe suite a un saut
                
            }
            else if (this.transform.position.y <= futurePosition.y) // si on est pas au pic du saut
            {
                this.transform.position += new Vector3(0, JumpForce * Time.deltaTime, 0); // saut
                actualPosition = this.transform.position;   //on prend la position actuel
            }
        }

        if(IsFalling == true && isGrounded == false){ // on doit etre en train de tomber sans toucher le sol
            this.transform.position -= new Vector3(0, JumpForce * Time.deltaTime, 0); // on tombe
            IsJumping = false; // on est pas en train de sauter

        }
        if(IsJumping == false){ //on est pas en train de sauter
            if(isGrounded == false){ //on ne touche pas le sol
                this.transform.position -= new Vector3(0, JumpForce * Time.deltaTime, 0); // on tombe
            }
        }
        

    }
}
