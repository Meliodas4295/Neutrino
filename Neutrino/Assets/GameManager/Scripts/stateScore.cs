using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class stateScore : MonoBehaviour
{
    //[SerializeField] StateLeaderboard stateLeaderboard;
    //[SerializeField] StateMachine stateMatchine;
    //[SerializeField] ListPointTest gameplay;

    public Text count_text;
    private int score = 0;

    public string nameOfPlayer;
    public string saveName;

    public Text inputText;
    public Text loadedName;
    public GameObject buttonSave;
    public bool reAwake = false;
    public AudioSource audioMenu;
    public AudioSource audioIngame;
    public AudioSource audioEndgame;

    // Start is called before the first frame update
    void Start()
    {
        buttonSave.SetActive(true);

        //float score = listPointTest.Score;
        SetCountText();
        //score = (int)gameplay.Score;
    }

    // Update is called once per frame
    void Update()
    {
        nameOfPlayer = PlayerPrefs.GetString("name", "none");
        loadedName.text = nameOfPlayer;
    }

    void SetCountText()
    {
        count_text.text = score.ToString() + " m";
    }

    /*
    public void SetName()
    {
        saveName = inputText.text;
        PlayerPrefs.SetString("name", saveName);
        buttonSave.SetActive(true);
        buttonOk.SetActive(false);
    }
    */
    /*
    public void SaveLB()
    {
        saveName = inputText.text;
        PlayerPrefs.SetString("name", saveName);
        buttonSave.SetActive(false);
        stateLeaderboard.AddLeaderboardEntry(score, saveName);
    }

    public void OnClickMenu_Score()
    {
        audioMenu.Play();
        audioIngame.Stop();
        audioEndgame.Stop();
        StateMachine.instance.SetState(State.MENU);
        buttonSave.SetActive(true);
        reAwake = true;
    }

    public void OnClickIngame_Score()
    {
        audioMenu.Stop();
        audioIngame.Play();
        audioEndgame.Stop();
        StateMachine.instance.SetState(State.INGAME);
        buttonSave.SetActive(true);
        reAwake = true;

    }
    */
}
