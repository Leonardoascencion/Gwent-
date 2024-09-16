using System.Formats.Asn1;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using TokenClass;

/// <summary>
/// The class that creates the list of tokens to the phase of the parser who make the AST(syntax tree)
/// </summary>
/// </return>
/// One time create return the list of tokens of the source code 
/// </return>
public class Lexer
{
    public readonly string Source; // source of the code
    private string CurrentWord { get; set; } = string.Empty; // will manage the way that the token will be add to the list
    private int CurrentPosition { get; set; } = 0; // will manage how will travel the source
    public static List<Token> Tokens { get; set; } = new(); // the list of tokens(the only thing that matters )

    public Lexer(string source)
    {
        Source = source + " " + "EOF";
        Tokeny();
    }

    /// <summary>
    /// The manager who execute the tokeny 
    /// </summary>
    public void Tokeny()
    {
        Regex match = new(@"(?:[-]|[\s () {} +* /%^;.,:<>|=|&!$'\""]|\n)");
        MatchCollection TheMatch = match.Matches(Source);



        for (int i = 0; i < TheMatch.Count; i++)
        {

            if (TheMatch[i].Index < CurrentPosition)
                continue;

            if (TheMatch[i].Value == '"'.ToString())
            {
                for (int j = i + 1; j <= TheMatch.Count; j++)
                {

                    if (j == TheMatch.Count || TheMatch[j].Value == "\n")
                        throw new Error("Not valid declaration");

                    if (TheMatch[j].Value == '"'.ToString())
                    {
                        CurrentPosition = TheMatch[i].Index;
                        EspecialTokeny(TheMatch[j].Index);
                        i = j;
                        break;
                    }

                }
                CurrentPosition = TheMatch[i].Index;
                continue;
            }



            if (CurrentPosition != TheMatch[i].Index)
                Tokeny(TheMatch[i].Index);
            Tokeny(TheMatch[i].Index);
            CurrentPosition++;
        }

