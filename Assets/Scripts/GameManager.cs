using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using RankingSytem;
using static RankingSytem.RankingSystem;
using System.IO;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                return null;              
            }
            return instance;
        }
    }

    public GameObject quitPanel;      // ���� �ȳ� UI
    public GameObject gameOverUI;     // ���ӿ��� UI
    public GameObject inputNameUI;    // �̸��Է� UI

    public bool isGameOver { get; private set; }

    private bool isFullRanking;

    public string filePath;
    string buildPath = Directory.GetParent(Application.dataPath).FullName;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        // ��ŷ���� ���� ���
        #if UNITY_EDITOR
        filePath = Path.Combine(Application.dataPath + "/Editor", "Ranking.json");
        #else
        filePath = Path.Combine(buildPath + "/Rank Data", "Ranking.json");
        #endif

    }

    private void Start()
    {
        SoundManager.Instance.PlayBgmSound();
    }

    private void OnEnable()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        // ���ӿ��� UI ��Ȱ��ȭ
        if(gameOverUI != null)
        {
            gameOverUI.SetActive(false);
        }

        if(quitPanel != null)
        {
            //// ���� UI ��Ȱ��ȭ
            quitPanel.gameObject.SetActive(false);
        }
        

        Time.timeScale = 1.0f;

        //// ������ �� ���� ����
        //PlayerPrefs.DeleteKey("latestScore");
        //PlayerPrefs.DeleteKey("latestName");   
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

            // �ֱ� ���� �� �̸� �ʱ�ȭ
            highscores.latestScore = 0;
            highscores.latestName = string.Empty;

            // ������Ʈ�� JSON ���� ����
            string updatedJson = JsonUtility.ToJson(highscores);
            File.WriteAllText(filePath, updatedJson);
        }

        GetRankingListCount();
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

    // ��ŷ ����Ʈ ���� �ľ��ϱ�
    public bool GetRankingListCount()
    {
        //string jsonString = PlayerPrefs.GetString("highscoreTable");
        //if (!string.IsNullOrEmpty(jsonString))
        //{
        //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        //    if (highscores.highscoreEntries.Count >= 5)
        //    {
        //        return isFullRanking = true;
        //    }
        //    else
        //    {
        //        return isFullRanking = false;
        //    }
        //}
        //return isFullRanking = false;

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(jsonString))
            {
                Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
                return highscores.highscoreEntries.Count >= 5;
            }
        }
        return false;
    }

    // �õŷ�� ���ھ� ���� ��������
    public int GetLastRankingScore()
    {
        //string jsonString = PlayerPrefs.GetString("highscoreTable");
        //if (!string.IsNullOrEmpty(jsonString))
        //{
        //    Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
        //    if (highscores.highscoreEntries.Count > 0)
        //    {
        //        return highscores.highscoreEntries[highscores.highscoreEntries.Count - 1].score;
        //    }
        //}
        //return 0; // ��ŷ ���̺��� ����ִ� ��� 0 ��ȯ

        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            if (!string.IsNullOrEmpty(jsonString))
            {
                Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);
                if (highscores.highscoreEntries.Count > 0)
                {
                    return highscores.highscoreEntries[highscores.highscoreEntries.Count - 1].score;
                }
            }
        }
        return 0; // ��ŷ ���̺��� ����ִ� ��� 0 ��ȯ
    }

    public void GameOver()
    {
        if (isGameOver) return; // �̹� ���ӿ��� ���¶�� �������� ����

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
