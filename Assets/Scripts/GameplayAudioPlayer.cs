using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayAudioPlayer : MonoBehaviour
{
    public Sound[] gameplayMusicSounds;

    void Start()
    {
        AudioManage.Instance.PlaySound(gameplayMusicSounds[Random.Range(0, gameplayMusicSounds.Length)]);
    }

   
}
