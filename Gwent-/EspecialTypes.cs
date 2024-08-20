
/// <summary>
/// Represents the archetip of an effect
/// </summary>
public class Effect
{
    public string Name { get; }
    public List<object>? Params { get; }
    public Effect(string name) => Name = name;

}

/// <summary>
/// Represents the archetip of a card 
/// </summary>
public class Card
{
    public string Name { get; }

    public Card(string name) => Name = name;
}