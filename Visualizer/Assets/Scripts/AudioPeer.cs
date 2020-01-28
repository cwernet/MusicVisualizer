using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioPeer : MonoBehaviour
{
    //public bool _useMicrophone = false;
    //public AudioClip _audioClip;
    //public string _selectedDevice;

    AudioSource _audioSource;
    float[] _samples = new float[512];
    float[] _freqBand = new float[8];
    float[] _bandBuffer = new float[8];
    float[] _bufferDecrease = new float[8];

    float[] _freqBandHighest = new float[8];
    public static float[] _audioBand = new float[8];
    public static float[] _audioBandBuffer = new float[8];

    public static float _Amplitude, _AmplitudeBuffer;
    float _AmplitudeHighest;
    public float _audioProfile;

    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        AudioProfile(_audioProfile);

        //if (_useMicrophone)
        //{
        //    if (Microphone.devices.Length > 0)
        //    {
        //        _selectedDevice = Microphone.devices[0].ToString();
        //        _audioSource.clip = Microphone.Start(_selectedDevice, true, 10, AudioSettings.outputSampleRate);
        //    }
        //    else
        //    {
        //        _useMicrophone = false;
        //    }
        //}
        //else
        //{
        //    _audioSource.clip = _audioClip;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        GetSpectrumAudioSource();
        MakeFrequencyBands();
        BandBuffer();
        CreateAudioBands();
        GetAmplitude();
    }

    void AudioProfile(float audioProfile)
    {
        for (int i = 0; i < 8; i++)
        {
            _freqBandHighest[i] = audioProfile;
        }
    }

    void GetAmplitude()
    {
        float _CurrentAmplitude = 0, _CurrenAmplitudeBuffer = 0;

        for (int i = 0; i < 8; i++)
        {
            _CurrentAmplitude += _audioBand[i];
            _CurrenAmplitudeBuffer += _audioBandBuffer[i];
        }

        if (_CurrentAmplitude > _AmplitudeHighest)
        {
            _AmplitudeHighest = _CurrentAmplitude;
        }
        _Amplitude = _CurrentAmplitude / _AmplitudeHighest;
        _AmplitudeBuffer = _CurrenAmplitudeBuffer / _AmplitudeHighest;
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freqBand[i] > _freqBandHighest[i])
                _freqBandHighest[i] = _freqBand[i];

            _audioBand[i] = _freqBand[i] / _freqBandHighest[i];
            _audioBandBuffer[i] = _bandBuffer[i] / _freqBandHighest[i];
        }

    }


    void BandBuffer()
    {
        for (int g = 0; g < 8; ++g)
        {
            if (_freqBand[g] > _bandBuffer[g])
            {
                _bandBuffer[g] = _freqBand[g];
                _bufferDecrease[g] = 0.0005f;
            }

            if (_freqBand[g] < _bandBuffer[g])
            {
                _bandBuffer[g] -= _bufferDecrease[g];
                _bufferDecrease[g] *= 1.2f;
            }
        }

    }

    void GetSpectrumAudioSource()
    {
        _audioSource.GetSpectrumData(_samples, 0, FFTWindow.Blackman);
    }

    void MakeFrequencyBands()
    {
        /*
         *22050/512 = 43 hz per sample
         *
         * 20-60
         * 60-250
         * 250-500
         * 500-2000
         * 2000-4000
         * 6000-2000
         *
         * 0 -2 = 86 hertz
         * 1 - 4 = 172 hz - 87 -258 hz
         * 2 - 8 = 344 hz -  259 - 602
         * 3 - 16 = 688hz  603 - 1290
         * 4 - 32 = 2752  2667 - 5418
         * 5 - 64 = 2752   2667 5418
         * 6 - 128 = 5504   5419 - 10922
         * 7 - 256 = 11008 -  10923 - 21930 
         * 
         */


        int count = 0;

        for (int i = 0; i < 8; i++)
        {
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            float average = 0;
            if (i == 7)
                sampleCount += 2;

            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count] * (count + 1);
                count++;
            }

            average /= count;

            _freqBand[i] = average * 10;
        }


    }
}
