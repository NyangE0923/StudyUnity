using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerGroundedState
{
    public PlayerMoveState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    //�θ� Ŭ�����κ��� �޼ҵ带 �������̵�(������)�Ѵ�.
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

        //Player Ŭ������ SetVelocity �޼��忡 �ִ� xInput���� * moveSpeed�� rigidbody2D�� velocity�� y���� ��ӹ޴´�.
        player.SetVelocity(xInput * player.moveSpeed, rb.velocity.y);

        //xInput�� ���� 0�϶� idleState�� ���¸� ��ȯ�Ѵ�.
        if (xInput == 0 || player.IsWallDetected())
            stateMachine.ChangeState(player.idleState);
    }
}
