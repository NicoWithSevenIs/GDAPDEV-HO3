using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHandler : MonoBehaviour
{

    [SerializeField] private GameObject gameContainer;


    private void Start()
    {
        gameContainer.SetActive(false);
    }
    public void Play()
    {
        gameContainer.SetActive(true);
        gameObject.SetActive(false);
    }
}
