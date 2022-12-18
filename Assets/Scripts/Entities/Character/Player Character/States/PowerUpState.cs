using SuperAbilityMario.Character;
using UnityEngine;

public struct PowerUpState : IState
{

    void IState.Enter(Character character)
    {
        character.EntityMotor.ResetXVelocity();
        character.EntityMotor.ResetYVelocity();
        character.EntityMotor.DisableMotor();
    }

    void IState.Loop(Character character, float deltaTime)
    {
        if(Time.time - character.LastStateChangedTime > 0.5f)
        {
            character.EntityMotor.EnableMotor();
            character.SetState(States.WalkState);
        }
    }

    void IState.FixedLoop(Character character, float deltaTime)
    {
    }
}
