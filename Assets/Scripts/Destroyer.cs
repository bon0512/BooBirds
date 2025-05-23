// 경계에 있는 오브젝트가 화면 밖으로 나가는면 해당 오브젝트 제거

using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{ // MonoBehaviour 상속

    // Collider2D가 isTrigger로 설정된 오브젝트에 닿았을 때 호출
    // col: 닿은 오브젝트의 정보
    void OnTriggerEnter2D(Collider2D col)

    {
        string tag = col.gameObject.tag; // tag= 충돌한 오브젝트의 태그 
        if (tag == "Bird" || tag == "Pig" || tag == "Brick")
        // 만약 tag가 Bird, Pig, Brick 중 하나라면 
        // 해당 오브젝트 제거
        {
            Destroy(col.gameObject);
        }
    }
}
