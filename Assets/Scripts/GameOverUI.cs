using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    //public ScoreManager scoreManager;
    private TextMeshProUGUI scoreText;
    public TextMeshPro BestText;
    public bool isGameOver = false;
    public GameObject gameOver;
    public GameObject rankingUI;

    private void OnEnable()
    {
        // "Score Text (TMP)" �̸��� ���� ���� ������Ʈ�� ã�� TextMeshProUGUI ������Ʈ�� �Ҵ�
        scoreText = GameObject.Find("Score Text (TMP)").GetComponent<TextMeshProUGUI>();
        UpdateScoreText();
    }


    public void UpdateScoreText()
    {

    }

   

    public void ReStart()
    {
        //gameObject.SetActive(false);
        // ���� ���� ���� ���� �ٽ� �ҷ����� �ڵ�.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OUT()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void Ranking()
    {
        gameObject.SetActive(false); //�������� UI ����
        rankingUI.SetActive(true);   //��ŷ UI  �ѱ�
    }
}
