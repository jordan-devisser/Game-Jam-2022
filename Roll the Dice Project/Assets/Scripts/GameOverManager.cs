using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] SoundManager soundManager;
    private int score;

    void Start()
    {
        soundManager.PlayDeadMusic();

        GameObject scoreTracker = GameObject.FindWithTag("ScoreTracker");

        if (scoreTracker)
        {
            ScoreTracker tracker = scoreTracker.GetComponent<ScoreTracker>();
            score = tracker.GetScore();

            Debug.Log("Score was: " + score);

            Destroy(scoreTracker);
        }
        else
        {
            Debug.Log("Found " + scoreTracker);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
