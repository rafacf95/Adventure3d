using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Singleton;
using Cloth;

public class Player : Singleton<Player>
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

    [Space]
    [SerializeField] ClothChanger clothChanger;

    [Space]
    public float inputAxisVertical;

    private float _vSpeed = 0f;
    private bool _alive = true;
    private bool _jumping = false;
    private ClothSetup _currentClothSetup;
    public ClothSetup CurrentClothSetup
    {
        get { return _currentClothSetup; }
    }

    public void Flash(HealthBase h)
    {
        flashColors.ForEach(i => i.Flash());
        EffectsManager.Instance.ChangeVignette();
        ShakeCamera.Instance.Shake();
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

    public void ChangeSpeed(float boostedSpeed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(boostedSpeed, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float boostedSpeed, float duration)
    {
        var defaultSpeed = speed;
        speed *= boostedSpeed;
        yield return new WaitForSeconds(duration);
        speed = defaultSpeed;
    }

    public void ChangeJumpSpeed(float boostedJumpSpeed, float duration)
    {
        StartCoroutine(ChangeJumpSpeedCoroutine(boostedJumpSpeed, duration));
    }

    IEnumerator ChangeJumpSpeedCoroutine(float boostedJumpSpeed, float duration)
    {
        var defaultJumpSpeed = jumpSpeed;
        jumpSpeed *= boostedJumpSpeed;
        yield return new WaitForSeconds(duration);
        jumpSpeed = defaultJumpSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        clothChanger.ChangeTexture(setup);
        _currentClothSetup = setup;
        yield return new WaitForSeconds(duration);
        clothChanger.ResetTexture();
        _currentClothSetup = ClothManager.Instance.GetSetupByType(ClothType.BASE);
    }

    #region LOAD

    private void LoadFromFile()
    {
        healthBase.CurrentLife = SaveManager.Instance.SavedValues.playerHealth;
        _currentClothSetup = SaveManager.Instance.SavedValues.clothSetup;
    }

    #endregion


    void OnValidate()
    {
        if (healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    protected override void Awake()
    {
        base.Awake();

        OnValidate();

        healthBase.OnDamage += Flash;
        healthBase.OnKill += OnKill;

        if (_currentClothSetup == null)
        {
            _currentClothSetup = ClothManager.Instance.GetSetupByType(ClothType.BASE);
        }

        LoadFromFile();
    }

    void Update()
    {
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = inputAxisVertical * speed * transform.forward;

        if (characterController.isGrounded)
        {

            if (_jumping)
            {
                _jumping = false;
                animator.SetTrigger("Land");
            }

            _vSpeed = 0;
            if (Input.GetKeyDown(jumpkey))
            {
                _vSpeed = jumpSpeed;
                if (!_jumping)
                {
                    _jumping = true;
                    animator.SetTrigger("Jump");
                }
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
