using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Run,
    InAir,
    Dead
}

public class PlayerMovement : MonoBehaviour
{
    [Header("===== Basic Setting =====")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool playerStartWithFaceingRight = true;

    [Header("===== Get Component =====")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Animator ani;
    [SerializeField] private PlayerStatus ps;
    [SerializeField] private ScenceManager sm;

    [Header("===== Key Bind =====")]
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;

    private float defaultMoveSpeed;
    private float horizontal;
    private bool isFaceingRight;
    private PlayerState _state;
    private bool pausePlayer = false;
    private bool playerDead = false;

    public bool _isFaceingRight => isFaceingRight;
    public bool _pausePlayer => pausePlayer;
    
    //public PlayerState State { get { return state; } set { state = value; } }

    private void Start()
    {
        if (playerStartWithFaceingRight)
        {
            isFaceingRight = true;
        }
        else
        {
            isFaceingRight = false;
        }

        defaultMoveSpeed = moveSpeed;
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");    // This return -1,0,1
        Flip();
        UpdatePlayerState();
        UpdateAnimation();

        if (Input.GetKeyDown(jumpKey) && IsGrounded() && !pausePlayer)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        if (!pausePlayer)
        {
            rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if ((isFaceingRight && horizontal < 0f || !isFaceingRight && horizontal > 0f) && !pausePlayer)
        {
            isFaceingRight = !isFaceingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void UpdatePlayerState()
    {
        if (playerDead)
        {
            _state = PlayerState.Dead;
        }
        else if (horizontal == 0)
        {
            if (IsGrounded())
            {
                _state = PlayerState.Idle;
            }
            else
            {
                _state = PlayerState.InAir;
            }
        }
        else if (horizontal != 0)
        {
            if (IsGrounded())
            {
                _state = PlayerState.Run;
            }
            else
            {
                _state = PlayerState.InAir;
            }
        }
    }

    private void UpdateAnimation()
    {
        if (_state == PlayerState.Idle)
        {
            DisableAllAnimation();
            ani.SetBool("isIdle", true);
        }

        if (_state == PlayerState.Run)
        {
            DisableAllAnimation();
            ani.SetBool("isRun", true);
        }

        if (_state == PlayerState.InAir)
        {
            DisableAllAnimation();
            ani.SetBool("isInAir", true);
        }

        if (_state == PlayerState.Dead)
        {
            DisableAllAnimation();
            ani.SetBool("isDead", true);
        }
    }

    private void DisableAllAnimation()
    {
        ani.SetBool("isIdle", false);
        ani.SetBool("isRun", false);
        ani.SetBool("isInAir", false);
        ani.SetBool("isDead", false);
    }

    public void SetPlayerDie()
    {
        playerDead = true;
        pausePlayer = true;
        rb.simulated = false;
        Invoke(nameof(GoToEndGameScene), 3);
    }

    public void SetPausePlayer(bool pause)
    {
        if (pause)
        {
            pausePlayer = true;
        }
        else
        {
            pausePlayer = false;
        }
    }

    public void UpdateStatusChange()
    {
        this.moveSpeed = defaultMoveSpeed * ((ps._moveSpeedMultiply + 100) / 100);
    }

    private void GoToEndGameScene()     
    {
        sm.GoToGameOverScene();
    }
}
