using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;
using DG.Tweening;

public class SlingShot : MonoBehaviour
{
    [HideInInspector]
    public SlingshotState slingshotState;

    public Transform LeftSlingshotOrigin, RightSlingshotOrigin; //슬링샷의 양쪽 끝 Transform(줄 시작점)
    public LineRenderer SlingshotLineRenderer1; // 왼쪽 줄
    public LineRenderer SlingshotLineRenderer2; // 오른쪽 줄
    public LineRenderer TrajectoryLineRenderer; // 새가 날아갈 예상 경로 표시용 선

    [HideInInspector]
    public GameObject BirdToThrow; // 발사 대상인 새 - GameObject

    public Transform BirdWaitPosition; // 새가 대기할 위치
    public float ThrowSpeed; // 발사 속도

    [HideInInspector]
    public float TimeSinceThrown; // 새가 발사된 시간 기록

    private Vector3 birdDrawPosition; // 줄 그리기에 사용할 안정된 좌표값

    // 함수 초기 설정
    void Start()
    {
        // 각 줄(LineRenderer)들이 화면 맨 위로 보이도록 "Foreground"로 설정
        SlingshotLineRenderer1.sortingLayerName = "Foreground";
        SlingshotLineRenderer2.sortingLayerName = "Foreground";
        TrajectoryLineRenderer.sortingLayerName = "Foreground";

        // 슬링샷 상태를 "대기중(Idle)"으로 설정
        slingshotState = SlingshotState.Idle;

        // 줄의 시작점(Position[0])을 양쪽 새총 끝에 고정
        SlingshotLineRenderer1.SetPosition(0, LeftSlingshotOrigin.position);
        SlingshotLineRenderer2.SetPosition(0, RightSlingshotOrigin.position);

        birdDrawPosition = BirdWaitPosition.position; // 초기 위치
    }

