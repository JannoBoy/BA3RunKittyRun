using System;
using UnityEngine;
using Mirror;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private CharacterController controller; 
    [SerializeField] private float speed = 6f; 
    [SerializeField] private float jumpSpeed = 15f; 
    [SerializeField] private float turnSmoothTime = 0.1f; 

    private float turnSmoothVelocity; 
    private float gravity = -20f; 
    private float tempDirectionY = 0; 

    void Update()
    {
        HandleMovement(); 
        HandleCrouch();
    }

    private void HandleCrouch()
    {
        if (!isLocalPlayer) return;

        if(Input.GetKey(KeyCode.LeftControl))
        {
            transform.localScale = new Vector3(1f, 0.5f, 1f); 
        }
        else
        {
            transform.localScale = new Vector3(1f, 1f, 1f); 
        }
    }
    private void HandleMovement()
    {
        if (!isLocalPlayer) return; 

        float horizontal = Input.GetAxisRaw("Horizontal"); 
        float vertical = Input.GetAxisRaw("Vertical"); 

        Vector3 direction = new Vector3(horizontal, 0, vertical); 
        
        if (controller.isGrounded) 
        {
            if (Input.GetButtonDown("Jump")) 
            {
                tempDirectionY = jumpSpeed;
            }
        }
        
        
        tempDirectionY += gravity * Time.deltaTime;

        direction.y = tempDirectionY;
        
        controller.Move(direction * speed * Time.deltaTime);
    }
}
