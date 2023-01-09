using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class WaveManager : MonoBehaviour
{
    [Range(0f, 10f), Tooltip("Height of waves")]
    [SerializeField] private float waveHeight = 1f;
    
    [Range(0f, 10f), Tooltip("Frequency of waves")]
    [SerializeField] private float waveFrequency = 4f;

    [Range(0f, 20f), Tooltip("Speed of waves")]
    [SerializeField] private float waveSpeed = 6f;
    
    //[SerializeField] private bool sharedMaterial = true;

    public Transform water;

    private Material waterMat;
    private Texture2D wavesDisplacement;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SetVariables();
    }

    void SetVariables()
    {
        /*if (sharedMaterial)
        {
            waterMat =water.GetComponent<Renderer>().sharedMaterial : waterMat = water.GetComponent<Renderer>().material;
        }
        else
        {*/
        waterMat = water.GetComponent<Renderer>().material;
        
        wavesDisplacement = (Texture2D) waterMat.GetTexture("_WavesDisplacement");
    }

    public float WaterHeightAtPosition(Vector3 position)
    {
        return water.position.y + wavesDisplacement.GetPixelBilinear(position.x * (waveFrequency/100), 
            position.z * (waveFrequency/100) + Time.time * (waveSpeed/100)).g * waveHeight * water.localScale.x;
    }

    private void OnValidate()
    {
        if (!waterMat)
        {
            SetVariables();
        }

        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        waterMat.SetFloat("_WavesFrequency", waveFrequency/100);
        waterMat.SetFloat("_WavesSpeed",waveSpeed/100);
        waterMat.SetFloat("_WaveHeight",waveHeight);
    }
}
