/// <summary>
/// Selector that succeeeds if ControlledAI is marked as 'tagged'
/// </summary>
public class ActorIsTagged : Selector
{
    bool tagged;
    protected override bool CheckCondition()
    {
        tagged = false;
        if (ControlledAI.IsTagged) {
            tagged = true;
            return tagged;
        }
        return tagged;
    }
}