<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections;

public class ParallaxScrolling : MonoBehaviour {

	// Use this for initialization
	void Start () {
        camera = Camera.main;
        previousCameraTransform = camera.transform.position;
	}

    Camera camera;
	
	/// <summary>
	/// similar tactics just like the "CameraMove" script
	/// </summary>
	void Update () {
        Vector3 delta = camera.transform.position - previousCameraTransform;
        delta.y = 0; delta.z = 0;
        transform.position += delta / ParallaxFactor;


        previousCameraTransform = camera.transform.position;
	}

    public float ParallaxFactor;

    Vector3 previousCameraTransform;

    ///background graphics found here:
    ///http://opengameart.org/content/hd-multi-layer-parallex-background-samples-of-glitch-game-assets
=======
﻿// 유니티의 기본 클래스, 컴포넌트, 벡터 등을 사용하기 위한 네임스페이스
using UnityEngine;

using System.Collections;

public class ParallaxScrolling : MonoBehaviour
{
    // Unity에서 가장 먼저 실행되는 초기화 함수
    // 메인 카메라의 Transform을 저장하고, 이전 위치 기록 시작
    void Start()
    {
        camera = Camera.main;
        previousCameraTransform = camera.transform.position;
    }

    // 메인 카메라 참조용 변수
    Camera camera;

    // 매 프레임마다 호출되어 배경을 카메라 이동에 따라 천천히 움직임
    void Update()
    {
        // 카메라가 이동한 거리 계산 (y, z 방향은 무시) delta = 현재 위치 - 이전 위치
        Vector3 delta = camera.transform.position - previousCameraTransform;
        delta.y = 0; // y축 z축 이동 무시
        delta.z = 0;

        // 배경을 ParallaxFactor에 따라 덜 움직이도록 조절
        transform.position += delta / ParallaxFactor;

        // 현재 카메라 위치를 다음 계산을 위해 저장
        previousCameraTransform = camera.transform.position;
    }

    // 배경이 얼마나 느리게 따라갈지 결정하는 비율 (값이 클수록 느림)
    public float ParallaxFactor;

    // 이전 프레임에서의 카메라 위치 저장용
    Vector3 previousCameraTransform;

    // 참고한 배경 이미지 출처:
    // http://opengameart.org/content/hd-multi-layer-parallex-background-samples-of-glitch-game-assets
>>>>>>> upstream/master
}
