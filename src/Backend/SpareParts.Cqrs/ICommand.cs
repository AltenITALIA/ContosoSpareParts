namespace SpareParts.Cqrs
{
    public interface ICommand : IMessage
    {
    }

    public abstract class CommandBase : ICommand
    {
    }
}
