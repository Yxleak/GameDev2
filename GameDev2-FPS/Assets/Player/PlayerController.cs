using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Camera Variables
    public Camera cam;
    private Vector2 look_input = Vector2.zero;
    private float look_speed = 60f;
    private float horizontal_look_angle = 0f;
    public bool invert_x = false;
    public int invert_factor_x = 1;
    public bool invert_y = false;
    public int invert_factor_y = 1;
    [Range(0.01f, 1f)]public float sensitivity;

    // Start is called before the first frame update
    void Start()
    {
        // Hide Mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Invert Camera T_T (why?????)
        if (invert_x) invert_factor_x = -1;
        if (invert_y) invert_factor_y = -1;

    }

    // Update is called once per frame
    void Update()
    {
        Look();
    }

    public void GetLookInput(InputAction.CallbackContext context)
    {
        look_input = context.ReadValue<Vector2>();
    }

    private void Look()
    {
        // Left and Right Look
        transform.Rotate(Vector3.up, look_input.x * look_speed * Time.deltaTime * invert_factor_x * sensitivity);

        // Up and Down Look
        float angle = look_input.y * look_speed * Time.deltaTime * invert_factor_y * sensitivity;
        horizontal_look_angle -= angle;
        horizontal_look_angle = Mathf.Clamp(horizontal_look_angle, -90, 90);
        cam.transform.localRotation = Quaternion.Euler(horizontal_look_angle, 0, 0);
    }

}
