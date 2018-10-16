using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR;

public class Obstacle : MonoBehaviour, IGrabeable
{
    public void Grab(SteamVR_Input_Sources source, ControllerVR controller)
    {
        var joint = AddFixedJoint();
        joint.connectedBody = controller.gameObject.GetComponent<Rigidbody>();

        NavMeshObstacle obstacle = GetComponent<NavMeshObstacle>();

        if (obstacle != null)
            obstacle.enabled = false;
    }

    private FixedJoint AddFixedJoint()
    {
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    public void Release(SteamVR_Input_Sources source, ControllerVR controller)
    {
        var joint = GetComponent<FixedJoint>();
        if (joint)
        {
            joint.connectedBody = null;
            Destroy(joint);

            GetComponent<Rigidbody>().velocity += controller.Velocity * 30f;
            GetComponent<Rigidbody>().angularVelocity = controller.AngularVelocity;

            NavMeshObstacle obstacle = GetComponent<NavMeshObstacle>();

            if (obstacle != null)
                obstacle.enabled = true;
        }
    }
}
