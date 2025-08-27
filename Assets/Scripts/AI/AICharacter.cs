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
    public void MoveTo(Vector3 position)
    {
        gameObject.transform.position += (GetTargetPosition(position) - gameObject.transform.position).normalized * mSpeed * Time.deltaTime;
        transform.LookAt(GetTargetPosition(position));
    }

    public void SetLookAtPlayer(bool bLookAt = true)
    {
        bLookAtDog = bLookAt;
    }
    public void LateUpdate()
    {
        if (bLookAtDog)
        {
            Vector3 positionToLookAt = GetTargetPosition(GameObject.Find("Player").gameObject.transform.position);
            transform.LookAt(positionToLookAt);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            if (bIsDead == false)
            {
                OnGrandmaDied.Invoke();
                bIsDead = true;
                mBody.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
            }

        }
    }
}
