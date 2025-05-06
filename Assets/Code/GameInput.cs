using System;
using System.IO.Ports;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    SerialPort serialPort = new SerialPort ("/dev/ttyUSB0", 9600);

    public event EventHandler OnLeftDrumPressed;
    public event EventHandler OnCenterLeftDrumPressed;
    public event EventHandler OnCenterRightDrumPressed;
    public event EventHandler OnRightDrumPressed;


    public static GameInput Instance;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("GameInput++??? (mas de un gameinput en esta escena DSKFHDKSFHKFHJ)");
        }


    }



    void Start() {
        serialPort.Open();
        serialPort.ReadTimeout = 1;
    }


    void Update()
    {
        if(serialPort.IsOpen)
        {
            ReadArduino();
        }


        if(Input.GetKeyDown(KeyCode.S))
        {
            FireLeftDrumEvent();
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            FireCenterLeftDrumEvent();
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            FireCenterRightDrumEvent();
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            FireRightDrumEvent();
        }


    }


     public void ReadArduino()
    {
        try{
            int value = serialPort.ReadByte();
            Debug.Log(value);
            switch(value)
            {
                case 0:
                    FireLeftDrumEvent();
                    break;
                case 4:
                    FireCenterLeftDrumEvent();
                    break;
                case 1:
                    FireCenterRightDrumEvent();
                    break;
                case 5:
                    FireRightDrumEvent();
                    break;
                case 6:
                    FireLeftDrumEvent();
                    FireCenterRightDrumEvent();
                    break;
                case 7:
                    FireLeftDrumEvent();
                    FireCenterLeftDrumEvent();
                    break;
                case 8:
                    FireLeftDrumEvent();
                    FireRightDrumEvent();
                    break;
                case 9:
                    FireCenterLeftDrumEvent();
                    FireCenterRightDrumEvent();
                    break;
                case 2:
                    FireCenterRightDrumEvent();
                    FireRightDrumEvent();
                    break;
                case 3:
                    FireCenterLeftDrumEvent();
                    FireRightDrumEvent();
                    break;
            }
        }
        catch(System.Exception) 
        {
            //lol
        }
    }

    public SerialPort GetSerialPort()
    {
        return serialPort;
    }

    private void FireLeftDrumEvent()
    {
        OnLeftDrumPressed?.Invoke(this, EventArgs.Empty);
    }

    private void FireCenterLeftDrumEvent()
    {
        OnCenterLeftDrumPressed?.Invoke(this, EventArgs.Empty);
    }

    private void FireCenterRightDrumEvent()
    {
        OnCenterRightDrumPressed?.Invoke(this, EventArgs.Empty);
    }

    private void FireRightDrumEvent()
    {
        OnRightDrumPressed?.Invoke(this, EventArgs.Empty);
    }

    public void ResetStaticData()
    {
        Instance = null;
    }


    public void SendMessageToArduino(string msg){
        serialPort.WriteLine(msg);
    }

    public void PrenderTambores()
    {
        SendMessageToArduino(ArduinoConstants.modoOn);
    }

    public void ApagarTambores()
    {
        SendMessageToArduino(ArduinoConstants.modoOff);
    }
   
    
}
