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
    Context, // context
    NumberValue, // NumberValue (when find a number)
    WordValue, // WordValue (when find a word between "")
    BoleanValue, // Bool Value (when find true or false)

    //Especial methods 
    TriggerPlayer, // TriggerPlayer 
    Find, // Find 
    Push, // Push 
    SendBottom, // SendBottom 
    Pop, // Pop 
    Remove, // Remove 
    Shuffle, // Shuffle 
    Owner, //Owner 

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
    PlusEqual, //+= //creo q esto no lo voy a usar
    MinusEqual, //-= // creo q esto no lo voy a usar
    Equal, //==
    Less, //<
    LessEq, // <=
    More, //>
    MoreEq, //>=



    //Symbol
    SpaceConcatenation, //@@ include the white spaces
    Concatenation, //@
    Assign, //=
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
    Variable, // Any Combination of Letter and Number(Number First dont)

    EOF      //End of file(of the source)

}