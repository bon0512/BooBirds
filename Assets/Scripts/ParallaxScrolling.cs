using UnityEngine;
using System.Collections;

public class ParallaxScrolling : MonoBehaviour
{
    // 메인 카메라 참조
    Camera camera;

    // 카메라 이전 위치 저장용
    Vector3 previousCameraTransform;

    // 카메라 이동 시 배경이 덜 따라오게 만드는 배수
    public float ParallaxFactor;

    // 초기화 시점에 카메라 위치 기억
    void Start()
    {
        camera = Camera.main;
        previousCameraTransform = camera.transform.position;
    }

    // 매 프레임마다 카메라 이동에 따라 배경 이동
    void Update()
    {
        Vector3 delta = camera.transform.position - previousCameraTransform;
        delta.y = 0;
        delta.z = 0;

        transform.position += delta / ParallaxFactor;
        previousCameraTransform = camera.transform.position;
    }

    /// 참고한 배경 이미지 출처:
    /// http://opengameart.org/content/hd-multi-layer-parallex-background-samples-of-glitch-game-assets
}
