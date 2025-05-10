using UnityEngine;
using System.Collections.Generic;

public class BehaviorBuilder
{
    public static BehaviorTree MakeTree(EnemyController agent)
    {
        BehaviorTree result = null;

        if (agent.monster == "warlock")
        {
            // Warlocks attack and support allies, with debug logs to confirm behavior
            result = new Selector(new BehaviorTree[]
            {
                new Sequence(new BehaviorTree[] {
                    new NearbyEnemiesQuery(1, 5f),
                    new AbilityReadyQuery("heal"),
                    new DebugLog("Warlock is healing an ally."),
                    new Heal()
                }),
                new Sequence(new BehaviorTree[] {
                    new NearbyEnemiesQuery(1, 5f),
                    new AbilityReadyQuery("permabuff"),
                    new DebugLog("Warlock is applying a permanent buff."),
                    new PermaBuff()
                }),
                new Sequence(new BehaviorTree[] {
                    new NearbyEnemiesQuery(1, 5f),
                    new AbilityReadyQuery("buff"),
                    new DebugLog("Warlock is applying a temporary buff."),
                    new Buff()
                }),
                new Sequence(new BehaviorTree[] {
                    new AbilityReadyQuery("attack"),
                    new DebugLog("Warlock is attacking the player."),
                    new MoveToPlayer(agent.GetAction("attack").range - 1),
                    new Attack()
                }),
                new GoTo(AIWaypointManager.Instance.GetClosestByType(agent.transform.position, AIWaypoint.Type.SAFE).transform, 1.5f)
            });
        }
        else if (agent.monster == "zombie")
        {
            // Zombies avoid the player unless another zombie is near; otherwise wander forward
            result = new Selector(new BehaviorTree[]
            {
                new Sequence(new BehaviorTree[] {
                    new NearbyZombiesQuery(1, 4f),
                    new MoveToPlayer(agent.GetAction("attack").range),
                    new Attack()
                }),
                new GoTo(AIWaypointManager.Instance.GetClosestByType(agent.transform.position, AIWaypoint.Type.FORWARD).transform, 1.5f)
            });
        }
        else if (agent.monster == "skeleton")
        {
            // Skeletons flank then charge
            result = new Sequence(new BehaviorTree[]
            {
                new GoTo(AIWaypointManager.Instance.GetClosestByType(agent.transform.position, AIWaypoint.Type.FORWARD).transform, 1.5f),
                new MoveToPlayer(agent.GetAction("attack").range),
                new Attack()
            });
        }

        foreach (var node in result.AllNodes())
        {
            node.SetAgent(agent);
        }

        return result;
    }
}