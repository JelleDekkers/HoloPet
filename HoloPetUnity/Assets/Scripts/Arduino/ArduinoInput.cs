using System.Collections;
using System.IO.Ports;
using UnityEngine;
using UnityEngine.UI;
public class ArduinoInput : MonoBehaviour
{
    //Opens serial port
    public SerialPort serialPort = new SerialPort("COM3", 9600);
    public Transform frontPos;
    public Transform leftPos;
    public Transform rightPos;
    public float speed = 2;
    private Vector3 curAngle;

    // Use this for initialization
    void Start()
    {
        serialPort.Open();
        serialPort.ReadTimeout = 50;
        curAngle = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (serialPort.IsOpen)
        {
            try
            {
                //Debug.Log("Port Open");
            }
            catch (System.Exception)
            {
                Debug.Log("Can't Find port");
            }
        }
        string value = serialPort.ReadLine();
        float step = speed * Time.deltaTime;

        if (value == "0") {
            Debug.Log("No Motion");
            // curAngle = new Vector3(Mathf.LerpAngle(curAngle.x, angle1.x, 1f),
            //                        Mathf.LerpAngle(curAngle.y, angle1.y, 1f),
            //                        Mathf.LerpAngle(curAngle.z, angle1.z, 1f));
            //transform.eulerAngles = curAngle;
            }

        if (value == "1")
        {
            Debug.Log("Right Motion");
            //Look at right pos
        }
        if (value == "2")
        {
            Debug.Log("Left Motion");
            //Look at left pos
        }
        if (value == "3")
        {
            Debug.Log("Front Motion");
            //Look at front pos
        }

        
    }
}