        Tokens.Add(new Token(TokenType.EOF, "EOF"));
        System.Console.WriteLine("Termino el lexxer");

    }


    /// <summary>
    /// The Tokenyzer with all the cases except the words case
    /// Start in the CurrentPosition in the source and from that position to the end - 1 saves that word and later compare with the possible cases
    /// </summary>
    public void Tokeny(int end)
    {
        CurrentWord = Source[CurrentPosition].ToString();
        for (int j = CurrentPosition + 1; j <= end - 1; j++)
        {
            CurrentWord += Source[j];
        }

        //System.Console.WriteLine(CurrentWord);
        //System.Console.WriteLine(CurrentPosition);
        //System.Console.WriteLine(end);

        if (CurrentWord != string.Empty)
            switch (CurrentWord)
            {
                case "effect":
                    Tokens.Add(new Token(TokenType.Effect, CurrentWord));
                    break;

                case "card":
                    Tokens.Add(new Token(TokenType.Card, CurrentWord));
                    break;

                case "Name":
                    Tokens.Add(new Token(TokenType.Name, CurrentWord));
                    break;

                case "Params":
                    Tokens.Add(new Token(TokenType.Params, CurrentWord));
                    break;

                case "Action":
                    Tokens.Add(new Token(TokenType.Action, CurrentWord));
                    break;

                case "Type":
                    Tokens.Add(new Token(TokenType.Type, CurrentWord));
                    break;

                case "Faction":
                    Tokens.Add(new Token(TokenType.Faction, CurrentWord));
                    break;

                case "Power":
                    Tokens.Add(new Token(TokenType.Power, CurrentWord));
                    break;

                case "Range":
                    Tokens.Add(new Token(TokenType.Range, CurrentWord));
                    break;

                case "OnActivation":
                    Tokens.Add(new Token(TokenType.OnActivation, CurrentWord));
                    break;

                case "Selector":
                    Tokens.Add(new Token(TokenType.Selector, CurrentWord));
                    break;

                case "PostAction":
                    Tokens.Add(new Token(TokenType.PostAction, CurrentWord));
                    break;

                case "Source":
                    Tokens.Add(new Token(TokenType.Source, CurrentWord));
                    break;

                case "Single":
                    Tokens.Add(new Token(TokenType.Single, CurrentWord));
                    break;

                case "Predicate":
                    Tokens.Add(new Token(TokenType.Predicate, CurrentWord));
                    break;

                case "in":
                    Tokens.Add(new Token(TokenType.In, CurrentWord));
                    break;

                case "Hand":
                    Tokens.Add(new Token(TokenType.Hand, CurrentWord));
                    break;

                case "Deck":
                    Tokens.Add(new Token(TokenType.Deck, CurrentWord));
                    break;

                case "Field":
                    Tokens.Add(new Token(TokenType.Field, CurrentWord));
                    break;

                case "DeckOfPlayer":
                    Tokens.Add(new Token(TokenType.DeckOfPlayer, CurrentWord));
                    break;

                case "HandOfPlayer":
                    Tokens.Add(new Token(TokenType.HandOfPlayer, CurrentWord));
                    break;

                case "FieldOfPlayer":
                    Tokens.Add(new Token(TokenType.FieldOfPlayer, CurrentWord));
                    break;

                case "GraveyardOfPlayer":
                    Tokens.Add(new Token(TokenType.GraveyardOfPlayer, CurrentWord));
                    break;

                case "Graveyard":
                    Tokens.Add(new Token(TokenType.Graveyard, CurrentWord));
                    break;

                case "Board":
                    Tokens.Add(new Token(TokenType.Board, CurrentWord));
                    break;

                case "targets":
                    Tokens.Add(new Token(TokenType.Targets, CurrentWord));
                    break;

                case "context":
                    Tokens.Add(new Token(TokenType.Context, CurrentWord));
                    break;

                case "TriggerPlayer":
                    Tokens.Add(new Token(TokenType.TriggerPlayer, CurrentWord));
                    break;

                case "Find":
                    Tokens.Add(new Token(TokenType.Find, CurrentWord));
                    break;

                case "Push":
                    Tokens.Add(new Token(TokenType.Push, CurrentWord));
                    break;

                case "Sendbottom":
                    Tokens.Add(new Token(TokenType.SendBottom, CurrentWord));
                    break;

                case "Pop":
                    Tokens.Add(new Token(TokenType.Pop, CurrentWord));
                    break;

                case "Remove":
                    Tokens.Add(new Token(TokenType.Remove, CurrentWord));
                    break;

                case "Shuffle":
                    Tokens.Add(new Token(TokenType.Shuffle, CurrentWord));
                    break;

                case "Owner":
                    Tokens.Add(new Token(TokenType.Owner, CurrentWord));
                    break;

                case "true":
                case "false":
                    Tokens.Add(new Token(TokenType.BoleanValue, CurrentWord));
                    break;


                case "for":
                    Tokens.Add(new Token(TokenType.For, CurrentWord));
                    break;

                case "while":
                    Tokens.Add(new Token(TokenType.While, CurrentWord));
                    break;

                case "if":
                    Tokens.Add(new Token(TokenType.If, CurrentWord));
                    break;

                case "else":
                    Tokens.Add(new Token(TokenType.Else, CurrentWord));
                    break;

                case "!":
                    Tokens.Add(new Token(TokenType.Not, CurrentWord));
                    break;

                case "&":
                    switch (Source[CurrentPosition + 1])
                    {
                        case '&':
                            CurrentWord += Source[CurrentPosition + 1];
                            Tokens.Add(new Token(TokenType.And, CurrentWord));
                            end++;
                            break;

                        default:
                            Tokens.Add(new Token(TokenType.And, CurrentWord));
                            end++;
                            break;
                    }
                    break;

                case "|":
                    switch (Source[CurrentPosition + 1])
                    {
                        case '|':
                            CurrentWord += Source[CurrentPosition + 1];
                            Tokens.Add(new Token(TokenType.Or, CurrentWord));
                            end++;
                            break;

                        default:
                            throw new Error("Missing another OR operator");
                    }
                    break;

                case "^":
                    Tokens.Add(new Token(TokenType.Pow, CurrentWord));
                    break;

                case "+":
                    switch (Source[CurrentPosition + 1])
                    {
                        case '+':
                            CurrentWord += Source[CurrentPosition + 1];
                            Tokens.Add(new Token(TokenType.Increment, CurrentWord));
                            end++;
                            break;

                        case '=':
                            CurrentWord += Source[CurrentPosition + 1];
                            Tokens.Add(new Token(TokenType.PlusEqual, CurrentWord));
                            end++;
                            break;

                        default:
                            Tokens.Add(new Token(TokenType.Plus, CurrentWord));
                            break;
                    }
                    break;

                case "--":
                    Tokens.Add(new Token(TokenType.Decrement, CurrentWord));
                    break;
                case "-":
                    switch (Source[CurrentPosition + 1])
                    {
                        case '-':
                            CurrentWord += Source[CurrentPosition + 1];
                            Tokens.Add(new Token(TokenType.Decrement, CurrentWord));
                            end++;
                            break;

                        case '=':
                            CurrentWord += Source[CurrentPosition + 1];
                            Tokens.Add(new Token(TokenType.MinusEqual, CurrentWord));
                            end++;
                            break;

                        default:
                            Tokens.Add(new Token(TokenType.Minus, CurrentWord));
                            break;
                    }
                    break;

                case "*":
                    Tokens.Add(new Token(TokenType.Multiply, CurrentWord));
                    break;

                case "/":
                    Tokens.Add(new Token(TokenType.Divide, CurrentWord));
                    break;

                case "=":
                    switch (Source[CurrentPosition + 1])
                    {
                        case '=':
                            CurrentWord += Source[CurrentPosition + 1];
                            Tokens.Add(new Token(TokenType.Equal, CurrentWord));
                            end++;
                            break;

                        case '>':
                            CurrentWord += Source[CurrentPosition + 1];
                            Tokens.Add(new Token(TokenType.Arrow, CurrentWord));
                            end++;
                            break;

                        default:
                            Tokens.Add(new Token(TokenType.Assign, CurrentWord));
                            break;
                    }
                    break;


                case "<":
                    switch (Source[CurrentPosition + 1])
                    {
                        case '=':
                            CurrentWord += Source[CurrentPosition + 1];
                            Tokens.Add(new Token(TokenType.LessEq, CurrentWord));
                            end++;
                            break;

                        default:
                            Tokens.Add(new Token(TokenType.Less, CurrentWord));
                            break;
                    }
                    break;

                case ">":
                    switch (Source[CurrentPosition + 1])
                    {
                        case '=':
                            CurrentWord += Source[CurrentPosition + 1];
                            Tokens.Add(new Token(TokenType.MoreEq, CurrentWord));
                            end++;
                            break;
                        default:
                            Tokens.Add(new Token(TokenType.More, CurrentWord));
                            break;
                    }
                    break;

                case "@":
                    if (Source[CurrentPosition + 1] == '@')
                    {
                        CurrentWord += Source[CurrentPosition + 1];
                        Tokens.Add(new Token(TokenType.SpaceConcatenation, CurrentWord));
                    }
                    else
                        Tokens.Add(new Token(TokenType.Concatenation, CurrentWord));
                    break;

                case ".":
                    Tokens.Add(new Token(TokenType.Point, CurrentWord));
                    break;

                case ",":
                    Tokens.Add(new Token(TokenType.Comma, CurrentWord));
                    break;

                case ":":
                    Tokens.Add(new Token(TokenType.Colon, CurrentWord));
                    break;

                case ";":
                    Tokens.Add(new Token(TokenType.Semicolon, CurrentWord));
                    break;

                case "(":
                    Tokens.Add(new Token(TokenType.LParen, CurrentWord));
                    break;

                case ")":
                    Tokens.Add(new Token(TokenType.RParen, CurrentWord));
                    break;

                case "[":
                    Tokens.Add(new Token(TokenType.LBracket, CurrentWord));
                    break;

                case "]":
                    Tokens.Add(new Token(TokenType.RBracket, CurrentWord));
                    break;

                case "{":
                    Tokens.Add(new Token(TokenType.LCurly, CurrentWord));
                    break;

                case "}":
                    Tokens.Add(new Token(TokenType.RCurly, CurrentWord));
                    break;

                case "Number":
                    Tokens.Add(new Token(TokenType.Number, CurrentWord));
                    break;

                case "Bool":
                    Tokens.Add(new Token(TokenType.Bool, CurrentWord));
                    break;

                case "Id":
                    Tokens.Add(new Token(TokenType.Id, CurrentWord));
                    break;

                case "String":
                    Tokens.Add(new Token(TokenType.String, CurrentWord));
                    break;

                case string NumberType when double.TryParse(NumberType, out _): // case of a number
                    Tokens.Add(new Token(TokenType.NumberValue, CurrentWord));
                    break;

                case " ":
                    break;

                case "\n":
                    Tokens.Add(new Token(TokenType.LineChange, CurrentWord));
                    break;

                default:
                    if (CurrentWord == string.Empty || (int)CurrentWord[0] == 13 || (int)CurrentWord[0] == 34) break;

                    if (!double.TryParse(CurrentWord[0].ToString(), out _))
                        Tokens.Add(new Token(TokenType.VariableName, CurrentWord));
                    else
                        throw new Error("Not accepted variable declaration");
                    break;
            }

        CurrentPosition = end;
    }


    ///Especial Tokenyzer to the cases of words values
    public void EspecialTokeny(int end)
    {
        string Value = string.Empty;
        while (CurrentPosition < end)
        {
            Value += Source[CurrentPosition];
            CurrentPosition++;
        }
        Value = Value.Remove(0, 1);
        Tokens.Add(new Token(TokenType.WordValue, Value));
    }
}
