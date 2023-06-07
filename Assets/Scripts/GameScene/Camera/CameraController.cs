using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    // 마우스 카메라 움직이는 속도
    public float mouseSpeed = 4.0f;

    // 키보드 카메라 움직이는 속도
    public float keyboardSpeed = 0.1f;


    private float xRotate = 0.0f;
    private float yRotate = 0.0f;

    private float xRotateSize = 0.0f;
    private float yRotateSize = 0.0f;
    
    void Update()
    {
        controlWithMouse();
        controlWithKey();
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
    void controlWithKey()
    {
        
        xRotateSize += -Input.GetAxisRaw("Vertical") * keyboardSpeed;
        yRotateSize += Input.GetAxisRaw("Horizontal") * keyboardSpeed;
    }

    // 키보드와 마우스의 입력값을 기반으로, 카메라의 각도를 조절합니다.
    void updateCamera()
    {
        xRotate = Mathf.Clamp(xRotate + xRotateSize, 0, 90); // 중간값 : 45
        yRotate = Mathf.Clamp(yRotate + yRotateSize, -40, 130); // 중간값 : 45

        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

        // 값 초기화
        xRotateSize = 0f;
        yRotateSize = 0f;
    }
}