using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_MonsterDeath : MonoBehaviour
{

    public bool AffectLight = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AffectLight)
        {
          Destroy(gameObject, 2);   
        }
        
    }
}
