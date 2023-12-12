using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Sc_MousePosition : MonoBehaviour
{


    public Vector3 screenPosition;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        screenPosition = Input.mousePosition;

       
    }
}
