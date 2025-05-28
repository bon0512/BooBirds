// 유니티에서 제공하는 기본 기능 (GameObject, Rigidbody2D, MonoBehaviour 등)을 사용하기 위한 네임스페이스
using UnityEngine;

// 코루틴을 위한 C# 기본 컬렉션 네임스페이스 (IEnumerator 등)
using System.Collections;

// 프로젝트 내부에서 정의한 enum과 상수를 사용하기 위한 사용자 정의 네임스페이스
using Assets.Scripts;

// 이 컴포넌트가 붙은 오브젝트에 반드시 Rigidbody2D가 있어야 한다는 조건 지정
[RequireComponent(typeof(Rigidbody2D))]
public class Bird : MonoBehaviour
{
    // 새의 상태 (던지기 전 / 던져진 후)
    public BirdState State { get; private set; }

    // 유니티에서 오브젝트 생성 시 호출되는 초기화 함수
    void Start()
    {
        // 트레일 렌더러 비활성화 (처음엔 잔상 없음)
        GetComponent<TrailRenderer>().enabled = false;

        // 트레일 렌더러가 앞쪽 레이어에 보이도록 설정
        GetComponent<TrailRenderer>().sortingLayerName = "Foreground";

        // Rigidbody2D에 중력 미적용 (처음에는 멈춘 상태 유지)
        GetComponent<Rigidbody2D>().isKinematic = true;

        // 터치 및 드래그를 쉽게 하기 위해 콜라이더 반지름을 크게 설정
        GetComponent<CircleCollider2D>().radius = Constants.BirdColliderRadiusBig;

        // 상태 초기화: 아직 던지지 않음
        State = BirdState.BeforeThrown;
    }

    // 물리 연산용 업데이트 (프레임마다 호출됨)
    void FixedUpdate()
    {
        // 새가 던져진 상태이고, 속도가 거의 0에 가까우면
        if (State == BirdState.Thrown &&
            GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= Constants.MinVelocity)
        {
            // 2초 뒤에 새 오브젝트 제거
            StartCoroutine(DestroyAfter(2));
        }
    }

    // 새가 던져졌을 때 호출되는 함수
    public void OnThrow()
    {
        // 트레일 렌더러 활성화 (잔상 보이기 시작)
        GetComponent<TrailRenderer>().enabled = true;

        // 중력 적용 시작
        GetComponent<Rigidbody2D>().isKinematic = false;

        // 콜라이더 반지름을 정상 크기로 설정
        GetComponent<CircleCollider2D>().radius = Constants.BirdColliderRadiusNormal;

        // 상태 변경: 던져진 상태로
        State = BirdState.Thrown;
    }

    // 일정 시간 후 새 오브젝트 제거 (코루틴)
    IEnumerator DestroyAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
