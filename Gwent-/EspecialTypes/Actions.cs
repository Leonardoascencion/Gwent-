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
/// The asignation tree of a object
/// </summary>
public class ObjectAsignation : Action
{
    public Action ID { get; set; } //Id stands for identificator
    public Action Value { get; set; }

    public ObjectAsignation(Action id, Action value)
    {
        ID = id;
        Value = value;
    }
}

/// <summary>
/// The asignation tree of a variable 
/// </summary>
public class VariableAsignation : Action
{
    public string Name { get; }  //The name unique of each variable
    public Token Value { get; } //Represent the actions to determinate the value
    public VariableAsignation(string name, Token value)
    {
        Name = name;
        Value = value;
    }
}

/// <summary>
/// The representation of any defined primitive value 
/// </summary>
public class PrimitiveAtom : Action
{
    public Token PrimitiveValue { get; }
    public PrimitiveAtom(Token primitivevalue) => PrimitiveValue = primitivevalue;
}

public class WhileAction : Action
{
    public int Repetition { get; set; } = 0;
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
    public Card Reference { get; }

    public List<Card> Targets { get; }

    public List<Action> Actions { get; }

    public ForeachAction(Card reference, List<Card> targets, List<Action> actions)
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

public class MethodExecution : Action
{
    public Token FirstReference { get; set; }
    public Action LaterReference { get; set; }

    public MethodExecution(List<Token> tokens)
    {
        if (tokens.Count == 2)
        {
            FirstReference = tokens[1];
            LaterReference = new LaterReference(tokens[0]);
        }

        FirstReference = tokens[tokens.Count - 1];
        tokens.RemoveAt(tokens.Count - 1);
        LaterReference = new MethodExecution(tokens);

    }
}

public class LaterReference : Action
{
    public Token Reference { get; set; }
    public LaterReference(Token token) => Reference = token;
}