using UnityEngine;
using System.Collections;

<<<<<<< HEAD
public class CameraFixAspectRatio : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        Camera camera = GetComponent<Camera>();
        float aspect = Mathf.Round(camera.aspect * 100f) / 100f;

        //this is to be altered different Windows Phone 8 aspect ratios
        //there should be a better way of doing this
        if (aspect == 0.6f) //WXGA or WVGA
            camera.orthographicSize = 5;
        else if (aspect == 0.56f) //720p
=======

//카메라 이동시 화면 깨짐 방지
public class CameraFixAspectRatio : MonoBehaviour
{

    // 시작
    void Start()
    {
        
        //객체에 연결된 카메라 컴포넌트 정보를 가져온 후 카메라의 종횡비를 소수 둘째까지 반올림
        Camera camera = GetComponent<Camera>();
        float aspect = Mathf.Round(camera.aspect * 100f) / 100f;

        //낮은 해상도에서도  깨짐 방지
        if (aspect == 0.6f) 
            camera.orthographicSize = 5;
        else if (aspect == 0.56f) 
>>>>>>> upstream/master
        {
            camera.orthographicSize = 4.6f;
        }


    }
}
