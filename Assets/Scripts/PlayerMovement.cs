using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller; // Character controller component
    public float speed = 12f;
    public float gravity=-9.81f*2; // Speed of the player movement
    public float jumpHeight = 3f; // Height of the jump
    public float groundDistance = 0.4f; // Distance to check if the player is grounded
    public LayerMask groundMask; // Layer mask to check for ground
    public Transform groundCheck; // Transform to check if the player is grounded
    Vector3 velocity; // Velocity of the player
    bool isMoving; // Check if the player is moving
    bool isGrounded; // Check if the player is grounded
    private Vector3 lastPosition=new Vector3(0f,0f,0f); // Last position of the player
    void Start()
    {
       controller = GetComponent<CharacterController>();  
    }

    // Update is called once per frame
    void Update()
    {

       isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask); // Check if the player is grounded
       if (isGrounded && velocity.y < 0) // If the player is grounded and the velocity is less than 0
       {
           velocity.y = -2f; // Set the velocity to -2f to keep the player grounded
       }
         float x = Input.GetAxis("Horizontal"); // Get the horizontal input
         float z = Input.GetAxis("Vertical"); // Get the vertical input
            Vector3 move = transform.right * x + transform.forward * z; 
controller.Move(move * speed * Time.deltaTime); 
// Move the player based on the input
         if (Input.GetButtonDown("Jump") && isGrounded) // If the player presses the jump button and is grounded
         {
             velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity); // Set the velocity to jump
         }
velocity.y+= gravity * Time.deltaTime; // Apply gravity to the player
controller.Move(velocity * Time.deltaTime); // Move the player based on the velocity
         if(lastPosition!=gameObject.transform.position && isGrounded ==true) // If the player is moving and grounded
         {
             isMoving=true; // Set the player to moving
         }
         else{
                isMoving=false; // Set the player to not moving
         }
          lastPosition=gameObject.transform.position; // Set the last position to the current position
    }
}
