using Unity.Burst;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class AICharacter : MonoBehaviour
{
    public float mSpeed = 10.0f;
    public SpeechBubble mSpeechBubble;
    public void MoveTo(Vector3 position)
    {
        gameObject.transform.position += (GetTargetPosition(position) - gameObject.transform.position).normalized * mSpeed * Time.deltaTime;
        transform.LookAt(GetTargetPosition(position));
    }
    public void Teleport(Vector3 position)
    {
        gameObject.transform.position =  GetTargetPosition(position);
    }
    private Vector3 GetTargetPosition(Vector3 pos)
    {
        return new Vector3(pos.x, transform.position.y, pos.z);
    }
}
