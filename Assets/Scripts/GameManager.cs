using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RankingSytem;
using static RankingSytem.RankingSystem;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public GameObject quitPanel;      // ���� �ȳ� UI
    public GameObject gameOverUI;     // ���ӿ��� UI
    public GameObject inputNameUI;    // �̸��Է� UI
    public Image nextPlanetImage;

    public bool isGameOver { get; private set; }
    private int maxPlanetID;          // ������ �༺�� �ִ� ����

    private PlanetData currentPlanetData;
    private PlanetData nextPlanetData;

    public event System.Action<PlanetData> OnReload;
    private void Awake()
    {
        ResetGame();
    }

    public void OnClick_RetryButton()
    {
        // Ȱ��ȭ���� �� ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClick_QuitButton()
    {
        // ���� �Ͻ�����
        Time.timeScale = 0f;

        // ���� �ȳ��� Ȱ��ȭ
        quitPanel.gameObject.SetActive(true);
    }
    public void OnClick_ConfirmButton()
    {
        // ���� �Ϸ��� �������Ͽ��� ���� ���� ���
        Application.Quit();

        // ����Ƽ �����Ϳ��� ���� ���� ���
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void OnClick_CancleButton()
    {
        // ���� �簳
        Time.timeScale = 1f;

        // ���� �ȳ��� ���Y��ȭ
        quitPanel.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        // ���� ���� ���¸� ������ ����
        isGameOver = true;

        // ���� �Ͻ�����
        Time.timeScale = 0f;

       

        if(ScoreManager.Instance.score > Get10thScore())
        {
            inputNameUI.SetActive(true);
        }
        else
        {
            // ���� ���� UI�� Ȱ��ȭ
            gameOverUI.SetActive(true);
        }

    }

    public void ResetGame()
    {
        StartCoroutine(StartGame());  
    }

    IEnumerator StartGame()
    {
        // ���ӿ��� UI ��Ȱ��ȭ
        gameOverUI.SetActive(false);

        // ���� UI ��Ȱ��ȭ
        quitPanel.gameObject.SetActive(false);

        // ���� �簳
        Time.timeScale = 1f;

        maxPlanetID = 4;

        yield return new WaitForSeconds(3);

         gameOverUI.SetActive(true);
        //GameOver();
    }

    // ���� �༺ ����
    private PlanetData SelectRandomPlanet()
    {
        int id = UnityEngine.Random.Range(0, maxPlanetID + 1);

        //return PlanetManager.GetPlanetData((uint)id);

        return nextPlanetData;   // ���￹��
    }

    private void UpdatePlanetData()
    {
        currentPlanetData = nextPlanetData;
        nextPlanetData = SelectRandomPlanet();


        if (nextPlanetData.sprite != null)
        {
            nextPlanetImage.sprite = nextPlanetData.sprite;
        }
        else
        {
            nextPlanetImage.sprite = null;
        }

        // nextPlanetImage ������ ����
        nextPlanetImage.rectTransform.sizeDelta = 50 * nextPlanetData.radius * Vector2.one;
    }

    // �༺ ���ε�
    public void ReloadPlanet()
    {
        UpdatePlanetData();
        OnReload?.Invoke(currentPlanetData);
    }

    // 10���� ���ھ� ���� ��������
    public int Get10thScore()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        if (!string.IsNullOrEmpty(jsonString))
        {
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
            if (highscores.highscoreEntries.Count > 0)
            {
                return highscores.highscoreEntries[highscores.highscoreEntries.Count - 1].score;
            }
        }
        return 0; // ��ŷ ���̺��� ����ִ� ��� 0 ��ȯ
    }
}
