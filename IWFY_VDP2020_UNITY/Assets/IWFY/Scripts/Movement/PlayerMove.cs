using UnityEngine;
using System.Collections;

// Sebastian Lague https://github.com/SebLague/Spherical-Gravity

[RequireComponent(typeof(GravityBody))]
public class PlayerMove : MonoBehaviour {

    // public vars
    public float mouseSensitivityX = 1;
    public float mouseSensitivityY = 1;
    public float walkSpeed = 6;
    public float runSpeed = 10;
    public float jumpForce = 220;
    public LayerMask groundedMask;

    // System vars
    bool grounded;
    Vector3 moveAmount;
    Vector3 smoothMoveVelocity;
    float verticalLookRotation;
    Transform cameraTransform;
    Rigidbody rigidbody;

    GameObject planet;

    public bool lockUserInput = false;
    public bool freeCursor = false;

    DialogueManager dialogueManager;


    void Awake() {
        planet = GameObject.FindGameObjectWithTag("Planet");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cameraTransform = Camera.main.transform;
        rigidbody = GetComponent<Rigidbody>();

        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    void Update() {
    
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Application.Quit();
        }
        
        float inh = lockUserInput ? 0 : 1;

        //space to advance moved to dialogue manager, for code clarity purpose

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


        transform.rotation = Quaternion.FromToRotation(
            transform.up, 
            (transform.position - planet.transform.position).normalized) * transform.rotation;

        float speed = walkSpeed;
        if (Input.GetKey(KeyCode.LeftShift)) {
            speed = runSpeed;
        }

        // Calculate movement:
        float inputX = Input.GetAxisRaw("Horizontal") * inh;
        float inputY = Input.GetAxisRaw("Vertical") * inh;

        Vector3 moveDir = new Vector3(inputX, 0, inputY).normalized;
        Vector3 targetMoveAmount = moveDir * speed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothMoveVelocity, .15f);
    }

    void FixedUpdate() {
        // Apply movement to rigidbody
        Vector3 localMove = transform.TransformDirection(moveAmount) * Time.fixedDeltaTime;
        rigidbody.MovePosition(rigidbody.position + localMove);
    }
}