using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_body : MonoBehaviour
{
    [SerializeField]
    private HingeJoint wheelRight;
    
    [SerializeField]
    private HingeJoint wheelLeft;
    
    // Start is called before the first frame update
    void Start()
    {
        JointMotor motor = wheelRight.motor;
        motor.force = 10;
        motor.targetVelocity = 90;
        motor.freeSpin = false;
        wheelRight.motor = motor;

        motor = wheelLeft.motor;
        motor.force = 10;
        motor.targetVelocity = -90;
        motor.freeSpin = false;
        wheelLeft.motor = motor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
