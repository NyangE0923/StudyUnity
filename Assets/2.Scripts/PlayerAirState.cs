using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
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
        if (player.IsWallDetected())
            stateMachine.ChangeState(player.wallSlide);

        //���ǹ�if (rigidbody2D�� velocity�� Y���� 0�� ���ٸ� PlayerStateMachine���� ��ӹ��� PlayerIdleState�� �̵��Ѵ�.
        if (player.IsGroundDetected())
            stateMachine.ChangeState(player.idleState);

        //���ǹ�if (xInput �̵� �Է��� 0�� �ƴ� ���)
        if (xInput != 0)
        {
            //player�� SetVelocity�� moveSpeed�� 80%�� �ӵ��� �̵��ǵ��� �Ѵ�.
            player.SetVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
        }
    }
}
