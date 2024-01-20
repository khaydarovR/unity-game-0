using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CamerMover : MonoBehaviour
{
    [Header("Object for fololwing")]

    [Header("Camera settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _height;
    [SerializeField] private float _rareDistance;

    private Vector3 currentVector;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }


}
