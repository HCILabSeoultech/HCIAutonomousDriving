using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controller : MonoBehaviour
{
    public WheelCollider[] wheels = new WheelCollider[4]; //wheels를 WheelColliderdml 배열로 지정하고 배열의 크기를 4로 지정
    public GameObject[] wheelMeshs = new GameObject[4];   //wheelMeshs를 GameObject 배열로 지정하고 배열의 크기를 4로 지정
    public float Torque = 100f; // 바퀴를 회전시킬 힘
    public float streeringMaxAngle = 45f; // 바퀴의 회전각도
    public float speedLimit = 200f;  
    public float speed ;
    public float brakePower = 1000000000f;
    public float accel;
    public float back;
    public float push_break;

    public GameObject CenterOfMass;
    private bool isBraking = false;


    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
           

        }
    }

    
    private void FixedUpdate()
    {
        moveCar();
        steerCar();
        animateWheelMeshs();
        Braking();
        


    }

    private void moveCar()
    {
        LogitechGSDK.DIJOYSTATE2ENGINES rec;
        rec = LogitechGSDK.LogiGetStateUnity(0);


        speed = gameObject.GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
        /*gameObject.GetComponent<Rigidbody>().centerOfMass = CenterOfMass.transform.localPosition;*/
        if (speed <= speedLimit)
        {
            for (int i = 0; i < 4; i++)
            {
                if (rec.lY <= 32727)
                    accel = (rec.lY - 32727) / (-32727);
                if (rec.rglSlider[0] < 32727)
                    accel = (rec.rglSlider[0] - 32727) / (32727);

                wheels[i].motorTorque = Torque * accel;


            }
        }
    }

    

    private void steerCar()
    {
        for (int i = 0;i<2; i++)
        {
            wheels[i].steerAngle = streeringMaxAngle * Input.GetAxis("Horizontal");
        }
    }    
    
    private void animateWheelMeshs()
    {
        Vector3 pos = Vector3.zero;
        Quaternion rot = Quaternion.identity;

        for(int i = 0; i<4; i++)
        {
            wheels[i].GetWorldPose(out pos, out rot);
            wheelMeshs[i].transform.position = pos;
            wheelMeshs[i].transform.rotation = rot;
        }
    }

    private void Braking()
    {
        LogitechGSDK.DIJOYSTATE2ENGINES rec;
        rec = LogitechGSDK.LogiGetStateUnity(0);
        
        if (rec.lRz < 32725)
        {
            isBraking = true;
        }
        else
        {
            isBraking = false;
        }
        if (isBraking )
        for (int i = 0; i < 4; i++)
        {
                wheels[i].brakeTorque = -10000000000 * (rec.lRz-(32728));
        }
        else
        for (int i = 0; i < 4; i++)
        {
            wheels[i].brakeTorque = 0;
        }
    }
}
