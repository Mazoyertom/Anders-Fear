using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_AboveCheck : MonoBehaviour
{
    public bool isThereSomethingAbove;

    public LayerMask aboveLayer;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
          isThereSomethingAbove = true;
        }
    }
}
