using System.Collections.Generic;

public class Selector : InteriorNode
{
    public override Result Run()
    {
        if (current_child >= children.Count)
        {
            current_child = 0;
            return Result.FAILURE;
        }

        var result = children[current_child].Run();

        if (result == Result.SUCCESS)
        {
            current_child = 0;
            return Result.SUCCESS;
        }
        else if (result == Result.IN_PROGRESS)
        {
            return Result.IN_PROGRESS;
        }
        else
        {
            current_child++;
            return Result.IN_PROGRESS;
        }
    }

    public Selector(IEnumerable<BehaviorTree> children) : base(children) { }

    public override BehaviorTree Copy()
    {
        return new Selector(CopyChildren());
    }
}
