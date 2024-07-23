using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField]
    private bool _runCreateCard = false;

    [SerializeField]
    private bool _runDrawCard = false;

    private void Update()
    {
        if(this._runCreateCard)
        {
            _runCreateCard = false; ;
            HearthstoneAPIManager.instance.CreateCard();
        }

        if(this._runDrawCard)
        {
            _runDrawCard = false;
           // DeckOfCardsAPIManager.Instance.DrawCard();
        }
    }
}
