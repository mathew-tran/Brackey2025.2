using System.Collections;
using System.Threading.Tasks;
using UnityEngine;





public class Jukebox : MonoBehaviour
{
    public enum SONG_TYPE
    {
        CHEERY,
        TENSE,
        DEATH
    }

    public AudioSource mCheerySource;
    public AudioSource mTenseSource;
    public AudioSource mDeathSource;

    public void PlayerKilled()
    {
        PlaySong(SONG_TYPE.DEATH);
    }
    public async Task PlaySong(SONG_TYPE song)
    {
        await StartFade(mCheerySource, .1f, 0);
        await StartFade(mTenseSource, .1f, 0);
        await StartFade(mDeathSource, .1f, 0);

        mCheerySource.Pause();
        mTenseSource.Pause();
        mDeathSource.Pause();

        switch (song)
        {
            case SONG_TYPE.CHEERY:
                ResumeSong(mCheerySource);
                break;
            case SONG_TYPE.TENSE:
                ResumeSong(mTenseSource);
                break;
            case SONG_TYPE.DEATH:
                ResumeSong(mDeathSource);
                break;
        }
    }

    private void ResumeSong(AudioSource source)
    {
        if (source.isPlaying == false)
        {
            source.Play();
        }
        else
        {
            source.UnPause();
        }
        using var _ = StartFade(source, 1.0f, 100);
    }

    public async Task StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            await Task.Yield();
        }
    }

}
