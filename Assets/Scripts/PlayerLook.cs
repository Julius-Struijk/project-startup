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
        backUpsensX = sensX;
        backUpsensY = sensY;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;

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
