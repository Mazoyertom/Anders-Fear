using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_LineOfSight : MonoBehaviour
{

    public LayerMask targetMask;
    public LayerMask obstructionMask;

    public bool seeingThePlayer;


    // Start is called before the first frame update
    void Start()
    {
        seeingThePlayer = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    
}
