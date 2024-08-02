using System.Formats.Asn1;
using System.Text.RegularExpressions;
using TokenClass;
public class Lexer
{
    public readonly string Source;
    public static List<Token> Tokens = new();
    string CurrentWord { get; set; } = string.Empty;
    bool Error { get; set; } = false;
    public int StateOfLexer; //where is the current baner of the lexer
    public int XPostion;
    public int YPostion;

    public Lexer(string source)
    {
        Source = source + " ";
        EspecialCases();
    }


    public void EspecialCases()
    {
        int start = 0;
        Regex match = new(@"[ \s () {} + - * / % ^ ; , : < > = | & ! $ ' "" \n]"); // no me detecta los []
        MatchCollection TheMatch = match.Matches(Source);

        for (int i = 0; i < TheMatch.Count; i++)
        {
            Tokeny(start, TheMatch[i].Index);

            start = TheMatch[i].Index;
            Tokeny(start, TheMatch[i].Index);

            start++;

        }
    }


    public void Tokeny(int start, int end)
    {
        CurrentWord = Source[start].ToString();
        for (int i = start + 1; i < end; i++)
        {
            CurrentWord += Source[i];
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

            case "Attack":
                Tokens.Add(new Token(TokenType.Attack, CurrentWord));
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

            case "NumberType":
                Tokens.Add(new Token(TokenType.Effect, CurrentWord));
                break;

            case "StringType":
                Tokens.Add(new Token(TokenType.StringValue, CurrentWord));
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
                switch (Source[start++])
                {
                    case '&':
                        Tokens.Add(new Token(TokenType.And, CurrentWord));
                        start = end++;
                        end++;
                        break;

                    default:
                        Error = true;
                        break;
                }
                break;

            case "|":
                switch (Source[start++])
                {
                    case '|':
                        Tokens.Add(new Token(TokenType.Or, CurrentWord));
                        start = end++; //tiene q sumar 2 en vez de 1
                        end++;
                        break;

                    default:
                        break;
                }
                break;

            case "^":
                Tokens.Add(new Token(TokenType.Pow, CurrentWord));
                break;

            case "+":
                switch (Source[start + 1])
                {
                    case '+':
                        Tokens.Add(new Token(TokenType.Increment, CurrentWord));
                        start = end++;
                        end++;
                        break;
                    case '=':
                        Tokens.Add(new Token(TokenType.PlusEqual, CurrentWord));
                        start = end++;
                        end++;
                        break;
                    default:
                        Tokens.Add(new Token(TokenType.Plus, CurrentWord));
                        break;
                }
                break;

            case "-":
                switch (Source[start + 1])
                {

                    case '=':
                        Tokens.Add(new Token(TokenType.MinusEqual, CurrentWord));
                        start = end++;
                        end++;
                        break;

                    case '-':
                        Tokens.Add(new Token(TokenType.Decrement, CurrentWord));
                        start = end++;
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
                switch (Source[start++])
                {
                    case '=':
                        start = end++;
                        end++;
                        Tokens.Add(new Token(TokenType.Equal, CurrentWord));
                        break;

                    case '>':
                        Tokens.Add(new Token(TokenType.Arrow, CurrentWord));
                        start = end++;
                        end++;
                        break;

                    default:
                        Tokens.Add(new Token(TokenType.Assign, CurrentWord));
                        break;
                }
                break;


            case "<":
                switch (Source[start++])
                {
                    case '=':
                        Tokens.Add(new Token(TokenType.LessEq, CurrentWord));
                        start = end++;
                        end++;
                        break;

                    default:
                        Tokens.Add(new Token(TokenType.Less, CurrentWord));
                        break;
                }
                break;

            case ">":
                switch (Source[start++])
                {
                    case '=':
                        Tokens.Add(new Token(TokenType.MoreEq, CurrentWord));
                        start = end++;
                        end++;
                        break;
                    default:
                        Tokens.Add(new Token(TokenType.More, CurrentWord));
                        break;
                }
                break;

            case "$":
                if (Source[start + 1] == '$')
                    Tokens.Add(new Token(TokenType.SpaceConcatenation, CurrentWord));
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

            case "=>":
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

            case "Id":
                Tokens.Add(new Token(TokenType.Id, CurrentWord));
                break;

            case "bool":
                Tokens.Add(new Token(TokenType.Boolean, CurrentWord));
                break;

            case "\"":
                Tokens.Add(new Token(TokenType.Words, CurrentWord));
                break;

            case "'": // chek if it work (not yet)(yes, work)
                Tokens.Add(new Token(TokenType.Words, CurrentWord));
                break;

            case string example when double.TryParse(example, out _): // case of a number
                for (int j = 0; j < Source.Length; j++)
                {

                }
                break;

            case " ":
            case "\n":
            default:
                break;
        }

    }
}
