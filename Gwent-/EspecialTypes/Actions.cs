

//Base class for all the posible acctions in the game
using System.Net.NetworkInformation;
using TokenClass;

public abstract class Action
{
}

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
/// nose si haga falta pa este proyecto
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
/// Necesary for if else statement
/// no seguro de si sirve pa este proyecto ya q no se definieron if else en el pdf
/// </summary>
public class TernaryAction : Action
{

    public Action Condition { get; }
    public Action Branch { get; }
    public Action ElseBranch { get; }

    public TernaryAction(Action condition, Action branch, Action elsebranch)
    {
        Condition = condition;
        Branch = branch;
        ElseBranch = elsebranch;
    }

}

/// <summary>
/// The asignation tree of a variable 
/// </summary>
public class Asignation : Action
{
    public Token Id { get; }  //the parameter unique of each variable
    public Action Value { get; } //represent the actions to determinate the value
    public Asignation(Token id, Action value)
    {
        Id = id;
        Value = value;
    }
}

/// <summary>
/// The representation of anytipe of variable for the body that have to first declare and later execute
/// </summary>
public class Atom : Action
{
    public Token Id { get; }
    public Atom(Token id) => Id = id;
}

/// <summary>
/// mi idea es crear la plantilla para un AST q contiene primero a lo q el metodo va ejecutar una vez sea llamado
/// este solo contendra la informacion y el metodo una vez q se ejecute llamara a esta instancia
/// </summary>
public class MethodDeclaration : Action
{
    public string Name { get; }
    public List<Atom> RequestVariable { get; }
    public Action Body { get; }
    public MethodDeclaration(string name, List<Atom> requestedvariable, Action body)
    {
        Name = name;
        RequestVariable = requestedvariable;
        Body = body;
    }
}

/// <summary>
/// It is the call of the previos declared method
/// </summary>
public class MethodCall : Action
{
    public string Name { get; }

    public List<Asignation> Params { get; }

    public MethodCall(string name, List<Asignation> parametros)
    {
        Name = name;
        Params = parametros;
    }
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

    public Token Context { get; }

    public List<Action> Actions { get; }

    public ForeachAction(Token reference, Token context, List<Action> actions)
    {
        Reference = reference;
        Context = context;
        Actions = actions;
    }
}

public class ActionInstruction : Action
{
    public Token Targets { get; }
    public Token Context { get; }
    public List<Action> Body { get; }
    public ActionInstruction(Token targets, Token context, List<Action> body)
    {
        Targets = targets;
        Context = context;
        Body = body;
    }
}


public class ExampleMomentary : Action
{
    public ExampleMomentary()
    {

    }
}
