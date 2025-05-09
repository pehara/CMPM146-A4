using System.Collections.Generic;

public class Selector : InteriorNode
{
    public override Result Run()
    {
        return Result.FAILURE;
    }

    public Selector(IEnumerable<BehaviorTree> children) : base(children)
    {
    }

    public override BehaviorTree Copy()
    {
        return new Selector(CopyChildren());
    }

}
