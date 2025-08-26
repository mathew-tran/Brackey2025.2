using UnityEngine;

public class StateChangeSong : State
{
    public Jukebox.SONG_TYPE SongToChangeTo;

    public override void CleanBehaviour()
    {
        
    }

    public override bool IsBehaviourValid()
    {
        return false;
    }

    public override void RunBehaviour()
    {
        
    }

    public override void StartBehaviour()
    {
        GameObject.FindGameObjectWithTag("Jukebox").GetComponent<Jukebox>().PlaySong(SongToChangeTo);
    }
}
