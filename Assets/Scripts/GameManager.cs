using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public enum GAME_STATE
    {
        PLAYING,
        GAME_OVER,
        GAME_WIN,
    }

    public UnityEvent OnPlayerKilled;
    public UnityEvent OnGrandmaKilled;
    public UnityEvent OnGameOver;
    public GAME_STATE mCurrentState = GAME_STATE.PLAYING;

    private void Awake()
    {
        Time.timeScale = 1.0f;
    }
    public void GameOverDog()
    {
        if (mCurrentState == GameManager.GAME_STATE.GAME_OVER)
        {
            
            return;
        }
        OnGameOver.Invoke();
        mCurrentState = GameManager.GAME_STATE.GAME_OVER;
        OnPlayerKilled.Invoke();

    }

    public void GameOverGrandma()
    {
        if (mCurrentState == GameManager.GAME_STATE.GAME_OVER)
        {
      
            return;
        }
        OnGameOver.Invoke();
        mCurrentState = GameManager.GAME_STATE.GAME_OVER;
        OnGrandmaKilled.Invoke();

    }
}
