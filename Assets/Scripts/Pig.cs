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

        // 충돌한 오브젝트가 새일 경우 파괴
        if (col.gameObject.tag == "Bird")
        {
            Destroy(gameObject);
        }
        else
        {
            // 다른 오브젝트와 충돌했을 때 속도에 따른 데미지 계산
            float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
            Health -= damage;

            // 체력이 특정 기준 이하로 떨어지면 스프라이트 변경
            if (Health < ChangeSpriteHealth)
            {
                GetComponent<SpriteRenderer>().sprite = SpriteShownWhenHurt;
            }

            // 체력이 0 이하일 경우 제거
            if (Health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
