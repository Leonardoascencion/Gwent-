
using TokenClass;

/// <summary>
/// Represents the archetip of an effect
/// </summary>
public class Effect
{
    public string? Name { get; set; }
    public List<Tuple<Token, VariableType>> Variables { get; set; } = new();
    public ActionInstruction? Action { get; set; }

}

/// <summary>
/// Represents the archetip of a card 
/// </summary>
public class Card
{
    public string? Name { get; set; }
    public string? Type { get; set; }
    public string? Faction { get; set; }
    public double Power { get; set; } = 0;
    public Range[] Range { get; set; } = new Range[3];
    public List<Effect>? OnActivation { get; set; }
    public List<Effect>? PostAction { get; set; }

}


public enum VariableType
{
    Number,
    Bool,
    String
}
public enum Range
{
    Melee,
    Ranged,
    Siege
}

public class CardType
{
    public static List<string> Types { get; } = new List<string>
    {

        "Oro",
        "Plata",
        "Lider",
        "Aumento",
        "Clima",
        "Señuelo"
    };

    public static void NewType(string newone)
    {
        Types.Add(newone);
    }
}
