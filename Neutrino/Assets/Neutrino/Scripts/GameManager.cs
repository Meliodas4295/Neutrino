using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
    public AudioSource _audioSource;
    public TextMeshProUGUI _nbLevel;
    public TextMeshProUGUI _finalScore;
    public Slider _slider;
    public float score = 0;
    private float _timer;
    public float _speed = 4;
    public int _level = 1;

    public float _life = 10;
    public float _scoreTimer;
    private float _t;
    public bool _isGameOver;


    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;
        _scoreTimer += Time.deltaTime;
        _t += Time.deltaTime;
        _slider.value = _life/10;
        if (_timer > 30)
        {
            _level++;
            _nbLevel.text = _level.ToString();
            _speed += 0.3f * _speed;
            _timer = 0;
        }
        if(_t>6)
        {
            _life -= _level;
            _t = 0;
        }
        if(_life <= 0 && !_isGameOver)
        {
            _isGameOver = true;
            _finalScore.text = ((int)_scoreTimer).ToString();
        }
        if (_isGameOver)
        {
            stateMachine.instance.SetState(State.SCORE);
        }
        Debug.Log(_life);
    }

    public void SwapPause()
    {
        if(Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        SceneManager.LoadScene("Romain");
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
