using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]

public class ObjectBuoyancy : MonoBehaviour
{
    public Transform[] floatPoints;
    
    [SerializeField]private float underWaterDrag = 3f;
    [SerializeField]private float underWaterAngularDrag = 1f;

    [SerializeField]private float airDrag = 0f;
    [SerializeField]private float airAngularDrag = 0.05f;

    [SerializeField]private float floatingPower = 100;

    private WaveManager waveManager;

    Rigidbody _Rigidbody;

    private int floatPointsUnderwater;

    [SerializeField]private bool underwater;
    public bool inWater;
    
    // Start is called before the first frame update
    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water") || other.CompareTag("WaterSwim"))
        {
            inWater = true;
            
            if (waveManager != other.gameObject.GetComponentInParent<WaveManager>())
            {
                waveManager = other.gameObject.GetComponentInParent<WaveManager>();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water") || other.CompareTag("WaterSwim"))
        {
            inWater = false;
            SwitchState(false);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (inWater)
        {
            floatPointsUnderwater = 0;
            for (var i = 0; i < floatPoints.Length; i++)
            {
                var difference = floatPoints[i].position.y -
                                 waveManager.WaterHeightAtPosition(floatPoints[i].position);

                if (difference < 0)
                {
                    _Rigidbody.AddForceAtPosition(Vector3.up * (floatingPower * Mathf.Abs(difference)),
                        floatPoints[i].position,
                        ForceMode.Force);
                    floatPointsUnderwater += 1;
                    if (!underwater)
                    {
                        underwater = true;
                        SwitchState(underwater);
                    }
                }
            }


            if (underwater && floatPointsUnderwater == 0)
            {
                underwater = false;
                SwitchState(underwater);
            }
        }
    }

    void SwitchState(bool isUnderwater)
    {
        if (isUnderwater)
        {
            _Rigidbody.drag = underWaterDrag;
            _Rigidbody.angularDrag = underWaterAngularDrag;
        }
        else
        {
            _Rigidbody.drag = airDrag;
            _Rigidbody.angularDrag = airAngularDrag;
        }
        
    }
}
