using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public GameObject quitPanel;      // ���� �ȳ� UI
    public void OnClick_RetryButton()
    {
        SoundManager.Instance.PlayClickSound();

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
        SoundManager.Instance.PlayClickSound();

        StartCoroutine(QuitCoRoutine());
    }

    IEnumerator QuitCoRoutine()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        // ���� �ȳ��� Ȱ��ȭ
        quitPanel.gameObject.SetActive(true);

        Time.timeScale = 0f;
    }

    public void OnClick_ConfirmButton()
    {
        SoundManager.Instance.PlayClickSound();

        StartCoroutine(ConfirmCoRoutine());
    }

    IEnumerator ConfirmCoRoutine()
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

    public void OnClick_CancleButton()
    {
        SoundManager.Instance.PlayClickSound();

        StartCoroutine(CancleCoRoutine());
    }

    IEnumerator CancleCoRoutine()
    {
        yield return new WaitForSecondsRealtime(0.2f);

        Time.timeScale = 1.0f;
        // ���� �ȳ��� ���Y��ȭ
        quitPanel.gameObject.SetActive(false);
    }

}
