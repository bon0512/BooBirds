<<<<<<< HEAD
癤퓎sing UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour
{


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;

        Health -= damage;

        if (Health <= 0)
            Destroy(this.gameObject);
    }
    public float Health = 70f;


    //wood sound found in 
    //https://www.freesound.org/people/Srehpog/sounds/31623/
}
=======
// Brick (구조물) 충돌 처리, 데미지 시스템 구현
using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour // MonoBehaviour 상속
{

    // 2D 물리 엔진에서 충돌이 발생했을 때 호출되는 함수
    // col: 충돌한 상대 오브젝트에 대한 정보를 담고 있음
    void OnCollisionEnter2D(Collision2D col)

    {
        // 충돌한 오브젝트에 Rigidbody2D 컴포넌트가 없으면 return
        if (col.gameObject.GetComponent<Rigidbody2D>() == null) return;

        // 데미지 계산, 데미지= 속도의 크기* 10
        float damage = col.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude * 10;

        // 데미지만큼 체력 차감
        Health -= damage;

        // 체력이 0 이하가 되면 오브젝트 제거
        if (Health <= 0) Destroy(this.gameObject);

    }

    // 벽돌 체력, 0 이하면 오브젝트 제거
    public float Health = 70f;


}

>>>>>>> upstream/master
