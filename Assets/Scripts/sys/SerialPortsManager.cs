using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SerialPortsManager : MonoBehaviour
{
    SerialPort sp = new SerialPort();

    float mouseX = 0;
    float mouseY = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseX += Input.GetAxis("Mouse X") * 10;
        mouseY += Input.GetAxis("Mouse Y") * -10;
        transform.eulerAngles = new Vector3(mouseY, mouseX, 0);
    }
}
