using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    //PlayerStateMachine �ν��Ͻ��� �����ϴ� ����
    //protected = ��� ���� �ڽ� Ŭ������ ��� ����
    protected PlayerStateMachine stateMachine;
    protected float stateTimer;
    //Player �ν��Ͻ��� �����ϴ� ����
    protected Player player;
    protected Rigidbody2D rb;

    [Header("Input")]
    protected float xInput;
    protected float yInput;

    //���� ���¿��� ���� �ִϸ��̼� bool ������ �̸�
    private string animBoolName;


    //PlayerState�� ������
    //�÷��̾�, �÷��̾��� ���� �ӽ�, �ִϸ��̼� bool ������ �̸��� �Ű������� �޴´�.
    public PlayerState(Player _player, PlayerStateMachine _stateMachine, string animBoolName)
    {
        //���� �Ű������� ���� �ʱ�ȭ
        this.player = _player;
        this.stateMachine = _stateMachine;
        this.animBoolName = animBoolName;
    }

    //���¿� �������� �� ȣ��Ǵ� ���� �޼���
    //virtual �ٸ� Ŭ���������� override(������)�� ���� ������ �� �ֵ��� ������ִ� ��
    public virtual void Enter()
    {
        //���¿� ������ �� ����� �ڵ� �ۼ� ����
        //PlayerState�� rigidbody2D�� ���� playerŬ������ rigidbody2D�� ���� ����Ѵ�.(������� getcomponent ����?
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
    }

    //�� �����Ӹ��� ȣ��Ǵ� ���� �޼���
    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;

        //�� �����Ӹ��� ����� �ڵ� �ۼ� ����
        Debug.Log("I'm in " + animBoolName);
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");

        // �÷��̾� ĳ������ ���� �ӵ��� �ִϸ��̼ǿ� �����Ͽ� ���� �ִϸ��̼��� �����մϴ�.
        // player: �÷��̾� ��ü
        // anim: �÷��̾��� �ִϸ����� ������Ʈ
        // rb: �÷��̾��� Rigidbody ������Ʈ
        // "yVelocity": �ִϸ��̼� ���� Ʈ������ ���� �Ķ���� �̸�
        // rb.velocity.y: �÷��̾��� ���� �ӵ�
        player.anim.SetFloat("yVelocity", rb.velocity.y);
    }

    //���¸� �������� �� ȣ��Ǵ� ���� �޼���
    public virtual void Exit()
    {
        //���¸� �������� �� ����� �ڵ� �ۼ� ����
        player.anim.SetBool(animBoolName, false);
    }
}
