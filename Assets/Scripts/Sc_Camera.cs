using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Camera : MonoBehaviour
{

    public float mouseSensitivity;

    public Transform playerBody;
    public Transform playerHand;

    //float xRotation = 0f;

    public Vector3 screenPosition;

    float mouseX, mouseY;
    float cameraSensitivity = 20f;




    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        //playerHand = playerBody.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        // Lock le curseur au centre de l'écran et le rendre invisible
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/

        // Récupérer les inpput de la souris
        mouseX += Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        mouseY += Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        screenPosition = Input.mousePosition;


        //xRotation = xRotation - mouseY;
        //mouseX = xRotation - mouseY;

        // Empecher le joueur de regarde plus haut plus loin que "dessus" ou "dessous"
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        mouseY = Mathf.Clamp(mouseY, -90f, 90f);

        //playerHand.localRotation = Quaternion.Euler(mouseY, -mouseX, 0f);

        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.localRotation = Quaternion.Lerp(playerBody.localRotation, Quaternion.Euler(0, mouseX, 0), cameraSensitivity*Time.deltaTime);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(-mouseY,0, 0), cameraSensitivity*Time.deltaTime);



    
    }
}
