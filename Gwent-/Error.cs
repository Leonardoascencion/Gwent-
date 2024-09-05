using TokenClass;

/* public class Error
{
    bool Lexer;
    bool Parser;
    bool ParserSemantyc;
    int CP = 0;

    public Error(List<Token> tokens)
    {
        while (CP < tokens.Count)
        {
            if (tokens[CP].Type == TokenType.Effect)
                CaseEffect(tokens);
            if (tokens[CP].Type == TokenType.Card)
                CaseEffect(tokens);
            if (tokens[CP].Type == TokenType.EOF)
                break;
            CP++;
        }
    }

    void ErrorLexer() => Parser = true;
    void ErrorParser() => Parser = true;
    void ErrorParserSemantyc() => ParserSemantyc = true;

    public void CaseEffect(List<Token> tokens)
    {
        if (CP + 2 < tokens.Count())
        {
            CP++;
            if (tokens[CP].Type == TokenType.LineChange)
                CP++;
            if (tokens[CP].Type != TokenType.LCurly)
                ErrorParser();
            while (CP < tokens.Count())
            {
                if (tokens[CP].Type == TokenType.LineChange)
                {
                    CP++;
                    continue;
                }

                if (tokens[CP].Type == TokenType.Name)
                    CaseName(tokens);
                else
                    ErrorParser();
                if (tokens[CP].Type == TokenType.Params)
                    CaseParamas(tokens);
                if (tokens[CP].Type == TokenType.Action)
                    CaseParamas(tokens);

                if (tokens[CP].Type == TokenType.RCurly)
                    break;
                CP++;
            }
            if (CP > tokens.Count())
                ErrorParser();
        }
        else
            ErrorParser();
    }

    public void CaseName(List<Token> tokens)
    {
        if (CP + 4 < tokens.Count())
        {
            CP++;
            if (tokens[CP].Type == TokenType.Colon)
                CP++;
            if (tokens[CP].Type == TokenType.String)
                CP++;
            if (tokens[CP].Type == TokenType.Comma)
                CP++;
        }
        else
            ErrorParser();
    }

    public void CaseParamas(List<Token> tokens)
    {
        if (CP + 7 < tokens.Count)
        {
            CP++;
            if (tokens[CP].Type == TokenType.Colon)
                CP++;
            if (tokens[CP].Type == TokenType.LCurly)
                CP++;
            if (tokens[CP].Type == TokenType.LineChange)
                CP++;
            while (CP < tokens.Count)
            {
                if (tokens[CP].Type == TokenType.RCurly)
                    break;
                if (tokens[CP].Type == TokenType.LineChange)
                    continue;
                if (tokens[CP].Type == TokenType.Variable)
                    CP++;
                if (tokens[CP].Type == TokenType.Colon)
                    CP++;
                else
                {
                    ErrorParser();
                    break;
                }
                if (tokens[CP].Type == TokenType.Number || tokens[CP].Type == TokenType.String || tokens[CP].Type == TokenType.Boolean)
                    continue;
                else
                    ErrorParser();
                CP++;
            }
            if (CP == tokens.Count)
                ErrorParser();
        }
        else
            ErrorParser();
    }

    public void CaseAction(List<Token> tokens)
    {
        CP++;
        if (CP + 9 < tokens.Count)
        {
            if (tokens[CP].Type == TokenType.Colon)
                CP++;
            else
                ErrorParser();
            if (tokens[CP].Type == TokenType.LParen)
                CP++;
            else
                ErrorParser();
            if (tokens[CP].Type == TokenType.Variable)
                CP++;
            else
                ErrorParser();
            if (tokens[CP].Type == TokenType.Comma)
                CP++;
            else
                ErrorParser();
            if (tokens[CP].Type == TokenType.Context)
                CP++;
            else
                ErrorParser();
            if (tokens[CP].Type == TokenType.RParen)
                CP++;
            else
                ErrorParser();
            if (tokens[CP].Type == TokenType.Arrow)
                CP++;
            else
                ErrorParser();
            if (tokens[CP].Type == TokenType.LCurly)
                CP++;
            else
                ErrorParser();
            CP++;
            CaseBody(tokens);
        }
        else
            ErrorParser();

    }

    public void CaseBody(List<Token> tokens)
    {
        while (CP < tokens.Count)
        {
            if (tokens[CP].Type == TokenType.For)
            {
                CP++;
                CaseBody(tokens);
            }
            if (tokens[CP].Type == TokenType.While)
            {
                if (tokens[CP].Type == TokenType.LParen)
                    CP++;
                else
                    ErrorParser();
                CP++;
                if (CP < tokens.Count && tokens[CP].Type == TokenType.RParen)
                    ErrorParser();
                else
                    while (CP < tokens.Count)
                    {
                        if (tokens[CP].Type == TokenType.RParen)
                            break;
                        if (tokens[CP].Type == TokenType.LParen)
                        {
                            ErrorParser();
                            break;
                        }
                        if (tokens[CP].Type == TokenType.LCurly)
                        {
                            ErrorParser();
                            break;
                        }
                        if (tokens[CP].Type == TokenType.RCurly)
                        {
                            ErrorParser();
                            break;
                        }
                        CP++;
                    }
                CaseBody(tokens);
                continue;
            }
            if (tokens[CP].Type == TokenType.LParen)
            {

            }
            CP++;
        }
    }
} */

public class Error : Exception
{
    public string Errormesage { get; }
    public Error(string errormesage) => Errormesage = errormesage;
}