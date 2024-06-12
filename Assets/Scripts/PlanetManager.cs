using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEditor;
using UnityEditor.Networking.PlayerConnection;
using UnityEngine;
using UnityEngine.UI;



public class PlanetManager : MonoBehaviour
{
    private static PlanetManager instance;
    public GameObject planetPrefab;
    public PlanetSetting planetSetting;
    public Image nextPlanetImage;
    //public Sprite nextPlanetSprite;
    public Transform planetSpawnPoint;

    private PlanetData currentPlanetRandomData;
    private PlanetData nextPlanetRandomData;


    public static PlanetManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlanetManager>();
            }
            return instance;
        }
    }
    private void Start()
    {
        currentPlanetRandomData = GetRandomPlanetData();
        nextPlanetRandomData = GetRandomPlanetData();
        AfterShootPlanet();
    }


    public Planet SpawnPlanet(PlanetData data, Vector2 spawnPos)
    {
        Planet planet = Instantiate(planetPrefab, spawnPos, Quaternion.identity).GetComponent<Planet>();
        planet.SetData(data);

        return planet;
    }

    public PlanetData GetRandomPlanetData()     //�������� �迭 4������ �迭���� �����͸� return
    {
        int id = UnityEngine.Random.Range(0, 4);
        return planetSetting.planetDatas[id];
    }

    public PlanetData NextPlanetIndex(int currentData)   //�����༺�� �ε��� + 1�� ������ return;
    {
        return planetSetting.planetDatas[currentData + 1];
    }
    

    public void AfterShootPlanet()
    {
        currentPlanetRandomData = nextPlanetRandomData; 
        nextPlanetRandomData = GetRandomPlanetData();

        if(currentPlanetRandomData != null)
        {
            nextPlanetImage.sprite = nextPlanetRandomData.sprite;
        }

        SpawnPlanet(currentPlanetRandomData, planetSpawnPoint.position);

    }
}