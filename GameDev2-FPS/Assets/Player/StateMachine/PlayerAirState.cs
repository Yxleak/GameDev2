using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerBaseState
{
    public float max_speed = 6;
    public float acceleration = 60;
    public float gravity = 20;

    public override void EnterState(PlayerStateMachine stateMachine)
    {

    }
    public override void ExitState(PlayerStateMachine stateMachine)
    {
        stateMachine.player_velocity.y = -2;
    }
    public override void UpdateState(PlayerStateMachine stateMachine)
    {

    }
    public override void FixedUpdateState(PlayerStateMachine stateMachine)
    {
        //Gravity
        stateMachine.player_velocity.y -= gravity * Time.deltaTime;

        //Set Velocity
        stateMachine.player_velocity = MoveAir(stateMachine.wish_dir, stateMachine.player_velocity);

        //Switch States
        if (stateMachine.character_controller.isGrounded )
        {
            stateMachine.SwitchState(this, stateMachine.ground_state);
        }
    }
    private Vector3 MoveAir(Vector3 wish_dir, Vector3 current_velocity)
    {
        return Accelerate(wish_dir, current_velocity, acceleration, max_speed);
    }
}
