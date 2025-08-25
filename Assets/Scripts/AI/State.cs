using UnityEngine;

public abstract class State : MonoBehaviour
{
    public AICharacter mAICharacter;

    public abstract void StartBehaviour();
    public abstract void RunBehaviour();
    public abstract bool IsBehaviourValid();

    public abstract void CleanBehaviour();


    public Vector3 GetAgentPosition()
    {
        return mAICharacter.gameObject.transform.position;
    }
    public float GetAgentSpeed()
    {
        return mAICharacter.mSpeed;
    }

    public AICharacter GetAgent()
    {
        return mAICharacter;
    }
}
