using UnityEngine;
using System.Collections;

/// <summary>
/// ロボット制御
/// </summary>
public class RobotController : MonoBehaviour {
    /// <summary>
    /// 左車輪
    /// </summary>
    [SerializeField]
    private HingeJoint wheelLeft;
    /// <summary>
    /// 右車輪
    /// </summary>
    [SerializeField]
    private HingeJoint wheelRight;
    /// <summary>
    /// 照度センサ
    /// </summary>
    [SerializeField]
    private IlluminanceSensor illumSensor;

    /// <summary>
    /// 目標速度
    /// </summary>
    [SerializeField]
    private float speed = 500;

    /// <summary>
    /// 照度センサ閾値
    /// </summary>
    [SerializeField]
    private float threshold = 0.5f;

    /// <summary>
    /// 比例ゲイン
    /// </summary>
    [SerializeField]
    private float kp = 1500;
    /// <summary>
    /// 積分ゲイン
    /// </summary>
    [SerializeField]
    private float ki = 500000;
    /// <summary>
    /// 微分ゲイン
    /// </summary>
    [SerializeField]
    private float kd = 500;

    /// <summary>
    /// 制御偏差の積分値
    /// </summary>
    private float errorInt = 0;

    /// <summary>
    /// 1フレーム前の制御偏差
    /// </summary>
    private float errorPrev = 0;

    /// <summary>
    /// 照度センサ値に基づくフィードバック制御
    /// </summary>
    private void Update() {
        // 制御偏差計算
        var error = threshold - illumSensor.illuminance;

        // 制御偏差の微積分値計算
        errorInt = (error + errorPrev) * Time.deltaTime / 2;
        var errorDiff = (error - errorPrev) / Time.deltaTime;

        // 制御量計算(PID制御)
        var output = kp * error + ki * errorInt + kd * errorDiff;

        // 車輪目標速度設定
        JointMotor motor = wheelLeft.motor;
        //motor.targetVelocity = speed - output;
        
        motor.force = -50;
        motor.targetVelocity = 900;
        motor.freeSpin = false;
        wheelLeft.motor = motor;
        wheelLeft.useMotor = true;
        

        motor = wheelRight.motor;
        motor.targetVelocity = speed + output;
        wheelRight.motor = motor;

        errorPrev = error;
        
        Debug.Log(wheelLeft.motor);
    }
}