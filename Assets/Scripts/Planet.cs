using System.Collections;
using System.Collections.Generic;
using UnityEngine;

<<<<<<< HEAD
=======
<<<<<<< HEAD

>>>>>>> parent of 78f00ea (Revert "데이터 생성 완료, 충돌 시 처리 구현")
[System.Serializable]
public class PlanetData 
{
    public string name;
    public int id;
=======
[System.Serializable]
public class PlanetData
{
    public string name;
    [HideInInspector] public uint id;
>>>>>>> dev
    public Sprite sprite;
    public Color color = Color.white;
    public float radius;
    public float mass;
    public int mergeScore;
<<<<<<< HEAD
    public Sprite nextSizeSprite;
=======
    public float radiusData;
>>>>>>> dev
}

public class Planet : MonoBehaviour
{
<<<<<<< HEAD

=======
<<<<<<< HEAD
>>>>>>> parent of 78f00ea (Revert "데이터 생성 완료, 충돌 시 처리 구현")
    public float radius;
    public Sprite nextSizeSprite;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Planet otherPlanet = collision.gameObject.GetComponent<Planet>();

        if (otherPlanet != null && Mathf.Approximately(otherPlanet.radius, this.radius))
        {
            Vector3 collisionPoint = collision.contacts[0].point;
            Destroy(otherPlanet.gameObject);
            Destroy(this.gameObject);

            if (nextSizeSprite != null)
            {
                GameObject newPlanet = new GameObject("Planet");
                newPlanet.transform.position = collisionPoint;
                newPlanet.transform.localScale = Vector3.one * (this.radius * 2); // ���� ũ��� ����

                SpriteRenderer spriteRenderer = newPlanet.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = nextSizeSprite;

                Rigidbody2D rb = newPlanet.AddComponent<Rigidbody2D>();
                rb.AddForce(collision.relativeVelocity, ForceMode2D.Impulse);

                Planet planetComponent = newPlanet.AddComponent<Planet>();
                planetComponent.radius = this.radius * 2; // ���� ũ��� ����
                planetComponent.nextSizeSprite = this.nextSizeSprite; // ���� ũ���� ��������Ʈ ����
            }
        }
    }
}
<<<<<<< HEAD

=======
=======
    public PlanetData newData;

    public void SetData(PlanetData data)
    {
        newData = data;
    }
    
}
>>>>>>> dev
>>>>>>> parent of 78f00ea (Revert "데이터 생성 완료, 충돌 시 처리 구현")
