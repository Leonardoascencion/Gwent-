/// <summary>
/// Represents all the effects defined at the moment by the interpreter
/// </summary>
public static class EffectScope
{
    public static Dictionary<string, Effect> Effects { get; set; } = new();

    public static void AddEffect(string name, Effect newOne) => Effects.Add(name, newOne);
}

/// <summary>
/// Represents all the cards defined at the moment by the interpreter
/// </summary>
public static class CardScope
{
    public static Dictionary<string, Card> Cards { get; set; } = new();

    public static void AddCard(string name, Card newOne) => Cards.Add(name, newOne);
}


