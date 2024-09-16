
using System.Collections.ObjectModel;
using TokenClass;

/// <summary>
/// Represents the archetip of an effect
/// </summary>
public class Effect
{
    public string Name { get; set; } = string.Empty;
    public Dictionary<string, VariableType> Params { get; set; } = new();
    public ActionInstruction? Action { get; set; }
    public Dictionary<string, object> Variables { get; set; } = new();
    public List<Card> Source { get; set; } = new();

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
    public OnActivation OnActivation { get; set; } = new();
    public Player Owner { get; set; }

    public Card()
    {
        if (Context.Player1Turn)
            Owner = Context.Player1;
        else
            Owner = Context.Player2;
    }

    public void DeclarerOwner(Player player) => Owner = player;

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
    public Dictionary<Effect, Dictionary<string, object>> ParamasOfEffect { get; set; } = new();
    public List<Selector> Selectors { get; set; } = new();
    public Dictionary<Effect, List<PostAction>> PostActions { get; set; } = new();

}

public class PostAction
{
    public Effect Effect { get; set; } = new();
    public Dictionary<string, object> Params { get; set; } = new();
    public Selector Selector { get; set; } = new();
    public PostAction() { }
    public PostAction(OnActivation onActivation) => Selector = onActivation.Selectors.Last();

}

public class Selector
{
    public List<Card> Source { get; set; } = new();
    public bool Single { get; set; } = false;
    public Action? Predicate { get; set; }
    public List<Card> FinalSource { get; set; } = new();

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
