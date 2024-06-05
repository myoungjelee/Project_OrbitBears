using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShooter : MonoBehaviour
{
    public Rigidbody2D planetRigidbody;    // �༺�� ������ٵ�2D ������Ʈ
   
    // [SerializeField] Rigidbody2D planetRigidbody; ��� �ܼ� �̷������ε� ��밡����
    public float forceMultiplier = 2f;   // ���� ���

    public GameObject planet;
    public GameObject landingSpot; // �������� �� �Ҵ��ϱ� (�±�)

    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private bool isDragging = false;

    public Transform gravityCenter;

    public float gravityStrength = 30f;
    public float decelerationRate = 10f; // �ӵ� ������ (0.95�� �� �����Ӹ��� 5%�� �ӵ��� ����)
    public float landingRadius = 1f;

    private bool isGravityActive = false; // �߷� Ȱ��ȭ ����
                                          // private bool isInFlight = false;  // ���� ���ư��� �ִ� �������� Ȯ��
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
        ///////////////////////////////////////
        // ���콺 Ŭ�� �̺�Ʈ ó��
        if (Input.GetMouseButtonDown(0) && isGravityActive && !hasLanded)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Collider2D collider = Physics2D.OverlapPoint(mousePos);
        }
        ///////////////////////////////////////
        if (Input.GetMouseButtonUp(0))          // ���⿡ ������ void Start()�Ʒ��� �Լ��� ������ ���� ���۽ÿ�
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
        }



    }
    void FixedUpdate()
    {
        /*if (isGravityActive )   // �߷��� Ȱ��ȭ �Ǿ����� �ϴ� ��ɽ���
        {
            // �߷����� �༺������ ���Ͱ��� ���
            Vector2 directionToCenter = (Vector2)gravityCenter.position - planetRigidbody.position;
            float distanceToCenter = directionToCenter.magnitude;
            // �߷� ���Ͱ� ���
            float gravityFroceMagnitude = gravityStrength / (distanceToCenter * distanceToCenter);
            Vector2 gravityForce = directionToCenter.normalized * gravityFroceMagnitude;

            // Calculate the gravity vector
            //Vector2 gravityForce = directionToCenter.normalized * gravityForceMagnitude;

            // Calculate the gravity force magnitude (using the inverse square law)
            float gravityForceMagnitude = gravityStrength / (distanceToCenter * distanceToCenter);

            

            // Apply the gravity force to the ball
            planetRigidbody.AddForce(gravityForce);

            // �༺�� �������� ��ó�� �����ߴ��� Ȯ��
            if (distanceToCenter <= landingRadius)
            {
                // �༺�� �߷������� ���������� ��ġ�� �ӵ� 0���� ����
                planetRigidbody.velocity = Vector2.zero;
                planetRigidbody.angularVelocity = 0.0f;
                planetRigidbody.position = gravityCenter.position;

                isGravityActive = false; // �߷� ��Ȱ��ȭ
                hasLanded = true;       
            }
        }*/
        /* if (isGravityActive && !hasLanded)
        {
            Vector2 directionToCenter = (Vector2)gravityCenter.position - planetRigidbody.position;
            float distanceToCenter = directionToCenter.magnitude;

            if (distanceToCenter > landingRadius)
             {
                 // ������ ũ���� �߷� ���͸� ����մϴ�.
                 Vector2 gravityForce = directionToCenter.normalized * gravityStrength;

                 // ���� �߷� ���͸� ���� �����մϴ�.
                 planetRigidbody.AddForce(gravityForce);
             }
             else
             {
                 void OnCollisionEnter2D(Collision2D collision)
                 {
                     // �浹�� ������Ʈ�� �±װ� "LandingSpot"���� Ȯ���մϴ�.
                     if (collision.gameObject.CompareTag("LandingSpot"))
                     {
                         // �ӵ��� Vector2.zero�� �����Ͽ� ������Ʈ�� �ӵ��� 0���� ����ϴ�.
                         planetRigidbody.velocity = Vector2.zero;
                     }
                 }
             }
        }
        else if (isDecelerating)
        {
            // �ӵ��� ���������� ����
            planetRigidbody.velocity *= decelerationRate;

            // ��� ���� �ӵ��� �پ��� ������ ����
            if (planetRigidbody.velocity.magnitude < 0.1f)
            {
                planetRigidbody.velocity = Vector2.zero;
                isDecelerating = false;
            }
        }*/
        /*           if (isGravityActive)
               {
                   Vector2 directionToLandingSpot = (landingSpot.transform.position - transform.position).normalized;
               float distance = Vector2.Distance(transform.position, landingSpot.transform.position);
                   // �ӵ� ���������� ����
                   planetRigidbody.velocity *= decelerationRate;
                   print("a");
               }*/
        if (isGravityActive)
        {
            Vector2 directionToLandingSpot = (landingSpot.transform.position - transform.position).normalized;
            float distance = Vector2.Distance(transform.position, landingSpot.transform.position);

            // �Ÿ��� �ݺ���ϴ� �߷��� ����
            float gravityForce = gravityStrength / distance; // �Ÿ��� �پ����� �߷� ����
            planetRigidbody.AddForce(directionToLandingSpot * gravityForce);

            // �ӵ� ���� ���� �߰�
            if (planetRigidbody.velocity.magnitude > 1f) // �ӵ��� �ʹ� �������� ���ߵ��� ��
            {
                planetRigidbody.velocity *= decelerationRate;
            }
        }
    }

/*    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == landingSpot)
        {
            // LandingSpot�� ������ �ӵ��� 0���� �����ϰ� �޶����
            planetRigidbody.velocity = Vector2.zero;
            planetRigidbody.isKinematic = true;
            transform.position = collision.contacts[0].point;
            isGravityActive = false;
        }
    }*/
}
