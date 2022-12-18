using SuperAbilityMario.Character;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CharacterInteraction : MonoBehaviour
{

    [SerializeField] private Character Character;
    [SerializeField] private GameConfig GameConfig;
    [Space]
    [SerializeField] private LevelGrid LevelGrid;
    [SerializeField] private float debugContactPointSize;

    private ContactPoint2D[] contactPoints;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        contactPoints = collision.contacts;

        if(GameConfig.InteractionLayerMask == (GameConfig.InteractionLayerMask | (1 << collision.gameObject.layer)) == false)
        {
            return;
        }
        
        IInteractable interactable = collision.gameObject.GetComponent<IInteractable>();
        if (interactable != null) 
        {
            interactable.Interact(Character, collision.GetContact(0).point);
        }
    }

    private void OnDrawGizmos()
    {
        if (contactPoints == null)
            return;

        foreach(ContactPoint2D contactPoint in contactPoints)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawCube(contactPoint.point, Vector3.one * debugContactPointSize);
        }
    }
}
