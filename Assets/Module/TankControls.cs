using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Driven;
using System;
using UnityEngine.Networking;

public class TankControls : Module {
    public float maxSpeed, treadSeparation, timeToMax, timeToZero, maxTreadSpeedDifference;

    private float leftSpeed, rightSpeed;
    private float leftThrottle, rightThrottle;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        leftSpeed = UpdateSpeed(leftSpeed, leftThrottle);
        rightSpeed = UpdateSpeed(rightSpeed, rightThrottle);

        EnforceSpeedDifference();

        Debug.Log("Left: " + leftSpeed + "\tRight: " + rightSpeed);
        MoveTank();
    }

    public override bool WorthProcessing(Driven.PanelInputs inputs) {
        //TODO: Make these more complex (like providing opposite input is faster to stop than just letting go).
        //TODO: Cause self-damage if you reverse while still going full on an axis.
        float leftAxis = inputs.topLeft - inputs.bottomLeft;
        float rightAxis = inputs.topRight - inputs.bottomRight;

        return !(Mathf.Approximately(leftAxis, leftThrottle) && Mathf.Approximately(rightAxis, rightThrottle));
    }

    public override void ProcessInputs(Driven.PanelInputs inputs) {
        leftThrottle = inputs.topLeft - inputs.bottomLeft;
        rightThrottle = inputs.topRight - inputs.bottomRight;
    }

    private void EnforceSpeedDifference() {
        if (leftSpeed < 0 != rightSpeed < 0) {
            leftSpeed = Mathf.Max(leftSpeed, -maxTreadSpeedDifference);
            leftSpeed = Mathf.Min(leftSpeed, maxTreadSpeedDifference);
            rightSpeed = Mathf.Max(rightSpeed, -maxTreadSpeedDifference);
            rightSpeed = Mathf.Min(rightSpeed, maxTreadSpeedDifference);
            return;
        }

        if (Math.Abs(leftSpeed - rightSpeed) > maxTreadSpeedDifference) {
            var negative = leftSpeed < 0;
            leftSpeed = Math.Abs(leftSpeed);
            rightSpeed = Math.Abs(rightSpeed);
            var minimum = Mathf.Min(leftSpeed, rightSpeed);
            leftSpeed = Mathf.Min(leftSpeed, minimum + maxTreadSpeedDifference);
            rightSpeed = Mathf.Min(rightSpeed, minimum + maxTreadSpeedDifference);

            if (negative) {
                leftSpeed = -leftSpeed;
                rightSpeed = -rightSpeed;
            }
        }
    }

    // Speed is current speed of tread. Throttle is -1, 0, or 1.
    private float UpdateSpeed(float speed, float throttle) {
        var speedNeg = speed < 0;
        var throttleNeg = throttle < 0;
        var throttleZero = Mathf.Approximately(throttle, 0);
        float newSpeed = Mathf.Abs(speed);
        throttle = Mathf.Abs(throttle);
        if (throttleNeg != speedNeg) {
            throttle = -throttle;
        }

        //Handle natural decelerating.
        if(throttleZero) {
            newSpeed -= maxSpeed / timeToZero;
        } else if(throttle < 0) {
            //Handle powered reversing.
            newSpeed -= maxSpeed / timeToMax;
        } else {
            //Handle powered accelerating.
            newSpeed += maxSpeed / timeToMax;
        }
       

        //Timing logic and constraints.
        if(speedNeg) {
            newSpeed = -newSpeed;
        }
        newSpeed = speed + Time.deltaTime * (newSpeed - speed);
        if (throttleZero && newSpeed < 0) {
            newSpeed = 0;
        }
        if(newSpeed > maxSpeed) {
            newSpeed = maxSpeed;
        }
        if (newSpeed < -maxSpeed) {
            newSpeed = -maxSpeed;
        }
        return newSpeed;
    }

    private void MoveTank() {
        mech.transform.Rotate(0, Mathf.Rad2Deg*(rightSpeed - leftSpeed) / treadSeparation * Time.deltaTime, 0);
        mech.transform.Translate(0, 0, (rightSpeed + leftSpeed) * Time.deltaTime);
    }

}
