public enum TokenType
{

    LineChange, // JumpLine

    //Keywords
    Effect, // Effect
    Card, // Card
    Name, // Name 
    Params, // Params
    Action, // Action 
    Type, // Type 
    Faction, // Faction 
    Power, // Attack 
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
    Field, // field
    Graveyard, // grave
    HandOfPlayer, // hand of player
    DeckOfPlayer, // deck of player
    FieldOfPlayer, // field of player
    GraveyardOfPlayer, // grave of player
    Targets, // targets
    Context, // context
    CardValue, // card
    ListofCardValue, // List of card
    PlayerValue, // player
    NumberValue, // NumberValue (when find a number)
    WordValue, // WordValue (when find a word between "")
    BoleanValue, // Bool Value (when find true or false)
    NullValue, // Null

    //Especial methods 
    TriggerPlayer, // TriggerPlayer 
    Find, // Find 
    Push, // Push 
    SendBottom, // SendBottom 
    Pop, // Pop 
    Remove, // Remove 
    Shuffle, // Shuffle 
    Owner, //Owner 
    Add, //Add

    //Actions
    For, // for
    While, // while
    If, // if
    Else, // else


    //Boolean
    Not, // ! 
    And, // &&
    Or, // ||

    //Operator

    //Matemathic
    Pow, //^
    Plus, //+
    Minus, //-
    Multiply, //*
    Divide, ///
    Increment, //++
    Decrement, //--

    //Logical
    Equal, //==
    Less, //<
    LessEq, // <=
    More, //>
    MoreEq, //>=

    //Symbol
    SpaceConcatenation, //@@ include the white spaces
    Concatenation, //@
    Assign, //=
    MinusEqual,//-=
    PlusEqual,//+=
    Point, //.
    Comma, //, 
    Colon, //: 
    Semicolon, //; 
    Arrow,    // => 
    LParen,   // (
    RParen,   // )
    LBracket, // [
    RBracket, // ]
    LCurly,   // {
    RCurly,   // }

    //Identifier declarators
    Number,  // Int
    String,  // String beteween " "
    Bool, // bool
    Id,      // Id
    VariableName, // Any Combination of Letter and Number(Number First dont)

    EOF      //End of file(of the source)

}