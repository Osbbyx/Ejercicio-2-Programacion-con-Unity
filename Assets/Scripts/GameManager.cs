using System.Collections;
using UnityEngine;
//using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    public BoxSpawner boxSpawner;
    public bool isGameActive;

    //[SerializeField] // para mostrar en unity siendo privada
    private int score;

    public GameObject panelGameplay;
    public GameObject panelGameOver;
    public TMP_Text textScoreGameOver;
    public TMP_Text textTopScoreGameOver;

    public TMP_Text textScore;

    public Sound gameOverSound;

    public Sound buttonSound;

    public int Score
    {
        get { return score; }
    }

    private void Awake()
    {
        Instance = this;

        isGameActive = true;
    }

    public void SetScore()
    {
        if (isGameActive)
        {
            AddScore();
            UpdateScore();
            CheckGameState();
        }
        
    }

    private void CheckGameState()
    {
       
            StartCoroutine(SpawnBoxCoroutine());
       
    }

    public void GameOver()
    {
        if (isGameActive)
        {
            AudioManage.Instance.PlaySound(gameOverSound);
            isGameActive = false;
            CamaraManager.Instance.ResetCamaraPosition();
            CheckHighScore();
            //StartCoroutine(ResetSceneCoroutine());
            StartCoroutine(ShowGameOverPanelCoroutine());
        }        
    }

    private void CheckHighScore()
    {
        if (score > PlayerPrefs.GetInt("TopScore"))
        {
            PlayerPrefs.SetInt("TopScore", score);
            Debug.Log("Nuevo puntaje maximo: " + PlayerPrefs.GetInt("TopScore"));
        }
    }

    private void AddScore()
    {
        score++;
    } 

    private void UpdateScore()
    {
        textScore.text = score.ToString();
    }

    //private IEnumerator ResetSceneCoroutine()
    //{
    //    yield return new WaitForSeconds(3.0f);
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}

    private IEnumerator SpawnBoxCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        boxSpawner.SpawnBox();
    }

    private IEnumerator ShowGameOverPanelCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        textScoreGameOver.text = score.ToString();
        textTopScoreGameOver.text = PlayerPrefs.GetInt("TopScore").ToString();

        panelGameplay.SetActive(false);
        panelGameOver.SetActive(true);
    }
    //-------------------------------------------------------------------------------------------------------------
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
