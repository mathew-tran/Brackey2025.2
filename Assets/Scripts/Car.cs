using Unity.VisualScripting;
using UnityEngine;

public class Car : MonoBehaviour
{
    public GameObject mPointA;
    public GameObject mPointB;
    public GameObject mCarBody;

    private float Progress = 0.0f;
    public float mTimeToComplete = 10.0f;

    void Update()
    {
        Progress += Time.deltaTime * 1/mTimeToComplete;
        if (Progress >= 1.0f)
        {
            Progress = 0.0f;
        }
        mCarBody.transform.position = Vector3.Lerp(mPointA.transform.position, mPointB.transform.position, Progress);
    }
}
