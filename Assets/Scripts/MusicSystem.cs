using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSystem : MonoBehaviour
{
    public AudioClip aAccomp;
    public AudioClip aFull;
    public AudioClip bAccomp;
    public AudioClip bFull;
    public AudioClip cFull;
    public AudioClip completeSong;
    public MusicTrack currentTrack = MusicTrack.None;
    public AudioSource source;

    private AudioClip nextClip;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TriggerMusicSwitch(MusicTrack track)
    {
        if (track == currentTrack)
            return; // do nothing

        if (currentTrack == MusicTrack.None)
        {
            switch (track)
            {
                case MusicTrack.None:
                    break;
                case MusicTrack.A_ACOMP:
                    source.clip = aAccomp;
                    currentTrack = MusicTrack.A_ACOMP;
                    source.Play();
                    break;
                case MusicTrack.A_FULL:
                    source.clip = aFull;
                    currentTrack = MusicTrack.A_FULL;
                    source.Play();
                    break;
                case MusicTrack.B_ACOMP:
                    source.clip = bAccomp;
                    currentTrack = MusicTrack.B_ACOMP;
                    source.Play();
                    break;
                case MusicTrack.B_FULL:
                    source.clip = bFull;
                    currentTrack = MusicTrack.B_FULL;
                    source.Play();
                    break;
                case MusicTrack.C_FULL:
                    source.clip = cFull;
                    currentTrack = MusicTrack.C_FULL;
                    source.Play();
                    break;
                case MusicTrack.COMPLETE_SONG:
                    source.clip = completeSong;
                    currentTrack = MusicTrack.COMPLETE_SONG;
                    source.Play();
                    break;
            }
            return;
        }

        float timeLeft = source.clip.length - source.time;

        switch (track)
        {
            case MusicTrack.None:
                break;
            case MusicTrack.A_ACOMP:
                nextClip = aAccomp;
                currentTrack = MusicTrack.A_ACOMP;
                break;
            case MusicTrack.A_FULL:
                nextClip = aFull;
                currentTrack = MusicTrack.A_FULL;
                break;
            case MusicTrack.B_ACOMP:
                nextClip = bAccomp;
                currentTrack = MusicTrack.B_ACOMP;
                break;
            case MusicTrack.B_FULL:
                nextClip = bFull;
                currentTrack = MusicTrack.B_FULL;
                break;
            case MusicTrack.C_FULL:
                nextClip = cFull;
                currentTrack = MusicTrack.C_FULL;
                break;
            case MusicTrack.COMPLETE_SONG:
                nextClip = completeSong;
                currentTrack = MusicTrack.COMPLETE_SONG;
                break;
        }

        IEnumerator coroutine = ScheduleMusicSwitchAtEndOfCurrent(timeLeft);

        StartCoroutine(coroutine);
    }

    private IEnumerator ScheduleMusicSwitchAtEndOfCurrent(float secondsToWait)
    {
        yield return new WaitForSeconds(secondsToWait);
        source.clip = nextClip;
        source.Play();
    }
}

public enum MusicTrack
{
    None,
    A_ACOMP,
    A_FULL,
    B_ACOMP,
    B_FULL,
    C_FULL,
    COMPLETE_SONG
}
