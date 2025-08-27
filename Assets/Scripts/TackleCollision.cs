using UnityEngine;
using UnityEngine.Events;

public class TackleCollision : MonoBehaviour
{
    public UnityEvent<Vector3> OnTackled;
    private void OnTriggerEnter(Collider other)
    {
        PlayerController controller = other.gameObject.GetComponent<PlayerController>();
        if (controller)
        {
            if (controller.IsTackling())
            {
                OnTackled.Invoke((gameObject.transform.position - other.gameObject.transform.position).normalized);
                Debug.Log("Tackled!");
            }
        }
    }
}
