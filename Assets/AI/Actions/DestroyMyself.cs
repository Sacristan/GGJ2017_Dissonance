using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class DestroyMyself : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
        KillMyself(ai.Body);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
    {
        return ActionResult.SUCCESS;
    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }

    private void KillMyself(GameObject go)
    {
        UnityEngine.GameObject.Destroy(go);
    }
}