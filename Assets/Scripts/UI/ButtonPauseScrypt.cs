using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPauseScrypt : MonoBehaviour
{
    public GameObject back;
    public GameObject imageMenu;
    public GameObject ButtonPauseMenuBack;
    public GameObject Pause;
    public GameObject OnGameMusic;
    private bool OnOffGameMusicMode;
    private Game _GameScrypt;
    private AudioSource audioOnGame;

    public void OpenPauseMenu()
    {
        _GameScrypt.isPause = true;
        Pause.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        _GameScrypt.isPause = false;
        Pause.SetActive(false);
    }
    public void MusicOnOff()
    {
        switch (OnOffGameMusicMode)
        {
            case(true):
                audioOnGame.Play();
                OnOffGameMusicMode = false;
            break;
            case (false):
                OnGameMusic.GetComponent<AudioSource>().Pause();
                OnOffGameMusicMode = true;
            break;
        }
    }

    void Start()
    {
        _GameScrypt = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        audioOnGame = GameObject.FindGameObjectWithTag("OnGameAudioSource").GetComponent<AudioSource>();
    }

}
