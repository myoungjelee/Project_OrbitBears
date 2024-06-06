using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static System.TimeZoneInfo;

public class CameraController : MonoBehaviour
{
    private Camera mainCamera;

    private float expendSize;
    private float originSize;

    private float zoomDurationTime = 1.2f; // ��ȯ �ð�

    private void Start()
    {
        mainCamera = Camera.main;

        originSize = mainCamera.orthographicSize;
        expendSize = 12.0f;

        StartCoroutine(test());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            // ī�޶� �� �ƿ�
            CameraZoomOut();

            // ������ ���� �ð� ���Ŀ� �� �� �޼ҵ� ȣ��
            Invoke("CameraZoomIn", zoomDurationTime);
        }
    }

    IEnumerator test()
    {
        yield return new WaitForSeconds(zoomDurationTime);

        // ī�޶� �� �ƿ�
        CameraZoomOut();

        // ������ ���� �ð� ���Ŀ� �� �� �޼ҵ� ȣ��
        Invoke("CameraZoomIn", zoomDurationTime);
    }

    private void CameraZoomOut()
    {
        StartCoroutine(CameraControl(expendSize));
    }

    private void CameraZoomIn()
    {
        StartCoroutine(CameraControl(originSize));
    }

    // ī�޶��� orthographic ũ�⸦ �ε巴�� �����ϴ� �ڷ�ƾ
    IEnumerator CameraControl(float targetSize)
    {
        float currentTime = 0;   // ����ð� �ʱ�ȭ                     
        float currentSize = mainCamera.orthographicSize;  // ���� orthographicSize ����

        // ������ �ð� ���� ���� ũ��� ��ǥ ũ�� ���̸� ����
        while (currentTime < zoomDurationTime)
        {
            mainCamera.orthographicSize = Mathf.Lerp(currentSize, targetSize, currentTime / zoomDurationTime);

            // ��� �ð� ����
            currentTime += Time.deltaTime;

            // ���� �����ӱ��� ���
            yield return null;
        }
    }
}
