using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float mSpeed = 10.0f;

    [SerializeField]
    private Vector3 mVelocity = Vector3.zero;

    [SerializeField]
    private float mGravity = -9.8f;

    private bool bIsGrounded = false;

    private PlayerInput mPlayerInput;
    private PlayerInput.DogControllerActions mDogControllerActions;


    public UnityEvent OnPlayerDeath;

    public CharacterController mController;
    private Vector3 mDeathPosition = Vector3.zero;

    public bool bIsDead = false;
    void Awake()
    {
        mPlayerInput = new PlayerInput();
        mDogControllerActions = mPlayerInput.DogController;
        mDogControllerActions.Restart.performed += ctx => Restart();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnEnable()
    {
        mDogControllerActions.Enable();
    }

    private void OnDisable()
    {
        mDogControllerActions.Disable();
    }

    private void LateUpdate()
    {
        if (bIsDead)
        {
            transform.position = mDeathPosition;
            return;
        }
        ProcessMove(mDogControllerActions.Move.ReadValue<Vector2>());
    }
    public void ProcessMove(Vector2 input)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
        mController.Move(transform.TransformDirection(moveDirection) * mSpeed * Time.deltaTime);
        mVelocity.y += mGravity * Time.deltaTime;
        if (bIsGrounded && mVelocity.y < 0)
        {
            mVelocity.y = -2f;
        }
        mController.Move(mVelocity * Time.deltaTime);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Car")
        {
            if (bIsDead == false)
            {
                OnPlayerDeath.Invoke();
                bIsDead = true;
                ProcessMove(Vector2.zero);
                mDeathPosition = transform.position;

            }
            
        }
    }
}
