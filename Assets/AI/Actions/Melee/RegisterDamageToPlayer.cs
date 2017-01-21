using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;
using RAIN.Representation;


namespace Sacristan.Melee
{
    [RAINAction]
    public class RegisterDamageToPlayer : RAINAction
    {
        public Expression Damage = new Expression();
        public Expression DamagePeriod = new Expression();

        GameObject playerObject;

        float nodeEntryTime;
        float lastAttackTime;

        private int damage;
        private float damagePeriod;
        private string memoryKey = "lastAttackTime";

        public override void Start(RAIN.Core.AI ai)
        {
            nodeEntryTime = Time.timeSinceLevelLoad;
            lastAttackTime = ai.WorkingMemory.GetItem<float>(memoryKey);
            playerObject = ai.WorkingMemory.GetItem<GameObject>("player");

            damage = System.Int32.Parse(Damage.ToString());
            damagePeriod = System.Single.Parse(DamagePeriod.ToString());
        }

        public override ActionResult Execute(RAIN.Core.AI ai)
        {
            if (nodeEntryTime - lastAttackTime > damagePeriod)
            {
                lastAttackTime = nodeEntryTime;
                ai.WorkingMemory.SetItem<float>(memoryKey, nodeEntryTime);
                playerObject.SendMessage("ReceiveDamage", damage);
                return ActionResult.SUCCESS;
            }
            else
            {
                return ActionResult.FAILURE;
            }


        }

    }
}