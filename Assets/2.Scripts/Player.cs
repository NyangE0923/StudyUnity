using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed = 12f;
    public float jumpForce = 12f;

    [Header("Dash info")]
    [SerializeField] private float dashCooldown; //�ν����� â���� ������ ��� ��Ÿ��
    private float dashUsageTimer; //Time.DeltaTime���� �ʱ�ȭ ��ų ��� ��Ÿ��
    public float dashSpeed;
    public float dashDuration;
    public float dashDir {  get; private set; }

    [Header("Collision info")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private Transform wallCheck;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    [Header("Wall Jump info")]
    public float xWallJumpForce;
    public float yWallJumpForce;

    public int facingDir { get; private set; } = 1;
    private bool facingRight = true;

    #region Componets
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    #endregion
    #region States
    public PlayerStateMachine StateMachine { get; private set; }
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }

    #endregion
    private void Awake()
    {
        //PlayerState���� ������ ������ Player��� ������Ʈ�� StateMachine�� �ִ� Idle �ִϸ��̼��� �����´�.
        StateMachine = new PlayerStateMachine();
        //idleState�� bool���� Idle�� �ش��Ѵ�.
        idleState = new PlayerIdleState(this, StateMachine, "Idle");
        //moveState�� bool���� Move�� �ش��Ѵ�.
        moveState = new PlayerMoveState(this, StateMachine, "Move");
        dashState = new PlayerDashState(this, StateMachine, "Dash");
        jumpState = new PlayerJumpState(this, StateMachine, "Jump");
        airState  = new PlayerAirState(this, StateMachine, "Jump");
        wallSlide = new PlayerWallSlideState(this, StateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, StateMachine, "Jump");
    }

    private void Start()
    {
        //���� ���������� ���۵ǹǷ� ������Һ��� ��� �ʱ�ȭ �ؾ���!!
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();

        StateMachine.Initialize(idleState);
    }

    private void Update()
    {
        StateMachine.currentState.Update();
        CheckForDashInput();
    }

    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;

        dashUsageTimer -= Time.deltaTime; //��� ��Ÿ���� ��Ÿ Ÿ�Ӹ�ŭ ����.

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0) //��� ��Ÿ���� 0�̸��� �Ǿ��� �� ��ø� ������
        {
            dashUsageTimer = dashCooldown; //�ٽ� �ν�����â���� ���ص� ��Ÿ������ �ǵ�����
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            StateMachine.ChangeState(dashState); //dashState�� �̵�
        }

    }

    //SetVelocity ��� �޼���(float x, y�� ����)�� �����Ѵ�.
    //�̶� rigidbody2D�� velocity�� ���ο� velocity _xVelocity, _yVelocity�� �����Ѵ�.
    public void SetVelocity(float _xVelocity, float _yVelocity)
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);
    }

    //�� Ž��
    public bool IsGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    //�� Ž�� right��� ������ facingDir�� ���� ���� ���� ��� Ž�� ����
    public bool IsWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);

    private void OnDrawGizmos()
    {
        // ���� üũ�� ���� �׸��ϴ�. groundCheck.position�� ���� groundCheck�� ��ġ�Դϴ�.
        // �׸��� ���ο� Vector3�� ����� groundCheck ��ġ���� (x ��ǥ�� groundCheck�� x ��ǥ�� ������,
        // y ��ǥ�� groundCheck�� y ��ǥ���� groundCheckDistance ��ŭ �Ʒ��� �ִ� �������� �����մϴ�).
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));

        // �� üũ�� ���� �׸��ϴ�. wallCheck.position�� ���� wallCheck�� ��ġ�Դϴ�.
        // �׸��� ���ο� Vector3�� ����� wallCheck ��ġ���� (x ��ǥ�� wallCheck�� x ��ǥ����
        // wallCheckDistance ��ŭ �����ʿ� �ִ� ��������, y ��ǥ�� wallCheck�� y ��ǥ�� ���� ������ �����մϴ�).
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));

    }

    public void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }
    
    public void FlipController(float _x)
    {
        //Player�� X�� �̵��� 0���� ũ�鼭 Player�� �������� �ٶ󺸰� ���� �ʴٸ� Flip�Լ��� ����Ѵ�.
        if(_x > 0 && !facingRight)
        {
            Flip();
        }
        //Player�� X�� �̵��� 0���� �����鼭 Player�� �������� �ٶ󺸰� �ִٸ� Flip�Լ��� ����Ѵ�.
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }
}
