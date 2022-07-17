using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] SoundManager soundManager;
    [SerializeField] TextMeshProUGUI textField;
    private int score;

    void Start()
    {
        soundManager.PlayDeadMusic();

        GameObject scoreTracker = GameObject.FindWithTag("ScoreTracker");

        if (scoreTracker)
        {
            ScoreTracker tracker = scoreTracker.GetComponent<ScoreTracker>();
            score = tracker.GetScore();

            textField.text = "Your Score: " + score;

            Destroy(scoreTracker);
        }
        else
        {
            Debug.Log("Error! No score tracker found.");
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }
}
