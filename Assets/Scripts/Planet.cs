using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

[System.Serializable]
public class PlanetData
{
    public string name;
    public int id;
    public Sprite sprite;
    public Color color = Color.white;
    public float radius;
    public float mass;
    public int mergeScore;
    public Sprite nextSizeSprite;
}

public class Planet : MonoBehaviour
{
    private PlanetData data;
    private HashSet<GameObject> contactedObjects = new HashSet<GameObject>(); //�ߺ��� ������� �ʴ�

    public bool outGravityField = false;
    public bool isMerge = false;
    public bool touchPlanet = false;


    public void SetData(PlanetData newData)
    {
        data = newData;
        if (newData.sprite != null)
        {
            GetComponent<SpriteRenderer>().sprite = data.sprite;

        }
        else
        {
            GetComponent<Rigidbody2D>().mass = data.mass;
        }
        transform.localScale = new Vector3(data.radius * 2.5f, data.radius * 2.5f, data.radius * 2.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            Planet otherPlanet = collision.gameObject.GetComponent<Planet>();

            touchPlanet = true;

            if (otherPlanet.data == data)   //
            {
                PlanetData nextPlanetData = PlanetManager.Instance.NextPlanetIndex(data.id);
                otherPlanet.SetData(nextPlanetData);
                ScoreManager.Instance.AddScore(data.mergeScore);
                SoundManager.Instance.PlayMergeSound();

                isMerge = true;
                otherPlanet.isMerge = true;         //�������� �� �༺ ���� ��ȯ

                Destroy(gameObject);
                return;
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // GravityField�� ���� ���� Ȯ��
        if (collision.gameObject.CompareTag("GravityField"))      //�༺�� ����
        {
            // ���˵� ��ü�� �̹� �ִ��� Ȯ��
            if (!contactedObjects.Contains(collision.gameObject))   //HashSet
            {
                // ���˵� ��ü �߰�
                contactedObjects.Add(collision.gameObject);

                // Ư�� �Լ� ȣ��
                PlanetManager.Instance.AfterShootPlanet();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)  //�༺�� ������ ����
    {
        if (collision.gameObject.CompareTag("GravityField"))
        {
            Debug.Log("Exit");
            outGravityField = true;
            CheckGameOver();
        }
    }

    private void CheckGameOver()
    {
        Debug.Log("gameOver");
        // �༺�� �������� �ʾҰ�, �߷����� ������ �� ���� ����
        if (!isMerge && outGravityField && touchPlanet)
        {
            GameManager.Instance.GameOver();
        }
    }

}