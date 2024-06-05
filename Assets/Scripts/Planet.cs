using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetData
{
    public string name;
    public uint id;
    public Sprite sprite;
    public Color color = Color.white;
    public float radius;
    public float mass;
    public int mergeScore;
}

public class Planet : MonoBehaviour
{
    public float size; //�༺ ũ��
    public float speed; //���ư� �� �༺ �ӵ�
    public GameObject mergedPlanetPrefab;
    private Rigidbody2D rigidbody; //rigidbody��� ����

    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>(); 
        
    }

    void OnCollisionEnter2D (Collision2D collision) //2���� collision 2D�� �浹 �� ȣ��. ������ �浹 ���� �Լ�
    {
        Planet otherPlanet = collision.gameObject.GetComponent<Planet>();   //�����տ� planet ������Ʈ �־������� ���� ����
        if (otherPlanet != null && otherPlanet.size == size)    //���࿡ �浹�� ��ü�� ���� ũ���� �༺�̶�� merge() �Լ��� ��ü
        {
            Merge(otherPlanet);
        }
    }

    void Merge(Planet otherPlanet)  //�浹 �� ���� ������ �༺ ��ġ�� ���� ��ġ�� ����
    {
        Vector3 mergedPosition = (transform.position + otherPlanet.transform.position) / 2;     //�浹�� �� �༺�� ��� ������ �༺�� ����

        float mergedSize = size + otherPlanet.size;   //�� �༺ size�� ������ ������ �༺ ũ�� ����

        GameObject mergedPlanet = Instantiate(mergedPlanetPrefab, mergedPosition, Quaternion.identity); //������ �༺ ����
        mergedPlanet.GetComponent<Planet>().size = mergedSize;


    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
