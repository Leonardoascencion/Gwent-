
/// <summary>
/// Represents all the effects defined at the moment
/// </summary>
public static class EffectScope
{
    public static Dictionary<string, Effect> Effects { get; } = new();

    public static void AddEffect(string name, Effect newOne) => Effects.Add(name, newOne);
}

/// <summary>
/// Represents all the cards defined at the moment
/// </summary>

public class CardScope
{
    public static Dictionary<string, Card> Cards { get; } = new();

    public static void AddCard(string name, Card newOne) => Cards.Add(name, newOne);
}

/// <summary>
/// Represents all the variables that everyone can use in all the code
/// </summary>
public class GlobalVariable
{
    public static List<Asignation> Variables { get; } = new();
    public void AddVariable(Asignation NewOne) => Variables.Add(NewOne);

}

/// <summary>
/// Represents all the variables that everyone can use for the actions where is written and the actions inside that action
/// </summary>
public class VariableScope
{

    public List<Asignation> Variables { get; } = new();


    public VariableScope()//Case for the 1st method
    {
    }
    public VariableScope(VariableScope Padre)// Case for the body of the method
    {
        Variables = Padre.Variables;
    }
    public void AddVariable(Asignation Variable) => Variables.Add(Variable);

}