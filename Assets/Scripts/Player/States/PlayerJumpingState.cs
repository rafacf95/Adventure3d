using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.StateMachine;

public class PlayerJumpingState : StateBase
{
    private Player _player;
    public override void OnStateEnter(object o = null)
    {
        base.OnStateEnter(o);
        _player = (Player)o;

    }

    public override void OnStateStay()
    {
        base.OnStateStay();

        if (_player.characterController.isGrounded && _player.inputAxisVertical == 0)
        {
            _player.stateMachine.stateMachine.SwitchState(PlayerStateMachine.PlayerStates.IDLE, _player);
        }
        else if (_player.characterController.isGrounded && _player.inputAxisVertical != 0)
        {
            _player.stateMachine.stateMachine.SwitchState(PlayerStateMachine.PlayerStates.RUNNING, _player);
        }

    }
}
