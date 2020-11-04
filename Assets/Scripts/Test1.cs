using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        HingeJoint hinge = GetComponent<HingeJoint>();

        // Make the hinge motor rotate with 90 degrees per second and a strong force.
        JointMotor motor = hinge.motor;
        motor.force = 10;
        motor.targetVelocity = -90;
        motor.freeSpin = false;
        hinge.motor = motor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
