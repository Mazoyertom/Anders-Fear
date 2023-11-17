/*
###################################
PARTIEL : Le Garde 
VOTRE NOM : ______
VOTRE PRENOM : ______
###################################

CODEZ UNE MACHINE A ETAT POUR UN GARDE PAS TRES TEMERAIRE 
- En temps normal, le garde fait sa ronde (patrouille) : 
    - Il se déplace vers le drapeau A. 
    - Quand il est sur le drapeau A, il se déplace vers le drapeau B. 
    - Quand il est sur le drapeau B, il se déplace vers le drapeau A 
    - Et ainsi de suite. 
- Lorsque le garde est en patrouille, si j'appuie sur la barre espace, le garde se déplace (fuite) vers le drapeau C. 
- Une fois qu'il a atteint le drapeau C, il y reste deux secondes (cachette), puis retourne vers le drapeau A (retour), avant de reprendre sa ronde (patrouille). 
- Il ne peut pas retourner se cacher tant qu'il n'a pas repris sa patrouille. 

Points bonus : 
- Le garde regarde dans la direction dans laquelle il marche. 
- Le garde se déplace plus vite quand il fuit et plus lentement quand il revient. 

Conseils : 
- Prenez un papier et un stylo pour faire des schémas de la machine a état du garde. 
- Essayez de faire le maximum dans le temps imparti. Je préfère voir un grand code qui marche pas plutôt que rien du tout. 
- Pour tester s'il est sur la position d'un drapeau, recherchez ceci sur Google : Vector3.Distance() ...

Vous enverrez le script complet (Sc_Guard.cs) par mail : m.gaulmier@intervenant-ggeedu.fr

*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Patrol : MonoBehaviour
{
    public GameObject goFlag_A;
    public GameObject goFlag_B;
    public GameObject goFlag_C;

    public bool bState_Patrol_To_A = true;
    public bool bState_Patrol_To_B = false;
    public bool run_To_C = false;
    public bool hide_In_C = false;
    public float hiding_Time = 2f;

    public bool bFlag_A_reached = false;
    public bool bFlag_B_reached = false;
    public bool bFlag_C_reached = false;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
     {
        // MOVE TO A
        if(bState_Patrol_To_A == true)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, goFlag_A.transform.position, Time.deltaTime);
            float dist_flag_A = Vector3.Distance(goFlag_A.transform.position, this.transform.position);
            if(dist_flag_A < 0.1f)
            {
                bFlag_A_reached = true;
            }
            else
            {
                bFlag_A_reached = false;
            }

            if(bFlag_A_reached == true)
            {
                bState_Patrol_To_B = true;
                bState_Patrol_To_A = false;
            }
        }

        // MOVE TO B
        if(bState_Patrol_To_B == true)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, goFlag_B.transform.position, Time.deltaTime);
            float dist_flag_B = Vector3.Distance(goFlag_B.transform.position, this.transform.position);
            if(dist_flag_B < 0.1f)
            {
                bFlag_B_reached = true;
            }
            else
            {
                bFlag_B_reached = false;
            }

            if(bFlag_B_reached == true)
            {
                bState_Patrol_To_A = true;
                bState_Patrol_To_B = false;
            }
        }

        // MOVE TO C
        if(bState_Patrol_To_A == true || bState_Patrol_To_B == true)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                run_To_C = true;
            }

            if(run_To_C == true)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, goFlag_C.transform.position, Time.deltaTime);
                float dist_flag_C = Vector3.Distance(goFlag_C.transform.position, this.transform.position);
                if(dist_flag_C < 0.1f)
                {
                    bFlag_C_reached = true;
                }
                else
                {
                  bFlag_C_reached = false;
                }

                if(bFlag_C_reached == true)
                {
                    run_To_C = false;
                    hide_In_C = true;
                }

                if(hide_In_C == true)
                {
                    hiding_Time -= Time.deltaTime;
                }

                if(hiding_Time <= 0)
                {
                    hide_In_C = false;
                    bState_Patrol_To_A = true;
                }



            }


            
        }







    }
}
