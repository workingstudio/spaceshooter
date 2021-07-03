using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput Instance { get { return s_Instance; } }


    private static PlayerInput s_Instance;

    //left pad
        public float axis1 = Input.GetAxis("Axis 1");
        public float axis2 = Input.GetAxis("Axis 2");
        //right pad
        public float axis3 = Input.GetAxis("Axis 3");
        public  float axis4 = Input.GetAxis("Axis 4");

        public float thrust = Input.GetAxis("Axis 6") + 1; // negative = no thrust
        public float stop = Input.GetAxis("Axis 5"); // negative = no thrust
   
    void Awake()
    {
        s_Instance = this;
    }
    public void Update()
    {

            
      
    }
    
 
}

