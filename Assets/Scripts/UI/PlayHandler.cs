using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHandler : MonoBehaviour
{

    [SerializeField] private GameObject gameContainer;


    private void Start()
    {
        gameContainer.SetActive(false);
        Time.timeScale = 0f;
    }
    public void Play()
    {
        gameContainer.SetActive(true);
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
