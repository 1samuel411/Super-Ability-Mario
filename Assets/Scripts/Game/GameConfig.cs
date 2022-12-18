using UnityEngine;

[CreateAssetMenu(fileName = "Game Config", menuName = "Super Ability Mario/Game Config")]
public class GameConfig : ScriptableObject
{
    public LayerMask GroundedLayerMask { get => _groundedLayerMask; }
    public LayerMask InteractionLayerMask { get => _interactionLayerMask; }

    [SerializeField] private LayerMask _groundedLayerMask;
    [SerializeField] private LayerMask _interactionLayerMask;

}
