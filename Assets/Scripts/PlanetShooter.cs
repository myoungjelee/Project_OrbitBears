using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class PlanetShooter : MonoBehaviour
{
    public Rigidbody2D planetRigidbody;    // �༺�� ������ٵ�2D ������Ʈ

    public float launchForce = 0.0001f;   // ���콺�� �߻��ϴ� ���� ���

    public Vector2 landingSpot;         // �������� �� �Ҵ��ϱ� (�±�)

    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private bool isDragging = false;     // ���콺 ���� �ڵ�

    private bool isGravityActive = false;  // �߷� Ȱ��ȭ ����

    private bool isLaunched = false;       // �߻� ���� Ȯ��
    private bool hasLanded = false;        // �༺�� �������� Ȯ��

    private LineRenderer lineRenderer;
    public int resolution = 30;             // ������ �ػ� (����Ʈ ��)

    public ParticleSystem flyingTrailEffect;

    private Planet planet;

    void Start()
    {
        planet = GetComponent<Planet>();
        planetRigidbody = GetComponent<Rigidbody2D>();

        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();   // LineRenderer�� �������� ����
        }

        landingSpot = new Vector2(4, 0);

        flyingTrailEffect = GetComponent<ParticleSystem>();
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

                Vector2 Force = direction * dragDistance * launchForce;
                float maxMagnitude = 20.0f; // �ִ� ũ��
                Vector2 shootForce = Vector2.ClampMagnitude(Force, maxMagnitude);

                planetRigidbody.velocity = shootForce;

                isGravityActive = true;   // ���콺�� �߻��� ���� �߷� Ȱ��ȭ
                isLaunched = true;
                if (!planet.isTouch)
                {
                    flyingTrailEffect.Play();
                }
                else
                {
                    flyingTrailEffect.Stop();
                }
            }
        }
    }

    void AttractToLandingSpot()
    {
        Vector2 direction = (Vector3)landingSpot - transform.position;     // ���� ���
        float distance = direction.magnitude;                                        // landingspot������ �Ÿ� ���
        Vector2 gravityDirection = direction.normalized;                             // �߷��� ����

        float adjustedDistance = Mathf.Max(distance, 0.001f);                        // �ּ� �Ÿ� ���� #�� �����Ͽ� �Ÿ��� #���� �۾����� �ʵ��� ��
        float gravityStrength = 100 / adjustedDistance;                               // ������ �Ÿ��� ����Ͽ� �߷� ���� ��� 

        //planetRigidbody.velocity += gravityDirection * gravityStrength * Time.fixedDeltaTime;
        planetRigidbody.velocity += gravityDirection * 0.7f;
    }

    void AttractToLandingSpot_MJ()
    {
        Vector2 direction = (Vector3)landingSpot - transform.position;  // ���� ���
        float distance = direction.magnitude;                           // landingSpot������ �Ÿ� ���
        Vector2 gravityDirection = direction.normalized;                // �߷��� ����

        float adjustedDistance = Mathf.Max(distance, 0.001f);           // �ּ� �Ÿ� ���� 0.001�� �����Ͽ� �Ÿ��� 0���� �۾����� �ʵ��� ��

        // �Ÿ��� �������� ������ �۾������� ���� ��� (�Ÿ��� �������� ������ �۾���)
        float distanceRatio = Mathf.Clamp01(adjustedDistance / 10.0f);  // �Ÿ��� 10���� ������ ������ ���� (���� ����)

        Vector2 gravity = gravityDirection * 9.8f * distanceRatio;      // ������ �����Ͽ� �߷� �� ���

        planetRigidbody.velocity += gravity * 0.25f;                     // �߷� ���� �����Ͽ� �ӵ� ������Ʈ
    }



    void FixedUpdate()
    {
        planetRigidbody.drag = 3f;           // �߻�� �༺�� �ӵ� ���׷� ����

        if (isLaunched)
        {
           // AttractToLandingSpot();
            AttractToLandingSpot_MJ();
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