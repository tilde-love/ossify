namespace Ossify.Bindings
{
    // public class Change
    // {
    //     public long Current { get; private set; }
    //
    //     public void Increment() => Current = (Current + 1) % (long.MaxValue - 1);
    // }
    //
    // public class ChangeChecker
    // {
    //     private readonly Change change;
    //
    //     private long lastChange = -1;
    //
    //     public bool HasChanged { get; private set; }
    //
    //     public ChangeChecker(Change change) => this.change = change;
    //
    //     /// <summary>
    //     /// Check the state and update the lastActionId 
    //     /// </summary>
    //     /// <returns>true if the action has changed</returns>
    //     public bool Check()
    //     {
    //         HasChanged = lastChange != change.Current;
    //
    //         lastChange = change.Current;
    //
    //         return HasChanged;
    //     }
    //
    //     public void Reset() => lastChange = -1;
    // }
}