public enum TokenType
{

    LineChange, // JumpLine
    Whitespace, // JumpHollow

    //Keywords

    Effect, // Effect
    Card, // Card
    Name, // Name 
    Params, // Params
    Amount, // Amount        
    Action, // Action 
    Type, // Type 
    Faction, // Faction 
    Attack, // Attack 
    Range, // Range 
    OnActivation, // OnActivation 
    Selector, // Selector 
    PostAction, // PostAction 
    Source, // Source 
    Single, // Single 
    Predicate, // Predicate 
    In, // in 
    Hand, // hand 
    Deck, // deck 
    Board, // board 
    Context, // context 
    TriggerPlayer, // TriggerPlayer 
    Find, // Find 
    Push, // Push 
    SendBottom, // SendBottom 
    Pop, // Pop 
    Remove, // Remove 
    Shuffle, // Shuffle 
    Owner, //Owner 
    NumberValue, // NumberValue
    StringValue, // StringValue

    //Boolean
    True, // true 
    False, // false 
    For, // for
    While, // while
    If, // if
    ElIf, // elif
    Else, // else
    Not, // ! 
    And, // &&
    Or, // ||

    //Operator
    Pow, //^
    PlusEqual, //+= 
    MinusEqual, //-= 
    Increment, //++
    Decrement, //--
    Plus, //+
    Minus, //-
    Multiply, //*
    Divide, ///
    Equal, //==
    Less, //<
    LessEq, // <=
    More, //>
    MoreEq, //>=



    //Symbol
    SpaceConcatenation, //$$
    Concatenation, //$
    Assign, //=
    Colon, //: 
    Comma, //, 
    Semicolon, //; 
    Arrow,    // => 
    LParen,   // (
    RParen,   // )
    LBracket, // [
    RBracket, // ]
    LCurly,   //{
    RCurly,   //}

    //Identifier
    Number,  // Int
    Words,  // String
    Id,      // Id number
    Boolean, // bool
    Variable, // Any Combination of Letter and Number(Number First dont)

}