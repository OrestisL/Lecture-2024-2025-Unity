//Abstract command class can be used for any command pattern implementation.
public abstract class Command
{
    public abstract void Execute();
    public abstract bool IsFinished { get; }
}