
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
    public List<Range> Range { get; set; } = new();
    public OnActivation? OnActivation { get; set; }
    public PostAction PostAction { get; set; } = new();
    public Player Owner { get; set; } = new();


    #region Methods for Predicate

    public bool NameComparer(Card predicate)
    {
        if (predicate.Name == "")
            return true;
        return predicate.Name == Name;
    }
    public bool FactionComparer(Card predicate)
    {
        if (predicate.Faction == "")
            return true;
        return predicate.Faction == Faction;
    }

    public bool TypeComparer(Card predicate)
    {
        if (predicate.Type == "")
            return true;
        return predicate.Type == Type;
    }

    public bool PowerComparer(Card predicate)
    {
        if (predicate.Power == 0)
            return true;
        return predicate.Power == Power;
    }

    public bool RangeComparer(Card predicate)
    {

        if (predicate.Range.Count() == 0)
            return true;

        foreach (var predicaterange in predicate.Range)
        {
            foreach (var cardrange in Range)
            {
                if (predicaterange == cardrange)
                    return true;
            }
        }
        return false;
    }


    public bool OwnerComparer(Card predicate)
    {
        if (predicate.Owner == null)
            return true;
        return predicate.Owner == Owner;
    }


    #endregion
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
    public List<Card> ContextList { get; set; } = new();
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
