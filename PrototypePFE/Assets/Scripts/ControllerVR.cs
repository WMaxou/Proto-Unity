using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Valve.VR;

public class ControllerVR : MonoBehaviour
{
    private bool teleportPressed;
    public SteamVR_Input_Sources source;

    IGrabeable collidingObject;
    IGrabeable objectInHand;
    IStopeable stopedObject;

    public Vector3 Velocity { get
        {
            switch (source)
            {
                case SteamVR_Input_Sources.LeftHand:
                    return SteamVR_Input._default.inActions.SkeletonLeftHand.GetVelocity(source);

                case SteamVR_Input_Sources.RightHand:
                    return SteamVR_Input._default.inActions.SkeletonRightHand.GetVelocity(source);

                default:
                    return Vector3.zero;
            }
        }
    }

    public Vector3 AngularVelocity { get
        {
            switch (source)
            {
                case SteamVR_Input_Sources.LeftHand:
                    return SteamVR_Input._default.inActions.SkeletonLeftHand.GetAngularVelocity(source);

                case SteamVR_Input_Sources.RightHand:
                    return SteamVR_Input._default.inActions.SkeletonRightHand.GetAngularVelocity(source);

                default:
                    return Vector3.zero;
            }
        }
    }

    private void Update()
    {
        if (SteamVR_Input._default.inActions.Teleport.GetStateDown(source))
        {
            teleportPressed = true;
        }
        if (SteamVR_Input._default.inActions.Teleport.GetStateUp(source))
        {
            teleportPressed = false;
            if (stopedObject != null)
                stopedObject.StartMove();
        }

        if (SteamVR_Input._default.inActions.GrabPinch.GetStateDown(source))
        {
            if (collidingObject != null)
                collidingObject.Grab(source, this);
        }

        if (SteamVR_Input._default.inActions.GrabPinch.GetStateUp(source))
        {
            if (collidingObject != null)
                collidingObject.Release(source, this);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    {
        SetCollidingObject(other);
        StopObject(other);
    }

    public void OnTriggerExit(Collider other)
    {
        if (stopedObject == null)
            return;

        IStopeable obj = other.GetComponent<IStopeable>();
        if (obj != null)
        {
            obj.StartMove();
            stopedObject = null;
        }
    }

    private void StopObject(Collider other)
    {
        if (teleportPressed == false || stopedObject != null)
            return;

        IStopeable obj = other.GetComponent<IStopeable>();
        if (obj != null)
        {
            obj.StopMove();
            stopedObject = obj;
        }
    }

    private void SetCollidingObject(Collider col)
    {
        if (collidingObject != null)
            return;

        IGrabeable obj = col.gameObject.GetComponent<IGrabeable>();

        if (obj != null)
            collidingObject = obj;
    }
}
