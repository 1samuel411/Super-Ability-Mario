using UnityEngine;

public class OneUpMushroom : PowerUp, IModifiesPlayer
{

    public PlayerConfig NewPlayerConfig => newPlayerConfig;

    [SerializeField] private PlayerConfig newPlayerConfig;

    public override void ApplyPowerUp(PlayerCharacter character)
    {
        character.ApplyPowerUp(this);
    }
}
