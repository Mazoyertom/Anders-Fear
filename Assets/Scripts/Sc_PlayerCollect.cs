using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_PlayerCollect : MonoBehaviour
{

    public int NumberOfSc_Collect { get; private set; }

    public void ObjectCollected()
    {
        NumberOfSc_Collect++;
    }
}
