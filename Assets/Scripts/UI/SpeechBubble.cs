using TMPro;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{

    public TMP_Text mText;

    private void Awake()
    {
        StopTalking();
    }
    public void Talk(string message)
    {
        mText.text = message;
        gameObject.SetActive(true);
    }

    public void StopTalking()
    {
        mText.text = "";
        gameObject.SetActive(false);
    }
}
