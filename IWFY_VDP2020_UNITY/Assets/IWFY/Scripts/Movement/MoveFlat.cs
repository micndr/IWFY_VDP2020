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
        // cache components
        characterController = GetComponent<CharacterController>();
        // make the cursor invisible and stuck at the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Camera.main assumes only one main camera is in the scene
        cam = Camera.main.transform;
    }

    void Update() {
        float inh = lockUserInput ? 0 : 1;
        if (!freeCursor) {
            // Look rotation
            // horizontal rotates the whole player
            transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX * inh);
            // vertical only the cam
            verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY * inh;
            verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90, 90);
            cam.localEulerAngles = Vector3.left * verticalLookRotation;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        } else {
            // if the cursor has to click on the screen, the look rotation is inhibited
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // calculating strafe with Vector3.cross, to get the orthogonal vec w.r.t. player
        // forward and parallel w.r.t the ground. then just add forward/backward movement.
        acc = (Vector3.Cross(Vector3.up, transform.forward)) * Input.GetAxis("Horizontal")
            + transform.forward * Input.GetAxis("Vertical");
        // normalizing because otherwise if the player is holding up and left it goes
        // sqrt(2) and not 1 fast
        if (acc.magnitude > 1) acc.Normalize();

        // reset gravity
        if (characterController.isGrounded) velocity = Vector3.zero;

        if (Application.isEditor) {
            // debug for saving
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
        // apply acceleration only in fixed updates
        if (lockUserInput) acc = Vector3.zero;
        characterController.Move(acc * Time.fixedDeltaTime * speed
            + velocity * Time.fixedDeltaTime);
        velocity += Vector3.down * 9.8f * Time.fixedDeltaTime * 3;
    }

}
