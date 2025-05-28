using UnityEngine;
using System.Collections;

public class Pig : MonoBehaviour
{
    public float Health = 150f;
    public Sprite SpriteShownWhenHurt;
    private float ChangeSpriteHealth;

    void Start()
    {
        ChangeSpriteHealth = Health - 30f;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        // �浹�� ������Ʈ�� ���� ��� �ı�
        if (col.gameObject.tag == "Bird")
        {
            Destroy(gameObject);
        }
        else
        {
            // �ٸ� ������Ʈ�� �浹���� �� �ӵ��� ���� ������ ���
            float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
            Health -= damage;

            // ü���� Ư�� ���� ���Ϸ� �������� ��������Ʈ ����
            if (Health < ChangeSpriteHealth)
            {
                GetComponent<SpriteRenderer>().sprite = SpriteShownWhenHurt;
            }

            // ü���� 0 ������ ��� ����
            if (Health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
