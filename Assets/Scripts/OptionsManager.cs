using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public Sound buttonSound;
    public void ResetTopScore()
    {
        PlayerPrefs.SetInt("TopScore", 0);
        AudioManage.Instance.PlaySound(buttonSound);
    }

    public void ChangeScenceAfterButtonSound(string sceneToLoad)
    {
        AudioManage.Instance.PlaySound(buttonSound);
        StartCoroutine(ChangeScenceAfterButtonSoundCoroutine(sceneToLoad));
    }

    private IEnumerator ChangeScenceAfterButtonSoundCoroutine(string sceneToLoad)
    {
        yield return new WaitForSeconds(buttonSound.clip.length);
        GameSceneManager.Instance.ChangeScene(sceneToLoad);
    }
}
