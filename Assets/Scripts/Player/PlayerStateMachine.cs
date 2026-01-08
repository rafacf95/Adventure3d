using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.StateMachine;

public class PlayerStateMachine : MonoBehaviour
{
    public enum PlayerStates
    {
        IDLE,
        RUNNING,
        JUMPING
    }

    public Player player;

    public StateMachine<PlayerStates> stateMachine;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        stateMachine = new StateMachine<PlayerStates>();
        stateMachine.Init();

        stateMachine.RegisterStates(PlayerStates.IDLE, new PlayerIdelState());
        stateMachine.RegisterStates(PlayerStates.RUNNING, new StateBase());
        stateMachine.RegisterStates(PlayerStates.JUMPING, new StateBase());

        stateMachine.SwitchState(PlayerStates.IDLE);
    }
}
