using System;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterSFX : MonoBehaviour
{
    public AudioClip mDying;
    public AudioClip mSighing;
    public AudioClip mGettingAngry;
    public AudioClip mCurious;
    public AudioClip mGettingExcited;
    public AudioClip mGettingHitByCar;

    private AudioSource mSource;

    private void Awake()
    {
        mSource = GetComponent<AudioSource>();
    }
    public enum SFX_TYPE
    {
        DYING,
        SIGHING,
        GETTING_ANGRY,
        CURIOUS,
        GETTING_EXCITED,
        GETTING_HIT_BY_CAR,
    }

    public void PlayOnKilled()
    {
        PlaySFX(SFX_TYPE.DYING);
    }
    public void PlaySFX(SFX_TYPE type, bool mPitchShift = false)
    {
        AudioClip clipToPlay = mDying;
        switch(type)
        {
            case SFX_TYPE.DYING:
                clipToPlay = mDying;
                break;
            case SFX_TYPE.SIGHING:
                clipToPlay = mSighing;
                break;
            case SFX_TYPE.GETTING_ANGRY:
                clipToPlay = mGettingAngry;
                break;
            case SFX_TYPE.CURIOUS:
                clipToPlay = mCurious;
                break;
            case SFX_TYPE.GETTING_EXCITED:
                clipToPlay = mGettingExcited;
                break;
            case SFX_TYPE.GETTING_HIT_BY_CAR:
                clipToPlay = mGettingHitByCar;
                break;

        }
        if (mPitchShift)
        {
            mSource.pitch = UnityEngine.Random.Range(.9f, 1.2f);
        }
        else {
            mSource.pitch = 1;
        }
        mSource.clip = clipToPlay;
        mSource.Play();
        
    }
}
