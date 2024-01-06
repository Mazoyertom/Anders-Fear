using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Collectible : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Sc_PlayerCollect sc_PlayerCollect = other.GetComponent<Sc_PlayerCollect>();

        if (sc_PlayerCollect != null)
        {
            sc_PlayerCollect.ObjectCollected();
            gameObject.SetActive(false);
        }
    }
}
