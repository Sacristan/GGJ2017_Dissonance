using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;

[RAINAction]
public class SetLastKnownPosition : RAINAction
{

    public Expression sourceVariable = new Expression();
    public Expression formVariable = new Expression();

    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        GameObject player = ai.WorkingMemory.GetItem<GameObject>(sourceVariable.ToString());
        ai.WorkingMemory.SetItem<Vector3>(formVariable.ToString(), player.transform.position);
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}