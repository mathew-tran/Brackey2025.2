using System.Collections;
using System.Linq;
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
        mText.text = "";
        gameObject.SetActive(true);
        StartCoroutine(AnimateText(message));
    }

    private IEnumerator AnimateText(string message)
    {
        mText.maxVisibleCharacters = 0;
        mText.text = message;
        for(int i = 0; i < message.Length; ++i)
        {
            yield return new WaitForSeconds(.01f);
            mText.maxVisibleCharacters += 1;
        }
        
    }

    public void StopTalking()
    {
        mText.text = "";
        gameObject.SetActive(false);
    }
}
