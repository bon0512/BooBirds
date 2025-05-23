using UnityEngine;
using System.Collections;


/// <summary>
/// 유니티 공식문서에서 참고하여 작성함 모바일 버전
/// http://unity3d.com/pt/learn/tutorials/modules/beginner/platform-specific/pinch-zoom
/// 2D 게임에서는 orthograpic 카메라 사용
/// </summary>
public class CameraPinchToZoom : MonoBehaviour
{
    public float perspectiveZoomSpeed = 0.5f;        //원근 카메라일 경우 field of view 변경속도
    public float orthoZoomSpeed = 0.5f;        // 직교 카메라일 경우 orthograpic size 변경 속도


    void Update()
    {
        // 너치가 2개일 때만 작동
        if (Input.touchCount == 2)
        {
            // 첫번째 터치 두번째 터치 객체 가져오기
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // 이전 프레임 터치의 위치 계산
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // 이전프레임과 현재프레임 손가락 사이 거리 계산
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // 두 프레임간 거리 차이
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // 카메라가 orthographic 모드.
            if (GetComponent<Camera>().orthographic)
            {
                //거리차이 계산후 줌 인 아웃
                GetComponent<Camera>().orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // 최소 최대 크기 제한
                GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize, 3f, 5f);
            }
            else //perspective 모드 우리는 orthographic 모드이기 때문에 예외처리만 작성
            {
                // fov 시야각 조정
                GetComponent<Camera>().fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // 최소/최대 시야각 제한
                GetComponent<Camera>().fieldOfView = Mathf.Clamp(GetComponent<Camera>().fieldOfView, 0.1f, 179.9f);
            }
        }
    }
}