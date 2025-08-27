using UnityEngine;

public class StateMakeNoise : State
{
    public CharacterSFX.SFX_TYPE mNoiseToMake;
    public override void CleanBehaviour()
    {
        
    }

    public override bool IsBehaviourValid()
    {
        return false;
    }

    public override void RunBehaviour()
    {
       
    }

    public override void StartBehaviour()
    {
        GetCharacterSFX().PlaySFX(mNoiseToMake);
       
    }

}
