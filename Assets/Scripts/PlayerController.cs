using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _multiplier;
    [SerializeField] private Rigidbody _spaceShipBody;
    private PlayerInput _pInput;


    private void Start()
    {
        _pInput = GetComponent<PlayerInput>();


    }

    private void Update()
    {

        var rotation = _spaceShipBody.transform.TransformDirection(20f * _pInput.axis1 * Vector3.forward);
        var upDown = _spaceShipBody.transform.TransformDirection(-10f * _pInput.axis2 * Vector3.right);
        _spaceShipBody.AddForce(_spaceShipBody.transform.forward * (_pInput.thrust * _multiplier));

        _spaceShipBody.AddTorque(rotation);
        _spaceShipBody.AddTorque(upDown);


    }
    
}
