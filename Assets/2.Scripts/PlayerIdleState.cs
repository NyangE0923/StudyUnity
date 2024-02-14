using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player _player, PlayerStateMachine _stateMachine, string animBoolName) : base(_player, _stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //Idle���·� ���������� ��� �ӵ��� 0���� �����Ѵ�.
        rb.velocity = new Vector2(0, 0);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (xInput == player.facingDir && player.IsWallDetected())
            return;

        //xInput�� ���� 0�� �ƴҶ� moveState�� ���¸� ��ȯ�Ѵ�.
        if(xInput != 0)
            stateMachine.ChangeState(player.moveState);
    }
}
