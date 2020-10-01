using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class SongNotification : MonoBehaviour
{
    public void newSong (SongInfo song)
    {
        UnityEngine.Debug.Log("new song");
    }
}
