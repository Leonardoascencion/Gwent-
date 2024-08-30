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

    public static List<Token> Tokens = new(); // the list of tokens(the only thing that matters of the )
    public static bool LexerError { get; set; } = false; // will say if it is an error of writting(will continue the code but the next fase need this propety be equal to false)

    public Lexer(string source)
    {
        Source = source + " " + "EOF";
        EspecialCases();
    }

    /// <summary>
    /// The manager who execute the tokeny 
    /// </summary>
    public void EspecialCases()
    {
        Regex match = new(@"[ \s () {} + - * / % ^ ; . , : < > = | & ! $ ' "" \n ]"); // no me detecta los []
        MatchCollection TheMatch = match.Matches(Source);

        for (int i = 0; i < TheMatch.Count; i++)
        {
            if (TheMatch[i].Index < CurrentPosition)
                continue;

            if (TheMatch[i].Value.Contains('"'))
            {
                for (int j = i + 1; j < TheMatch.Count; j++)
                {
                    if (TheMatch[j].Value.Contains('"'))
                    {
                        EspecialTokeny(TheMatch[i].Index + 1, j);
                        break;
                    }

                    if (j == TheMatch.Count || TheMatch[j].Value == "\n")
                        LexerError = true;
                    i++;
                }
                CurrentPosition = TheMatch[i].Index;
                continue;
            }


            Tokeny(TheMatch[i].Index);

            CurrentPosition = TheMatch[i].Index;
            Tokeny(TheMatch[i].Index);

            CurrentPosition++;
        }
    }


    /// <summary>
    /// The Tokenyzer with all the cases except the words case
    /// Start in the CurrentPosition in the source and from that position to the end - 1 saves that word and later compare with the possible cases
    /// </summary>
    public void Tokeny(int end)
    {
        CurrentWord = Source[CurrentPosition].ToString();
        for (int j = CurrentPosition + 1; j < end - 1; j++)
        {
            CurrentWord += Source[j];
        }

        switch (CurrentWord)
        {
            case "Effect":
                Tokens.Add(new Token(TokenType.Effect, CurrentWord));
                break;

            case "Card":
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

            case "In":
                Tokens.Add(new Token(TokenType.In, CurrentWord));
                break;

            case "Hand":
                Tokens.Add(new Token(TokenType.Hand, CurrentWord));
                break;

            case "Deck":
                Tokens.Add(new Token(TokenType.Deck, CurrentWord));
                break;

            case "Board":
                Tokens.Add(new Token(TokenType.Board, CurrentWord));
                break;

            case "Context":
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
                Tokens.Add(new Token(TokenType.True, CurrentWord));
                break;

            case "false":
                Tokens.Add(new Token(TokenType.False, CurrentWord));
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
                switch (Source[CurrentPosition++])
                {
                    case '&':
                        CurrentWord += Source[CurrentPosition++];
                        Tokens.Add(new Token(TokenType.And, CurrentWord));
                        end++;
                        CurrentPosition = end;
                        break;

                    default:
                        LexerError = true;
                        break;
                }
                break;

            case "|":
                switch (Source[CurrentPosition++])
                {
                    case '|':
                        CurrentWord += Source[CurrentPosition++];
                        Tokens.Add(new Token(TokenType.Or, CurrentWord));
                        end++;
                        CurrentPosition = end;
                        break;

                    default:
                        break;
                }
                break;

            case "^":
                Tokens.Add(new Token(TokenType.Pow, CurrentWord));
                break;

            case "+":
                switch (Source[CurrentPosition + 1])
                {
                    case '+':
                        CurrentWord += Source[CurrentPosition++];
                        Tokens.Add(new Token(TokenType.Increment, CurrentWord));
                        end++;
                        CurrentPosition = end;
                        break;

                    case '=':
                        CurrentWord += Source[CurrentPosition++];
                        Tokens.Add(new Token(TokenType.PlusEqual, CurrentWord));
                        end++;
                        CurrentPosition = end;
                        break;
                    default:
                        Tokens.Add(new Token(TokenType.Plus, CurrentWord));
                        break;
                }
                break;

            case "-":
                switch (Source[CurrentPosition + 1])
                {

                    case '=':
                        CurrentWord += Source[CurrentPosition++];
                        Tokens.Add(new Token(TokenType.MinusEqual, CurrentWord));
                        end++;
                        CurrentPosition = end;
                        break;

                    case '-':
                        CurrentWord += Source[CurrentPosition++];
                        Tokens.Add(new Token(TokenType.Decrement, CurrentWord));
                        end++;
                        CurrentPosition = end;
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
                switch (Source[CurrentPosition++])
                {
                    case '=':
                        CurrentWord += Source[CurrentPosition++];
                        Tokens.Add(new Token(TokenType.Equal, CurrentWord));
                        end++;
                        CurrentPosition = end;
                        break;

                    case '>':
                        CurrentWord += Source[CurrentPosition++];
                        Tokens.Add(new Token(TokenType.Arrow, CurrentWord));
                        end++;
                        CurrentPosition = end;
                        break;

                    default:
                        Tokens.Add(new Token(TokenType.Assign, CurrentWord));
                        break;
                }
                break;


            case "<":
                switch (Source[CurrentPosition++])
                {
                    case '=':
                        CurrentWord += Source[CurrentPosition++];
                        Tokens.Add(new Token(TokenType.LessEq, CurrentWord));
                        end++;
                        CurrentPosition = end;
                        break;

                    default:
                        Tokens.Add(new Token(TokenType.Less, CurrentWord));
                        break;
                }
                break;

            case ">":
                switch (Source[CurrentPosition++])
                {
                    case '=':
                        CurrentWord += Source[CurrentPosition++];
                        Tokens.Add(new Token(TokenType.MoreEq, CurrentWord));
                        end++;
                        CurrentPosition = end;
                        break;
                    default:
                        Tokens.Add(new Token(TokenType.More, CurrentWord));
                        break;
                }
                break;

            case "@":
                if (Source[CurrentPosition + 1] == '@')
                {
                    CurrentWord += Source[CurrentPosition++];
                    Tokens.Add(new Token(TokenType.SpaceConcatenation, CurrentWord));
                }
                else
                    Tokens.Add(new Token(TokenType.Concatenation, CurrentWord));
                break;

            case ":":
                Tokens.Add(new Token(TokenType.Colon, CurrentWord));
                break;

            case ",":
                Tokens.Add(new Token(TokenType.Comma, CurrentWord));
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

            case "Int":
                Tokens.Add(new Token(TokenType.Number, CurrentWord));
                break;

            case "bool":
                Tokens.Add(new Token(TokenType.Boolean, CurrentWord));
                break;

            case "Id":
                Tokens.Add(new Token(TokenType.Id, CurrentWord));
                break;

            case "string":
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

            case "EOF":
                Tokens.Add(new Token(TokenType.EOF, CurrentWord));
                break;

            case "targets":
                Tokens.Add(new Token(TokenType.Variable, CurrentWord));
                break;

            default:
                if (!double.TryParse(CurrentWord[0].ToString(), out _))
                    Tokens.Add(new Token(TokenType.Variable, CurrentWord));
                else
                    LexerError = true;
                break;
        }

    }


    ///Especial Tokenyzer to the cases of words values
    public void EspecialTokeny(int start, int end)
    {
        string Value = string.Empty;
        while (start < end)
        {
            Value += Source[start];
            start++;
        }
        Tokens.Add(new Token(TokenType.WordValue, Value));
    }
}
