using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStartUI : MonoBehaviour
{ 
    public void PlayGame()
    {
        // ���� �ε��Ͽ� ������ �����մϴ�.
        SceneManager.LoadScene("Solar_System");
    }
}
