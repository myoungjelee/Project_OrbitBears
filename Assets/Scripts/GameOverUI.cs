using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public GameObject rnakingImage;

    private void Awake()
    {
        // "Score Text (TMP)" �̸��� ���� ���� ������Ʈ�� ã�� TextMeshProUGUI ������Ʈ�� �Ҵ�
        scoreText = GameObject.Find("Score Text (TMP)").GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
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
       StartCoroutine(ReStartCoRoutine());   
    }

    IEnumerator ReStartCoRoutine()
    {
        SoundManager.Instance.PlayClickSound();

        yield return new WaitForSecondsRealtime(0.2f);

        // Ȱ��ȭ���� �� ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        SoundManager.Instance.PlayClickSound();

        StartCoroutine(QuitGameCoRoutine()); 
    }

    IEnumerator QuitGameCoRoutine()
    {     
        yield return new WaitForSecondsRealtime(0.2f);

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
