using SuperAbilityMario.Character;
using UnityEngine;

public struct WalkState : IState
{

    void IState.Enter(Character character)
    {
    }

    void IState.Loop(Character character, float deltaTime)
    {
        InputMap inputMap = character.CharacterInput.InputMap;
        CharacterConfig characterConfig = character.CharacterConfig;
        bool pressedSpace = Time.time - inputMap.LastPressedSpaceTime <= characterConfig.JumpBufferTime;
        if (pressedSpace && character.IsGrounded)
        {
            character.SetState(States.JumpState);
            return;
        }
        if (Time.time - character.LastGroundedTime <= characterConfig.CoyoteJumpTime && pressedSpace)
        {
            character.SetState(States.JumpState);
            return;
        }
        if (character.JumpCount < characterConfig.MaxJumps && pressedSpace)
        {
            character.SetState(States.JumpState);
            return;
        }
    }

    void IState.FixedLoop(Character character, float deltaTime)
    {
        InputMap inputMap = character.CharacterInput.InputMap;
        CharacterConfig characterConfig = character.CharacterConfig;

        Vector2 velocityTarget = new Vector2(inputMap.X * characterConfig.AccelerationSpeed, 0);

        // Apply base movement force and limit the X Velocity
        character.CharacterMotor.ApplyForce(velocityTarget, ForceMode2D.Force);
        character.CharacterMotor.LimitXVelocity(characterConfig.MoveSpeed);

        // Handle flipping character and checking if we're changing directions 
        if ((velocityTarget.x > 0 && !character.FacingRight) || (velocityTarget.x < 0 && character.FacingRight))
        {
            character.Flip();
        }
        bool changingDirections = (velocityTarget.x > 0 && character.CharacterMotor.Velocity.x < 0) || (velocityTarget.x < 0 && character.CharacterMotor.Velocity.x > 0);

        if (character.IsGrounded)
        {
            // Apply different horizontal drags if we're grounded or changing directions
            if (Mathf.Abs(velocityTarget.x) < 0.4f || changingDirections)
            {
                character.CharacterMotor.ApplyHorizontalDrag(characterConfig.LinearDrag);
            }
            character.ResetJumpCount();
        }
        else
        {
            // When in the air and moving upwards, apply vertical drag if we're not holding space
            if (character.CharacterMotor.Velocity.y > 0 && !inputMap.Space)
            {
                character.CharacterMotor.ApplyVerticalDrag(characterConfig.VerticalDrag);
            }
        }
    }
}
