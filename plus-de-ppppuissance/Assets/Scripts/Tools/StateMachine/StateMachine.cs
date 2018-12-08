using System;
using System.Collections;
using System.Collections.Generic;

public class StateMachine<State, Command>
    where State : struct, IConvertible, IComparable, IFormattable
    where Command : struct, IConvertible, IComparable, IFormattable
{
    protected Dictionary<StateTransition<State, Command>, State> transitions;
    public State currentState { get; private set; }

    public StateMachine(State currentState)
    {
        this.currentState = currentState;
        transitions = new Dictionary<StateTransition<State, Command>, State>(){};
    }

    public State GetNext(Command command)
    {
        StateTransition<State, Command> transition = new StateTransition<State, Command>(currentState, command);

        State nextState;

        if (!transitions.TryGetValue(transition, out nextState))
        {
            return currentState;
        }

        return nextState;
    }

    public State MoveNext(Command command)
    {
        currentState = GetNext(command);
        return currentState;
    }
}
