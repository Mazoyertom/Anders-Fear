using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Blinded : MonoBehaviour
{
    public bool isBlindTrigger;

    public GameObject lightCheck;
    [SerializeField] private Sc_Light playerLight;

    void Start()
    {
        playerLight = lightCheck.GetComponent<Sc_Light>();
    }


    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Light Beam" && playerLight.lightBurst == true)
        {
            isBlindTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (lightCheck.GetComponent<Sc_Light>().lightBurst == false)
        {
            isBlindTrigger = false;
        }
    }
}
