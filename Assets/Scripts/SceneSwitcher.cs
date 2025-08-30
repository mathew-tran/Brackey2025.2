using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public Animator mAnimator;
    public static SceneSwitcher mInstance;

    public static SceneSwitcher GetInstance()
    {
        if (mInstance == null)
        {
            GameObject obj = Resources.Load<GameObject>("SceneSwitcher");
            Instantiate(obj);
        }
        return mInstance;
    }
    private void Awake()
    {
        if (mInstance)
        {
            Destroy(this.gameObject);
            return;
        }
        mInstance = this;
        DontDestroyOnLoad(this.gameObject);
       
    }
    private void Start()
    {
    }
    public void SwitchToScene(string sceneName)
    {
        Debug.Log("Switch to scene " + sceneName);
        mAnimator.speed = 1;
        mAnimator.SetBool("FadeIn", true);
        StartCoroutine(CompleteTransition(sceneName));
        Debug.Log("Start Fade Transition");


    }

    private IEnumerator CompleteTransition(string nextScene)
    {
        yield return new WaitUntil(() => mAnimator.GetCurrentAnimatorStateInfo(0).IsName("FadeIn") && mAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= .8f);
        Debug.Log("Move to new scene");
        SceneManager.LoadScene(nextScene);
        mAnimator.SetBool("FadeIn", false);
    }
}
