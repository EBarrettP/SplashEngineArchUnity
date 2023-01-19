using System;
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
    
    [SerializeField] private bool sharedMaterial = true;

    public Transform water;
    [SerializeField] private Material baseWaterMat;
    
    private Material waterMat;
    private Texture2D wavesDisplacement;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SetVariables();
        UpdateMaterial();
    }

    void SetVariables()
    {
        water.GetComponent<Renderer>().material = baseWaterMat;
        waterMat = sharedMaterial ? water.GetComponent<Renderer>().sharedMaterial : water.GetComponent<Renderer>().material;
        wavesDisplacement = (Texture2D) waterMat.GetTexture("_WavesDisplacement");
    }

    public float WaterHeightAtPosition(Vector3 position)
    {
        waveFrequency = waterMat.GetFloat("_WavesFrequency");
        waveHeight = waterMat.GetFloat("_WaveHeight");
        waveSpeed = waterMat.GetFloat("_WavesSpeed");
                    
        return water.position.y + wavesDisplacement.GetPixelBilinear(position.x * (waveFrequency), 
            position.z * (waveFrequency) + Time.time * (waveSpeed)).g * waveHeight * water.localScale.x;
    }

    private void OnValidate()
    {
        SetVariables();
        UpdateMaterial();
    }

    void UpdateMaterial()
    {
        waterMat.SetFloat("_WavesFrequency", waveFrequency/100);
        waterMat.SetFloat("_WavesSpeed",waveSpeed/100);
        waterMat.SetFloat("_WaveHeight",waveHeight);
    }
}
