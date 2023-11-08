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

    public SerialPort sp;
    public char _key;

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
        // Debug.Log(abc[0] + "   " + abc[1]);
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
        if (!sp.IsOpen)
        {
            sp.Close();
        }

        try
        {
            _key = (char)sp.ReadChar();
        }
        catch (System.Exception e)
        {
            Debug.Log("Timeout " + e);
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