    // 상태 기반 실행
    void Update()
    {
        switch (slingshotState)
        {
            case SlingshotState.Idle:
                InitializeBird(); // 새 위치 초기화
                DisplaySlingshotLineRenderers(); // 줄 렌더러 갱신
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    // 클릭한 위치가 새의 콜라이더 영역 안이면 -> 당기기 상태로 전환
                    if (BirdToThrow.GetComponent<CircleCollider2D>() == Physics2D.OverlapPoint(location))
                    {
                        slingshotState = SlingshotState.UserPulling;
                    }
                }
                break;

            case SlingshotState.UserPulling:
                DisplaySlingshotLineRenderers();

                if (Input.GetMouseButton(0)) // 마우스를 누르고 있는 동안 위치 추적
                {
                    Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    location.z = 0;
                    Vector3 middle = GetSlingshotMiddleVector(); // 실시간으로 중심 위치 계산

                    // 너무 멀리 당기지 않기 위한 거리 제한
                    if (Vector3.Distance(location, middle) > 1.5f)
                    {
                        var maxPosition = (location - middle).normalized * 1.5f + middle;
                        BirdToThrow.transform.position = maxPosition;
                        birdDrawPosition = maxPosition; // 안정된 위치로 설정
                    }
                    else
                    {
                        BirdToThrow.transform.position = location;
                        birdDrawPosition = location;
                    }

                    float distance = Vector3.Distance(middle, BirdToThrow.transform.position);
                    DisplayTrajectoryLineRenderer2(distance); // 궤적 표시
                }
                else // 마우스 버튼을 뗌
                {
                    SetTrajectoryLineRenderesActive(false);
                    TimeSinceThrown = Time.time;
                    Vector3 middle = GetSlingshotMiddleVector();
                    float distance = Vector3.Distance(middle, BirdToThrow.transform.position);

                    if (distance > 1) // 일정 거리 이상 당겼다면 발사
                    {
                        SetSlingshotLineRenderersActive(false);
                        slingshotState = SlingshotState.BirdFlying;
                        ThrowBird(distance, middle);
                    }
                    else // 너무 짧게 당겼다면 제자리로 되돌림
                    {
                        BirdToThrow.transform.DOMove(BirdWaitPosition.transform.position, distance / 10f)
                            .OnUpdate(() =>
                            {
                                birdDrawPosition = BirdToThrow.transform.position; // 애니메이션 중에도 안정된 위치로 갱신
                            })
                            .OnComplete(() =>
                            {
                                InitializeBird(); // 새 위치 복귀
                                slingshotState = SlingshotState.Idle; // 상태 복귀
                                GameManager.CurrentGameState = GameState.Playing;
                            });
                    }
                }
                break;

            case SlingshotState.BirdFlying:
                break;
        }
    }

    // 왼쪽과 오른쪽 새총 사이의 중앙 지점(조준 중심) 계산 함수
    private Vector3 GetSlingshotMiddleVector()
    {
        return new Vector3(
            (LeftSlingshotOrigin.position.x + RightSlingshotOrigin.position.x) / 2,
            (LeftSlingshotOrigin.position.y + RightSlingshotOrigin.position.y) / 2,
            0);
    }

    // 새 발사 처리
    private void ThrowBird(float distance, Vector3 middle)
    {
        Vector3 velocity = middle - BirdToThrow.transform.position; // 방향 벡터 계산
        BirdToThrow.GetComponent<Bird>().OnThrow(); // 새 상태 변경
        BirdToThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, velocity.y) * ThrowSpeed * distance; // 속도 설정

        if (BirdThrown != null)
            BirdThrown(this, EventArgs.Empty); // 이벤트 호출
    }

    public event EventHandler BirdThrown;

    // 새 위치 초기화
    private void InitializeBird()
    {
        BirdToThrow.transform.position = BirdWaitPosition.position;
        birdDrawPosition = BirdWaitPosition.position;
        slingshotState = SlingshotState.Idle;
        SetSlingshotLineRenderersActive(true);
    }

    // 양쪽 줄 끝점 새 위치로 갱신
    void DisplaySlingshotLineRenderers()
    {
        SlingshotLineRenderer1.SetPosition(0, LeftSlingshotOrigin.position);  // ← 줄 시작점 항상 갱신
        SlingshotLineRenderer1.SetPosition(1, birdDrawPosition); // ← 안정된 위치 사용

        SlingshotLineRenderer2.SetPosition(0, RightSlingshotOrigin.position); // ← 줄 시작점 항상 갱신
        SlingshotLineRenderer2.SetPosition(1, birdDrawPosition); // ← 안정된 위치 사용
    }

    // 줄 활성/비활성
    void SetSlingshotLineRenderersActive(bool active)
    {
        SlingshotLineRenderer1.enabled = active;
        SlingshotLineRenderer2.enabled = active;
    }

    // 궤적 선 활성/비활성
    void SetTrajectoryLineRenderesActive(bool active)
    {
        TrajectoryLineRenderer.enabled = active;
    }

    // 예상 궤적 그리기
    void DisplayTrajectoryLineRenderer2(float distance)
    {
        SetTrajectoryLineRenderesActive(true);
        Vector3 middle = GetSlingshotMiddleVector();
        Vector3 v2 = middle - birdDrawPosition; // ← 안정된 위치 사용
        int segmentCount = 15;
        float segmentScale = 2;
        Vector2[] segments = new Vector2[segmentCount];

        segments[0] = birdDrawPosition;
        Vector2 segVelocity = new Vector2(v2.x, v2.y) * ThrowSpeed * distance;
        float time = segmentScale / segVelocity.magnitude;

        for (int i = 1; i < segmentCount; i++)
        {
            float time2 = i * Time.fixedDeltaTime * 5;
            segments[i] = segments[0] + segVelocity * time2 + 0.5f * Physics2D.gravity * Mathf.Pow(time2, 2);
        }

        TrajectoryLineRenderer.SetVertexCount(segmentCount);
        for (int i = 0; i < segmentCount; i++)
            TrajectoryLineRenderer.SetPosition(i, segments[i]);
    }
}
