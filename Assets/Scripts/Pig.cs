
// 적 (돼지)의 체력, 충돌 처리, 데미지 처리, 스프라이트 변경
using UnityEngine;
using System.Collections;

public class Pig : MonoBehaviour // MonoBehaviour 상속
{
    // 적 (돼지)의 체력 기본 150
    public float Health = 150f;
    // 체력이 일정 이하로 떨어졌을 때 바뀔 스프라이트
    public Sprite SpriteShownWhenHurt;
    // 스프라이트 바뀔 체력값 (기준)
    private float ChangeSpriteHealth;

    void Start() // 스프라이트 바꾸도록 기준 설정
    {
        ChangeSpriteHealth = Health - 30f; // 체력 30 줄어들면 스프라이트 변경
    }

    void OnCollisionEnter2D(Collision2D col)
    {	// 움직이지 않는 오브젝트 충돌 무시
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        // 새 (탄환, 부)가 부딪힌 경우
        if (col.gameObject.tag == "Bird")
        {	// 오브젝트 제거
            Destroy(gameObject);
        }
        else // 새가 아닌 다른 오브젝트에 부딪힌 경우
        {
            // 데미지= 속도 * 10
            float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;
            // 체력- 데미지
            Health -= damage;

            if (Health < ChangeSpriteHealth) // 체력이 스프라이트 바뀌는 기준값보다 낮으면
            {	// 스프라이트 변경
                GetComponent<SpriteRenderer>().sprite = SpriteShownWhenHurt;
            }
            // 체력이 0 이하가 되면 오브젝트 제거
            if (Health <= 0) Destroy(this.gameObject);
        }
    }


}


