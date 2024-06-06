using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class PlanetShooter : MonoBehaviour
{
    public Rigidbody2D planetRigidbody;    // �༺�� ������ٵ�2D ������Ʈ
    // [SerializeField] Rigidbody2D planetRigidbody; ��� �ܼ� �̷������ε� ��밡����

    public float launchForce = 10f;   // ���콺�� �߻��ϴ� ���� ���

    public GameObject planet;
    public GameObject landingSpot;         // �������� �� �Ҵ��ϱ� (�±�)

    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private bool isDragging = false;       // ���콺 ���� �ڵ�

    private bool isGravityActive = false;  // �߷� Ȱ��ȭ ����

    private bool isLaunched = false;       // �߻� ���� Ȯ��
    private bool hasLanded = false;        // �༺�� �������� Ȯ��

    //////////////////////////�ε�������///////////////////////////
   
    private LineRenderer lineRenderer;
    public int resolution = 30;             // ������ �ػ� (����Ʈ ��)

    void Start()
    {
        planetRigidbody = GetComponent<Rigidbody2D>();

        if (lineRenderer == null )
        {
            lineRenderer = GetComponent<LineRenderer>();   // LineRenderer�� �������� ����
        }
    }

    void Update()
    {     
        if (Input.GetMouseButtonDown(0)) // ���콺 ��ư ��������
        {
            // �巡�� ���� ��ġ ���
            dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
            isDragging = true;
            /////�ε������� ��ɾ�/////
            dragStartPosition = planetRigidbody.position;
            ///////////////////////////
        }
        if (Input.GetMouseButtonUp(0))   // ���콺 ��ư ������
        {
            dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = false;
            //////�ε������� ��ɾ�/////
            ClearTrajectory();  // ���� �����
            ////////////////////////////
            Vector2 dragVector = (dragStartPosition - dragEndPosition); // ���콺 Ŭ��ON ��ǥ - Ŭ��OFF ��ǥ �� �ؼ� �����
            Vector2 direction = dragVector.normalized;                  // �߻���� ���
            float dragDistance = dragVector.magnitude;                  // �巡�� �Ÿ� ���
           
            planetRigidbody.AddForce(direction * dragDistance * launchForce, ForceMode2D.Impulse);

            isGravityActive = true;   // ���콺�� �߻��� ���� �߷� Ȱ��ȭ
            isLaunched = true;
        }
        if (isLaunched)
        {
            AttracToLandingSpot();
        }
    }
    void AttracToLandingSpot()
    {
        Vector2 direction = landingSpot.transform.position - transform.position;     // ���� ���
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
        planetRigidbody.AddForce(gravityDirection * gravityStrength);                // ������ �߷� ����
    }
    //public float maxSpeed = 100f;                                                  // �ִ� �ӵ� ����
    void FixedUpdate()
    {
        planetRigidbody.drag = 1.5f;           // �߻�� �༺�� �ӵ� ���׷� ����
    }

    void ShowTrajectory(Vector2 startPosition, Vector2 startVelocity)
    {
        Vector3[] points = new Vector3[resolution];
        float timeStep = 0.1f;   // �ð� ����

        for (int i = 0; i < resolution; i++)
        {
            float time = i * timeStep;
            Vector2 point = startPosition + startVelocity * time * 0.5f * Physics2D.gravity * time * time; // ����Ʈ ���
            points[i] = new Vector3(point.x, point.y, 0); // 2D ����Ʈ�� 3D�� ��ȯ
        }

        lineRenderer.positionCount = resolution;
        lineRenderer.SetPositions(points); // LineRenderer�� ����Ʈ ����
    }
    
    void ClearTrajectory()
    {
        lineRenderer.positionCount = 0; // ���� �����
    }
    /*    public float surfaceFriction = 0.01f;    // (�޶� ���� ��) ǥ�� ������
        public float spinFriction = 5f;            // (�޶� ���� ��) ȸ�� ������

        void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject == landingSpot)
            {
                // landingSpot �浹 �� ������ ����
                planetRigidbody.drag = surfaceFriction;
                planetRigidbody.angularDrag = spinFriction;
            }
        }*/
    /*    public float maxSpeed = 100f;                                                // �ִ� �ӵ� ����
        void FixedUpdate()
        {
                                         if (planetRigidbody.velocity.magnitude > maxSpeed)
                                         {
                                         ���� �ӵ� ������ �����ϸ鼭 �ӵ��� ũ�⸦ maxSpeed�� ����
                                         planetRigidbody.velocity = planetRigidbody.velocity.normalized * maxSpeed;
                                         }
        }*/
}
