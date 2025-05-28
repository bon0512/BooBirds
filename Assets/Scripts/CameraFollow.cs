using UnityEngine;
using Assets.Scripts; // SlingshotState enum 등의 정의가 포함된 네임스페이스 참조

/// <summary>
/// 카메라가 새를 따라가도록 하는 스크립트
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [HideInInspector]
    public bool IsFollowing;            // 새를 따라갈지 여부
    [HideInInspector]
    public Transform BirdToFollow;      // 따라갈 새의 Transform
    public SlingShot slingShot;         // 슬링샷 오브젝트 참조 (인스펙터에서 할당)

    private const float minCameraX = 0f;     // 카메라가 이동할 수 있는 최소 x값
    private const float maxCameraX = 30f;    // 카메라가 이동할 수 있는 최대 x값

    public Vector3 StartingPosition;   // 초기 카메라 위치 저장용

    void Start()
    {
        StartingPosition = transform.position;
    }

    void Update()
    {
        if (IsFollowing && BirdToFollow != null)
        {
            // 새가 발사된 상태일 때만 카메라가 따라감
            if (slingShot.slingshotState == SlingshotState.BirdFlying)
            {
                // 새의 x 좌표를 카메라 이동 제한 범위 내에서 유지
                float x = Mathf.Clamp(BirdToFollow.position.x, minCameraX, maxCameraX);

                // 카메라 x만 새 위치로 이동 (y, z는 고정)
                transform.position = new Vector3(x, StartingPosition.y, StartingPosition.z);
            }
        }
    }
}
