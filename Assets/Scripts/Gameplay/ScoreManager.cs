﻿using UnityEngine;
using Sacristan.Messaging;
using System;

public class ScoreManager : MonoBehaviour
{
    #region Fields

    public delegate void ScoreChanged();
    public static event ScoreChanged OnScoreChanged;

    private static ScoreManager singletone;

    [SerializeField]
    private int score = 0;
    #endregion

    #region Properties
    public static int Score { get { return singletone.score; } }
    #endregion

    #region MonoBehaviour
    private void Awake()
    {
        if (singletone == null) singletone = this;
        else Destroy(singletone);

        Messenger.AddListener(Messages.DiedNest, HandleDiedNest);
        Messenger.AddListener(Messages.DiedRangedMob, HandleDiedRangedMob);
        Messenger.AddListener(Messages.DiedMeleeMob, HandleDiedMeleeMob);
        Messenger.AddListener(Messages.DiedNestMob, HandleDiedNestMob);

        Messenger<float>.AddListener(Messages.ReceivedDamageNest, HandleReceivedDamageNest);
        Messenger<float>.AddListener(Messages.ReceivedDamageRangedMob, HandleReceivedDamageRangedMob);
        Messenger<float>.AddListener(Messages.ReceivedDamageMeleeMob, HandleReceivedDamageMeleeMob);
        Messenger<float>.AddListener(Messages.ReceivedDamageNestMob, HandleReceivedDamageNestMob);
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(Messages.DiedNest, HandleDiedNest);
        Messenger.RemoveListener(Messages.DiedRangedMob, HandleDiedRangedMob);
        Messenger.RemoveListener(Messages.DiedMeleeMob, HandleDiedMeleeMob);
        Messenger.RemoveListener(Messages.DiedNestMob, HandleDiedNestMob);

        Messenger<float>.RemoveListener(Messages.ReceivedDamageNest, HandleReceivedDamageNest);
        Messenger<float>.RemoveListener(Messages.ReceivedDamageRangedMob, HandleReceivedDamageRangedMob);
        Messenger<float>.RemoveListener(Messages.ReceivedDamageMeleeMob, HandleReceivedDamageMeleeMob);
        Messenger<float>.RemoveListener(Messages.ReceivedDamageNestMob, HandleReceivedDamageNestMob);
    }
    #endregion

    #region Score Calc
    private void AddScore(int scoreToAdd)
    {
        if (OnScoreChanged != null) OnScoreChanged();
        score += scoreToAdd;
    }
    #endregion

    #region Messenger Event Handlers
    private void HandleDiedNest()
    {
        AddScore(ScoreTable.KilledNestScore);
    }

    private void HandleDiedRangedMob()
    {
        AddScore(ScoreTable.KilledRangedMobScore);
    }

    private void HandleDiedMeleeMob()
    {
        AddScore(ScoreTable.KilledMeeleeMobScore);
    }

    private void HandleDiedNestMob()
    {
        AddScore(ScoreTable.KilledNestMobScore);
    }

    private void HandleReceivedDamageNestMob(float arg1)
    {
        AddScore(ScoreTable.DamagedNestMobScore);
    }

    private void HandleReceivedDamageMeleeMob(float arg1)
    {
        AddScore(ScoreTable.DamagedMeleeMobScore);
    }

    private void HandleReceivedDamageRangedMob(float arg1)
    {
        AddScore(ScoreTable.DamagedRangedMobScore);
    }

    private void HandleReceivedDamageNest(float arg1)
    {
        AddScore(ScoreTable.DamagedNestScore);
    }

    #endregion
}