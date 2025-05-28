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
    // 시작 시 초기 설정을 수행하는 함수 (유니티의 Start 이벤트 함수)
    void Start()
    {
        // 처음엔 트레일(잔상) 표시 안 함
        GetComponent<TrailRenderer>().enabled = false;

        // 트레일 렌더러의 출력 레이어를 앞쪽으로 설정
        GetComponent<TrailRenderer>().sortingLayerName = "Foreground";

        // 처음에는 중력 적용되지 않도록 설정 (공중에 정지 상태 유지)
        GetComponent<Rigidbody2D>().isKinematic = true;

        // 터치가 쉽도록 콜라이더 반지름 크게 설정
        GetComponent<CircleCollider2D>().radius = Constants.BirdColliderRadiusBig;

        // 새의 상태를 "던지기 전" 상태로 초기화
        State = BirdState.BeforeThrown;
    }

    // 물리 업데이트 함수 (프레임마다 호출되며 중력, 속도 계산에 적합)
    void FixedUpdate()
    {
        // 만약 새가 이미 던져졌고, 거의 멈춘 상태라면
        if (State == BirdState.Thrown &&
            GetComponent<Rigidbody2D>().velocity.sqrMagnitude <= Constants.MinVelocity)
        {
            // 2초 후에 새를 제거
            StartCoroutine(DestroyAfter(2));
        }
    }

    // 새가 던져졌을 때 호출되는 메서드
    public void OnThrow()
<<<<<<< HEAD
    {   
        //show the trail renderer
=======
    {
        // 사운드 재생
        GetComponent<AudioSource>().Play();

        // 트레일 렌더링 시작
>>>>>>> upstream/master
        GetComponent<TrailRenderer>().enabled = true;

        // 중력 적용 시작
        GetComponent<Rigidbody2D>().isKinematic = false;

        // 콜라이더 크기를 정상 크기로 설정
        GetComponent<CircleCollider2D>().radius = Constants.BirdColliderRadiusNormal;

        // 상태를 던져진 상태로 변경
        State = BirdState.Thrown;
    }

    // 일정 시간 후 오브젝트 제거하는 코루틴
    IEnumerator DestroyAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

    // 현재 새의 상태 (던지기 전/후)를 저장하는 프로퍼티
    public BirdState State
    {
        get;
        private set;
    }
}

    
