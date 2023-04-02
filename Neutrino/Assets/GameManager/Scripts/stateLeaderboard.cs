using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class stateLeaderboard : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;

    private void Awake()
    {
        entryContainer = transform.Find("LeaderboardEntryContainer");
        entryTemplate = entryContainer.Find("LeaderboardEntryTemplate");

        entryTemplate.gameObject.SetActive(false);
        float templateHeight = 40f;
        for (int i = 0; i < 10; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, templateHeight * i);
            entryTransform.gameObject.SetActive(true);

            int rank = i + 1;
            string rankString;
            switch (rank)
            {
            default:
                rankString = rank + "ieme";break;
                case 1: rankString = "1er";break;

            }

            entryTransform.Find("Rank_Text").GetComponent<Text>().text = rankString;

            int score = Random.Range(0, 10000);

            entryTransform.Find("Score_Text").GetComponent<Text>().text = score.ToString();
            entryTransform.Find("Name_Text").GetComponent<Text>().text = "AAA";
        }

    }
}