using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuManager : MonoBehaviour
{
    [SerializeField] SoundManager soundManager;

    void Start()
    {
        soundManager.PlayMenuMusic();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space)) SceneManager.LoadScene("GameScene", LoadSceneMode.Single);    
    }
}
