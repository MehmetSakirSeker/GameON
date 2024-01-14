using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public static int score;
    [SerializeField] private Text scoreText;
    void Start()
    {
        scoreText.text = "Score : " + score;
    }
}
