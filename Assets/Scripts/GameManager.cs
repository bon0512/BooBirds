<<<<<<< HEAD
﻿using UnityEngine;
using System.Collections.Generic;
using Assets.Scripts;
using System.Linq;
using DG.Tweening;

public class GameManager : MonoBehaviour
{

    public CameraFollow cameraFollow;
    int currentBirdIndex;
    public SlingShot slingshot;
    [HideInInspector]
    public static GameState CurrentGameState = GameState.Start;
    private List<GameObject> Bricks;
    private List<GameObject> Birds;
    private List<GameObject> Pigs;

    // Use this for initialization
    void Start()
    {
        CurrentGameState = GameState.Start;
        slingshot.enabled = false;
        //find all relevant game objects
        Bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
        Birds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bird"));
        Pigs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pig"));
        //unsubscribe and resubscribe from the event
        //this ensures that we subscribe only once
        slingshot.BirdThrown -= Slingshot_BirdThrown; slingshot.BirdThrown += Slingshot_BirdThrown;
    }


    // Update is called once per frame
    void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.Start:
                //if player taps, begin animating the bird 
                //to the slingshot
                if (Input.GetMouseButtonUp(0))
                {
                    AnimateBirdToSlingshot();
                }
                break;
            case GameState.BirdMovingToSlingshot:
                //do nothing
                break;
            case GameState.Playing:
                //if we have thrown a bird
                //and either everything has stopped moving
                //or there has been 5 seconds since we threw the bird
                //animate the camera to the start position
                if (slingshot.slingshotState == SlingshotState.BirdFlying &&
                    (BricksBirdsPigsStoppedMoving() || Time.time - slingshot.TimeSinceThrown > 5f))
                {
                    slingshot.enabled = false;
                    AnimateCameraToStartPosition();
                    CurrentGameState = GameState.BirdMovingToSlingshot;
                }
                break;
            //if we have won or lost, we will restart the level
            //in a normal game, we would show the "Won" screen 
            //and on tap the user would go to the next level
            case GameState.Won:
            case GameState.Lost:
                if (Input.GetMouseButtonUp(0))
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// A check whether all Pigs are null
    /// i.e. they have been destroyed
    /// </summary>
    /// <returns></returns>
    private bool AllPigsDestroyed()
    {
        return Pigs.All(x => x == null);
    }

    /// <summary>
    /// Animates the camera to the original location
    /// When it finishes, it checks if we have lost, won or we have other birds
    /// available to throw
    /// </summary>
    private void AnimateCameraToStartPosition()
    {
        float duration = Vector2.Distance(Camera.main.transform.position, cameraFollow.StartingPosition) / 10f;
        if (duration == 0.0f) duration = 0.1f;
        //animate the camera to start
        Camera.main.transform.DOMove(cameraFollow.StartingPosition, duration). //end position
            OnComplete(() =>
                        {
                            cameraFollow.IsFollowing = false;
                            if (AllPigsDestroyed())
                            {
                                CurrentGameState = GameState.Won;
                            }
                            //animate the next bird, if available
                            else if (currentBirdIndex == Birds.Count - 1)
                            {
                                //no more birds, go to finished
                                CurrentGameState = GameState.Lost;
                            }
                            else
                            {
                                slingshot.slingshotState = SlingshotState.Idle;
                                //bird to throw is the next on the list
                                currentBirdIndex++;
                                AnimateBirdToSlingshot();
                            }
                        });
    }

    /// <summary>
    /// Animates the bird from the waiting position to the slingshot
    /// </summary>
    void AnimateBirdToSlingshot()
    {
        CurrentGameState = GameState.BirdMovingToSlingshot;
        Birds[currentBirdIndex].transform.DOMove
            (slingshot.BirdWaitPosition.transform.position, //final position
            Vector2.Distance(Birds[currentBirdIndex].transform.position / 10,
            slingshot.BirdWaitPosition.transform.position) / 10). //position
                OnComplete(() =>
                        {   CurrentGameState = GameState.Playing;
                            slingshot.enabled = true; //enable slingshot
                            //current bird is the current in the list
                            slingshot.BirdToThrow = Birds[currentBirdIndex];
                        });
    }

    /// <summary>
    /// Event handler, when the bird is thrown, camera starts following it
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void Slingshot_BirdThrown(object sender, System.EventArgs e)
    {
        cameraFollow.BirdToFollow = Birds[currentBirdIndex].transform;
        cameraFollow.IsFollowing = true;
    }

