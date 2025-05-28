using UnityEngine;
<<<<<<< HEAD
using Assets.Scripts; // ← 이게 필요할 수 있음 (SlingShot enum 등 정의 위치)

public class CameraFollow : MonoBehaviour
{
    public Vector3 StartingPosition;

    [HideInInspector]
    public bool IsFollowing;

    [HideInInspector]
    public Transform BirdToFollow;

    public SlingShot slingShot; // ← 슬링샷 참조를 인스펙터에서 할당

    private const float minCameraX = 0;
    private const float maxCameraX = 30f;

=======
using System.Collections;


//카메라가 새 위치를 따라가는 코드
public class CameraFollow : MonoBehaviour
{

    // 초기 카메라 위치 저장
>>>>>>> upstream/master
    void Start()
    {
        StartingPosition = transform.position;
    }

<<<<<<< HEAD
    void Update()
    {
        if (IsFollowing && BirdToFollow != null)
        {
            // 슬링샷에서 발사된 상태일 때만 따라감
            if (slingShot.slingshotState == SlingshotState.BirdFlying)
            {
                float x = Mathf.Clamp(BirdToFollow.position.x, minCameraX, maxCameraX);
                transform.position = new Vector3(x, StartingPosition.y, StartingPosition.z);
            }
        }
    }
=======
    // 매 프레임마다 업데이트
    void Update()
    {
        // 새를 추적중인경우
        if (IsFollowing)
        {
            if (BirdToFollow != null) // 새가 장면 밖으로 나가면 제거 되기 때문에 null로 조건 처리를 해주어야한다.
            {
                var birdPosition = BirdToFollow.transform.position;
                
                //새의 위치x를 카메라 이동범위 안으로 제한해 화면 밖으로 튀어나가지 않도록한다.
                float x = Mathf.Clamp(birdPosition.x, minCameraX, maxCameraX);
                

                //카메라 x 위치를 새의 위치로 업데이트 x부분만 업데이트 하고 y,z는 그대로 유지
                transform.position = new Vector3(x, StartingPosition.y, StartingPosition.z);
            }
            else
                IsFollowing = false;    //새가 사라졌으면 추적 중단.
        }
    }

    //처음 카메라의 위치를 저장해두는 변수이다.
    [HideInInspector]
    public Vector3 StartingPosition;


    //각 변수
    private const float minCameraX = 0; //카메라 이동 최소값

    private const float maxCameraX = 13;    //카메라 이동 최대값 각 설정에 따라 다르게 적용가능
    
    [HideInInspector]   //카메라가 새를 추적할 지 여부
    public bool IsFollowing;
    [HideInInspector]   //추적할 새의 트랜스폼
    public Transform BirdToFollow;
>>>>>>> upstream/master
}
