using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFlat : MonoBehaviour {

    CharacterController characterController;
    Transform cameraTransform;
    Vector3 acc;
    public Vector3 velocity;
    float verticalLookRotation;

    public float mouseSensitivityX = 1;
    public float mouseSensitivityY = 1;
    public float speed = 9;

    public bool lockUserInput = false;
    public bool freeCursor = false;

    void Start() {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraTransform = Camera.main.transform;
    }

    void Update() {
        float inh = lockUserInput ? 0 : 1;
        if (!freeCursor) {
            // Look rotation:
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX * inh);
            verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY * inh;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90, 90);
            cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        acc = (Vector3.Cross(Vector3.up, transform.forward)) * Input.GetAxis("Horizontal")
            + transform.forward * Input.GetAxis("Vertical");
        if (acc.magnitude > 1) acc.Normalize();

        if (characterController.isGrounded) velocity = Vector3.zero;
    }

    void FixedUpdate() {
        if (lockUserInput) acc = Vector3.zero;
        characterController.Move(acc * Time.fixedDeltaTime * speed
            + velocity * Time.fixedDeltaTime);
        velocity += Vector3.down * 9.8f * Time.fixedDeltaTime * 3;
    }

}
