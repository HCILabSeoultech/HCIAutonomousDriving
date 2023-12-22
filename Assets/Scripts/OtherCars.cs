using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCars : MonoBehaviour
{
    public WheelCollider[] wheels = new WheelCollider[4]; //wheels를 WheelColliderdml 배열로 지정하고 배열의 크기를 4로 지정
    public GameObject[] wheelMeshs = new GameObject[4];   //wheelMeshs를 GameObject 배열로 지정하고 배열의 크기를 4로 지정
    public float Torque = 100f; // 바퀴를 회전시킬 힘
    public float streeringMaxAngle = 45f; // 바퀴의 회전각도
    public float speedLimit = 50f;  
    public float speed ;
    public float accel;
    public float back;

    public GameObject CenterOfMass;



    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
           

        }
    }

    
    private void FixedUpdate()
    {
        moveCar();
        animateWheelMeshs();
        Braking();
        


    }

    private void moveCar()
    {



        speed = gameObject.GetComponent<Rigidbody>().velocity.magnitude * 3.6f;
        /*gameObject.GetComponent<Rigidbody>().centerOfMass = CenterOfMass.transform.localPosition;*/
        if (speed <= speedLimit)
        {
            for (int i = 0; i < 4; i++)
            {
       

                wheels[i].motorTorque = Torque ;


            }
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
       
    }
}

