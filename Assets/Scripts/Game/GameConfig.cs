using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Config", menuName = "Super Ability Mario/Game Config")]
public class GameConfig : ScriptableObject
{
    public LayerMask LayerMask { get => _layerMask; }
    [SerializeField] private LayerMask _layerMask;

}
