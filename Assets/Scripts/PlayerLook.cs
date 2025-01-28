using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    private float backUpsensX, backUpsensY;

    [SerializeField] float sensX;
    [SerializeField] float sensY;
    [SerializeField] Transform PlayerOrientation;

    float xRotation;
    float yRotation;

    void Start()
    {
        //QualitySettings.vSyncCount = 2;
        //Application.targetFrameRate = 60;

        backUpsensX = sensX;
        backUpsensY = sensY;
       // Cursor.lockState = CursorLockMode.Locked;
       // Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensX;
        float mouseY = Input.GetAxis("Mouse Y") * sensY;

        yRotation += mouseX * Time.deltaTime;
        xRotation -= mouseY * Time.deltaTime;
        //Debug.LogFormat("Look speed: {0}", PlayerOrientation.rotation.x);

        xRotation = Mathf.Clamp(xRotation, -90f, 80f);
        transform.rotation = Quaternion.Euler(0, yRotation, 0);
        PlayerOrientation.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    }

    public void SetSensitivity(float sensitivity)
    {
        sensX = sensitivity;
        sensY = sensitivity;
        backUpsensX = sensX;
        backUpsensY = sensY;
    }

    public void SetEnabledLook(bool enable)
    {
        if (enable)
        {
            sensX = backUpsensX;
            sensY = backUpsensY;
        }
        else
        {
            sensX = 0;
            sensY = 0;
        }
    }
}
