using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class CameraMove : MonoBehaviour
{


    // 프레임마다 호출되는 업데이트 매서드
    void Update()
    {
        if (SlingShot.slingshotState == SlingshotState.Idle && GameManager.CurrentGameState == GameState.Playing)
        {
            //마우스 왼쪽 버튼 누르면 드래그 시작
            if (Input.GetMouseButtonDown(0))
            {
                
                timeDragStarted = Time.time;    //시간 기록
                dragSpeed = 0f;                 // 속도 초기화
                previousPosition = Input.mousePosition; //마우스 현재 위치 저장
            }
            

            // 마우스 누르고 있고 드래그 시작후 0.05초 이상 경과했을때 드래그 인정
            else if (Input.GetMouseButton(0) && Time.time - timeDragStarted > 0.05f)
            {
                //이전 위칭촤 현재위치 차이를 이용해 이동량 계산
                Vector3 input = Input.mousePosition;
                float deltaX = (previousPosition.x - input.x)  * dragSpeed;
                float deltaY = (previousPosition.y - input.y) * dragSpeed;

                //이동 후 카메라 위치가 화면 경계를 넘지 않도록 제한
                float newX = Mathf.Clamp(transform.position.x + deltaX, 0, 13.36336f);
                float newY = Mathf.Clamp(transform.position.y + deltaY, 0, 2.715f);
                

                //매순간마다 카메라 위치 갱신
                transform.position = new Vector3(
                    newX,
                    newY,
                    transform.position.z);

                previousPosition = input;
                //드래그 속도 점진적으로 증가
                if(dragSpeed < 0.1f) dragSpeed += 0.002f;
            }
        }
    }

    //각 변수 설정
    //드래그 설정
    private float dragSpeed = 0.01f;
    
    //드래그 시작된 시간 저장 변수
    private float timeDragStarted;
    
    //프레임에서의 마우스 위치 저장
    private Vector3 previousPosition = Vector3.zero;

    //슬링샷 객체
    public SlingShot SlingShot;
}
