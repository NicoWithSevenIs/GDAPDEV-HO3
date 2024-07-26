using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private bool _runCreateCard = false;


    private void Update()
    {
        if(this._runCreateCard)
        {
            _runCreateCard = false; ;
            HearthstoneAPIManager.instance.CreateCard();
        }
    }
}
