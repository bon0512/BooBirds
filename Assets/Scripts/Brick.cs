using UnityEngine;
using System.Collections;

/// <summary>
/// Brick (벽돌, 구조물)의 충돌 감지 및 데미지 처리 스크립트
/// 충돌한 물체의 속도를 기반으로 데미지를 계산하고,
/// 체력이 0 이하가 되면 오브젝트를 제거합니다.
/// </summary>
public class Brick : MonoBehaviour
{
    // Brick의 체력 (초기값: 70)
    public float Health = 70f;

    // 물체와 충돌했을 때 호출됨
    void OnCollisionEnter2D(Collision2D col)
    {
        // 충돌한 오브젝트에 Rigidbody2D가 없으면 충돌 무시
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        // 속도 기반 데미지 계산 (속도의 크기 × 10)
        float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10f;

        // 체력에서 데미지만큼 차감
        Health -= damage;

        // 체력이 0 이하이면 오브젝트 제거
        if (Health <= 0)
            Destroy(this.gameObject);
    }

    // 참고: 충돌 사운드 효과 (나무 부딪힘)
    // 출처: https://www.freesound.org/people/Srehpog/sounds/31623/
}
