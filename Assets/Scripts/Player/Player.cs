using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public PlayerStateMachine stateMachine;
    public CharacterController characterController;
    public float speed = 15f;
    public float turnSpeed = 25f;

    [Header("Jump Setup")]
    public float gravity = 9.8f;
    public float jumpSpeed = 15f;
    public KeyCode jumpkey = KeyCode.Space;

    [Header("Run Setup")]
    public KeyCode runKey = KeyCode.LeftShift;
    public float runSpeed = 1.5f;

    public float inputAxisVertical;

    private float _vSpeed = 0f;

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = inputAxisVertical * speed * transform.forward;

        if (characterController.isGrounded)
        {
            _vSpeed = 0;
            if (Input.GetKeyDown(jumpkey))
            {
                _vSpeed = jumpSpeed;
            }
        }

        var isWalking = inputAxisVertical != 0;


        if (isWalking)
        {
            animator.SetBool("Run", true);
            if (Input.GetKey(runKey))
            {
                speedVector *= runSpeed;
                animator.speed = runSpeed;
            }
            else
            {
                animator.speed = 1;
            }
        }
        else
        {
            animator.SetBool("Run", false);
        }

        _vSpeed -= gravity * Time.deltaTime;
        speedVector.y = _vSpeed;

        characterController.Move(speedVector * Time.deltaTime);
    }
}
