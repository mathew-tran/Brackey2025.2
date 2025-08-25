using UnityEngine;

public class StateTalk : State
{
    public string mMessageToSay;
    public float TimeAfterSpeaking = 3.0f;
    private float Progress = 0.0f;

    public override void CleanBehaviour()
    {
        GetSpeechBubble().StopTalking();
    }

    public override bool IsBehaviourValid()
    {
        return Progress < TimeAfterSpeaking;
    }

    public override void RunBehaviour()
    {
        Progress += Time.deltaTime;
    }

    public override void StartBehaviour()
    {
        GetSpeechBubble().Talk(mMessageToSay);
    }
}

