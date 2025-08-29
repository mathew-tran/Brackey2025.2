using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    public Button mButton;
    public AudioSource mAudioSource;
    public void RestartGame()
    {
        mButton.enabled = false;
        mAudioSource.Play();
        StartCoroutine(WaitForSoundToFinish());
    }

    public IEnumerator WaitForSoundToFinish()
    {
        yield return new WaitUntil(() => mAudioSource.isPlaying == false);
        SceneSwitcher.GetInstance().SwitchToScene("Scene1");
    }
}
