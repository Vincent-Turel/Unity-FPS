using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{

    public CharacterController controller;
    public Transform groundCheck;
    private float groundDistance = 0.4f;
    private bool isGrounded;
    private Vector3 velocity;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public LayerMask groundMask; 
    public Animator anim;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }
         
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move / 7);
        
        anim.SetFloat("speed", move.magnitude);

        velocity.y += gravity * 1.8f * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
    }
}
