using FPS.Attack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.Controller
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 10f;
        [SerializeField] private float runSpeed = 12f;
        [SerializeField] private float jumpPower = 10f;
        [SerializeField] private float maxJump = 2f;
        [SerializeField] private float gravityModifier = 2f;
        [SerializeField] private float mouseSensitivity = 10f;
        [SerializeField] private CharacterController characterController = null;
        [SerializeField] private Animator animator = null;
        [SerializeField] private Transform cameraTransform = null;    
        [SerializeField] private bool invertX = false;
        [SerializeField] private bool invertY = false;
        [SerializeField] private LayerMask groundLayer;

        private bool canJump = false;
        private float jumpCount = 0f;
        private Vector3 moveInput;

        void Update()
        {
            // rotate the player
            Rotate();

            // move the player
            Move();

            // fire
            if(Input.GetButton("Fire1"))
                transform.GetComponent<Shoot>().Fire();
        }

        private void Move()
        {
            float sumGravity = moveInput.y;

            moveInput.z = Input.GetAxis("Vertical");
            moveInput.x = Input.GetAxis("Horizontal");
            moveInput.y = 0;            

            moveInput.Normalize();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveInput *= runSpeed;

                if (moveInput.x != 0 || moveInput.z != 0 && characterController.isGrounded)
                    animator.SetFloat("moveSpeed", 3);
                else
                    animator.SetFloat("moveSpeed", 0);
            }

            else
            {
                moveInput *= moveSpeed;

                if (moveInput.x != 0 || moveInput.z != 0 && characterController.isGrounded)
                    animator.SetFloat("moveSpeed", 2);
                else
                    animator.SetFloat("moveSpeed", 0);
            }          

            moveInput = transform.TransformDirection(moveInput);

            moveInput.y = sumGravity;
            moveInput.y += Physics.gravity.y * gravityModifier * Time.deltaTime;

            if (characterController.isGrounded)
            {             
                moveInput.y = Physics.gravity.y * gravityModifier * Time.deltaTime;
            }

            canJump = Physics.OverlapSphere(transform.position, .15f, groundLayer).Length > 0;
            if ((Input.GetKeyDown(KeyCode.Space) && canJump) || (Input.GetButtonDown("Jump") && jumpCount < maxJump && jumpCount != 0))
            {
                moveInput.y = jumpPower;
                jumpCount++;
            }
            else if(canJump)
            {
                jumpCount = 0;
            }

            characterController.Move(moveInput * Time.deltaTime);
        
        }

        private void Rotate()
        {
            Vector2 mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")) * mouseSensitivity;

            if (invertX)
                transform.Rotate(0, -mouseInput.x * Time.deltaTime, 0);
            else
                transform.Rotate(0, mouseInput.x * Time.deltaTime, 0);

            if (invertY)
                cameraTransform.Rotate(mouseInput.y * Time.deltaTime, 0, 0);
            else
                cameraTransform.Rotate(-mouseInput.y * Time.deltaTime, 0, 0);
        }

    }
}
