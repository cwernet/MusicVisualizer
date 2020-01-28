using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDisplacementController : MonoBehaviour
{
    public float vertDisplacementAmount, emissDisplacementAmount, _emissionIntensity;

    //public ParticleSystem explosionParticles;
    Renderer _renderer;
    Material _material;
    public int _band;
    Color _ogColor;
    Color _ogColor2;

    // Start is called before the first frame update
    void Start()
    {
        //_material.EnableKeyword("_EMISSION");
        
        _renderer = GetComponent<Renderer>();
        _ogColor = _renderer.material.GetColor("_EmissionColor1");
        _ogColor2 = _renderer.material.GetColor("_EmissionColor2");

        //_material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;
    }

    // Update is called once per frame
    void Update()
    {

        //shader stufff
        float displacement = vertDisplacementAmount * AudioPeer._audioBandBuffer[_band];
        float emissionDisplacement = emissDisplacementAmount * AudioPeer._audioBandBuffer[_band];
        float emissionDisplacement2 = emissDisplacementAmount * AudioPeer._audioBandBuffer[_band];

        //vertex speed
        displacement = Mathf.Lerp(displacement, 0, Time.deltaTime);
        _renderer.material.SetFloat("_Amount", displacement);

        //emmission speed
        emissionDisplacement = Mathf.Lerp(emissionDisplacement, 0, Time.deltaTime);
        _renderer.material.SetFloat("_EmissionAmount", emissionDisplacement);

        emissionDisplacement2 = Mathf.Lerp(emissionDisplacement2, 0, Time.deltaTime);
        _renderer.material.SetFloat("_EmissionAmount", emissionDisplacement2);

        //emission color intensity
        _renderer.material.SetColor("_EmissionColor1", _ogColor * (AudioPeer._audioBandBuffer[_band] * _emissionIntensity));

        //emission color intensity
        _renderer.material.SetColor("_EmissionColor2", _ogColor2 * (AudioPeer._audioBandBuffer[_band] * _emissionIntensity));

    }
}
