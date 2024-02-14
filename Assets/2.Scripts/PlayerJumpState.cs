using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.velocity = new Vector2(rb.velocity.x, player.jumpForce);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        //���ǹ�if (rigidbody2D�� velocity�� Y���� 0�̸��̶�� PlayerStateMachine���� ��ӹ��� PlayerAirState�� �̵��Ѵ�.
        if (rb.velocity.y < 0)
            stateMachine.ChangeState(player.airState);
    }
}
