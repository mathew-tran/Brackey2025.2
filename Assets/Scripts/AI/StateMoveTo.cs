using UnityEngine;

public class StateMoveTo : State
{
    public GameObject mEndPosition;

    public override void CleanBehaviour()
    {
        GetAgent().Teleport(mEndPosition.transform.position);
    }

    public override bool IsBehaviourValid()
    {
        return Vector3.Distance(GetAgentPosition(), mEndPosition.transform.position) > 3.0f;
    }

    public override void RunBehaviour()
    {
        GetAgent().MoveTo(mEndPosition.transform.position);
    }

    public override void StartBehaviour()
    {
        Debug.Log("Start Move Behaviour: " + name);
    }
}
