//Base class for all the posible acctions in the game
using TokenClass;
public abstract class Action { }

/// <summary>
/// Representation of a AST for operation with two element(+ > ||)
/// </summary>
public class BinaryAction : Action
{
    public List<Action> Terms { get; set; } = new();
    public List<TokenType> Operators { get; set; } = new();

    public BinaryAction() { }
    public BinaryAction(Action action)
    {
        if (action is BinaryAction binaryAction)
        {
            Terms.AddRange(binaryAction.Terms);
            Operators.AddRange(binaryAction.Operators);
        }
        else
            Terms.Add(action);

    }

    public void Complete(Action action)
    {
        if (action is BinaryAction binaryAction)
        {
            Terms.AddRange(binaryAction.Terms);
            Operators.AddRange(binaryAction.Operators);
        }
        else
            Terms.Add(action);

    }



}


/// <summary>
/// case of actions that only needs a one parametrs(++ and --)
/// </summary>
public class UnaryAction : Action
{
    public Action ID { get; }
    public Token Operation { get; }

    public UnaryAction(Action id, Token op)
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
    public Token AssignType { get; set; }
    public Action Value { get; set; }

    public ObjectAsignation(Action id, Token token, Action value)
    {
        ID = id;
        AssignType = token;
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
    public string Reference { get; }

    public string Targets { get; }

    public List<Action> Body { get; }

    public ForeachAction(string reference, string targets, List<Action> actions)
    {
        Reference = reference;
        Targets = targets;
        Body = actions;
    }
}

public class ActionInstruction : Action
{
    public List<Card> Targets { get; } = new();
    public List<Action> Body { get; }
    public ActionInstruction(List<Action> body)
    {
        Body = body;
    }
}

public class InstructionExecution : Action
{
    public Action FirstReference { get; set; }
    public Action LaterReference { get; set; }

    public InstructionExecution(Action firstreference, Action laterReference)
    {
        FirstReference = firstreference;
        LaterReference = laterReference;
    }
}
public class MethodCall : Action
{
    public Token Instruction { get; set; }
    public Action? Params { get; set; }
    public string? Index { get; set; }

    public MethodCall(Token instruction) => Instruction = instruction;
    public void ParamsCreation(Action parmas) => Params = parmas;
    public void Indexation(string index) => Index = index;
}

public class Predicate : Action
{
    public Token Reference { get; set; }
    public Action Condition { get; set; }

    public Predicate(Token reference, Action condition)
    {
        Reference = reference;
        Condition = condition;
    }
}
