using UnityEngine;

public class CameraControllerTest : MonoBehaviour
{
    private Camera mainCamera;
    private bool isDragging = false;

    public float zoomOutSize = 9.0f; // �� �ƿ� �� ī�޶� ũ��
    private float originSize; // ���� ī�޶� ũ��
    public float zoomSpeed = 3f; // �� ��/�ƿ� �ӵ�

    void Start()
    {
        mainCamera = Camera.main; // ���� ī�޶� ����
        originSize = mainCamera.orthographicSize; // ���� ī�޶� ũ�� ����
    }

    void Update()
    {
        // ���콺 ��ư�� ������ ��
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }

        // ���콺 ��ư�� �������� ��
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        // �巡�� ������ ��, ī�޶� ������ �� �ƿ�
        if (isDragging)
        {
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, zoomOutSize, Time.deltaTime * zoomSpeed);
        }
        else
        {
            // �巡�� ���°� �ƴ� ��, ī�޶� ������ ���� ũ��� ����
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, originSize, Time.deltaTime * zoomSpeed);
        }
    }
}