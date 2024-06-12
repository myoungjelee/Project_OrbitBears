using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class PlanetShooter : MonoBehaviour
{
    public Rigidbody2D planetRigidbody;    // �༺�� ������ٵ�2D ������Ʈ

    public float launchForce = 0.01f;   // ���콺�� �߻��ϴ� ���� ���

    public Vector2 landingSpot;         // �������� �� �Ҵ��ϱ� (�±�)

    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private bool isDragging = false;     // ���콺 ���� �ڵ�

    private bool isGravityActive = false;  // �߷� Ȱ��ȭ ����

    private bool isLaunched = false;       // �߻� ���� Ȯ��
    private bool hasLanded = false;        // �༺�� �������� Ȯ��

    private LineRenderer lineRenderer;
    public int resolution = 20;             // ������ �ػ� (����Ʈ ��)

    public ParticleSystem flyingTrailEffect;

    private Planet planet;

    void Start()
    {
        planet = GetComponent<Planet>();
        planetRigidbody = GetComponent<Rigidbody2D>();

        // LineRenderer �ʱ� ����
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default")); // �⺻ ���̴� ���
        lineRenderer.textureMode = LineTextureMode.Tile; // �ؽ�ó�� Ÿ�� ���·� �ݺ�
        lineRenderer.widthMultiplier = 0.7f; // ���� �β� ����

        if (lineRenderer == null)
        {
            lineRenderer = GetComponent<LineRenderer>();   // LineRenderer�� �������� ����
        }

        landingSpot = new Vector2(4, 0);

        flyingTrailEffect = GetComponent<ParticleSystem>();
    }
    void Update()
    {
        if (!isLaunched) // �߻���� �ʾ��� ���� ���콺 �Է��� ó��
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
            if (Input.GetMouseButton(0) && isDragging) // ���콺�� Ŭ�� + �巡�׸� �ϴ� ����
            {
                dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 dragVector = (dragStartPosition - dragEndPosition); // Ŭ��&�巡�� �ϴ� ������ ���� ���

                // �巡�� ������ ũ�� ����
                float maxDragDistance = 5.0f; // �ִ� �巡�� �Ÿ�
                dragVector = Vector2.ClampMagnitude(dragVector, maxDragDistance);

                // ���� ����
                float angleLimit = 90.0f; // ���� ����
                float angle = Vector2.SignedAngle(Vector2.right, dragVector);
                if (Mathf.Abs(angle) > angleLimit)
                {
                    angle = Mathf.Sign(angle) * angleLimit;
                    dragVector = Quaternion.Euler(0, 0, angle) * Vector2.right * dragVector.magnitude;
                }

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
                Vector2 dragVector = (dragStartPosition - dragEndPosition); // ���콺 Ŭ��ON ��ǥ - Ŭ��OFF ��ǥ�� �ؼ� �� ���

                // �巡�� ������ ũ�� ����
                float maxDragDistance = 5.0f; // �ִ� �巡�� �Ÿ�
                dragVector = Vector2.ClampMagnitude(dragVector, maxDragDistance);

                // ���� ����
                float angleLimit = 90.0f; // ���� ����
                float angle = Vector2.SignedAngle(Vector2.right, dragVector);
                if (Mathf.Abs(angle) > angleLimit)
                {
                    angle = Mathf.Sign(angle) * angleLimit;
                    dragVector = Quaternion.Euler(0, 0, angle) * Vector2.right * dragVector.magnitude;
                }

                Vector2 direction = dragVector.normalized;                  // �߻���� ���
                float dragDistance = dragVector.magnitude;                  // �巡�� �Ÿ� ���

                planetRigidbody.velocity = direction * dragDistance * launchForce;

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

    void AttracToLandingSpot()
    {
        Vector2 direction = (Vector3)landingSpot - transform.position;     // ���� ���
        float distance = direction.magnitude;                                        // landingspot������ �Ÿ� ���
        Vector2 gravityDirection = direction.normalized;                             // �߷��� ����

        float adjustedDistance = Mathf.Max(distance, 0.001f);                        // �ּ� �Ÿ� ���� #�� �����Ͽ� �Ÿ��� #���� �۾����� �ʵ��� ��
        float gravityStrength = 0.01f / adjustedDistance;                               // ������ �Ÿ��� ����Ͽ� �߷� ���� ��� 

        //planetRigidbody.velocity += gravityDirection * gravityStrength * Time.fixedDeltaTime;
        planetRigidbody.velocity += gravityDirection * 1f;

       // float clampedValue = Mathf.Clamp(gravityStrength,  )  // Ŭ���� : �ּҰ� �ִ밪 ����
    }

    void FixedUpdate()
    {
        planetRigidbody.drag = 2f;           // �߻�� �༺�� �ӵ� ���׷� ����

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