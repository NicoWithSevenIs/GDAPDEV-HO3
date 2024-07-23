using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HearthstoneAPIManager : MonoBehaviour
{




    #region Singleton

    public static HearthstoneAPIManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else Destroy(gameObject);
    }

    #endregion

}
