using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Light : MonoBehaviour
{
    [Header("Light")]
    public bool lightFocus;
    public bool lightUnfocus;
    public bool lightBurst;
    bool bCanBurstAgain;
    public Light playerLight;
    public GameObject objectLight;

    float startLightIntensity = 5f;
    public float currentLightIntensity;

    float startLightInnerAngle = 10f;
    public float currentLightInnerAngle;

    float startLightOuterAngle = 35f;
    public float currentLightOuterAngle;

    float fBurstTime;
    private KeyCode lightFocusKey = KeyCode.Mouse0;


    // Start is called before the first frame update
    void Start()
    {
        playerLight.intensity = startLightIntensity;
        currentLightIntensity = playerLight.intensity;

        playerLight.innerSpotAngle = startLightInnerAngle;
        currentLightInnerAngle = playerLight.innerSpotAngle;



        lightFocus = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(lightFocusKey) && lightBurst == false && bCanBurstAgain == true)
        {
            lightFocus = true;
        }

        if(lightFocus == true)
        {
            if(playerLight.intensity >= 20f)
            {
                lightBurst = true; 
                lightFocus = false;
                bCanBurstAgain = false;
                playerLight.intensity = 20f;
            }
            else
            {
                playerLight.intensity += Time.deltaTime * 5;
                //playerLight.innerSpotAngle += Time.deltaTime * 5;
            }

            if(Input.GetKeyUp(lightFocusKey))
            {
                lightUnfocus = true;
                lightFocus = false;
            }
        }

        if(lightUnfocus == true)
        {
            if(playerLight.intensity <= startLightIntensity)
            {
                lightBurst = false;
                lightUnfocus = false;
                bCanBurstAgain = true;
                playerLight.intensity = startLightIntensity;
            }
            else
            {
                playerLight.intensity -= Time.deltaTime * 5;
            }

            if(Input.GetKey(lightFocusKey))
            {
                lightFocus = true;
                lightUnfocus = false;
            }
        }

        
        if(lightBurst == true)
        {
           playerLight.intensity = 50f;
           fBurstTime += Time.deltaTime * 5;

           if(fBurstTime >= 3f)
           {
                lightBurst = false;
                lightUnfocus = false;
                lightFocus = false;
                playerLight.intensity = startLightIntensity;
                fBurstTime = 0f;
           }

           if(Physics.Raycast(objectLight.transform.position, objectLight.transform.forward, out var hit, 10f))
           {
                if(hit.collider.gameObject.tag == "Light Target")
                {
                    Debug.Log("aaaaaaaaaaaaaaaa");
                    //Debug.DrawRay(transform.position, transform.forward, Color.green); print("Hit");
                }
           }
        }

        if(Input.GetKeyUp(lightFocusKey) && lightBurst == false)
        {
            bCanBurstAgain = true;
        }
    }
}


   




