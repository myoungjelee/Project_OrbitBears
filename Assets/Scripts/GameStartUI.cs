using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartUI : MonoBehaviour
{
    private TextMeshProUGUI playButton;
    public GameObject PlayUI;

    public void OnEnable()
    {
        // "Score Text (TMP)" �̸��� ���� ���� ������Ʈ�� ã�� TextMeshProUGUI ������Ʈ�� �Ҵ�
        playButton = GameObject.Find("Play Button").GetComponent<TextMeshProUGUI>();
        
    }

    public void PlayGame()
    {
        // ���� �ε��Ͽ� ������ �����մϴ�.
        SceneManager.LoadScene("Test_MJ");
    }


    public void Play()
    {
        PlayUI.SetActive(true);
    }

}
