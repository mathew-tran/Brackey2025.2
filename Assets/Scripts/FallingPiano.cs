using System.Collections;
using UnityEngine;

public class FallingPiano : MonoBehaviour
{
    public GameObject mShadowObject;
    public float DistanceToCheck = 50.0f;
    public LayerMask mInteractableLayers;
    public AudioSource mAudioSource;
    public AudioClip mCrashClip;

    public enum PIANO_STATE { 
        FALLING,
        FELL,
        SET
    }

    public PIANO_STATE mState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        mState = PIANO_STATE.FALLING;   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = Vector3.down * DistanceToCheck;

        RaycastHit hit;

        Debug.DrawRay(rayOrigin, rayDirection);
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, DistanceToCheck, mInteractableLayers))
        {
            mShadowObject.transform.position = hit.point + new Vector3(0, .1f, 0);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (mState == PIANO_STATE.FALLING)
        {
            mAudioSource.clip = mCrashClip;
            mAudioSource.Play();
            mShadowObject.SetActive(false);
            mState = PIANO_STATE.FELL;
            StartCoroutine(SetObject());
        }
        
    }

    private IEnumerator SetObject()
    {
        yield return new WaitForSeconds(.3f);
        mState = PIANO_STATE.SET;
        tag = "Untagged";

    }
}
