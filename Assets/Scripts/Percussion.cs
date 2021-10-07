using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;
using System.IO.Ports;
using UnityEngine;
using UniRx;

public class Percussion : MonoBehaviour
{

    public string portName;
    public int baurate;

    SerialPort serial;
    bool isLoop = true;
    float roll;
    float pitch;
    bool foot_up = false;
    [SerializeField]
    GameObject cube;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip Bass_drum;

    void Start()
    {
        this.serial = new SerialPort(portName, baurate, Parity.None, 8, StopBits.One);

        try
        {
            this.serial.Open();
            Scheduler.ThreadPool.Schedule(() => ReadData()).AddTo(this);
        }
        catch (System.Exception e)
        {
            Debug.Log("can not open serial port");
        }
    }

    private void Update()
    {
        cube.transform.rotation = Quaternion.Euler(0, 0, (int)roll);
        if(roll > -20)
        {
            foot_up = true;
        }

        if(foot_up && roll < -80)
        {
            foot_up = false;
            audioSource.PlayOneShot(Bass_drum);
        }


    }

    public void ReadData()
    {
        while (this.isLoop)
        {
            string message = this.serial.ReadLine();
            string[] temp = message.Split(',');
            roll = float.Parse(temp[0]);
            pitch = float.Parse(temp[1]);
            Debug.Log("roll=" + roll);
            Debug.Log("Pitch=" + pitch);
        }
    }

    void OnDestroy()
    {
        this.isLoop = false;
        this.serial.Close();
    }
}