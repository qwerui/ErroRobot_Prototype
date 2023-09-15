using System.Collections;
using System.Collections.Generic;
using System.Security.AccessControl;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // 마우스 카메라 움직이는 속도
    public float mouseSpeed = 4.0f;

    // 키보드 카메라 움직이는 속도
    public float keyboardSpeed = 0.1f;
    float acceleration = 0.0f;
    const float accelerationConst = 0.005f; 
    float speedMultiplier;
    public float keyboardSpeed = 0.05f;

    Vector2 moveDirection;

    const string cameraSpeedPref = "CameraSpeed";
    const float initialMultiplier = 0.01f;


    private float xRotate = 0.0f;
    private float yRotate = 0.0f;

    private float xRotateSize = 0.0f;
    private float yRotateSize = 0.0f;

    void Start()
    {
        xRotate = transform.eulerAngles.x;
        yRotate = transform.eulerAngles.y;
        keyboardSpeed = PlayerPrefs.GetInt(cameraSpeedPref, 50) / 100.0f;
        speedMultiplier = initialMultiplier;
    }
    
    void Update()
    {
        //controlWithMouse();
        //controlWithKey();
    }

    private void LateUpdate() 
    {
        updateCamera();
    }
    
    // 마우스 입력값을 받아옵니다.
    void controlWithMouse()
    { 
        // -1을 곱하지 않으면 위 아래가 반대로 돌아감
        xRotateSize += Input.GetAxis("Mouse Y") * mouseSpeed * -1f;
        yRotateSize += Input.GetAxis("Mouse X") * mouseSpeed;
    }
    
    // 키보드 입력값을 받아옵니다.
    public void controlWithKey(Vector2 direction)
    {
        moveDirection = direction * keyboardSpeed * 2;
        
        // xRotateSize += direction.x * keyboardSpeed;
        // yRotateSize += direction.y * keyboardSpeed;
    }

    // 키보드와 마우스의 입력값을 기반으로, 카메라의 각도를 조절합니다.
    void updateCamera()
    {
        if(Mathf.Abs(moveDirection.x) + Mathf.Abs(moveDirection.y) > Mathf.Epsilon)
        {
            acceleration += Time.deltaTime * accelerationConst;
            speedMultiplier += acceleration;
            speedMultiplier = Mathf.Clamp01(speedMultiplier);
        }
        else
        {
            acceleration = 0;
            speedMultiplier = initialMultiplier;
        }

        xRotate = Mathf.Clamp(xRotate - moveDirection.y * speedMultiplier, 0, 90);
        yRotate = Mathf.Clamp(yRotate + moveDirection.x * speedMultiplier, -40, 130);
        // xRotate = Mathf.Clamp(xRotate + xRotateSize, 0, 90); // 중간값 : 45
        // yRotate = Mathf.Clamp(yRotate + yRotateSize, -40, 130); // 중간값 : 45

        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

        // 값 초기화
        xRotateSize = 0f;
        yRotateSize = 0f;
    }

    public void DisableRotation()
    {
        moveDirection = Vector2.zero;
    }

    public RaycastHit RaycastCheck()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.forward, out hit);
        return hit;
    }
}