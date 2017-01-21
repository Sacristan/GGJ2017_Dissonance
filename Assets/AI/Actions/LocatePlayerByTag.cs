using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

[RAINAction]
public class LocatePlayerByTag : RAINAction
{
    public Expression tagName = new Expression();
    public Expression targetVariable = new Expression();

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
        GameObject player = GameObject.FindGameObjectWithTag(tagName.ToString());
        ai.WorkingMemory.SetItem<GameObject>(targetVariable.ToString(), player);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}