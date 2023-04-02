using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Particle : MonoBehaviour
{
    [SerializeField]
    private List<Material> _mats;
    //[SerializeField]
    private float _speed = 4;
    private Rigidbody _rb;
    private VisualEffect _vfx;
    private ElementType _state;
    private float p1 = 3;
    private int p2 = 4;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _vfx = GetComponent<VisualEffect>();
        p1 = (0.8f - (1.0f / (GameManager._instance._level + 1))) * 10;
        ChooseTypeParticle();
        AffectMaterial();
        //_speed += _speed * 0.15f * (GameManager._instance._level - 1);
        _speed = GameManager._instance._speed;
    }

    // Update is called once per frame
    void Update()
    {
        MoveToCenter();
        if (Mathf.Pow(transform.position.x, 2) + Mathf.Pow(transform.position.y, 2) <= Mathf.Pow(Element._instance.radius, 2))
        {
            Destroy(gameObject);
            //gameObject.SetActive(false);
        }
    }

    private void AffectMaterial()
    {
        if (_state == ElementType.Blue)
        {
            gameObject.GetComponent<Renderer>().material = _mats[0];
        }
        if (_state == ElementType.Red)
        {
            gameObject.GetComponent<Renderer>().material = _mats[1];
        }
        if (_state == ElementType.None)
        {
            Destroy(gameObject);
        }
    }

    private void ChooseTypeParticle()
    {
        float randomP = Random.Range(0, 10);
        if (randomP >= p1)
        {
            _state = ElementType.None;
        }
        else
        {
            float random = Random.Range(0, 10);
            if (random <= p2)
            {
                _state = ElementType.Red;
            }
            if (random >= p2)
            {
                _state = ElementType.Blue;
            }
        }
    }

    void MoveToCenter()
    {
        Vector3 direction = (Element._instance.detector.transform.position - transform.position).normalized;
        Vector3 directionYX = new Vector3(direction.x, direction.y, 0);
        _rb.velocity = directionYX * _speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Detector>())
        {
            if (_state == ElementType.Blue)
            {
                //Sound._instance._audioSource.PlayOneShot(Sound._instance._audioClip[0]);
                GameManager._instance.score++;
                GameManager._instance._life++;
                Sound._instance._pblue += 1;
            }
            if (_state == ElementType.Red)
            {
                //Sound._instance._audioSource.PlayOneShot(Sound._instance._audioClip[1]);
                GameManager._instance.score--;
                GameManager._instance._life--;
                Sound._instance._pred += 1;
            }
            Destroy(gameObject);
        }
        /*if (other.CompareTag("Origin"))
        {
            gameObject.SetActive(false);
        }*/
    }
}
