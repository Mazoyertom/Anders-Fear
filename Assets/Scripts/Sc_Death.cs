using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Death : MonoBehaviour
{
    public bool isTouchingPlayer;

    public GameObject playerRef;
    public GameObject playerBody;

    
   private void OnTriggerEnter(Collider other)
   {
        if(other.gameObject.tag == "Player")
        {
            isTouchingPlayer = true;
            playerDeath();
        }
   }

   private void playerDeath()
   {
    //if(isTouchingPlayer == true);
   // {
        Destroy(playerBody.gameObject);
   // }
   }

      private void OnTriggerExit()
   {
        isTouchingPlayer = false;
   }



}
