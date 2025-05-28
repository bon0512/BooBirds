using UnityEngine;
using System.Collections;

/// <summary>
/// 카메라의 화면 비율(aspect ratio)에 따라 적절한 orthographicSize를 자동 설정해
/// 다양한 해상도에서도 화면 깨짐 없이 보여주도록 조정하는 스크립트입니다.
/// </summary>
public class CameraFixAspectRatio : MonoBehaviour
{
    void Start()
    {
        // 현재 오브젝트에 연결된 Camera 컴포넌트 가져오기
        Camera camera = GetComponent<Camera>();

        // 카메라의 가로세로 비율(aspect)을 소수점 둘째자리까지 반올림
        float aspect = Mathf.Round(camera.aspect * 100f) / 100f;

        // 다양한 해상도(WXGA, WVGA, 720p 등)에 따라 카메라 크기 조정
        if (aspect == 0.6f) // 예: WXGA 또는 WVGA 해상도
            camera.orthographicSize = 5f;
        else if (aspect == 0.56f) // 예: 720p 해상도
            camera.orthographicSize = 4.6f;
    }
}
