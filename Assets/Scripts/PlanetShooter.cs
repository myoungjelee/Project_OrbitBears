using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class PlanetShooter : MonoBehaviour
{
    public Rigidbody2D planetRigidbody;    // �༺�� ������ٵ�2D ������Ʈ
    // [SerializeField] Rigidbody2D planetRigidbody; ��� �ܼ� �̷������ε� ��밡����

    public float launchForce = 7.5f;   // ���콺�� �߻��ϴ� ���� ���

    public GameObject planet;
    public Vector2 landingSpot;         // �������� �� �Ҵ��ϱ� (�±�)


    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private bool isDragging = false;     // ���콺 ���� �ڵ�

    private bool isGravityActive = false;  // �߷� Ȱ��ȭ ����

    private bool isLaunched = false;       // �߻� ���� Ȯ��
    private bool hasLanded = false;        // �༺�� �������� Ȯ��
    //////////////////////////�ε�������///////////////////////////

    private LineRenderer lineRenderer;
    public int resolution = 30;             // ������ �ػ� (����Ʈ ��)

    //////////////////////////�ε�������///////////////////////////
    void Start()
    {
        planetRigidbody = GetComponent<Rigidbody2D>();

        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();   // LineRenderer�� �������� ����
        }

        landingSpot = new Vector2(4, 0);
    }   

    void Update()
    {
        if (!isLaunched) // �߻���� �ʾ��� ���� ���콺 �Է��� ó�� = �߻�� ���Ŀ� ���콺 �Է¿����� ��������
        {
            if (Input.GetMouseButtonDown(0)) // ���콺 ��ư ������ ��
            {
                // �巡�� ���� ��ġ ���
                dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isDragging = true;
                /////�ε������� ��ɾ�/////
                dragStartPosition = planetRigidbody.position;
                ///////////////////////////
            }
            if (Input.GetMouseButton(0) && isDragging) // ���콺�� Ŭ�� + �巡�׸� �ϴµ��� ~
            {
                dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 dragVector = (dragStartPosition - dragEndPosition); // Ŭ��&�巡�� �ϴµ����� ���� ���
                Vector2 direction = dragVector.normalized;                  // �߻���� ���
                float dragDistance = dragVector.magnitude;                  // �巡�� �Ÿ� ��� (�巡�� ����)

                ShowTrajectory(dragStartPosition, direction * dragDistance * launchForce);
            }
            if (Input.GetMouseButtonUp(0))   // ���콺 ��ư ���� ��
            {
                dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                isDragging = false;
                //////�ε������� ��ɾ�/////
                ClearTrajectory();  // ����(ǥ�ñ�) �����
                ////////////////////////////
                Vector2 dragVector = (dragStartPosition - dragEndPosition); // ���콺 Ŭ��ON ��ǥ - Ŭ��OFF ��ǥ �� �ؼ� �����
                Vector2 direction = dragVector.normalized;                  // �߻���� ���
                float dragDistance = dragVector.magnitude;                  // �巡�� �Ÿ� ���

                planetRigidbody.AddForce(direction * dragDistance * launchForce, ForceMode2D.Impulse);

                isGravityActive = true;   // ���콺�� �߻��� ���� �߷� Ȱ��ȭ
                isLaunched = true;
            }
        }

    }

    void AttracToLandingSpot()
    {
        Vector2 direction = (Vector3)landingSpot - transform.position;     // ���� ���
        float distance = direction.magnitude;                                        // landingspot������ �Ÿ� ���
        Vector2 gravityDirection = direction.normalized;                             // �߷��� ����

        float adjustedDistance = Mathf.Max(distance, 0.001f);                        // �ּ� �Ÿ� ���� #�� �����Ͽ� �Ÿ��� #���� �۾����� �ʵ��� ��
        float gravityStrength = 10 / adjustedDistance;                               // ������ �Ÿ��� ����Ͽ� �߷� ���� ��� 
        /*  if (distance < 1f)  // ������ (landingSpot)�� �ſ� �����������
          {
              planetRigidbody.velocity = Vector3.ClampMagnitude(planetRigidbody.velocity, 50f); // �ӵ��� �ִ� #f�� ����
              gravityStrength = Mathf.Lerp(gravityStrength, 0, 1 - distance);
              planetRigidbody.velocity *= 0.01f; 
          }*/
       // planetRigidbody.AddForce(gravityDirection * gravityStrength);                // ������ �߷� ����
        planetRigidbody.velocity += gravityDirection * 0.7f;
    }
    //public float maxSpeed = 100f;                                                  // �ִ� �ӵ� ����
    void FixedUpdate()
    {
        planetRigidbody.drag = 1.5f;           // �߻�� �༺�� �ӵ� ���׷� ����

        if (isLaunched)
        {
            AttracToLandingSpot();
        }
    }

    void ShowTrajectory(Vector2 startPosition, Vector2 startVelocity)
    {
        resolution = 3; // ������ �ػ󵵸� ���缭 ������ ���̸� ����

        Vector3[] points = new Vector3[resolution];
        float timeStep = 0.1f;   // �ð� ����

        // �巡�� ���⿡ ���� ���� ��ġ ����
        float trajectoryOffset = startVelocity.y > 0 ? -2f : 2f; // ���� �巡���ϸ� ������ �Ʒ���, �Ʒ��� �巡���ϸ� ������ ���� ����

        for (int i = 0; i < resolution; i++)
        {
            float time = i * timeStep;
            Vector2 point = startPosition + startVelocity * time + 0.5f * Physics2D.gravity * time * time; // �� ���

            points[i] = new Vector3(point.x, point.y, 0); // 2D ���� 3D�� ��ȯ
       
        }

        lineRenderer.positionCount = resolution;
        lineRenderer.SetPositions(points); // LineRenderer�� �� ����

    }
    void ClearTrajectory()
    {
        lineRenderer.positionCount = 0; // ���� �����
    }
}
