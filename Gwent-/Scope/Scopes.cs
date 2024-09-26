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


public static class PosibleInstruction
{
    public static List<TokenType> ValidInstruction { get; set; } = new()
    {
        TokenType.Board,
        TokenType.DeckOfPlayer,
        TokenType.Deck,
        TokenType.Graveyard,
        TokenType.GraveyardOfPlayer,
        TokenType.HandOfPlayer,
        TokenType.Hand,
        TokenType.FieldOfPlayer,
        TokenType.Field,
        TokenType.Find,
        TokenType.Push,
        TokenType.SendBottom,
        TokenType.Pop,
        TokenType.Remove,
        TokenType.Shuffle,
        TokenType.Type,
        TokenType.Name,
        TokenType.Faction,
        TokenType.Power,
        TokenType.Range,
        TokenType.Owner,
        TokenType.VariableName,
        TokenType.Context,
        TokenType.NumberValue,
        TokenType.WordValue,
        TokenType.BoleanValue,
        TokenType.TriggerPlayer,
    };
    public static List<TokenType> EndingInstruction { get; set; } = new()
    {
         TokenType.TriggerPlayer,
         TokenType.Type,
         TokenType.Name,
         TokenType.Faction,
         TokenType.Power,
         TokenType.Range,
         TokenType.Owner,
    };
    public static List<TokenType> EndOperation { get; set; } = new()
    {
        TokenType.RBracket,
        TokenType.RParen,
        TokenType.Comma,
        TokenType.LineChange,
    };
    public static List<TokenType> EndOfLeftTerm { get; set; } = new()
    {
        TokenType.Assign,
        TokenType.PlusEqual,
        TokenType.MinusEqual,
        TokenType.Increment,
        TokenType.Decrement,

    };
    public static List<TokenType> MethodCallParams { get; set; } = new()
    {
         TokenType.DeckOfPlayer,
         TokenType.GraveyardOfPlayer,
         TokenType.HandOfPlayer,
         TokenType.FieldOfPlayer,
         TokenType.Find,
         TokenType.Push,
         TokenType.SendBottom,
         TokenType.Remove,
         TokenType.Add,
    };
    public static List<TokenType> BynariOperations { get; set; } = new()
    {
        TokenType.Plus,
        TokenType.Minus,
        TokenType.Multiply,
        TokenType.Divide,
        TokenType.Pow,
        TokenType.More,
        TokenType.MoreEq,
        TokenType.Less,
        TokenType.LessEq,
        TokenType.Equal,
        TokenType.Or,
        TokenType.And,
        TokenType.Concatenation,
        TokenType.SpaceConcatenation
    };

    public static List<TokenType> Methods { get; set; } = new()
    {
     TokenType.HandOfPlayer,
     TokenType.DeckOfPlayer,
     TokenType.FieldOfPlayer,
     TokenType.GraveyardOfPlayer,
     TokenType.Find,
     TokenType.Push,
     TokenType.SendBottom,
     TokenType.Pop,
     TokenType.Remove,
     TokenType.Shuffle,
    };

    public static List<TokenType> EmptyMethods { get; set; } = new()
    {
     TokenType.Shuffle,
     TokenType.Pop,
    };

    public static List<TokenType> CardsMethods { get; set; } = new()
    {
     TokenType.SendBottom,
     TokenType.Push,
     TokenType.Remove,
    };
    public static List<TokenType> PlayersMethods { get; set; } = new()
    {
     TokenType.HandOfPlayer,
     TokenType.DeckOfPlayer,
     TokenType.FieldOfPlayer,
     TokenType.GraveyardOfPlayer,
     };

    public static List<TokenType> IsaList { get; set; } = new()
    {
        TokenType.Board,
        TokenType.DeckOfPlayer,
        TokenType.Deck,
        TokenType.Graveyard,
        TokenType.GraveyardOfPlayer,
        TokenType.HandOfPlayer,
        TokenType.Hand,
        TokenType.FieldOfPlayer,
        TokenType.Field,
        TokenType.Find,
        TokenType.VariableName,

    };
}


