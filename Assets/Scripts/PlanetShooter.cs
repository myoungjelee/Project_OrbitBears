/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetShooter : MonoBehaviour
{
    public Rigidbody2D planetRigidbody;    // �༺�� ������ٵ�2D ������Ʈ
    // [SerializeField] Rigidbody2D planetRigidbody;
    public float forceMultiplier = 5f;   // ���� ���

    private Vector2 dragStartPosition;
    private Vector2 dragEndPosition;
    private bool isDragging = false;

    void Start()
    {

    }
    void Update()
    {
        // ���콺 Ŭ�� �Ҷ�
        if (Input.GetMouseButtonDown(0))  //(0) == ��Ŭ��
                                          //(1) == ��Ŭ��
        {
            // �巡�� ���� ��ġ ���
            dragStartPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }

        if (Input.GetMouseButtonUp(0))  // ���⿡ ������ void Start()�Ʒ��� �Լ��� ������ ���� ���۽ÿ�
                                          // ���콺�� ������ ���� �����̹Ƿ� ���� if���� �ٷ� ����� ��÷�
                                          // ����� Ŀ�ǵ� �̱⿡ Update�� �Ű���
        {
            dragEndPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = false;
            
            Vector2 dragVector = dragStartPosition - dragEndPosition; // ���콺 Ŭ��ON ��ǥ - Ŭ��OFF ��ǥ �� �ؼ� �����
            Vector2 direction = dragVector.normalized;  // �߻���� ���
            float dragDistance = dragVector.magnitude;  // �巡�� �Ÿ� ���

            planetRigidbody.AddForce(direction * dragDistance * forceMultiplier, ForceMode2D.Impulse);

        }
    }
}
*/