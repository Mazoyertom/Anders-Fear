// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class Sc_MonsterAI : MonoBehaviour
// {


//     public GameObject goGoal_1;
//     public GameObject goGoal_2;
//     public GameObject goGoal_3;

//     public bool bState_Patrol_To_1 = true;
//     public bool bState_Patrol_To_2   = false;

//     public bool bGoal_1_reached;
//     public bool bGoal_2_reached;


//     // Update is called once per frame
//     void Update()
//     {
//         // MOVE TO A
//         if(bState_Patrol_To_A == true)
//         {
//             this.transform.position = Vector3.Lerp(this.transform.position, goFlag_A.transform.position, Time.deltaTime);
//             float dist_flag_A = Vector3.Distance(goFlag_A.transform.position, this.transform.position);
//             if(dist_flag_A < 0.1f)
//             {
//                 bFlag_A_reached = true;
//             }
//             else
//             {
//                 bFlag_A_reached = false;
//             }

//             if(bFlag_A_reached == true)
//             {
//                 bState_Patrol_To_B = true;
//                 bState_Patrol_To_A = false;
//             }
//         }

//         // MOVE TO B
//         if(bState_Patrol_To_B == true)
//         {
//             this.transform.position = Vector3.Lerp(this.transform.position, goFlag_B.transform.position, Time.deltaTime);
//             float dist_flag_B = Vector3.Distance(goFlag_B.transform.position, this.transform.position);
//             if(dist_flag_B < 0.1f)
//             {
//                 bFlag_B_reached = true;
//             }
//             else
//             {
//                 bFlag_B_reached = false;
//             }

//             if(bFlag_B_reached == true)
//             {
//                 bState_Patrol_To_A = true;
//                 bState_Patrol_To_B = false;
//             }
//         }
        



//     }
// }
