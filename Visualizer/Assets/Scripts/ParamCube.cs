using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParamCube : MonoBehaviour
{
    public int _band;
    public float _startScale, _scaleMultiplier, _emissionIntensity;
    public bool _useBuffer;
    Material _material;
    Renderer _renderer;
    Color _ogColor;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        _material = _renderer.sharedMaterial;
        _ogColor = _material.GetColor("_EmissionColor1");

        _material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

    }

    // Update is called once per frame
    void Update()
    {
        if (_useBuffer)
        {

            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._audioBandBuffer[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
            _material.SetColor("_EmissionColor1", _ogColor * (AudioPeer._audioBandBuffer[_band] * _emissionIntensity));
            //Debug.Log(_color);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._audioBand[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
            Color _color = new Color(AudioPeer._audioBand[_band], AudioPeer._audioBand[_band], AudioPeer._audioBand[_band]);
            _material.SetColor("_EmissionColor1", _color);
        }
    }
}
