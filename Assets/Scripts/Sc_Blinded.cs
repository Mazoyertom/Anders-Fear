using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Blinded : MonoBehaviour
{
    public GameObject lightCheck;

    public bool isBlinded;
    public bool AAA;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Light Beam")
        {
            AAA = true;
            Debug.Log("LUMIERE");
        }

        if (other.gameObject.tag == "Light Beam" && lightCheck.GetComponent<Sc_Light>().lightBurst == true)
        {
            isBlinded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Light Beam" || lightCheck.GetComponent<Sc_Light>().lightBurst == false)
        {
            isBlinded = false;
        }
    }
}
