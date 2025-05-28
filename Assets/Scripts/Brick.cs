using UnityEngine;
using System.Collections;

/// <summary>
/// Brick (����, ������)�� �浹 ���� �� ������ ó�� ��ũ��Ʈ
/// �浹�� ��ü�� �ӵ��� ������� �������� ����ϰ�,
/// ü���� 0 ���ϰ� �Ǹ� ������Ʈ�� �����մϴ�.
/// </summary>
public class Brick : MonoBehaviour
{
    // Brick�� ü�� (�ʱⰪ: 70)
    public float Health = 70f;

    // ��ü�� �浹���� �� ȣ���
    void OnCollisionEnter2D(Collision2D col)
    {
        // �浹�� ������Ʈ�� Rigidbody2D�� ������ �浹 ����
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        // �ӵ� ��� ������ ��� (�ӵ��� ũ�� �� 10)
        float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10f;

        // ü�¿��� ��������ŭ ����
        Health -= damage;

        // ü���� 0 �����̸� ������Ʈ ����
        if (Health <= 0)
            Destroy(this.gameObject);
    }

    // ����: �浹 ���� ȿ�� (���� �ε���)
    // ��ó: https://www.freesound.org/people/Srehpog/sounds/31623/
}
