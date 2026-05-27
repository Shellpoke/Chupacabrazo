using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;

    //Movement Speed here
    [Header("Movement")]
    public float walkSpeed = 50f;
    public float runSpeed = 100f;

    //Camera Settings Here
    [Header("Camera")]
    public Transform CameraHolder;
    public float mouseSensitivity = 300f;
    public float minLookAngle = -20f;
    public float maxLookAngle = 60f;
    private float CameraPitch = 15f;
    private float verticalVelocity;

    //Jumping settings here
    [Header("Jumping")]
    public float jumpVelocity = 50f;
    public float glideGravity = -10f;
    public float gravity = -100f;


 //----------------------------------------------------------UNITY FUNCTIONS HERE------------------------------------------------//
    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }



    void Update()
    {
        MovePlayer();
        RotatePlayerAndCamera();
    }


 //-----------------------------------------------------------OTHER FUNCTIONS HERE-------------------------------------------------//
    void MovePlayer()
    {
        //this boolean checks if the player is on ground, to decide if jumping or gliding
        bool isGrounded = controller.isGrounded;

        //keeps player on the cround
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        //input collected and speed declared
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        float moveSpeed = walkSpeed;
        Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f); //prevents extra speed from double input from diagonals
        
        //running trigger
        if (Input.GetButton("Run"))
        {
            moveSpeed = runSpeed;
        }
        Vector3 movement = moveDirection * moveSpeed; 

        //Jump trigger
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
                verticalVelocity = jumpVelocity;
        }

        //glide trigger
        if(Input.GetButton("Jump") && !isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = glideGravity;
        }

        //fall trigger (as long as user is not pressing then it falls with gravity as intended)
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }
        
        movement.y = verticalVelocity; //actual falling
        controller.Move(movement * Time.deltaTime); //actual movement
    }

    void RotatePlayerAndCamera()
    {
        //captuing mouse movement for camera
        float mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        //Rotate player left/right
        transform.Rotate(Vector3.up * mouseX);

        //Rotate camera up/down around the player
        CameraPitch -= mouseY;
        CameraPitch = Mathf.Clamp(CameraPitch, minLookAngle, maxLookAngle);

        //set camera position on site.
        if (CameraHolder != null)
        {
            CameraHolder.localRotation = Quaternion.Euler(CameraPitch, 0f, 0f);
        }
    }
}