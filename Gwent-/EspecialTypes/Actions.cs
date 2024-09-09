//Base class for all the posible acctions in the game
using TokenClass;
public abstract class Action { }

/// <summary>
/// AST for operation with two element(+ - / * ||)
/// </summary>
public class BinaryAction : Action
{
    public Action Left { get; }
    public Token Operator { get; }
    public Action Right { get; }

    public BinaryAction(Action left, Token Operator, Action right)
    {
        this.Operator = Operator;
        Right = right;
        Left = left;
    }
}

/// <summary>
/// case of actions that only needs a one parametrs(++ and --)
/// </summary>
public class UnaryAction : Action
{
    public Token ID { get; }
    public Token Operation { get; }

    public UnaryAction(Token id, Token op)
    {
        ID = id;
        Operation = op;
    }
}

/// <summary>
/// The asignation tree of a variable 
/// </summary>
public class Asignation : Action
{
    public string Name { get; }  //The name unique of each variable
    public Action Value { get; } //Represent the actions to determinate the value
    public Asignation(string name, Action value)
    {
        Name = name;
        Value = value;
    }
}

/// <summary>
/// The representation of anytipe of value 
/// </summary>
public class Atom : Action
{
    public Token Id { get; }
    public Atom(Token id) => Id = id;
}

public class WhileAction : Action
{
    public Action Condition { get; }
    public List<Action> Body { get; }

    public WhileAction(Action condition, List<Action> body)
    {
        Condition = condition;
        Body = body;
    }
}

public class ForeachAction : Action
{
    public Token Reference { get; }

    public List<Card> Targets { get; }

    public List<Action> Actions { get; }

    public ForeachAction(Token reference, List<Card> targets, List<Action> actions)
    {
        Reference = reference;
        Targets = targets;
        Actions = actions;
    }
}

public class ActionInstruction : Action
{
    public List<Card> Targets { get; } = new();
    public List<Action> Body { get; }
    public ActionInstruction(List<Card> targets, List<Action> body)
    {
        Targets = targets;
        Body = body;
    }
}


public class Predicate : Action
{
    public Card SourceToCompare { get; set; } = new();
    public Token Operation { get; set; }
    public Action ComparerWith { get; set; }

    public Predicate(Card card, Token token, Action action)
    {
        SourceToCompare = card;
        Operation = token;
        ComparerWith = action;
    }

}
