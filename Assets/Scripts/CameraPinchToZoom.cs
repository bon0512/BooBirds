using UnityEngine;
using System.Collections;

/// <summary>
/// 유니티 공식 튜토리얼 참고:
/// http://unity3d.com/pt/learn/tutorials/modules/beginner/platform-specific/pinch-zoom
/// 이 코드는 perspective와 orthographic 모드를 모두 다루지만,
/// 이 2D 게임에서는 orthographic 카메라만 사용합니다.
/// </summary>
public class CameraPinchToZoom : MonoBehaviour
{
    public float perspectiveZoomSpeed = 0.5f; // 원근 카메라일 때 시야각(FOV) 변화 속도
    public float orthoZoomSpeed = 0.5f;       // 직교 카메라일 때 화면 크기 변화 속도

    void Update()
    {
        // 화면을 터치한 손가락이 두 개일 때만 실행
        if (Input.touchCount == 2)
        {
            // 두 손가락의 현재 터치 정보 가져오기
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // 두 손가락의 이전 프레임 위치 계산
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // 이전 프레임과 현재 프레임 사이의 손가락 간 거리 계산
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // 두 거리 간 차이를 계산 (줌인/줌아웃 기준)
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // 카메라가 직교(Orthographic) 모드인 경우
            if (GetComponent<Camera>().orthographic)
            {
                // 손가락 거리 변화량에 따라 orthographicSize 조절
                GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // 카메라 크기가 너무 작거나 크지 않도록 제한
                GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize, 3f, 10f);
            }
            else // 원근(Perspective) 카메라인 경우
            {
                // FOV(Field of View)를 손가락 거리 변화에 따라 조절
                GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // FOV가 0.1 ~ 179.9 사이에 있도록 제한
                GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 0.1f, 179.9f);
            }
        }
    }
}
