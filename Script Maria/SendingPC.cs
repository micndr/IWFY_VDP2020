using UnityEngine;
using System.IO.Ports;
using System.Linq;

public class SendingPC : MonoBehaviour
{
    public string portName = "COM3";
    public static SerialPort sp;
    public string sendChar;
    private static bool init = false;
    float timePassed = 0.0f;
    // Use this for initialization
    void Start()
    {
        if (!init)
        {
            sp = new SerialPort("\\\\.\\" + portName, 9600);
            OpenConnection();
            init = true;
        }
    }

    public void OpenConnection()
    {
        { Debug.Log("funziona"); }
        if (sp != null)
        {
            if (sp.IsOpen)
            {
                sp.Close();
                print("Closing port, because it was already open!");
            }
            else
            {
                sp.Open();            // opens the connection
                sp.ReadTimeout = 16;  // sets the timeout value before reporting error
                print("Port Opened!");
            }
        }
        else
        {
            if (sp.IsOpen)
            {
                print("Port is already open");
            }
            else
            {
                print("Port == null");
            }
        }
    }

    void OnApplicationQuit()
    {
        sp.Close();
        Debug.Log("Close");
    }

    void OnMouseDown()
    {
        Debug.Log("Now send");
        Debug.Log(sendChar);
        sp.Write(sendChar);
    }

}