using System;
using UnityEngine;

namespace SuperAbilityMario.Character
{
    [Serializable]
    public struct InputMap
    {
        public float X { get => x; set => x = value; }
        public bool Space { get => space; set 
            { 
                if(space == false && value) 
                    lastPressedSpaceTime = Time.time;
                space = value;
            }
        }

        public float LastPressedSpaceTime { get => lastPressedSpaceTime; }

        private float x;
        private bool space;
     
        private float lastPressedSpaceTime;
    }

    public abstract class CharacterInput : MonoBehaviour
    {

        public InputMap InputMap => _inputMap;

        [SerializeField] protected InputMap _inputMap;
    }
}