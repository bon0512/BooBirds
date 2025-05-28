using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class CameraMove : MonoBehaviour
{
    void Update()
    {
        // 새를 당기고 있지 않은 상태에서만 카메라 드래그 허용
        if (SlingShot.slingshotState != SlingshotState.UserPulling &&
            GameManager.CurrentGameState == GameState.Playing)
        {
            if (Input.GetMouseButtonDown(0))
            {
                timeDragStarted = Time.time;
                dragSpeed = 0f;
                previousPosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0) && Time.time - timeDragStarted > 0.05f)
            {
                Vector3 input = Input.mousePosition;
                float deltaX = (previousPosition.x - input.x) * dragSpeed;
                float deltaY = (previousPosition.y - input.y) * dragSpeed;

                float newX = Mathf.Clamp(transform.position.x + deltaX, -10f, 30f);
                float newY = Mathf.Clamp(transform.position.y + deltaY, -5f, 5f);

                transform.position = new Vector3(newX, newY, transform.position.z);

                previousPosition = input;
                if (dragSpeed < 0.1f) dragSpeed += 0.002f;
            }
        }
    }

    private float dragSpeed = 0.01f;
    private float timeDragStarted;
    private Vector3 previousPosition = Vector3.zero;

    public SlingShot SlingShot;
}
