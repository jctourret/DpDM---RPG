using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    float mouseX;
    float mouseY;
    float mouseSensitivity= 100f;
    float xRotation;
    float yRotation;
    float angelLimit = 20f;
    Quaternion rotation;
    Quaternion baseRotation = new Quaternion(0,0,1,0);
    Gyroscope gyro;
    bool gyroEnabled;
    private Transform body;
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Confined;
        body = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_STANDALONE || UNITY_EDITOR
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity*Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-angelLimit,angelLimit);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        body.Rotate(Vector3.up*mouseX);
#endif
#if UNITY_ANDROID
        body.Rotate(Vector3.up*Input.acceleration.x*mouseSensitivity*Time.deltaTime);
#endif
    }

}
