using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 15f;
    public float turnSpeed = 25f;
    public float gravity = -9.8f;

    private float _vSpeed = 0f;

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        _vSpeed = gravity * Time.deltaTime;

        characterController.Move(speedVector * Time.deltaTime);
    }
}
