using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;


public class BackgroundMusic : MonoBehaviour
{
  /* Description --
    This script will manage that background music and will ranomize it
  */ 
  /* Notes --
   *    Maybe make it so that you cant randomly get the same song twice in a row
   */

    public AudioClip[] playList;
    public AudioSource audioSource;
    [Space]
    public bool playingMusic = true;
    [Space]
    public TextMeshProUGUI songName;
    public TextMeshProUGUI artistName;
    public TextMeshProUGUI albumName;
    [Space]
    public SongInfo songInformation;
    public UnityEvent SongChanging;

    public AudioClip getRandomSong ()//this function will return a random song from the playList[];
    {
      int start2 = UnityEngine.Random.Range(0, playList.Length);
      return playList[start2];
    }
    //this function will return a random song from the playList[];

    IEnumerator Start()
    {
        while(playingMusic)
        {
          AudioClip song = getRandomSong();
          audioSource.clip = song;
          audioSource.Play();

            songInformation = new SongInfo();
            songInformation.songName = "Radioactive";
            songInformation.artistName = "ImagineDragons";
            songInformation.albumName = "Continued Silence EP";
            songNotification(songInformation);
            SongChanging.Invoke();

            SongChanging.Invoke();

          yield return new WaitForSeconds(audioSource.clip.length);
        }
    }
    //this IEnumerator will always play the background music, when one song ends it starts up another

    public void songNotification (SongInfo song)
    {
        songName.text = song.songName;
        artistName.text = song.artistName;
        albumName.text = song.albumName;
    }
}
