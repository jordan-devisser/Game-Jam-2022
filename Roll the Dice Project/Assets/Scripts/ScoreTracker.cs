using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTracker : MonoBehaviour
{
    public int score = 0;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public int GetScore()
    {
        return score;
    }

    public void AddToScore(int numAdd)
    {
        score += numAdd;
    }

    public void SetScore(int newScore)
    {
        score = newScore;
    }

    public void ResetScore()
    {
        score = 0;
    }
}
