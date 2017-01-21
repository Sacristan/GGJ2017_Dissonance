using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private static ScoreManager singletone;

    #region MonoBehaviour
    private void Awake()
    {
        if (singletone == null) singletone = this;
        else Destroy(singletone);
    }

    private void OnDestroy()
    {

    }
    #endregion

}
