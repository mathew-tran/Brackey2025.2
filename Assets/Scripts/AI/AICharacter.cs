using System.Runtime.ConstrainedExecution;
using Unity.Burst;
using UnityEngine;
using UnityEngine.Events;

public class AICharacter : MonoBehaviour
{
    public float mSpeed = 10.0f;
    public SpeechBubble mSpeechBubble;

    public bool bIsDead = false;

    public bool bLookAtDog = false;

    public UnityEvent OnGrandmaDied;

    public GameObject mBody;

    public GameObject CharacterSounds;

    public enum STATE
    {
        MOVING,
        TACKLED
    }

    public STATE mCurrentState;
    public float mTackleProgress = 0.0f;
    public float mTackleLength = .2f;
    public Vector3 mTackleDirection = Vector3.zero;

    public float mTackleStrength = 20.0f;
    public void MoveTo(Vector3 position)
    {
        if (mCurrentState == STATE.TACKLED)
        {
            return;
        }

        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, GetTargetPosition(position), mSpeed * Time.deltaTime);
        transform.LookAt(GetTargetPosition(position));
    }

    public void SetLookAtPlayer(bool bLookAt = true)
    {
        bLookAtDog = bLookAt;
    }

    private void FixedUpdate()
    {
      
        if (bLookAtDog)
        {
            Vector3 positionToLookAt = GetTargetPosition(GameObject.Find("Player").gameObject.transform.position);
            Debug.Log(positionToLookAt);
            transform.LookAt(positionToLookAt);
        }

    }

    private void Update()
    {
        if (mCurrentState == STATE.TACKLED)
        {
            transform.position += mTackleDirection * mTackleStrength * Time.deltaTime;
            mTackleProgress += Time.deltaTime;
            if (mTackleProgress > mTackleLength)
            {
                mCurrentState = STATE.MOVING;
            }

        }
    }

    public void LookAtObject(GameObject go)
    {
        bLookAtDog = false;
        Vector3 positionToLookAt = GetTargetPosition(go.transform.position);
        transform.LookAt(positionToLookAt);
    }

    public void Teleport(Vector3 position)
    {
        gameObject.transform.position =  GetTargetPosition(position);
    }
    private Vector3 GetTargetPosition(Vector3 pos)
    {
        return new Vector3(pos.x, transform.position.y, pos.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Car")
        {
            if (bIsDead == false)
            {
                OnGrandmaDied.Invoke();
                bIsDead = true;
                bLookAtDog = false;
                mBody.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
                var dir = (gameObject.transform.position - collision.gameObject.transform.position).normalized;
                mTackleStrength *= 1.5f;
                OnTackled(dir);
            }

        }
    }

    public void OnTackled(Vector3 dir)
    {
        mTackleDirection = dir;
        mTackleDirection.y = 0;
        mCurrentState = STATE.TACKLED;
        mTackleProgress = 0.0f;
        CharacterSounds.GetComponent<CharacterSFX>().PlaySFX(CharacterSFX.SFX_TYPE.GETTING_ANGRY, true);
    }
}
