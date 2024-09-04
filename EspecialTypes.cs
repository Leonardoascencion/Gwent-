
using System.Collections.ObjectModel;
using TokenClass;

/// <summary>
/// Represents the archetip of an effect
/// </summary>
public class Effect
{
    public string Name { get; set; } = string.Empty;
    public List<Tuple<Token, VariableType>> Variables { get; set; } = new();
    public ActionInstruction? Action { get; set; }

}

/// <summary>
/// Represents the archetip of a card 
/// </summary>
public class Card
{
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Faction { get; set; } = string.Empty;
    public double Power { get; set; } = 0;
    public Range[] Range { get; set; } = new Range[3];
    public OnActivation? OnActivation { get; set; }
    public PostAction PostAction { get; set; } = new();

}


public class OnActivation
{
    public List<Effect> Effects { get; set; } = new();
    public Dictionary<Effect, List<Tuple<Token, object>>> ParamasOfEffect { get; set; } = new();
    public Selector Selector { get; set; } = new Selector();

    public OnActivation() { }
}
public class PostAction
{



}
public class Selector
{
    public List<Card> Context { get; set; } = new();
    public bool Single { get; set; } = false;
    public Card Predicate { get; set; } = new Card();

    public Selector()
    {

    }

    public Selector(bool single) => Single = single;

}
public enum VariableType
{
    Number,
    Boolean,
    String
}
public enum Range
{
    Melee,
    Ranged,
    Siege
}

public static class CardType
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
