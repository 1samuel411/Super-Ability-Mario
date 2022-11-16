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

        if (inputMap.space)
        {
            character.SetState(States.JumpState);
            return;
        }

        Vector2 curVelocity = character.CharacterMotor.Velocity;
        Vector2 velocityTarget = Vector2.SmoothDamp(curVelocity, new Vector2(inputMap.x * characterConfig.MoveSpeed, 0), ref curVelocity, characterConfig.AccelerationSpeed * deltaTime);
        character.CharacterMotor.ApplyVelocity(velocityTarget, ForceMode2D.Force);
    }
}
