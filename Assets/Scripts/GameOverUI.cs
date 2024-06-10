using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    //public ScoreManager scoreManager;
    private TextMeshProUGUI scoreText;
    public GameObject rnakingImage;

    private void OnEnable()
    {
        // "Score Text (TMP)" �̸��� ���� ���� ������Ʈ�� ã�� TextMeshProUGUI ������Ʈ�� �Ҵ�
        scoreText = GameObject.Find("Score Text (TMP)").GetComponent<TextMeshProUGUI>();
        //rankingImage = GameObject.Find("Ranking Image").GetComponent<Image>();
        UpdateScoreText();
    }


    public void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = $"SCORE  :  {ScoreManager.Instance.score}";
        }
        else
        {
            Debug.Log("���ھ��ؽ�Ʈ�� �����ϴ�.");
        }
    }

    public void ReStart()
    {
        // StartCoroutine(ReStartCoRoutine());
        // Ȱ��ȭ���� �� ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator ReStartCoRoutine()
    {
        yield return new WaitForSeconds(0.2f);

        // Ȱ��ȭ���� �� ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        SoundManager.Instance.PlayClickSound();

        // ����Ƽ �����Ϳ��� ���� ���� ���
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        // ���� �Ϸ��� �������Ͽ��� ���� ���� ���
        Application.Quit();

        //StartCoroutine(QuitGameCoRoutine()); 
    }

    IEnumerator QuitGameCoRoutine()
    {
        SoundManager.Instance.PlayClickSound();

        yield return new WaitForSeconds(0.2f);

        // ����Ƽ �����Ϳ��� ���� ���� ���
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ���� �Ϸ��� �������Ͽ��� ���� ���� ���
        Application.Quit();
#endif
    }

    public void Ranking()
    {
        bool isRnak = rnakingImage.activeSelf;
        rnakingImage.SetActive(!isRnak);
    }
}
