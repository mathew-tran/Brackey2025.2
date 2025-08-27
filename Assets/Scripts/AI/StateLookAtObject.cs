using UnityEngine;

public class StateLookAtObject : State
{
    public GameObject mObjectToLookAt;
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
        GetAgent().LookAtObject(mObjectToLookAt);
    }

}
