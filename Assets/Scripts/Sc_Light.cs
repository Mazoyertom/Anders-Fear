using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Light : MonoBehaviour
{


    [Header("Light")]
    public bool lightFocus;
    public bool lightBurst;
    public Light playerLight;
    public float lightIntensity;
    private KeyCode lightFocusKey = KeyCode.Mouse0;


    // Start is called before the first frame update
    void Start()
    {
        playerLight = GetComponent<Light>();
        playerLight.intensity = 8f;
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(lightFocusKey))
        {
            lightFocus = true;
        }

        if(lightFocus == true)
        {
            playerLight.intensity = Mathf.PingPong(Time.time * 6, 32);
            Debug.Log("Light intensity : " + playerLight.intensity);
        }


        if(playerLight.intensity >= 32)
        {
            lightBurst = true; 
            lightFocus = false;
        }

        if(lightBurst == true)
        {
            
        }


        










        /*
        // Lock le curseur au centre de l'écran et le rendre invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // R�cup�rer les inpput de la souris
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSensitivity;

        xRotation = xRotation - mouseY;

        // Empecher le joueur de regarde plus haut plus loin que "dessus" ou "dessous"
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);*/






    }
}
