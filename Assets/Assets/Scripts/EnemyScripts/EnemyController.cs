using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    public BehaviourManager behaviourManager;
    public Animator animator;
    public Rigidbody2D rb;
    private GroundCheckScrips groundCheckScript;
    [SerializeField] private GameObject feet;
    [SerializeField] private GameObject detectionBox;
    [SerializeField]private Transform hpBarTransform;
    public BoxCollider2D hitBoxTrigger;
    public InstalBarRenderer iBarRenderer;
    private EnemyStateHandler stateHandler;

    [SerializeField] private GameObject player;
    [SerializeField] public AdvancedPlayerController playerController;

    #region BehaviourVars
    public bool playerDetected;
    public float distanceToPlayer;
    public int playerPosition;
    #endregion

    #region sfmVars
    public float actionVelocity; // Velocity due attack, dash and smt...
    public float actionVelocityY;
    public bool stuned;
    public bool isAttacking;
    public bool canAttack = false;

    #endregion 

    #region Timers
    public float stunlockDuration;
    public float attackDuration;
    public float KnokOutDuration;
    public float KnokDownDuration;
    public float GetUpDuration;
    #endregion


    #region Attack Buffer
    [System.NonSerialized] public Queue<int> attackIndexQueue = new Queue<int>();
    public float queueCount;
    public int queueFirsElement;
    public int attackIndex;
    #endregion

    #region GameVars
    private int health = 15;
    private int maxHealth = 15;
    private int balance = 4;
    #endregion


    #region Misc
    [SerializeField] Transform hiTransform;
    [SerializeField] private Animator hitImpactAnimator;
    public int facingDirection;
    public bool grounded;
    public float runSpeed;
    #endregion
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheckScript = feet.GetComponent<GroundCheckScrips>();
        animator = GetComponent<Animator>();
        stateHandler = new EnemyStateHandler();
        InitializeStates();
    }

    void Update()
    {
        grounded = groundCheckScript._Grounded;
        stateHandler.StateUpdate();
        AttackDequeue();
        queueCount = attackIndexQueue.Count;

        UpdateAnimations();
        if (health == 0)
        {
            Destroy(gameObject);
        }

        if (balance ==0)
        {
            balance = 2;
        }
    }

    void FixedUpdate()
    {
        stateHandler.FixedStateUpdate();

    }

    #region UnityMisc
    private void UpdateAnimations()
    {
        animator.SetInteger("AttackIndex", queueFirsElement);
        animator.SetBool("Stuned", stuned);
    }
    private void InitializeStates()
    {
        stateHandler.AddState(new EnemyRun(stateHandler, this));
        stateHandler.AddState(new EnemyIdle(stateHandler, this));
        stateHandler.AddState(new RevenantAttack1(stateHandler, this));
        stateHandler.AddState(new EnemyStunLock(stateHandler, this));
        stateHandler.AddState(new EnemySkyKnock(stateHandler, this));
        stateHandler.AddState(new EnemyKnockDown(stateHandler, this));
        stateHandler.SetState<EnemyIdle>();
    }
    #endregion

    #region AttackBufferMethods
    public void AttackQueueEnter()
    {
        attackIndexQueue.Enqueue(attackIndex);
        attackIndexQueue.TryPeek(out queueFirsElement);
        canAttack = false;
    }
    public void AttackDequeue()
    {
        if (attackDuration <= 0 && queueCount > 0)
        {
            attackIndexQueue?.Dequeue();
            attackIndexQueue.TryPeek(out queueFirsElement);
        }
    }
    #endregion

    #region fsmMethods
    public void SetNeutralState()
    {

    }
    public void SetStunLockState()
    {
        stateHandler.SetState<EnemyIdle>();
        stateHandler.SetState<EnemyStunLock>();
    }
    public void SetKnockOutState()
    {
        stateHandler.SetState<EnemyIdle>();
        stateHandler.SetState<EnemySkyKnock>();
    }
    public void SetKnockDownState()
    {
        stateHandler.SetState<EnemyIdle>();
        stateHandler.SetState<EnemyKnockDown>();
    }
    public void Run()
    {
        rb.linearVelocityX = runSpeed * facingDirection;
    }
    public void PerformStunlockAction()
    {
        rb.linearVelocityX = facingDirection * actionVelocity;
        stunlockDuration -= Time.fixedDeltaTime;
    }
    public void PerformKnokOutAction()
    {
        rb.linearVelocityY = actionVelocityY;
        rb.linearVelocityX = facingDirection * actionVelocity;
        KnokOutDuration -= Time.fixedDeltaTime;
    }
    public void PerformAttackAction()
    {
        rb.linearVelocityX = facingDirection * actionVelocity;
        attackDuration -= Time.fixedDeltaTime;
        if (attackDuration <= 0) canAttack = false;
    }
    #endregion
    public void AddInList()
    {
        behaviourManager.addToList(this);
    }

    public void RemoveFromList()
    {
        behaviourManager.removeFromList(this);
    }

    public void SetFacigDirection()
    {
        if(playerDetected)
        {
            if(player.transform.position.x<transform.position.x)
            {
                facingDirection = -1;
                transform.localScale = new Vector3(1, 1, 1);
            }
            if(player.transform.position.x > transform.position.x)
            {
                facingDirection = 1;
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }
    public void CalulateDistanceToPlayer()
    {
        if (facingDirection == -1)
        {
            distanceToPlayer = math.abs(player.transform.position.x-transform.position.x);
        }
        else if (facingDirection == 1)
        {
            distanceToPlayer = math.abs(transform.position.x - player.transform.position.x);
        }
    }
    public void UpdateHpBar(float currentHp, float maxHp)
    {
        Vector3 scale = hpBarTransform.localScale;
        scale.x = currentHp/maxHp;
        
        hpBarTransform.localScale = scale;
    }
    void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.layer == LayerMask.NameToLayer("PlayerHitBox"))
        {
            hiTransform.localScale = new Vector3(playerController.facingDirection, 1, 1);
            hiTransform.localPosition = transform.position;
            hitImpactAnimator.Play("hit");
            if (health > 0)
            {
                balance -= 1;
                health = health - 1;
                UpdateHpBar(health, maxHealth);
            }
            Debug.Log("Урон получен");
            iBarRenderer.ChangeInstalPoints(0.5f);
            if (playerController.queueFirsElement == 3 || playerController.queueFirsElement == 7)
            {
                SetKnockDownState();
            }
            else if (playerController.queueFirsElement == 4 || !grounded)
            {
                SetKnockOutState();
            }
            else if (balance <= 0)
            {
                SetStunLockState();
            }
        }
        if (player.gameObject.layer == LayerMask.NameToLayer("PlayerGuardBox"))
        {
            Debug.Log("Парирование");
            SetStunLockState();
            iBarRenderer.ChangeInstalPoints(1);
        }
    }
}