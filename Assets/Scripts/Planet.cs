using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< Updated upstream
public class Planet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
=======
[System.Serializable]
public class PlanetData
{
    public string name;
    [HideInInspector] public int id;
    public Sprite sprite;
    public Color color = Color.white;
    public float radius;
    public float mass;
    public int mergeScore;
}

public class Planet : MonoBehaviour
{   
    public string planetName;
    public Sprite planetSprite;
    public Color planetColor = Color.white;
    public float planetRadius;    
    public float planetMass;
    public int planetMergeScore;

    private bool evolved = false;
    string planetTag;

    private void Start()
    {
        planetTag = this.gameObject.name;
    }

    public void SetData(PlanetData data)
    {
        planetSprite = data.sprite;
        planetColor = data.color;
        planetRadius = data.radius;
        planetMass = data.mass;
        planetMergeScore = data.mergeScore;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if (collision.gameObject.CompareTag($"{planetTag}"))
        //{
        //    Evolution(planetTag);
        //}
        if (!evolved && collision.gameObject.CompareTag($"{planetTag}")) // ũ�Ⱑ ������ �ʾҰ�, �浹�� ������Ʈ�� �༺ �±��� ���
        {
            Evolution(planetTag); // Evolution �޼��� ȣ��
            evolved = true; // evolved ������ true�� �����Ͽ� ũ�� ��ȭ�� �̹� �Ͼ���� ǥ��
        }
    }

    public void Evolution(string name)      //�浹 ó��
    {

        int planetIndex = GetPlanetIndex(name);
        var nextPlanet = FindAnyObjectByType<PlanetManager>().planetPrefabs[planetIndex + 1]; // ���� ũ���� �༺ ��������

      /*  switch (name)      
        {
            case "Moon":
                var nextPlanet1 = FindAnyObjectByType<PlanetManager>().planetPrefabs[1];
                Instantiate(nextPlanet1);
                Destroy(this.gameObject);
                break;

            case "Mercury":
                var nextPlanet2 = FindAnyObjectByType<PlanetManager>().planetPrefabs[2];
                Instantiate(nextPlanet2);
                Destroy(this.gameObject);
                break;
            case "Venus":
                var nextPlanet3 = FindAnyObjectByType<PlanetManager>().planetPrefabs[3];
                Instantiate(nextPlanet3);
                Destroy(this.gameObject);
                break;
            case "Earth":
                var nextPlanet4 = FindAnyObjectByType<PlanetManager>().planetPrefabs[4];
                Instantiate(nextPlanet4);
                Destroy(this.gameObject);
                break;
            case "Mars":
                var nextPlanet5 = FindAnyObjectByType<PlanetManager>().planetPrefabs[5];
                Instantiate(nextPlanet5);
                Destroy(this.gameObject);
                break;
            case "Uranus":
                var nextPlanet6 = FindAnyObjectByType<PlanetManager>().planetPrefabs[6];
                Instantiate(nextPlanet6);
                Destroy(this.gameObject);
                break;
            case "Neptune":
                var nextPlanet7 = FindAnyObjectByType<PlanetManager>().planetPrefabs[7];
                Instantiate(nextPlanet7);
                Destroy(this.gameObject);
                break;
            case "Saturn":
                var nextPlanet8 = FindAnyObjectByType<PlanetManager>().planetPrefabs[8];
                Instantiate(nextPlanet8);
                Destroy(this.gameObject);
                break;
            case "Jupiter":
                var nextPlanet9 = FindAnyObjectByType<PlanetManager>().planetPrefabs[9];
                Instantiate(nextPlanet9);
                Destroy(this.gameObject);
                break;
            case "sun":
                break;

           
        }
      */

        Instantiate(nextPlanet, transform.position, Quaternion.identity);

        Destroy(gameObject); // ���� �༺ �ı�
    }

    private int GetPlanetIndex(string name) // �༺ �ε��� ��������
    {
        switch (name)
        {
            case "Moon": return 1;
            case "Mercury": return 2;
            case "Venus": return 3;
            case "Earth": return 4;
            case "Mars": return 5;
            case "Uranus": return 6;
            case "Neptune": return 7;
            case "Saturn": return 8;
            case "Jupiter": return 9;
            case "sun": return 10;
            default: return -1; // �߸��� �༺ �̸�
        }
>>>>>>> Stashed changes
    }
}
