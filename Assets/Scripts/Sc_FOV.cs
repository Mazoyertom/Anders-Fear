using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_FOV2 : MonoBehaviour
{
    public LayerMask targetMask;
    public LayerMask obstructionMask;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        function OnTriggerEnter(other : Collider);
        {
            if(other.collider.tag == Player.tag)
            {
                Script = GetComponent(Sc_Chase); 
                Script.enabled = true; 
            }
        }
    }
}
