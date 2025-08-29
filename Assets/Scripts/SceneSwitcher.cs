using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public Animator mAnimator;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
       
    }
    private void Start()
    {
        SwitchToScene("GameStart");
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
