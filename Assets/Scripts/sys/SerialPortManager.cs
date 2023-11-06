using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class SerialPortManager : MonoBehaviour
{
    private static SerialPortManager _instance = null;

    public enum PortNumber
    {
        COM1, COM2, COM3, COM4, COM5,
        COM6, COM7, COM8, COM9, COM10,
        COM11, COM12, COM13, COM14, COM15
    }

    [SerializeField]
    private PortNumber portNumber = PortNumber.COM1;

    [SerializeField]
    private int baudRate = 9600;

    private SerialPort sp;

    public float axisX = 0;
    public float axisY = 0;
    public bool aIsPush = false;
    public bool bIsPush = false;

    string key_str;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public static SerialPortManager Instance
    {
        get
        {
            if (_instance == null)
            {
                return null;
            }

            return _instance;

        }
    }

    // Start is called before the first frame update
    void Start()
    {
        string[] abc = SerialPort.GetPortNames();
        Debug.Log(abc[0] + "   " + abc[1]);
        sp = new SerialPort("COM4", baudRate, Parity.None, 8, StopBits.One);

        if (!sp.IsOpen)
        {
            sp.Open();
        }

        sp.ReadTimeout = 0;

    }

    // Update is called once per frame
    void Update()
    {
        /*
        Debug.Log(sp.IsOpen + " " + sp.BaudRate.ToString());
        if (sp.IsOpen)
        {
            switch (sp.ReadByte())
            {
                case 75: //right
                    axisX += -1;
                    break;

                case 77: //left
                    axisX += 1;
                    break;

                case 72: //up
                    axisY += -1;
                    break;

                case 80: //down
                    axisY += 1;
                    break;

                case 97:
                    aIsPush = true;
                    break;

                case 98:
                    bIsPush = true;
                    break;

                default:
                    axisX = axisX;
                    axisY = axisY;
                    aIsPush = false;
                    aIsPush = false;
                    break;
            }
        }

        Debug.Log("Axis X : " + axisX + ", Axis Y : " + axisY + ", a is push : " + aIsPush + ", b is push : " + bIsPush + "\n");

        //transform.eulerAngles = new Vector3(axisY * 1, axisX * 1, 0);


        //axisX += Input.GetAxis("Mouse X") * 10;
        //axisY += Input.GetAxis("Mouse Y") * -10;
        //transform.eulerAngles = new Vector3(axisY, axisX, 0);
        */
        try
        {
            Debug.Log((char)sp.ReadByte());
            key_str = sp.ReadByte().ToString();
        }
        catch (System.Exception e)
        {
            Debug.Log("Timeout");
        }

    }

    private void OnApplicationQuit()
    {
        if (sp.IsOpen)
        {
            sp.Close();
        }
    }
}
