using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShooter : MonoBehaviour
{
    public Rigidbody2D planetRigidbody;    // �༺�� ������ٵ�2D ������Ʈ

    // [SerializeField] Rigidbody2D planetRigidbody; ��� �ܼ� �̷������ε� ��밡����
    public float forceMultiplier = 80f;   // ���� ���

    public GameObject planet;
    public GameObject landingSpot; // �������� �� �Ҵ��ϱ� (�±�)

    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private bool isDragging = false;   // ���콺 ���� �ڵ�


    private bool isGravityActive = false; // �߷� Ȱ��ȭ ����
                                          // private bool isInFlight = false;  // ���� ���ư��� �ִ� �������� Ȯ��

    private bool isLaunched = false; // �߻� ���� Ȯ��
    private bool hasLanded = false;   // ���� �����ߴ��� Ȯ��


    //private bool isDecelerating = false;

    void Start()
    {
        planetRigidbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // ���콺 Ŭ�� �Ҷ�
        if (Input.GetMouseButtonDown(0))

        {
            // �巡�� ���� ��ġ ���
            dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }
        if (Input.GetMouseButtonUp(0))          //���⿡ ������ void Start()�Ʒ��� �Լ��� ������ ���� ���۽ÿ�
                                                // ���콺�� ������ ���� �����̹Ƿ� ���� if���� �ٷ� ����� ��÷�
                                                // ����� Ŀ�ǵ� �̱⿡ Update�� �Ű���
        {
            dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = false;

            Vector2 dragVector = (dragStartPosition - dragEndPosition); // ���콺 Ŭ��ON ��ǥ - Ŭ��OFF ��ǥ �� �ؼ� �����
            Vector2 direction = dragVector.normalized;  // �߻���� ���
            float dragDistance = dragVector.magnitude;  // �巡�� �Ÿ� ���

            planetRigidbody.AddForce(direction * dragDistance * forceMultiplier, ForceMode2D.Impulse);

            isGravityActive = true;
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

        float adjustedDistance = Mathf.Max(distance, 0.1f);                          // �ּ� �Ÿ� ���� 0.5�� �����Ͽ� �Ÿ��� 0.5���� �۾����� �ʵ��� ��
        float gravityStrength = 10 / adjustedDistance;                               // ������ �Ÿ��� ����Ͽ� �߷� ���� ���
        //float gravityStrength = Mathf.Clamp(10 / distance, 0.1f, 10);              // �Ÿ��� ���� �߷� ���� ����, �ּҰ��� �ִ밪 ����
    
      /*  if (distance < 1f)  // ������ (landingSpot)�� �ſ� �����������
        {
            planetRigidbody.velocity = Vector3.ClampMagnitude(planetRigidbody.velocity, 50f); // �ӵ��� �ִ� #f�� ����
            gravityStrength = Mathf.Lerp(gravityStrength, 0, 1 - distance);
            planetRigidbody.velocity *= 0.01f; 
        }*/
        planetRigidbody.AddForce(gravityDirection * gravityStrength);                // ������ �߷� ����
    }
    //public float maxSpeed = 100f;                                                    // �ִ� �ӵ� ����
    void FixedUpdate()
    {
        planetRigidbody.drag = 1.5f;         // ���׷� ����  1.6f Ȳ��
    }

    public float surfaceFriction = 0.01f;     // ǥ�� ������
    public float spinFriction = 5f;        // ȸ�� ������

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == landingSpot)
        {
            // �浹 �� ������ ����
            planetRigidbody.drag = surfaceFriction;
            planetRigidbody.angularDrag = spinFriction;
        }
    }
    /*    public float maxSpeed = 100f;                                                    // �ִ� �ӵ� ����
        void FixedUpdate()
        {
                                         if (planetRigidbody.velocity.magnitude > maxSpeed)
                                         {
                                         ���� �ӵ� ������ �����ϸ鼭 �ӵ��� ũ�⸦ maxSpeed�� ����
                                         planetRigidbody.velocity = planetRigidbody.velocity.normalized * maxSpeed;
                                         }
        }*/
}
