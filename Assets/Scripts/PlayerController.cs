using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
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

    private Vector2 mTackleDirection = Vector2.zero;

    public float TackleProgress = 0;
    public float TackleLength = .1f;
    public float TackleSpeedModifier = 1.5f;

    public UnityEvent OnTackleStart;
    public UnityEvent onTackleEnd;

    private Material mNormalMaterial;

    public CharacterSFX mCharacterSFX;
    public Material mTackleMaterial;
    public GameObject mBody;
    public Animator mAnimator;

    public Vector3 mHitDirection = Vector3.zero;
    private float HitProgress = 0.0f;
    public enum CHARACTER_STATE
    {
        MOVING,
        TACKLE,
        HIT,
        DEAD,
    }

    public CHARACTER_STATE mCurrentState;
    void Awake()
    {
        mPlayerInput = new PlayerInput();
        mDogControllerActions = mPlayerInput.DogController;
        mDogControllerActions.Restart.performed += ctx => Restart();
        mDogControllerActions.Tackle.performed += ctx => Tackle();
        mNormalMaterial = GetComponent<MeshRenderer>().material;
    }

    public void Tackle()
    {
        if (IsHit())
        {
            return;
        }
        if (IsDead())
        {
            return;
        }
        if (IsTackling())
        {
            return;
        }
        mTackleDirection = mDogControllerActions.Move.ReadValue<Vector2>();
        if (mTackleDirection == Vector2.zero)
        {
            return;
        }

        mCharacterSFX.PlaySFX(CharacterSFX.SFX_TYPE.GETTING_EXCITED);
        GetComponent<MeshRenderer>().material = mTackleMaterial;
        mCurrentState = CHARACTER_STATE.TACKLE;
        OnTackleStart.Invoke();
        TackleProgress = 0.0f;
       
    }

    public bool IsHit()
    {
        return mCurrentState == CHARACTER_STATE.HIT;
    }
    public bool IsDead()
    {
        return mCurrentState == CHARACTER_STATE.DEAD;
    }
    public bool IsTackling()
    {
        return mCurrentState == CHARACTER_STATE.TACKLE;
    }

    public bool IsMoving()
    {
        return mCurrentState == CHARACTER_STATE.MOVING;
    }

    public void Restart()
    {
        SceneSwitcher.GetInstance().SwitchToScene("Scene1");
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
        if (IsDead())
        {
            transform.position = mDeathPosition;
            return;
        }
        if (IsHit())
        {
            var hitDir = new Vector2(mHitDirection.x, mHitDirection.z);
            ProcessMove(hitDir, mSpeed * 3);
            HitProgress += Time.deltaTime;
            if (HitProgress > .2f)
            {
                SetPlayerToIncapacitated();
                return;
            }
        }
        if (IsMoving())
        {
            ProcessMove(mDogControllerActions.Move.ReadValue<Vector2>(), mSpeed);
        }
        if (IsTackling())
        {
            ProcessMove(mTackleDirection, mSpeed * TackleSpeedModifier);
            TackleProgress += Time.deltaTime;
            if (TackleProgress > TackleLength)
            {
                mCurrentState = CHARACTER_STATE.MOVING;
                onTackleEnd.Invoke();
                GetComponent<MeshRenderer>().material = mNormalMaterial;
            }
        }
       
    }
    public void ProcessMove(Vector2 input, float speed)
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;

        if (input == Vector2.zero)
        {
            mAnimator.SetBool("bIsMoving", false);
            
        }
        else
        {
            mAnimator.SetBool("bIsMoving", true);
            mBody.transform.rotation = Quaternion.LookRotation(moveDirection);
        }
        
        mController.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        
        mVelocity.y += mGravity * Time.deltaTime;
        if (bIsGrounded && mVelocity.y < 0)
        {
            mVelocity.y = -2f;
        }
        mController.Move(mVelocity * Time.deltaTime);

    }


    public void SetPlayerToIncapacitated()
    {
        if (IsDead() == false)
        {
            mCurrentState = CHARACTER_STATE.DEAD;
            mAnimator.SetBool("bIsDead", true);
            OnPlayerDeath.Invoke();
            mBody.transform.rotation = Quaternion.LookRotation(Vector3.up);
            ProcessMove(Vector2.zero, 0);
            mDeathPosition = transform.position;
            GetComponent<MeshRenderer>().material = mNormalMaterial;

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Car" && IsHit() == false)
        {

            if (IsDead())
            {
                return;
            }
            
            mCurrentState = CHARACTER_STATE.HIT;
            mCharacterSFX.PlaySFX(CharacterSFX.SFX_TYPE.GETTING_HIT_BY_CAR);
           
            var particle = Resources.Load<GameObject>("HitParticle");
            var dir = (gameObject.transform.position - collision.gameObject.transform.position).normalized;
            mHitDirection = dir;
            Instantiate(particle, transform.position, Quaternion.LookRotation(dir));

        }
    }
}
