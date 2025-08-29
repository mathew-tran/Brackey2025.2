using UnityEngine;
using UnityEngine.Events;

public class StateActivateObject : State
{
    public UnityEvent OnObjectActivated;

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
        OnObjectActivated.Invoke();
    }
}
