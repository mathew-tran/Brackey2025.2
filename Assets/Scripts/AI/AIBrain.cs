using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIBrain : MonoBehaviour
{
    [SerializeField]
    private State mCurrentState;

    public AICharacter mAICharacter;
    public string mSceneString;

    public bool bIsActive = true;
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

    public void DestroyBrain()
    {
        Destroy(gameObject);
    }
    public void DeactivateBrain()
    {
        bIsActive = false;
    }

    public void ActivateBrain()
    {
        bIsActive = true;
    }

    private void FixedUpdate()
    {
        if (bIsActive == false)
        {
            return;
        }

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
            if (mSceneString != "")
            {
                if (mSceneString != "GameWin")
                {
                    SceneSwitcher.GetInstance().SwitchToScene(mSceneString);
                }
                else
                {
                    SceneManager.LoadScene(mSceneString);
                }
                    mSceneString = "";
            }

        }

    }
}
