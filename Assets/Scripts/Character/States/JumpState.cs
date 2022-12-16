using SuperAbilityMario.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct JumpState : IState
{
    void IState.Enter(Character character)
    {
        character.IncrementJumpCount();
    }

    void IState.Loop(Character character, float deltaTime)
    {
    }
    void IState.FixedLoop(Character character, float deltaTime)
    {
        CharacterConfig characterConfig = character.CharacterConfig;
        if (Time.time >= character.LastStateChangedTime + characterConfig.JumpTimer)
        {
            float jumpForce = character.CharacterMotor.CalculateJumpForce(characterConfig.JumpHeight);

            character.CharacterMotor.ResetYVelocity();
            character.CharacterMotor.ApplyForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        if (Time.time >= character.LastStateChangedTime + characterConfig.JumpTimer + 0.1f)
        {
            character.SetState(States.WalkState);
        }
    }
}
