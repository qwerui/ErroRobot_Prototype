using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SerialPortManager : MonoBehaviour
{
    SerialPort sp = new SerialPort("COM3", 9600);

    public float axisX = 0;
    public float axisY = 0;
    public bool aIsPush = false;
    public bool bIsPush = false;

    // Start is called before the first frame update
    void Start()
    {
        if(!sp.IsOpen)
        {
            sp.Open();
        }

        sp.ReadTimeout = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            axisX += -1;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            axisX = axisX;
        }
        
        if (Input.GetKey(KeyCode.RightArrow))
        {
            axisX += 1;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            axisX = axisX;
        }
        
        if (Input.GetKey(KeyCode.UpArrow))
        {
            axisY += -1;
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            axisY = axisY;
        }
        
        if (Input.GetKey(KeyCode.DownArrow))
        {
            axisY += 1;
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            axisY = axisY;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            aIsPush = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            aIsPush = false;
        }

        if (Input.GetKey(KeyCode.B))
        {
            bIsPush = true;
        }
        else if (Input.GetKeyUp(KeyCode.B))
        {
            bIsPush = false;
        }
  

        //Debug.Log("Axis X : "+ axisX +", Axis Y : "+ axisY  + ", a is push : " + aIsPush+ ", b is push : " + bIsPush + "\n");
        //transform.eulerAngles = new Vector3(axisY * 1, axisX * 1, 0);
        //switch (sp.ReadLine())
        //{
        //    case "RIGHT":
        //        axisX += -1;
        //        break;

        //    case "LEFT":
        //        axisX += 1;
        //        break;

        //    case "UP":
        //        axisY += -1;
        //        break;

        //    case "DOWN":
        //        axisY += 1;
        //        break;

        //    case "A":
        //        aIsPush = true;
        //        break;

        //    case "B":
        //        bIsPush = true;
        //        break;

        //    default:
        //        axisX = axisX;
        //        axisY = axisY;
        //        aIsPush = false;
        //        aIsPush = false;
        //        break;
        //}

        //axisX += Input.GetAxis("Mouse X") * 10;
        //axisY += Input.GetAxis("Mouse Y") * -10;
        //transform.eulerAngles = new Vector3(axisY, axisX, 0);
    }

    private void OnApplicationQuit()
    {
        if(sp.IsOpen)
        {
            sp.Close();
        }
    }
}
