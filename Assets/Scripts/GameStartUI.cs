using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartUI : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.PlayBgmSound();
    }
    public void PlayGame()
    {
        StartCoroutine(PlayGameCoroutine());
    }

    IEnumerator PlayGameCoroutine()
    {
        SoundManager.Instance.PlayBgmSound();

        SoundManager.Instance.PlayStartClickSound();

        yield return new WaitForSecondsRealtime(1);

        // ���� �ε��Ͽ� ������ �����մϴ�.
        SceneManager.LoadScene("OrbitBears");
    }
}
