using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
    [SerializeField]
    private HingeJoint wheelRight;
    
    [SerializeField]
    private HingeJoint wheelLeft;
    
    [SerializeField]
    private IlluminanceSensor illumSensor;

    [SerializeField]
    private float speed;
    
    [SerializeField]
    private float kp;
    
    [SerializeField]
    private float ki;
    
    [SerializeField]
    private float kd;

    [SerializeField] 
    private float threshold ;  //閾値
    
    private float diviation_now = 0;  //現在の偏差

    private float diviation_before = 0; //1フレーム前の偏差

    private float integral = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        /*
        int targetVel = 90;
        
        JointMotor motor = wheelRight.motor;
        //motor.force = 50;
        //motor.targetVelocity = targetVel;
        motor.freeSpin = false;
        wheelRight.motor = motor;

        motor = wheelLeft.motor;
        motor.force = 50;
        //motor.targetVelocity = -targetVel;
        motor.freeSpin = false;
        wheelLeft.motor = motor;
        */
    }

    // Update is called once per frame
    void Update()
    {
        diviation_before = diviation_now;  
        diviation_now = illumSensor.illuminance - threshold;  //現在の偏差を求める
        
        Debug.Log("diviation  " + diviation_now);

        integral += (diviation_before + diviation_now) * Time.deltaTime / 2;

        var p = kp * diviation_now;
        var i = ki * integral;
        var d = kd * (diviation_now - diviation_before) / Time.deltaTime;

        var power = p + i + d;  //PIDの出力
        
        Debug.Log(power);
        
        var output = -(speed - power);
        //var output = (speed - power);

        
        JointMotor motor = wheelRight.motor;
        motor.targetVelocity = output;
        wheelRight.motor = motor;
        Debug.Log("Right  " + output);

        output = speed + power;
        motor = wheelLeft.motor;
        motor.targetVelocity = output;
        wheelLeft.motor = motor;
        Debug.Log("Left  " + output);
    }
}
