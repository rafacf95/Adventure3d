using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("Setup")]
    public Animator animator;
    public PlayerStateMachine stateMachine;
    public CharacterController characterController;
    public HealthBase healthBase;
    public List<Collider> colliders;
    public float speed = 15f;
    public float turnSpeed = 25f;

    [Header("Jump Setup")]
    public float gravity = 9.8f;
    public float jumpSpeed = 15f;
    public KeyCode jumpkey = KeyCode.Space;

    [Header("Run Setup")]
    public KeyCode runKey = KeyCode.LeftShift;
    public float runSpeed = 1.5f;

    [Header("Flash")]

    public List<FlashColor> flashColors;

    public float inputAxisVertical;

    private float _vSpeed = 0f;
    private bool _alive = true;

    public void Flash(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
    }

    public void OnKill(HealthBase h)
    {
        if (_alive)
        {
            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);

            Invoke(nameof(Revive), 2f);
        }
    }

    private void Respawn()
    {
        if (CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.Instance.GetLastCheckpointPosition();
        }
    }

    private void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);
    }

    private void Revive()
    {
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        _alive = true;
        Respawn();

        Invoke(nameof(TurnOnColliders), 1f);
    }

    void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    void Awake()
    {
        OnValidate();

        healthBase.OnDamage += Flash;
        healthBase.OnKill += OnKill;
    }

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
            if (animator != null) animator.SetBool("Run", true);
            if (Input.GetKey(runKey))
            {
                speedVector *= runSpeed;
                if (animator != null) animator.speed = runSpeed;
            }
            else
            {
                if (animator != null) animator.speed = 1;
            }
        }
        else
        {
            if (animator != null) animator.SetBool("Run", false);
        }

        _vSpeed -= gravity * Time.deltaTime;
        speedVector.y = _vSpeed;

        characterController.Move(speedVector * Time.deltaTime);
    }
}
