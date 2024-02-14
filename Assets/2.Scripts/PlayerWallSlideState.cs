using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }
    
    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //Input.GetKeyDown �����̽��ٸ� ������ PlayerWallJumpState�� �̵��ϰ� ��ȯ�Ѵ�.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJump);
            return;
        }

        //�ٶ󺸴� ����� �ٸ� �������� �Է��� �߻��ϸ� idle ���·� �̵� 
        if (xInput != 0 && player.facingDir != xInput)
            stateMachine.ChangeState(player.idleState);

        //yInput = Vertical(Y���� �̵�)�� �̸����� �Էµɰ�� �� �Ʒ��� �̵��ϰ��� �Ѵٸ� �⺻������ ����������
        //�װ� �ƴ϶��(�Է����� �ʴ´ٸ�) 70%�� �ӵ��� ��������.
        if(yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);
        else
            rb.velocity = new Vector2(0, rb.velocity.y * 0.7f);


        if (player.IsGroundDetected() || !player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
