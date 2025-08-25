using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    [SerializeField]
    private State mCurrentState;

    public AICharacter mAICharacter;

    private void Awake()
    {
  
    }

    private void Start()
    {
        GetNextState();
    }
    private void GetNextState()
    {
        if (mCurrentState)
        {
            DestroyImmediate(mCurrentState.gameObject);
        }
        mCurrentState = GetComponentInChildren<State>();
        mCurrentState.mAICharacter = mAICharacter;
        mCurrentState.StartBehaviour();
    }
    private void FixedUpdate()
    {
        if (mCurrentState != null)
        {
            if (mCurrentState.IsBehaviourValid())
            {
                mCurrentState.RunBehaviour();
            }
            else
            {
                mCurrentState.CleanBehaviour();
                GetNextState();
            }
           
        }
        else
        {
            Debug.Log("No Behaviours Left for: " + mAICharacter.gameObject.name);
        }

    }
}
