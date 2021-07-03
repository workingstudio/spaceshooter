using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _speed;
    void FixedUpdate()
    {
        this.transform.position = _target.position + _target.transform.TransformDirection(_offset);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation,_target.transform.rotation, _speed * Time.fixedDeltaTime);
    }
}
