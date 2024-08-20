

//Base class for all the posible acctions in the game
using TokenClass;

public abstract class Action
{
}

/// <summary>
/// AST for operation with two element(+ - / * ||)
/// </summary>
public class BynariAction : Action
{
    public Action Left { get; }
    public Token Operator { get; }
    public Action Right { get; }

    public BynariAction(Action left, Token Operator, Action right)
    {
        this.Operator = Operator;
        Right = right;
        Left = left;
    }
}

/// <summary>
/// case of actions that only needs a one parametrs(sen(x) , x! ,etc)
/// nose si haga falta pa este proyecto
/// </summary>
public class UnaryAction : Action
{
    public Token Operator { get; }

    public Action Right { get; }

    public UnaryAction(Token op, Action right)
    {
        Operator = op;
        Right = right;
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
public class Variable : Action
{
    public Token Id { get; }
    public Variable(Token id) => Id = id;
}

/// <summary>
/// mi idea es crear la plantilla para un AST q contiene primero a lo q el metodo va ejecutar una vez sea llamado
/// este solo contendra la informacion y el metodo una vez q se ejecute llamara a esta instancia
/// </summary>
public class MethodDeclaration : Action
{
    public string Name { get; }
    public List<Variable> RequestVariable { get; }
    public Action Body { get; }
    public MethodDeclaration(string name, List<Variable> requestedvariable, Action body)
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

    public List<Action> Params { get; }

    public MethodCall(string name, List<Action> parametros)
    {
        Name = name;
        Params = parametros;
    }


}