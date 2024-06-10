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
        SoundManager.Instance.PlayBgmSound();

        // ���� �ε��Ͽ� ������ �����մϴ�.
        SceneManager.LoadScene("Solar_System");
    }
}
