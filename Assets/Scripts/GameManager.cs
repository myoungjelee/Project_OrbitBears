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

    public bool isGameOver { get; private set; }

    private bool isFullRanking;

    private void Awake()
    {
        ResetGame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameOver();
        }

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            ScoreManager.Instance.AddScore(100);
        }

        if (Input.GetKeyDown(KeyCode.RightAlt))
        {
            PlayerPrefs.DeleteKey("highscoreTable");
        }
    }

    public void OnClick_RetryButton()
    {
        StartCoroutine(RetryCoRoutine());
    }

    IEnumerator RetryCoRoutine()
    {
        yield return new WaitForSeconds(0.2f);

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
        // ����Ƽ �����Ϳ��� ���� ���� ���
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ���� �Ϸ��� �������Ͽ��� ���� ���� ���
        Application.Quit();
#endif
    }

    public void OnClick_CancleButton()
    {
        // ���� �簳
        Time.timeScale = 1f;

        // ���� �ȳ��� ���Y��ȭ
        quitPanel.gameObject.SetActive(false);
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

        // ������ �� ���� ����
        PlayerPrefs.DeleteKey("latestScore");
        PlayerPrefs.DeleteKey("latestName");

        GetRankingListCount();

        yield return new WaitForSeconds(3);

    }

    // ��ŷ ����Ʈ ���� �ľ��ϱ�
    public bool GetRankingListCount()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        if (!string.IsNullOrEmpty(jsonString))
        {
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
            if (highscores.highscoreEntries.Count >= 5)
            {
                return isFullRanking = true;
            }
            else
            {
                return isFullRanking = false;
            }
        }
        return isFullRanking = false;
    }

    // �õŷ�� ���ھ� ���� ��������
    public int GetLastRankingScore()
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
    public void GameOver()
    {
        // ���� ���� ���¸� ������ ����
        isGameOver = true;

        // ���� �Ͻ�����
        Time.timeScale = 0f;

        if (isFullRanking)
        {
            if (ScoreManager.Instance.score > GetLastRankingScore())
            {
                inputNameUI.SetActive(true);
            }
            else
            {
                // ���� ���� UI�� Ȱ��ȭ
                gameOverUI.SetActive(true);
            }
        }
        else
        {
            inputNameUI.SetActive(true);
        }

    }
}
