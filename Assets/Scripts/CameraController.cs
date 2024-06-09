using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Camera mainCamera; // ���� ī�޶�
    public float zoomOutSize = 7f; // �ܾƿ� �� ī�޶� ũ��
    public float zoomInSize = 5f; // ����(����) �� ī�޶� ũ��
    public float zoomSpeed = 2f; // �� ��ȯ �ӵ�

    private bool isDragging = false; // �巡�� ���� Ȯ��
    private bool isZoomedOut = false; // �ܾƿ� ���� Ȯ��
    private float targetZoomSize; // ��ǥ �� ũ��

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        targetZoomSize = mainCamera.orthographicSize;
    }

    void Update()
    {
        // ���콺 Ŭ�� �巡�� ���� Ȯ��
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            StartZoomOut();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            StartZoomIn();
        }

        // ī�޶� �� ��ȯ
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetZoomSize, Time.deltaTime * zoomSpeed);
    }

    void StartZoomOut()
    {
        if (!isZoomedOut)
        {
            targetZoomSize = zoomOutSize;
            isZoomedOut = true;
        }
    }

    void StartZoomIn()
    {
        if (isZoomedOut)
        {
            targetZoomSize = zoomInSize;
            isZoomedOut = false;
        }
    }
}