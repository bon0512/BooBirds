using UnityEngine;
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

    void Start()
    {
        StartingPosition = transform.position;
    }

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
}
