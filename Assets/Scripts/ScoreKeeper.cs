using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    int score = 0;
    TextMeshProUGUI scoretext;
    private void Start()
    {
        scoretext= GetComponent<TextMeshProUGUI>();
        scoretext.text = "Start";
    }

    public void IncreaseScore(int value)
    {
        score += value;
        scoretext.text = $"score :{score}";
    }
}
