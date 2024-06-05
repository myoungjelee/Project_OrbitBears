using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;

    private float targetSize;
    private float originSize;

    private bool isZoomOut = false; // ��ȯ ������ ���θ� ��Ÿ���� �÷���
    private float zoomingTime = 1.2f; // ��ȯ �Ⱓ

    private float zoomInTime = 0f; // ��ȯ �ð��� ����ϱ� ���� Ÿ�̸� ����
    private float zoomuOutTime = 0f; // ��ȯ �ð��� ����ϱ� ���� Ÿ�̸� ����

    private void Start()
    {
        mainCamera = Camera.main;

        originSize = mainCamera.orthographicSize;
        targetSize = 12.0f;

        //StartCoroutine(test());
    }

    //private void LateUpdate()
    //{
    //    if (isZoomOut)
    //    {
    //        // ��ȯ �ð��� �Ⱓ���� ������
    //        if (currentTime < zoomTime)
    //        {
    //            // ��ȯ Ÿ�̸Ӹ� ������Ű��, ������ ũ�⸦ �����մϴ�.
    //            currentTime += Time.deltaTime;
    //            float t = Mathf.Clamp01(currentTime / zoomTime); // 0�� 1 ���̷� ����ȭ
    //            mainCamera.orthographicSize = Mathf.Lerp(originSize, targetSize, t);
    //        }
    //        else
    //        {
    //            // ��ȯ �ð��� �Ⱓ�� �ʰ��ϸ� ��ȯ ����
    //            isZoomOut = false;
    //            currentTime += Time.deltaTime;
    //            float t = Mathf.Clamp01(currentTime / zoomTime); // 0�� 1 ���̷� ����ȭ
    //            mainCamera.orthographicSize = Mathf.Lerp(targetSize, originSize, t);
    //        }
    //    }
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    //mainCamera.orthographicSize = Mathf.Lerp(originSize, targetSize, 0.5f);
    //    isZoomOut = true;
    //    currentTime = 0f;
    //}

    //IEnumerator test()
    //{
    //    yield return new WaitForSeconds(3);

    //    isZoomOut = true;
    //}
}
