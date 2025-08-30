using UnityEngine;

public class CameraMoveOnDead : MonoBehaviour
{

    private bool bMoveOnDead = false;
    public GameObject mObjectToFocus;


    public void FocusObject(GameObject go)
    {
        mObjectToFocus = go;
        bMoveOnDead = true;
        transform.SetParent(gameObject.transform, true);
    }

    public void FixedUpdate()
    {
        if (bMoveOnDead == false)
        {
            return;
        }

        Vector3 positionToMoveTo = mObjectToFocus.transform.position;
        positionToMoveTo.y = transform.position.y;
        positionToMoveTo.z -= 5;

        transform.position = Vector3.MoveTowards(transform.position, positionToMoveTo, 2 * Time.deltaTime);
        if (Vector3.Distance(transform.position, positionToMoveTo) <= .1f)
        {

            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(-90, 0, 0), .01f);
            
           
            
        }
        
    }
}
