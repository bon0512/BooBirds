using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;
using DG.Tweening;

public class SlingShot : MonoBehaviour
{
    private Vector3 SlingshotMiddleVector;
    //왼쪽과 오른쪽 새총 사이의 중앙 지점(조준 중심)

    [HideInInspector]
    public SlingshotState slingshotState;

    //슬링샷의 양쪽 끝 Transform(줄 시작점)
    public Transform LeftSlingshotOrigin, RightSlingshotOrigin;

    // 왼쪽/오른쪽 줄을 나타내는선(2줄 구성)
    public LineRenderer SlingshotLineRenderer1;
    public LineRenderer SlingshotLineRenderer2;
    
    //새가 날아갈 예상 경로 표시용 선
    public LineRenderer TrajectoryLineRenderer;
    
    [HideInInspector]
    //발사 대상인 새 - GameObject
    public GameObject BirdToThrow;

    //새가 대기할 위치
    public Transform BirdWaitPosition;

    //발사 속도
    public float ThrowSpeed;

    [HideInInspector]
    //새가 발사된 시간 기록 - 나중에 시간 계산에 사용
    public float TimeSinceThrown;

    // 함수 초기 설정
    void Start()
    {
        //각줄(LineRenderer)들이 화면 맨 위로 보이도록 "Foreground"로 설정
        SlingshotLineRenderer1.sortingLayerName = "Foreground";
        SlingshotLineRenderer2.sortingLayerName = "Foreground";
        TrajectoryLineRenderer.sortingLayerName = "Foreground";

        //슬링샷 상태를 "대기중(Idle)"로 설정
        slingshotState = SlingshotState.Idle;
        SlingshotLineRenderer1.SetPosition(0, LeftSlingshotOrigin.position);
        SlingshotLineRenderer2.SetPosition(0, RightSlingshotOrigin.position);

        //줄의 시작점(Position[0])을 양쪽 새총 끝에 고정
        SlingshotMiddleVector = new Vector3((LeftSlingshotOrigin.position.x + RightSlingshotOrigin.position.x) / 2,
            (LeftSlingshotOrigin.position.y + RightSlingshotOrigin.position.y) / 2, 0);
        //왼족과 오른쪽 사이의 중앙값을 계산해서 조준 기준점을 설정
    }

    // Update - 상태 기반 실행
    void Update()
    {
        switch (slingshotState)
        {
            case SlingshotState.Idle:
                //새의 위치 수정
                InitializeBird();
                //줄을 현재 새 위치까지 그리기
                DisplaySlingshotLineRenderers();
                if (Input.GetMouseButtonDown(0))
                {
                    //마우스를 클릭한 지점의 월드 좌표 구하기
                    Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //클릭한 위치가 새의 콜라이더 영역 안이면 -> 당기기 상태로 전환
                    if (BirdToThrow.GetComponent<CircleCollider2D>() == Physics2D.OverlapPoint(location))
                    {
                        slingshotState = SlingshotState.UserPulling;
                    }
                }
                break;
            //사용자가 당길때
            case SlingshotState.UserPulling:
                DisplaySlingshotLineRenderers();

                //줄을 계속 새 위치로 갱신
                if (Input.GetMouseButton(0))
                {
                    //마우스를 누르고 있는 동안 위치를 계속 추적
                    Vector3 location = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    location.z = 0;
                    //너무 멀리 당기지 않기 위한 거리 제한
                    //아니라면 그대로 위치 설정
                    if (Vector3.Distance(location, SlingshotMiddleVector) > 1.5f)
                    {
                        var maxPosition = (location - SlingshotMiddleVector).normalized * 1.5f + SlingshotMiddleVector;
                        BirdToThrow.transform.position = maxPosition;
                    }
                    else
                    {
                        BirdToThrow.transform.position = location;
                    }
                    float distance = Vector3.Distance(SlingshotMiddleVector, BirdToThrow.transform.position);
                    //당긴 거리 계산에 다른 궤적(trajectory) 표시
                    DisplayTrajectoryLineRenderer2(distance);
                }
                else 
                {
                    //마우스 버튼을 뗌 -> 궤적 숨김 -> 발사시간 기록
                    SetTrajectoryLineRenderesActive(false);
                    TimeSinceThrown = Time.time;
                    float distance = Vector3.Distance(SlingshotMiddleVector, BirdToThrow.transform.position);
                    
                    //일정거리 이상 당겼다면 ThrowBird 함수 실행
                    if (distance > 1)
                    {
                        SetSlingshotLineRenderersActive(false);
                        slingshotState = SlingshotState.BirdFlying;
                        ThrowBird(distance);
                    }
                    
                    else //너무 짧게 당겼다면 제자리로 되돌림
                    {
                        BirdToThrow.transform.DOMove(BirdWaitPosition.transform.position,
                        distance / 10).
                            OnComplete(() =>
                        {
                            InitializeBird();
                        });

                    }
                }
                break;
            case SlingshotState.BirdFlying:   //GameManager에서 처리
                break;
            default:
                break;
        }

    }

