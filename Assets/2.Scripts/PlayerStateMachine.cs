using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine
{
    //������Ƽ public(����) PlayerState(Ŭ������)�� currentState(������)���� �б�� ���⸦ �־��ִ� ��
    //�ش� ������Ƽ�� public���� �Ǿ��ֱ� ������ ���Ⱑ ������ set �տ� private�� �ο��ϴ� ��
    public PlayerState currentState {  get; private set; }
    //�ʱ�ȭ �޼��� : ���� ���¸� �����ϰ� �ش� ���·� ����
    public void Initialize(PlayerState _startState)
    {
        //������ ������ PlayerState�� _startState�� �ʱⰪ set
        currentState = _startState;
        //_startState�� ����
        currentState.Enter();
    }

    //���� ���� �޼��� : ���ο� ���·� ��ȯ
    public void ChangeState(PlayerState _newState)
    {
        //���� ���¿��� ��������
        currentState.Exit();
        //���ο� ���·� ����
        currentState = _newState;
        //���ο� ���¿� ����
        currentState.Enter();
    }
}