    /// <summary>
    /// Check if all birds, pigs and bricks have stopped moving
    /// </summary>
    /// <returns></returns>
    bool BricksBirdsPigsStoppedMoving()
    {
        foreach (var item in Bricks.Union(Birds).Union(Pigs))
        {
            if (item != null && item.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > Constants.MinVelocity)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Found here
    /// http://www.bensilvis.com/?p=500
    /// </summary>
    /// <param name="screenWidth"></param>
    /// <param name="screenHeight"></param>
    public static void AutoResize(int screenWidth, int screenHeight)
    {
        Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight);
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f));
    }

    /// <summary>
    /// Shows relevant GUI depending on the current game state
    /// </summary>
    void OnGUI()
    {
        AutoResize(800, 480);
        switch (CurrentGameState)
        {
            case GameState.Start:
                GUI.Label(new Rect(0, 150, 200, 100), "Tap the screen to start");
                break;
            case GameState.Won:
                GUI.Label(new Rect(0, 150, 200, 100), "You won! Tap the screen to restart");
                break;
            case GameState.Lost:
                GUI.Label(new Rect(0, 150, 200, 100), "You lost! Tap the screen to restart");
                break;
            default:
                break;
        }
    }


}
=======
﻿// 유니티의 기본 클래스와 기능을 사용하기 위한 네임스페이스
using UnityEngine;

// 리스트, 딕셔너리 등 제네릭 컬렉션을 사용하기 위한 네임스페이스
using System.Collections.Generic;

// 프로젝트 내부 스크립트에 정의된 클래스 및 enum을 가져오기 위한 사용자 네임스페이스
using Assets.Scripts;

// 컬렉션을 조건 기반으로 처리할 수 있는 LINQ 기능을 사용하기 위한 네임스페이스
using System.Linq;

