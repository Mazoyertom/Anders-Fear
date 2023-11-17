using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_Camera : MonoBehaviour
{

    public float mouseSensitivity;

    public Transform playerBody;

    float xRotation = 0f;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Lock le curseur au centre de l'écran et le rendre invisible
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;



        // Récupérer les inpput de la souris
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSensitivity;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSensitivity;

        xRotation = xRotation - mouseY;

        // Empecher le joueur de regarde plus haut plus loin que "dessus" ou "dessous"
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);



    
    }
}
