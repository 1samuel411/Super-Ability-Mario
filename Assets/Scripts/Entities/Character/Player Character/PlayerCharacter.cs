using SuperAbilityMario.Character;

public class PlayerCharacter : Character
{

    public void ApplyPowerUp(PowerUp powerUp)
    {
        SetState(States.PowerUpState);
        if(powerUp is IModifiesPlayer)
        {
            IModifiesPlayer playerModification = powerUp as IModifiesPlayer;
            SetCharacterConfig(playerModification.NewPlayerConfig);
        }
    }
}
