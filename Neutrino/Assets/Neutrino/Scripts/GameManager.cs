using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;
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
        if (_timer > 30)
        {
            _level++;
            _speed += 0.3f * _speed;
            _timer = 0;
        }
        if(_t>6)
        {
            _life -= _level;
            _t = 0;
        }
        if(_life == 0)
        {
            _isGameOver = true;
        }
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