// DOTween 라이브러리의 트윈 애니메이션 기능을 사용하기 위한 네임스페이스
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    // 새가 날아간 후 따라갈 카메라를 제어하는 스크립트
    public CameraFollow cameraFollow;

    // 현재 사용할 새의 인덱스 (Birds 리스트에서 몇 번째 새인지)
    int currentBirdIndex;

    // 새를 발사하는 슬링샷 오브젝트
    public SlingShot slingshot;

    // 현재 게임 상태를 저장하는 정적 변수 (시작, 진행 중, 승리, 패배 등)
    [HideInInspector]
    public static GameState CurrentGameState = GameState.Start;

    // 게임 내에 존재하는 오브젝트들 저장용 리스트
    private List<GameObject> Bricks; // 부서질 수 있는 구조물
    private List<GameObject> Birds;  // 발사할 새들
    private List<GameObject> Pigs;   // 제거 대상인 적들

    // 게임 시작 시 호출
    void Start()
    {
        CurrentGameState = GameState.Start;
        slingshot.enabled = false; // 게임 시작 전엔 새 발사 비활성화

        // 태그를 이용해 씬에 있는 게임 오브젝트들을 찾아 리스트에 저장
        Bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
        Birds = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bird"));
        Pigs = new List<GameObject>(GameObject.FindGameObjectsWithTag("Pig"));

        // 새 발사 이벤트 중복 방지를 위해 등록 해제 후 재등록
        slingshot.BirdThrown -= Slingshot_BirdThrown;
        slingshot.BirdThrown += Slingshot_BirdThrown;
    }

    // 매 프레임마다 호출됨
    void Update()
    {
        switch (CurrentGameState)
        {
            case GameState.Start:
                // 시작 상태에서 마우스를 클릭하면 첫 번째 새가 슬링샷으로 이동
                if (Input.GetMouseButtonUp(0))
                    AnimateBirdToSlingshot();
                break;

            case GameState.BirdMovingToSlingshot:
                // 새가 이동 중이면 아무 작업도 하지 않음
                break;

            case GameState.Playing:
                // 새 발사 이후, 모든 오브젝트가 멈췄거나 5초 이상 지나면 다음 새 준비, 카메라 원래 위치
                if (slingshot.slingshotState == SlingshotState.BirdFlying &&
                    (BricksBirdsPigsStoppedMoving() || Time.time - slingshot.TimeSinceThrown > 5f))
                {
                    slingshot.enabled = false;
                    AnimateCameraToStartPosition(); // 카메라 복귀
                    CurrentGameState = GameState.BirdMovingToSlingshot;
                }
                break;

            case GameState.Won:
            case GameState.Lost:
                // 게임 끝났을 때 다시 클릭하면 현재 씬 재시작
                if (Input.GetMouseButtonUp(0))
                    Application.LoadLevel(Application.loadedLevel);  // 구버전 Unity용 API
                break;
        }
    }

    // 모든 돼지가 제거되었는지 확인
    private bool AllPigsDestroyed()
    {
        // 리스트 내의 모든 요소가 null인지 검사 (즉, 파괴된 경우)
        return Pigs.All(x => x == null);
    }

    // 카메라를 시작 위치로 되돌리는 애니메이션 함수
    private void AnimateCameraToStartPosition()
    {
        // 현재 위치와 시작 위치 사이 거리 비례로 애니메이션 시간 계산
        float duration = Vector2.Distance(Camera.main.transform.position, cameraFollow.StartingPosition) / 10f;
        if (duration == 0.0f) duration = 0.1f;

        // DOTween으로 카메라 이동
        Camera.main.transform.DOMove(cameraFollow.StartingPosition, duration)
            .OnComplete(() =>
            {
                cameraFollow.IsFollowing = false;

                // 모든 돼지를 제거했다면 승리
                if (AllPigsDestroyed())
                    CurrentGameState = GameState.Won;

                // 모든 새를 다 썼다면 패배
                else if (currentBirdIndex == Birds.Count - 1)
                    CurrentGameState = GameState.Lost;

                // 아직 새가 남았다면 다음 새 준비
                else
                {
                    slingshot.slingshotState = SlingshotState.Idle;
                    currentBirdIndex++;
                    AnimateBirdToSlingshot();
                }
            });
    }

    // 새를 대기 위치에서 슬링샷 위치로 이동하는 애니메이션 함수
    void AnimateBirdToSlingshot()
    {
        CurrentGameState = GameState.BirdMovingToSlingshot; // 게임 상태를 '새가 슬링샷으로 이동 중'으로 변경
                                                            // 현재 새를 슬링샷 대기 위치로 부드럽게 이동시킴 (DOTween 사용)
        Birds[currentBirdIndex].transform.DOMove(   
            slingshot.BirdWaitPosition.transform.position,
            Vector2.Distance(Birds[currentBirdIndex].transform.position, //이동거리 비례로 속도 조절(멀수록 오래걸림)
                             slingshot.BirdWaitPosition.transform.position) / 10f
        ).OnComplete(() =>    //이동이 끝난 후
        {
            CurrentGameState = GameState.Playing; //게임 플레이중으로 전환
            slingshot.enabled = true; //슬링샷 활성화
            slingshot.BirdToThrow = Birds[currentBirdIndex]; // 새를 슬링샷에 연결
        });
    }

    // 새가 발사되었을 때 카메라가 해당 새를 따라가도록 설정하는 이벤트 핸들러
    private void Slingshot_BirdThrown(object sender, System.EventArgs e)
    {
        cameraFollow.BirdToFollow = Birds[currentBirdIndex].transform;
        cameraFollow.IsFollowing = true;
    }

    // 게임 내 모든 물체가 멈췄는지 확인하는 함수
    bool BricksBirdsPigsStoppedMoving()
    {
        foreach (var item in Bricks.Union(Birds).Union(Pigs))
        {
            // 아직 움직이고 있는 오브젝트가 있다면 false
            if (item != null && item.GetComponent<Rigidbody2D>().velocity.sqrMagnitude > Constants.MinVelocity)
                return false;
        }
        return true;
    }

    // GUI 좌표계를 해상도에 맞게 자동으로 조절
    public static void AutoResize(int screenWidth, int screenHeight)
    {
        Vector2 resizeRatio = new Vector2((float)Screen.width / screenWidth, (float)Screen.height / screenHeight); // 현재 실제 화면 해상도와 기준 해상도의 비율 계산
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(resizeRatio.x, resizeRatio.y, 1.0f)); // 비율 적용한 스케일
    } 



    // 게임 상태에 따른 안내 메시지를 출력
    void OnGUI()
    {
        AutoResize(800, 480);  // 기준 해상도 설정

        switch (CurrentGameState)
        {
            case GameState.Start:
                GUI.Label(new Rect(0, 150, 200, 100), "Tap the screen to start"); // 게임 시작 전: 화면에 "시작하려면 터치하세요" 메시지 표시
                break;
            case GameState.Won:
                GUI.Label(new Rect(0, 150, 200, 100), "You won! Tap the screen to restart"); // 게임에서 승리했을 때: 재시작 안내 메시지 표시
                break;
            case GameState.Lost:
                GUI.Label(new Rect(0, 150, 200, 100), "You lost! Tap the screen to restart"); // 게임에서 패배했을 때: 재시작 안내 메시지 표시
                break;
        }
    }
}
>>>>>>> upstream/master
