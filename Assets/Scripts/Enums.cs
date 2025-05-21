//상태 관련 열거형(enum) 정의
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts     // Assets 폴더 안의 Scrpits 폴더에 있는것임을 나타냄
{
    //Slingshot 상태
    //Slingshot.cs에서 사용 예정
    //Slingshot이 어떤 상태인지에 따라 다른 행동을 하도록 제어
    public enum SlingshotState
    {
        Idle,             // 슬링샷 대기상태
        UserPulling,      // 사용자가 새 잡고 드래그 상태
        BirdFlying        // 새가 발사되서 날아가고 있는 상태
    }

    //전체 흐름 상태
    public enum GameState
    {
        Start,                       // 게임 처음 시작
        BirdMovingToSlingshot,       // 새를 slingshot 위치로 이동
        Playing,                     // 목표물 파괴시 승리
        Won,                         // 기회 소진 or 목표 실패 -> 게임 오버
        Lost
    }


    public enum BirdState         //새 상태 관련
    {
        BeforeThrown,
        Thrown
    }
    
}