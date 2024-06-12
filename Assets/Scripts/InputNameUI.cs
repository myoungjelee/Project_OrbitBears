using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using RankingSytem;
using UnityEngine.UI;


public class InputNameUI : MonoBehaviour
{
    public RankingSystem rankingSystem;
    public GameObject gameOverUI;
    private Image backGround;

    private TMP_InputField inputField;
    private int maxKoreanCharLimit = 5; // ���ϴ� �ѱ� ���� �� ����

    private void Awake()
    {
        inputField = transform.Find("InputField (TMP)").GetComponent<TMP_InputField>();
        backGround = transform.Find("BackGround").GetComponent<Image>();
        inputField.characterLimit = 10; // ���� ���ڼ� �Է�����
        inputField.onValueChanged.AddListener(OnInputValueChanged); // �Է°��� ����� �� ȣ��� �޼���
    }

    // �Է°��� ����� �� ȣ��Ǵ� �޼���
    private void OnInputValueChanged(string text)
    {
        if (GetKoreanCharacterCount(text) > maxKoreanCharLimit)
        {
            inputField.text = RemoveExcessKoreanCharacters(text);
        }
    }

    // ���ڿ����� �ѱ� ���� ���� ���� �޼���
    private int GetKoreanCharacterCount(string text)
    {
        int koreanCharCount = 0;
        foreach (char c in text)
        {
            // ���ڰ� �ѱ����� Ȯ��
            if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
            {
                koreanCharCount++;
            }
        }
        return koreanCharCount;
    }

    // �ʰ��� �ѱ� ���ڸ� �����ϴ� �޼���
    private string RemoveExcessKoreanCharacters(string text)
    {
        int koreanCharCount = 0;
        List<char> validChars = new List<char>();

        foreach (char c in text)
        {
            // ���ڰ� �ѱ����� Ȯ��
            if (char.GetUnicodeCategory(c) == System.Globalization.UnicodeCategory.OtherLetter)
            {
                if (koreanCharCount >= maxKoreanCharLimit)
                {
                    continue;
                }
                koreanCharCount++;
            }
            validChars.Add(c);
        }

        return new string(validChars.ToArray());
    }

    public void OnClick_CheckButton()
    {
        if(!string.IsNullOrEmpty(inputField.text))
        {
            rankingSystem.AddHighscoreEntry(ScoreManager.Instance.score, inputField.text);
            gameObject.SetActive(false);
            gameOverUI.SetActive(true);
        }
        else
        {
            StartCoroutine(ChangeColor());
        }
    }

    IEnumerator ChangeColor()
    {
        backGround.color = HexColor("#B3120F");

        yield return new WaitForSecondsRealtime(0.1f);

        backGround.color = Color.white;

        yield return new WaitForSecondsRealtime(0.1f);

        backGround.color = HexColor("#B3120F");

        yield return new WaitForSecondsRealtime(0.1f);

        backGround.color = Color.white;
    }

    public void OnClick_XButton()
    {
        gameObject.SetActive(false);
        gameOverUI.SetActive(true);
    }

    // ��簪 �÷� ��ȯ( �ڵ� ���� : RGBA )
    public static Color HexColor(string hexCode)
    {
        Color color;
        if (ColorUtility.TryParseHtmlString(hexCode, out color))
        {
            return color;
        }

        Debug.LogError("[UnityExtension::HexColor]invalid hex code - " + hexCode);
        return Color.white;
    }
}
