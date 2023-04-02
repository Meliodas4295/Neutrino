using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

enum ElementType
{
    Red,
    Blue,
    None,
}
public class Element : MonoBehaviour
{
    public static Element _instance;

    [SerializeField]
    private float _radius;
    [SerializeField]
    private float _radiusDetector;
    [SerializeField]
    private GameObject _pbParticles;
    [SerializeField]
    private GameObject _pbDetector;
    [SerializeField]
    private Transform _detectorPos;
    [SerializeField]
    private List<Material> _mat = new List<Material>();
    [SerializeField]
    private List<LineRenderer> _lines = new List<LineRenderer>();

    public float radius
    {
        get { return _radiusDetector; }
        set { _radiusDetector = value; }
    }


    public Transform detector
    {
        get
        {
            return _detectorPos;
        }
        set
        {
            _detectorPos = value;
        }
    }

    private float _angles = 30;
    private int _layer = 1;
    private int _numberOfSection = 12;
    private Vector2 _firstPointParticles;
    private Vector2 _firstPointDetector;
    private List<List<GameObject>> _lightCone = new List<List<GameObject>>();
    private int _index;
    private int _indexBuffer = 0;
    private float timer;
    private List<float> timerCircle = new List<float>();
    private int _indexCircle;
    private bool _b;
    private float _offset = 1f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < _numberOfSection; i++)
        {
            _lightCone.Add(new List<GameObject>());
        }
        for (int i = 0; i < 3; i++)
        {
            timerCircle.Add(0);
        }
        _instance = this;
        _firstPointDetector= new Vector2(0, _radiusDetector);
        _firstPointParticles = new Vector2(0, _layer * _radius);
        initializedParticles();
        //CircleLineRenderer._instance.DrawCircle(110, _layer * _radius - _offset, _lines[_indexCircle]);
        _offset += 0.1f;
        _indexCircle++;
        InitializedDetector();
        foreach(var obj in _lightCone[0]) 
        {
            obj.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red * 4f);
            obj.GetComponent<SphereCollider>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        MovePositionConeDetector();
        OpenCone();
        if(timer > 6 - ((GameManager._instance._level-1)*0.5f))
        {
            /*if(_indexCircle == 2)
            {
                _indexCircle = 0;
            }*/
            initializedParticles();
            //_indexCircle++;
            timer = 0;
        }
        /*for (int i = 0; i < (_b?3:_indexCircle); i++)
        {
            timerCircle[i] += Time.deltaTime;
            if ((_layer * _radius) - timerCircle[i] > _radiusDetector)
            {
                CircleLineRenderer._instance.DrawCircle(110, (_layer * _radius) - timerCircle[i]*_offset, _lines[i]);
            }
            else
            {
                timerCircle[i] = 0;
                _offset += 0.1f;
                //CircleLineRenderer._instance.DrawCircle(110, 0, _lines[i]);
                _b = true;
            }
        }*/

    }

    void InitializedDetector()
    {
        float initialradiusDetector = _radiusDetector;
        for (int i = 0; i < _numberOfSection; i++)
        {
            _radiusDetector = initialradiusDetector;
            _firstPointDetector = new Vector2(0, _radiusDetector);
            for (int j = 1; j < 4; j++)
            {
                _radiusDetector += j*0.5f;
                _firstPointDetector = new Vector2(0, _radiusDetector);
                GameObject b = Instantiate(_pbDetector, Quaternion.AngleAxis(_angles * i, Vector3.forward) * _firstPointDetector, _pbDetector.transform.rotation);
                b.GetComponent<Renderer>().material = _mat[i];
                _lightCone[i].Add(b);


            }
        }
    }

    void initializedParticles()
    {

        for (int i = 0; i < _numberOfSection; i++)
        {
            Instantiate(_pbParticles, Quaternion.AngleAxis(_angles*i,Vector3.forward)*_firstPointParticles, _pbParticles.transform.rotation);
        }
    }

    void MovePositionConeDetector()
    {
        if (Time.timeScale == 1)
        {
            MoveLeft();
            MoveRight();
        }
    }

    void OpenCone()
    {
        if(Time.timeScale == 1) 
        {
            Increase();
            Decrease();
        }

    }

    private void Decrease()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Sound._instance._audioSource.PlayOneShot(Sound._instance._audioClip[4]);
            if (_indexBuffer >= 0)
            {
                for (int j = 0; j < _lightCone[0].Count; j++)
                {
                    if (_index + _indexBuffer >= 12)
                    {
                        _lightCone[_index + _indexBuffer - 12][j].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
                        _lightCone[_index + _indexBuffer - 12][j].GetComponent<SphereCollider>().enabled = false;
                    }
                    else
                    {
                        _lightCone[_index + _indexBuffer][j].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
                        _lightCone[_index + _indexBuffer][j].GetComponent<SphereCollider>().enabled = false;
                    }
                }
                _indexBuffer--;
            }
        }
    }

    private void Increase()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Sound._instance._audioSource.PlayOneShot(Sound._instance._audioClip[3]);
            if (_indexBuffer < _numberOfSection)
            {
                _indexBuffer++;
                for (int j = 0; j < _lightCone[0].Count; j++)
                {
                    if (_index + _indexBuffer >= 12)
                    {
                        _lightCone[_index + _indexBuffer - 12][j].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red * 7f);
                        _lightCone[_index + _indexBuffer - 12][j].GetComponent<SphereCollider>().enabled = true;

                    }
                    else
                    {
                        _lightCone[_index + _indexBuffer][j].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red * 7f);
                        _lightCone[_index + _indexBuffer][j].GetComponent<SphereCollider>().enabled = true;
                    }
                }
            }
        }
    }

    private void MoveLeft()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Sound._instance._audioSource.PlayOneShot(Sound._instance._audioClip[2], 0.55f);
            foreach (var obj in _lightCone[_index])
            {
                obj.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
                obj.GetComponent<SphereCollider>().enabled = false;
            }
            if (_index == 11)
            {
                _index = 0;
            }
            else
            {
                _index++;
            }
            for (int i = 0; i < _indexBuffer+1; i++)
            {
                for(int j = 0; j < _lightCone[0].Count;j++)
                {
                    if (_index + i >= 12)
                    {
                        _lightCone[_index + i - 12][j].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red * 7f);
                        _lightCone[_index + i - 12][j].GetComponent<SphereCollider>().enabled = true;
                    }
                    else
                    {
                        _lightCone[_index + i][j].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red * 7f);
                        _lightCone[_index + i][j].GetComponent<SphereCollider>().enabled = true;
                    }
                }
            }
        }


    }

    private void MoveRight()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Sound._instance._audioSource.PlayOneShot(Sound._instance._audioClip[2],0.55f);
            for (int i = 0; i < _lightCone[0].Count; i++)
            {
                if(_index + _indexBuffer >= 12)
                {
                    _lightCone[_index + _indexBuffer - 12][i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
                    _lightCone[_index + _indexBuffer - 12][i].GetComponent<SphereCollider>().enabled = false;
                }
                else
                {
                    _lightCone[_index + _indexBuffer][i].GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.white);
                    _lightCone[_index + _indexBuffer][i].GetComponent<SphereCollider>().enabled = false;
                }
            }

            if (_index == 0)
            {
                _index = 11;
            }
            else
            {
                _index--;
            }
            foreach (var obj in _lightCone[_index])
            {
                obj.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red * 7f);
                obj.GetComponent<SphereCollider>().enabled=true;
            }
        }
    }
}
