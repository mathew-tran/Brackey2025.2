using UnityEngine;

public class StateLookAtDog : State
{
    public bool bLookAtDog = false;

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
        GetAgent().bLookAtDog = bLookAtDog;
    }
}
