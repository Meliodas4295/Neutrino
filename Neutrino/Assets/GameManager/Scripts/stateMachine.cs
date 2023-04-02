using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum State
{
    MENU,
    INGAME,
    SCORE,
    OPTION,

    TUTORIAL,

}

public class stateMachine : MonoBehaviour
{
    public State state;
    public GameObject guiMenu;
    public GameObject guiIngame;
    public GameObject guiScore;
    public GameObject guiOption;

    public GameObject guiTutorial;
    public GameObject tutorial_1;
    public GameObject tutorial_2;
    public GameObject tutorial_3;
    public GameObject tutorial_4;
    public GameObject button_1;
    public GameObject button_2;
    public bool commandState = true;
    public AudioSource audioSource;
    //public AudioSource audioMenu;
    //public AudioSource audioIngame;
    //public AudioSource audioEndgame;


    static public stateMachine instance;

    void Awake()
    {
        if (instance != null) Debug.LogError("Double Singleton!");
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //audioIngame.Stop();
        //audioEndgame.Stop();
        audioSource.Stop();
        Time.timeScale = 0;

    }

    // Update is called once per frame
    private void Update()
    {
        guiMenu.SetActive(state == State.MENU);
        guiIngame.SetActive(state == State.INGAME);
        guiScore.SetActive(state == State.SCORE);
        guiOption.SetActive(state == State.OPTION);

        guiTutorial.SetActive(state == State.TUTORIAL);

    }

    public void SetState(State newState)
    {
        state = newState;
    }

    public void OnClickOption()
    {
        SetState(State.OPTION);
    }

    public void OnClickTutorial()
    {
        SetState(State.TUTORIAL);
    }

    public void OnClickMenu()
    {
        SetState(State.MENU);
    }

    public void OnClickNext_1()
    {
        tutorial_1.SetActive(false);
        tutorial_2.SetActive(true);
    }

    public void OnClickNext_2()
    {
        tutorial_2.SetActive(false);
        tutorial_3.SetActive(true);
    }

    public void OnClickNext_3()
    {
        tutorial_3.SetActive(false);
        tutorial_4.SetActive(true);
    }

    public void OnClickPrevious_2()
    {
        tutorial_1.SetActive(true);
        tutorial_2.SetActive(false);
    }

    public void OnClickPrevious_3()
    {
        tutorial_2.SetActive(true);
        tutorial_3.SetActive(false);
    }
    public void OnClickPrevious_4()
    {
        tutorial_3.SetActive(true);
        tutorial_4.SetActive(false);
    }
    public void OnClickIngame()
    {
        audioSource.Play();
        Time.timeScale = 1;
        //audioMenu.Stop();
        //audioIngame.Play();
        //audioEndgame.Stop();
        SetState(State.INGAME);
    }

    public void OnClickChangeCommand_1()
    {
        commandState = false;
        button_1.SetActive(false);
        button_2.SetActive(true);
    }

    public void OnClickChangeCommand_2()
    {
        commandState = true;
        button_1.SetActive(true);
        button_2.SetActive(false);
    }


}