    //새 발사 처리
    private void ThrowBird(float distance)
    {
        //중앙에서 새 위치로 향하는 방향 벡터로 발사
        Vector3 velocity = SlingshotMiddleVector - BirdToThrow.transform.position;
        BirdToThrow.GetComponent<Bird>().OnThrow(); //make the bird aware of it
        //old and alternative way
        //BirdToThrow.GetComponent<Rigidbody2D>().AddForce
        //    (new Vector2(v2.x, v2.y) * ThrowSpeed * distance * 300 * Time.deltaTime);
        //set the velocity
        BirdToThrow.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x, velocity.y) * ThrowSpeed * distance;
        //거리, 속도 조합해서 실제 물리 속도로 발사

        if (BirdThrown != null)
            BirdThrown(this, EventArgs.Empty);
    }

    public event EventHandler BirdThrown;

    //새 대기 위치로 이동
    private void InitializeBird()
    {
        BirdToThrow.transform.position = BirdWaitPosition.position;
        slingshotState = SlingshotState.Idle;
        SetSlingshotLineRenderersActive(true);
    }

    //예상 경로 그리기
    //각 줄의 끝점(Position[1])을 새 위치에 따라 실시간으로 업데이트
    void DisplaySlingshotLineRenderers()
    {
        SlingshotLineRenderer1.SetPosition(1, BirdToThrow.transform.position);
        SlingshotLineRenderer2.SetPosition(1, BirdToThrow.transform.position);
    }

    void SetSlingshotLineRenderersActive(bool active)
    {
        SlingshotLineRenderer1.enabled = active;
        SlingshotLineRenderer2.enabled = active;
    }

    void SetTrajectoryLineRenderesActive(bool active)
    {
        TrajectoryLineRenderer.enabled = active;
    }


    /// <summary>
    /// Another solution (a great one) can be found here
    /// http://wiki.unity3d.com/index.php?title=Trajectory_Simulation
    /// </summary>
    /// <param name="distance"></param>
    /// 
    //예상경로 그리기
    void DisplayTrajectoryLineRenderer2(float distance)
    {
        SetTrajectoryLineRenderesActive(true);
        Vector3 v2 = SlingshotMiddleVector - BirdToThrow.transform.position;
        int segmentCount = 15;
        float segmentScale = 2;
        Vector2[] segments = new Vector2[segmentCount];

        // The first line point is wherever the player's cannon, etc is
        segments[0] = BirdToThrow.transform.position;

        // The initial velocity
        Vector2 segVelocity = new Vector2(v2.x, v2.y) * ThrowSpeed * distance;

        float angle = Vector2.Angle(segVelocity, new Vector2(1, 0));
        float time = segmentScale / segVelocity.magnitude;
        for (int i = 1; i < segmentCount; i++)
        {
            //x axis: spaceX = initialSpaceX + velocityX * time
            //y axis: spaceY = initialSpaceY + velocityY * time + 1/2 * accelerationY * time ^ 2
            //both (vector) space = initialSpace + velocity * time + 1/2 * acceleration * time ^ 2
            float time2 = i * Time.fixedDeltaTime * 5;
            segments[i] = segments[0] + segVelocity * time2 + 0.5f * Physics2D.gravity * Mathf.Pow(time2, 2);
        }

        TrajectoryLineRenderer.SetVertexCount(segmentCount);
        for (int i = 0; i < segmentCount; i++)
            TrajectoryLineRenderer.SetPosition(i, segments[i]);
    }



    ///http://opengameart.org/content/forest-themed-sprites
    ///forest sprites found on opengameart.com
    ///© 2012-2013 Julien Jorge <julien.jorge@stuff-o-matic.com>

}
