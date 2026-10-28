using JetBrains.Annotations;
using NUnit.Framework.Constraints;
using NUnit.Framework.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class AdvancedPlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public BoxCollider2D playerCollider;
    public BoxCollider2D guardCollider;
    [SerializeField] private BoxCollider2D playerHitBox;
    [SerializeField] private GameObject feet;
    [SerializeField] private Animator animator;
    private GroundCheckScrips groundCheckScript;
    [SerializeField] private EnemyController enemyController;

    public Transform hpBarTransform;
    private StateHandler stateHandler;


    #region Timers
    public float attackDuration;
    public float blockDuration;
    public float jumpBuffer = 1f;
    public float knokDownDuration;
    public float dashDuration;
    public float stunlockDuration;
    #endregion

    #region State Markers
    public bool isAttacking;
    public bool isBlocking;
    public bool isDashing;
    public bool knokedDown = false;
    public bool stunLock = false;
    public bool grounded;
    public bool isRunning;
    #endregion

    #region Attack vars
    public bool canLaunch;
    public bool canAttack;
    #endregion

    #region Attack Buffer
    [System.NonSerialized] public Queue<int> attackIndexQueue = new Queue<int>();
    public float queueCount;
    public int queueFirsElement;
    public int attackIndex;
    #endregion

    #region Misc
    public float actionVelocity; // Velocity due attack, dash and smt...
    public float facingDirection;
    public float inputAxisX;
    public float runSpeed;

    public int health = 10;
    #endregion

    #region InputMarkers
    public bool jumpWasPressed;
    public bool specialWasPressed;
    public bool attackWasPressed;
    #endregion


    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        groundCheckScript = feet.GetComponent<GroundCheckScrips>();
        animator = GetComponent<Animator>();

        stateHandler = new StateHandler();
        InitializeStates();

        facingDirection = 1f;
    }

    void Update()
    {
        attackWasPressed = Input.GetKeyDown(KeyCode.I);
        jumpWasPressed = Input.GetKeyDown(KeyCode.K);
        specialWasPressed = Input.GetKeyDown(KeyCode.L);
        inputAxisX = Input.GetAxisRaw("Horizontal");

        if (!Input.GetKey(KeyCode.Space))
        {
            FacingCheck();
        }

        grounded = groundCheckScript._Grounded;
        stateHandler.StateUpdate();

        AttackDequeue();

        queueCount = attackIndexQueue.Count;

        UpdateAnimations();
        UpdateHpBar(health,10);

        if (health == 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void FixedUpdate()
    {
        stateHandler.FixedStateUpdate();
    }

    private void UpdateAnimations()
    {
        animator.SetFloat("velocityX", math.abs(rb.linearVelocityX));
        animator.SetBool("onGround", grounded);
        animator.SetBool("isDashing", isDashing);
        animator.SetBool("isAttacking", isAttacking);
        animator.SetInteger("attackIndex", queueFirsElement);
        animator.SetBool("isBlocking", isBlocking);
        animator.SetBool("knokDown", knokedDown);
        animator.SetBool("stunLock", stunLock);
        animator.SetBool("isRuning", isRunning);
    }

    private void InitializeStates()
    {
        stateHandler.AddState(new DashState(stateHandler, this));
        stateHandler.AddState(new IdleState(stateHandler, this));
        stateHandler.AddState(new RunState(stateHandler, this));
        stateHandler.AddState(new JumpState(stateHandler, this));
        stateHandler.AddState(new FallingState(stateHandler, this));
        stateHandler.AddState(new Attack1(stateHandler, this));
        stateHandler.AddState(new Attack2(stateHandler, this));
        stateHandler.AddState(new Attack3(stateHandler, this));
        stateHandler.AddState(new LaunchAttack(stateHandler, this));
        stateHandler.AddState(new AirAttack1(stateHandler, this));
        stateHandler.AddState(new AirAttack2(stateHandler, this));
        stateHandler.AddState(new AirAttack3(stateHandler, this));
        stateHandler.AddState(new BlockState(stateHandler, this));
        stateHandler.AddState(new KnokdownState(stateHandler, this));
        stateHandler.AddState(new StunlockState(stateHandler, this));

        stateHandler.SetState<IdleState>();
    }

    private void FacingCheck()
    {
        if (!knokedDown && !stunLock)
        {
            if (rb.linearVelocityX > 0.1f && inputAxisX > 0.1f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                facingDirection = 1;
                //_isFacingRight = true;
            }
            else if (rb.linearVelocityX < -0.1f && inputAxisX < -0.1f)
            {
                transform.localScale = new Vector3(1, 1, 1);
                facingDirection = -1;
                //_isFacingRight = false;
            }
        }
    }

    public void SetFacingDirection(float fDirection)
    {
        transform.localScale = new Vector3(fDirection, 1, 1);
        facingDirection = fDirection * -1f;
    }
    public void UpdateHpBar(float currentHp, float maxHp)
    {
        Vector3 scale = hpBarTransform.localScale;
        scale.x = currentHp / maxHp * 64.27464f;
        hpBarTransform.localScale = scale;
    }

    public void Run()
    {
        runSpeed = 6f;
        rb.linearVelocityX = runSpeed * inputAxisX;
    }

    public void AttackQueueEnter()
    {
        if (Input.GetKey(KeyCode.Space) && attackWasPressed && queueCount < 2 && grounded && inputAxisX + facingDirection == 0)
        {
            attackIndexQueue.Enqueue(4);
            attackIndexQueue.TryPeek(out queueFirsElement);
            canAttack = false;
        }
        else if (attackWasPressed && queueCount < 2 && canAttack)
        {
            attackIndexQueue.Enqueue(attackIndex);
            attackIndexQueue.TryPeek(out queueFirsElement);
            canAttack = false;
        }

    }

    public void AttackDequeue()
    {
        if (attackDuration <= 0 && queueCount > 0)
        {
            attackIndexQueue?.Dequeue();
            attackIndexQueue.TryPeek(out queueFirsElement);
        }
    }

    #region sfmMethods
    public void SetBlockState()
    {
        if (specialWasPressed && grounded)
        {
            stateHandler.SetState<IdleState>();
            stateHandler.SetState<BlockState>();
        }
    }
    public void SetStunlockState()
    {
        if (grounded)
        {
            stateHandler.SetState<StunlockState>();
            playerHitBox.enabled = false;
        }
    }
    public void SetKnockDownState()
    {
        stateHandler.SetState<KnokdownState>();
        playerCollider.enabled = false;
    }

    public void PerformStunlockAction()
    {
        rb.linearVelocityX = (facingDirection * actionVelocity) * -1f;
        stunlockDuration -= Time.fixedDeltaTime;
    } //Fix facing!

    public void PerformAttackAction()
    {
        rb.linearVelocityX = facingDirection*actionVelocity;
        attackDuration -= Time.fixedDeltaTime;
    }

    public void PerformDashAction()
    {
        rb.linearVelocityX = facingDirection * actionVelocity;
        dashDuration -= Time.fixedDeltaTime;
    } //Fix facing!

    public void SetNeutralState()
    {
        if (queueCount == 0 && inputAxisX == 0 && grounded)
        {
            stateHandler.SetState<IdleState>();
        }
        else if (queueCount == 0 && inputAxisX != 0 && grounded)
        {
            stateHandler.SetState<RunState>();
        }
        else if (queueCount == 0 && !grounded)
        {
            stateHandler.SetState<FallingState>();
        }
        //поправить вызов метода
    }

    public void ClearFields()
    {
        actionVelocity = 0f;
        rb.linearVelocity = Vector2.zero;
        isAttacking = false;
        isBlocking = false;
        isDashing = false;
        knokedDown = false;
        stunLock = false;
        isRunning = false;

        attackIndexQueue.Clear();
        queueCount = 0;
        queueFirsElement = 0;
    } //Use this method carefuly

    public void NeutralDelayTimer()
    {
        jumpBuffer = jumpBuffer - Time.deltaTime;
    }//Jump state timer for NeutralState method activation
    #endregion
}
