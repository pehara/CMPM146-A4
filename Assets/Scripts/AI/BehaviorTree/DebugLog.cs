using UnityEngine;

public class DebugLog : BehaviorTree
{
    string message;

    public override Result Run()
    {
        Debug.Log(agent.monster + " (BT Log): " + message);
        return Result.SUCCESS;
    }

    public DebugLog(string message) : base()
    {
        this.message = message;
    }

    public override BehaviorTree Copy()
    {
        return new DebugLog(message);
    }
}
