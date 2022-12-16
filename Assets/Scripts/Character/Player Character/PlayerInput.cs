using SuperAbilityMario.Character;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : CharacterInput
{

    void Update()
    {
        _inputMap.X = Input.GetAxis("Horizontal");
        _inputMap.Space = Input.GetKey(KeyCode.Space);
    }

}
