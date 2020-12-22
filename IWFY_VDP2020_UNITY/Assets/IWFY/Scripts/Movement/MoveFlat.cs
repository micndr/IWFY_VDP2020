using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFlat : MonoBehaviour {

    public CharacterController characterController;
    Transform cameraTransform;
    Vector3 acc;
    public Vector3 velocity;
    float verticalLookRotation;

    public float mouseSensitivityX = 1;
    public float mouseSensitivityY = 1;
    public float speed = 9;

    public bool lockUserInput = false;
    public bool freeCursor = false;

    public Transform cam;

    void Awake() {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cam = Camera.main.transform;
    }

    void Update() {
        float inh = lockUserInput ? 0 : 1;
        if (!freeCursor) {
            // Look rotation:
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX * inh);
            verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY * inh;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90, 90);
            cam.localEulerAngles = Vector3.left * verticalLookRotation;
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

        if (Application.isEditor) {
            if (Input.GetKeyDown(KeyCode.H)) {
                Debug.Log(Application.persistentDataPath);
                GlobalState globalState = FindObjectOfType<GlobalState>();
                globalState.Save();
            }

            if (Input.GetKeyDown(KeyCode.K)) {
                GlobalState globalState = FindObjectOfType<GlobalState>();
                globalState.Load();
            }
        }
    }

    void FixedUpdate() {
        if (lockUserInput) acc = Vector3.zero;
        characterController.Move(acc * Time.fixedDeltaTime * speed
            + velocity * Time.fixedDeltaTime);
        velocity += Vector3.down * 9.8f * Time.fixedDeltaTime * 3;
    }

}
