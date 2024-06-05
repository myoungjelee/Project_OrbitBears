using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject quitPanel;      // ���� �ȳ� UI
    public GameObject gameOverUI;
    public Image nextPlanetImage;

    public bool isGameOver { get; private set; }
    private int maxPlanetID;          // ������ �༺�� �ִ� ����

    //private PlanetData currentPlanetData;
    //private PlanetData nextPlanetData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        ResetGame();
    }

    private void Start()
    {
        maxPlanetID = 4;
    }

    public void OnClick_RetryButton()
    {
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
        // ���� �Ϸ��� �������Ͽ��� ���� ���� ���
        Application.Quit();

        // ����Ƽ �����Ϳ��� ���� ���� ���
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void OnClick_CancleButton()
    {
        // ���� �簳
        Time.timeScale = 1f;

        // ���� �ȳ��� ���Y��ȭ
        quitPanel.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        // ���� ���� ���¸� ������ ����
        isGameOver = true;

        // ���� �Ͻ�����
        Time.timeScale = 0f;

        // ���� ���� UI�� Ȱ��ȭ
        gameOverUI.SetActive(true);
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

        yield return new WaitForSeconds(3);
    }

    //// ���� �༺ ����
    //private PlanetData SelectRandomPlanet()
    //{
    //    int id = UnityEngine.Random.Range(0, maxPlanetID + 1);

    //    return PlanetManager.GetPlanetData(id);
    //}

    //private void UpdatePlanetData()
    //{
    //    currentPlanetData = nextPlanetData;
    //    nextPlanetData = SelectRandomPlanet();


    //    if (nextPlanetData.sprite != null)
    //    {
    //        nextPlanetImage.sprite = nextPlanetData.sprite;
    //    }
    //    else
    //    {
    //        nextPlanetImage.sprite = null;
    //    }

    //    // nextPlanetImage ������ ����
    //    nextPlanetImage.rectTransform.sizeDelta = 50 * nextPlanetData.radius * Vector2.one;
    //}

    //// �༺ ���ε�
    //public void ReloadPlanet()
    //{
    //    UpdatePlanetData();
    //}
}
