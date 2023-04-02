using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static Sound _instance;
    public AudioSource _audioSource;
    public List<AudioClip> _audioClip = new List<AudioClip>();
    public float volume;
    public int _pred;
    public int _pblue;
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (_pred > 0 || _pblue > 0)
        {
            if (_pblue == 1)
            {
                _audioSource.PlayOneShot(_audioClip[0]);
            }
            if (_pred == 1)
            {
                _audioSource.PlayOneShot(_audioClip[1]);
            }
            if (_pblue >= 2 && _pblue <= 4)
            {
                _audioSource.PlayOneShot(_audioClip[5]);
            }
            if (_pblue > 4)
            {
                _audioSource.PlayOneShot(_audioClip[6]);
            }
            _pred = 0;
            _pblue = 0;
        }
    }
}
