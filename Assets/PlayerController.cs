using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _multiplier;
    [SerializeField] private Rigidbody _spaceShipBody;

    private void Update()
    {
        //left pad
        var axis1 = Input.GetAxis("Axis 1") ;
        var axis2 = Input.GetAxis("Axis 2");
        //right pad
        var axis3 = Input.GetAxis("Axis 3");
        var axis4 = Input.GetAxis("Axis 4");
        
        var thrust = Input.GetAxis("Axis 6") +1 ; // negative = no thrust
        var stop = Input.GetAxis("Axis 5"); // negative = no thrust
        var rotation = _spaceShipBody.transform.TransformDirection(20f * axis1*Vector3.forward);
        var upDown = _spaceShipBody.transform.TransformDirection(-10f * axis2*Vector3.right);
        _spaceShipBody.AddForce(_spaceShipBody.transform.forward * (thrust * _multiplier));

        _spaceShipBody.AddTorque(rotation);
        _spaceShipBody.AddTorque(upDown);


    }
    
}
