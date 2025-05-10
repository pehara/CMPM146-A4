using UnityEngine;
using System.Collections.Generic;

public class NearbyZombiesQuery : BehaviorTree
{
    int count;
    float distance;

    public override Result Run()
    {
        var nearby = GameManager.Instance.GetEnemiesInRange(agent.transform.position, distance);
        int zombieCount = 0;

        foreach (var enemy in nearby)
        {
            if (enemy == agent.gameObject) continue;

            var ec = enemy.GetComponent<EnemyController>();
            if (ec != null && ec.monster == "zombie")
            {
                zombieCount++;
            }
        }

        return (zombieCount >= count) ? Result.SUCCESS : Result.FAILURE;
    }

    public NearbyZombiesQuery(int count, float distance) : base()
    {
        this.count = count;
        this.distance = distance;
    }

    public override BehaviorTree Copy()
    {
        return new NearbyZombiesQuery(count, distance);
    }
}
