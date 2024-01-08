using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Patrouille : MonoBehaviour
{
    public GameObject goFlag_A;
    public GameObject goFlag_B;
    public GameObject goFlag_C;

    public bool bState_Patrol_To_A = true;
    public bool bState_Patrol_To_B = false;
    public bool run_To_C = false;
    public bool hide_In_C = false;
    public float hiding_Time = 2f;

    public bool bFlag_A_reached;
    public bool bFlag_B_reached;
    public bool bFlag_C_reached;



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
        if(Input.GetKeyDown(KeyCode.Space))
            {
                bState_Patrol_To_A = false;
                bState_Patrol_To_B = false;
                run_To_C = true;
            }

            if(run_To_C == true)
            {
                this.transform.position = Vector3.Lerp(this.transform.position, goFlag_C.transform.position, Time.deltaTime);
                float dist_flag_C = Vector3.Distance(goFlag_C.transform.position, this.transform.position);
                if(dist_flag_C < 0.1f)
                {
                    bState_Patrol_To_A = false;
                    bState_Patrol_To_B = false;
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
                    bFlag_C_reached = false;
                }

                while(hide_In_C == true || hiding_Time > 0)
                {
                    hiding_Time -= Time.deltaTime;
                }

                if(hiding_Time < 0) 
                {
                    hide_In_C = false;
                    bState_Patrol_To_A = true;
                    hiding_Time = 0;
                }



            }


            
        }
    }
