using System;

public class StateTransition <State, Command>
    where State : struct, IConvertible, IComparable, IFormattable
    where Command : struct, IConvertible, IComparable, IFormattable
{
    readonly State currentState;
    readonly Command command;

    public StateTransition(State currentState, Command command)
    {
        if (!typeof(State).IsEnum || !typeof(Command).IsEnum)
        {
            throw new ArgumentException("Error in state machine type");
        }

        this.currentState = currentState;
        this.command = command;
    }

    public override int GetHashCode()
    {
        return 17 + 31 * currentState.GetHashCode() + 31 * command.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        if (ReferenceEquals(this, obj))
            return true;

        StateTransition<State, Command> other = obj as StateTransition<State, Command>;
        return other != null && currentState.Equals(other.currentState) && this.command.Equals(other.command);
    }
}
