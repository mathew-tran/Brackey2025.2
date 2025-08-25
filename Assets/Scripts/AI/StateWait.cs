using UnityEngine;

public class StateWait : State
{
    public float WaitTime = 3.0f;
    private float Progress = 0.0f;

    public override void CleanBehaviour()
    {
    }

    public override bool IsBehaviourValid()
    {
        return Progress < WaitTime;
    }

    public override void RunBehaviour()
    {
        Progress += Time.deltaTime;
    }

    public override void StartBehaviour()
    {
        Progress = 0.0f;
    }

}
