using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] SoundManager soundManager;
    [SerializeField] Image splash;
    [SerializeField] Sprite tutorialImage;

    private bool tutorialYet = false;

    void Start()
    {
        soundManager.PlayMenuMusic();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {

            if (tutorialYet == false)
            {
                splash.sprite = tutorialImage;
                tutorialYet = true;
            }
            else
            {
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            }
        }
    }
}
