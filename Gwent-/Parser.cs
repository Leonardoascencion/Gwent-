using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using TokenClass;

class Parser
{
    public readonly List<Token> Tokens = new();
    public int CurrentPosition { get; set; } = 0;
    public static bool ParserError { get; set; } = false;
    public Parser(List<Token> tokens)
    {
        if (!Lexer.LexerError)
            Tokens = tokens;
        else
            ParserError = true;
    }

    /// <summary>
    /// the thing that will parse everything
    /// </summary>
    public void Parse()
    {
        while (!IsAtEnd())
        {
            if (Match(TokenType.Effect))
            {
                Effect Effect = new();
                Consume(TokenType.LCurly, "Expected Left Curly");
                Ignore();
                Consume(TokenType.Name, "Expected Name declaration");
                Consume(TokenType.Colon, "Expected asignator :");
                Consume(TokenType.WordValue, "Expected Value Name");
                Effect.Name = Previous().Lexeme;

                foreach (var effects in EffectScope.Effects)
                    if (Effect.Name == effects.Key)
                        throw new Error("Already declared effect");

                Consume(TokenType.Comma, "Expected Comma");
                Consume(TokenType.LineChange, "Not expected more implementation for Name");

                if (Peek().Type == TokenType.Params)
                {
                    Consume(TokenType.Colon, "Expected Colon");
                    Ignore();
                    Consume(TokenType.LCurly, "Expected Left Curly");
                    Ignore();
                    while (Peek().Type != TokenType.RCurly && Peek().Type != TokenType.EOF)
                    {
                        Consume(TokenType.Variable, "Expected a variable declaration name");
                        Token token = Previous();
                        Consume(TokenType.Colon, "Expected Colon");
                        if (Peek().Type != TokenType.Number || Peek().Type != TokenType.String || Peek().Type != TokenType.Boolean)
                            throw new Error("Expected a Variable Identificator");
                        VariableType variableType = VariableType(Peek());

                        Effect.Variables.Add(new(token, variableType));

                        Advance();
                        if (Peek().Type == TokenType.Comma)
                            Advance();
                        else break;
                    }
                    Ignore();
                    Consume(TokenType.RCurly, "Expected Right Curly");
                    Consume(TokenType.Comma, "Expected a comma");
                    Ignore();
                }

                Consume(TokenType.Action, "Expected a Action to execute");
                Consume(TokenType.Colon, "Expected : declaration");
                Consume(TokenType.LParen, "Expected left parentesis");
                while (Peek().Type != TokenType.RParen && Peek().Type != TokenType.EOF)
                {
                    Consume(TokenType.Variable, "Expected Targets call");
                    Consume(TokenType.Context, "Expected Context call");
                }
                Consume(TokenType.RParen, "Expected right parentesis");
                Consume(TokenType.Arrow, "Expected Arrow");
                Consume(TokenType.LCurly, "Expected Left Curly");
                Ignore();
                while (Peek().Type != TokenType.RCurly)
                {

                    //Effect.Action = new ActionCase(); Accion para cuerpo de metodo para mas adelante
                    //Consume(TokenType.Semicolon, "Expected Semicolon after a Action declaration");

                    Ignore();
                }
                Consume(TokenType.RCurly, "Expected Right Curly");
                Consume(TokenType.RCurly, "Expected Right Curly");
                Ignore();

                EffectScope.AddEffect(Effect.Name, Effect);

            }

            if (Match(TokenType.Card))
            {
                Card card = new();

                Consume(TokenType.Type, "Expected a declaration of a type");
                Consume(TokenType.Colon, "Expected a declarator :");
                Consume(TokenType.WordValue, "Expected a Type Name  ");
                card.Type = Previous().Lexeme;
                bool ErrorChek = false;
                foreach (var item in CardType.Types)
                    if (Previous().Lexeme != item)
                        ErrorChek = true;
                    else
                    {
                        ErrorChek = false;
                        break;
                    }

                if (ErrorChek)
                    throw new Error("Not defined Type");
                Consume(TokenType.Comma, "Expected a comma");
                Ignore();

                Consume(TokenType.Name, "Expected a Name");
                Consume(TokenType.Colon, "Expected a declarator :");
                Consume(TokenType.WordValue, "Expected a valid Name Value");
                card.Name = Previous().Lexeme;

                foreach (var item in CardScope.Cards)
                    if (card.Name == item.Key)
                        throw new Error("Name has been taken");


                Consume(TokenType.Comma, "Expected a comma");
                Ignore();

                Consume(TokenType.Faction, "Expected a Faction Name");
                Consume(TokenType.Colon, "Expected a declarator :");
                Consume(TokenType.WordValue, "Expected a Faction to belong");
                card.Faction = Previous().Lexeme;
                Consume(TokenType.Comma, "Expected a comma");
                Ignore();

                if (card.Type != "Clima" || card.Type != "Lider" || card.Type != "Aumento" || card.Type != "Target")
                {
                    Consume(TokenType.Power, "Expected a Power declaration");
                    Consume(TokenType.Colon, "Expected a declarator :");
                    Consume(TokenType.NumberValue, "Expected a Number Value");
                    double.TryParse(Previous().Lexeme, out double result);
                    card.Power = result;
                }
                else
                {
                    if (Peek().Type == TokenType.Power)
                    {
                        Consume(TokenType.Power, "Expected a Power declaration (IF declarated the power must doit raight)");
                        Consume(TokenType.Colon, "Expected a declarator : (IF declarated the power must doit raight)");
                        Consume(TokenType.NumberValue, "Expected a Number Value (IF declarated the power must doit raight)");
                    }
                }
                Ignore();

                if (card.Type != "Clima" || card.Type != "Lider" || card.Type != "Aumento" || card.Type != "Target")
                {
                    Consume(TokenType.Range, "Expected a Range comand for the battle (must have at least one)");
                    Consume(TokenType.Colon, "Expected a declarator :");
                    Consume(TokenType.RBracket, "Expected the start of the collection of Ranges");
                    Advance();
                    while (Peek().Type != TokenType.RBracket)
                    {
                        int i = 0;
                        if (i > 3)
                            throw new Error("Not accepted more than 3 Range declaration");

                        Consume(TokenType.WordValue, "Expected the Range asignation");
                        card.Range[i] = FindRangeType(Peek());
                        if (Peek().Type != TokenType.Comma)
                            break;
                        Consume(TokenType.Comma, "Expected a comma for next Range Asignation");
                        i++;
                    }
                    Consume(TokenType.RBracket, "Expected the end of the collection of Ranges");
                    Consume(TokenType.Comma, "Expected a comma");
                    Ignore();

                    Consume(TokenType.OnActivation, "Expected OnActivation Command");
                    Consume(TokenType.Colon, "Expected declarator :");
                    Consume(TokenType.RBracket, "Expected the start of the collection of Comands of OnActivation");

                }

                CardScope.AddCard(card.Name, card);
            }
        }

    }





    /*
        /// <summary>
        /// Parses the AST for the while, for, action and their instructions 
        /// </summary>
        /// <returns>The AST of any instruction</returns>
        public Action Action()
        {
            EndCheker = CurrentPosition;
            if (MatchCurrent(TokenType.While))
            {
                EndCheker++;
                if (MatchEnd(TokenType.LParen))
                {
                    return WhileCase();
                }
                else ParserError = true;

            }

            return new ExampleMomentary();
        }


        public Action ActionCase()
        {
            CurrentPosition++;
            if (MatchCurrent(TokenType.Colon))
                CurrentPosition++;
            else
                ParserError = true;
            if (!MatchCurrent(TokenType.LCurly))
                ParserError = true;
            return ActionContext();
        }

        public Action ActionContext()
        {
            Token targets;
            Token context;
            CurrentPosition++;
            targets = Tokens[CurrentPosition];
            if (MatchCurrent(TokenType.Comma))
                CurrentPosition++;
            context = Tokens[CurrentPosition];
            if (MatchCurrent(TokenType.RParen))
                CurrentPosition++;
            if (MatchCurrent(TokenType.Arrow))
                CurrentPosition++;
            if (!MatchCurrent(TokenType.LCurly))
                ParserError = true;
            return new ActionInstruction(targets, context, BodyAction());
        }

        /// <summary>
        /// Case for the while with only one action
        /// </summary>
        /// <returns>The AST of a while but whit the body with only one element</returns>
        public Action SimpleWhileCase()
        {
            Action Condition = ConditionalOperation();
            List<Action> actions = new() { Action() };
            return new WhileAction(Condition, actions);
        }

        /// <summary>
        /// Case for the while
        /// </summary>
        /// <returns>The AST of a while</returns>
        public Action WhileCase()
        {
            EndCheker++;
            if (!MatchEnd(TokenType.RParen))
            {
                while (!MatchEnd(TokenType.LCurly))
                {
                    EndCheker++;
                    if (MatchEnd(TokenType.EOF))
                        ParserError = true;
                }
            }
            else ParserError = true;

            return WhileAction();
        }

        /// <summary>
        /// Manage of create the AST for the while needs to have currentposition value in the while position on the tokens list
        /// </summary>
        /// <param name="startcondition">Mark of where the condition start</param>
        /// <param name="actionsstart">Mark of where all instruction of the body start</param>
        /// <returns>The AST of de while</returns>
        public Action WhileAction()
        {
            CurrentPosition++;
            Action Condition = ConditionalOperation();
            if (MatchCurrent(TokenType.RParen))
                CurrentPosition++;
            else
                ParserError = true;
            if (!MatchCurrent(TokenType.LCurly))
                ParserError = true;
            return new WhileAction(Condition, BodyAction());
        }

        /// <summary>
        /// Method in charge of create the AST of the for function
        /// </summary>
        /// <param name="reference">The object that wich all object from context have to get the type</param>
        /// <param name="context">The collection of object where the body will do their instructions</param>
        /// <returns>The AST of the for function</returns>
        public Action ForAction()
        {
            Token Object = ForHeadObject();
            CurrentPosition++;
            if (MatchCurrent(TokenType.In))
                CurrentPosition++;
            Token Context = ForHeadContext();
            CurrentPosition++;
            if (MatchCurrent(TokenType.LCurly))
                CurrentPosition++;
            else
                return new ForeachAction(Object, Context, SimpleForCase());
            return new ForeachAction(Object, Context, BodyAction());
        }

        /// <summary>
        /// Archive the variable name wich will be the name that all the objects for the context will be name
        /// </summary>
        /// <returns>The token that have the name value and acts like a variable</returns>
        public Token ForHeadObject()
        {
            if (!MatchCurrent(TokenType.Variable))
                ParserError = true;
            return Tokens[CurrentPosition];
        }

        /// <summary>
        /// Archive the context of the for 
        /// </summary>
        /// <returns>The tokens that represent the context where the for will work</returns>
        public Token ForHeadContext()
        {
            if (MatchCurrent(TokenType.Context))
                ParserError = true;
            return Tokens[CurrentPosition];
        }

        /// <summary>
        /// Case for the For action with only one instruction, at the end the current position stay in the semicolon
        /// </summary>
        /// <returns>A list of AST with the action with only one element(instruction)</returns>
        public List<Action> SimpleForCase()
        {
            List<Action> action = new() { Action() };
            return action;
        }

        /// <summary>
        /// Method in charge of the creation of the AST for each action between parenthesis
        /// </summary>
        /// <returns>The List of AST that represent each action</returns>
        public List<Action> BodyAction()
        {
            List<Action> actions = new();
            while (!MatchCurrent(TokenType.RCurly))
            {
                if (IsAtEnd())
                {
                    ParserError = true;
                    break;
                }
                actions.Add(Action());
            }
            return actions;
        }

        /// <summary>
        /// Method in charge to return the AST for the variables with they values 
        /// </summary>
        /// <param name="TokenPosition">Position of the Token in the list of tokens</param>
        /// <param name="ValuePosition">The position of the last character value assigned to the token</param>
        /// <returns>The AST of the variable with their respective value</returns>
        public Action Asignation(int TokenPosition, int ValuePosition)
        {
            return new Asignation(Tokens[TokenPosition], Operation(TokenPosition + 2, ValuePosition));
        }

        /// <summary>
        /// Recorre the line of code in search of a ) or a logical operator, in any case creates a AST for the operation
        /// </summary>
        /// <returns>The AST for the condition requeriment for the while</returns>
        public Action ConditionalOperation()
        {
            Action FirstElement = Action();
            if (!MatchCurrent(TokenType.RParen))
                if (LogicalOperator())
                    return new BinaryAction(FirstElement, Tokens[CurrentPosition], ConditionalOperation());
                else
                    ParserError = true;
            else
                ParserError = true;
            if (IsAtEnd())
                ParserError = true;
            return FirstElement;

        }

        /// <summary>
        /// Says if in the position it is a logical operator
        /// </summary>
        /// <returns></returns>
        public bool LogicalOperator()
        {
            if (Tokens[CurrentPosition].Type == TokenType.Equal)
                return true;
            if (Tokens[CurrentPosition].Type == TokenType.LessEq)
                return true;
            if (Tokens[CurrentPosition].Type == TokenType.MoreEq)
                return true;
            if (Tokens[CurrentPosition].Type == TokenType.Less)
                return true;
            if (Tokens[CurrentPosition].Type == TokenType.More)
                return true;
            if (Tokens[CurrentPosition].Type == TokenType.Or)
                return true;
            if (Tokens[CurrentPosition].Type == TokenType.And)
                return true;
            return false;
        }

        /// <summary>
        /// Method in charge to process the operation that includes (+ - * / ^)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns>Return the AST of any operation arithmetic</returns>
        public Action Operation(int start, int end)
        {
            int LastLessOperation = start;
            int LastMediumOperation = start;
            int LastMoreOperation = start;
            while (LastLessOperation < end)
            {
                if (LessImportantOperator(LastLessOperation))
                {
                    return new BinaryAction(Operation(start, LastLessOperation - 1), Tokens[LastLessOperation], Operation(LastLessOperation + 1, end));
                }
                LastLessOperation++;
            }
            while (LastMediumOperation < end)
            {
                if (MediumImportantOperator(LastMediumOperation))
                {
                    return new BinaryAction(Operation(start, LastMediumOperation - 1), Tokens[LastMediumOperation], Operation(LastMediumOperation + 1, end));
                }
            }
            while (LastMoreOperation < end)
            {
                if (MoreImportantOperator(LastMediumOperation))
                {
                    return new BinaryAction(Operation(start, LastMoreOperation - 1), Tokens[LastMoreOperation], Operation(LastMoreOperation + 1, end));
                }
            }
            return new Variable(Tokens[LastLessOperation]);
        }


        /// <summary>
        /// Method for the less important operation of a combinade (+ and -)
        /// </summary>
        /// <param name="n"></param>
        /// <returns>True if the position have a less important operator</returns>
        public bool LessImportantOperator(int n)
        {
            if (Tokens[n].Lexeme == "+" || Tokens[n].Lexeme == "-")
                return true;
            return false;
        }


        /// <summary>
        /// Method for the medium important operator of a combinade (* and /)
        /// </summary>
        /// <param name="n"></param>
        /// <returns>True if the position have a medium important operator</returns>
        public bool MediumImportantOperator(int n)
        {
            if (Tokens[n].Lexeme == "*" || Tokens[n].Lexeme == "/")
                return true;
            return false;
        }


        /// <summary>
        /// Method for the more important operator of a combinade (^)
        /// </summary>
        /// <param name="n"></param>
        /// <returns>True if in the position have a more important operator</returns>
        public bool MoreImportantOperator(int n)
        {
            if (Tokens[n].Lexeme == "^")
                return true;
            return false;
        }
     */


    /* 
        /// <summary>
        /// Same as MatchCurrent but with EndChek
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool MatchEnd(TokenType type)
        {
            return Tokens[EndCheker].Type == type;
        }
      */
    /// <summary>
    /// Match the token in the currentposition of the list of token with the type of the parameter
    /// </summary>
    /// <param name="type"></param>
    /// <returns> True if the parmeter have the same type of the token in the currentposition of the list of tokens </returns>
    public bool Match(TokenType type)
    {
        return Peek().Type == type;
    }

    /// <summary>
    /// Cheks if it is at the end of the tokens list
    /// </summary>
    /// <returns>True if it is at the end</returns>
    public bool IsAtEnd()
    {
        return Peek().Type == TokenType.EOF;
    }

    /// <summary>
    /// Search the token in the tokens list at the actual position
    /// </summary>
    /// <returns>The current token</returns>
    public Token Peek()
    {
        return Tokens[CurrentPosition];
    }

    /// <summary>
    /// Returns the previous token
    /// </summary>
    /// <returns>The previous token</returns>
    public Token Previous()
    {
        return Tokens[CurrentPosition - 1];
    }

    /// <summary>
    /// Advances to the next token in the tokens list
    /// </summary>
    /// <returns>The token in the previous position</returns>
    public Token Advance()
    {
        if (!IsAtEnd())
            CurrentPosition++;
        return Previous();
    }

    /// <summary>
    /// Consume the current token if have the type specifaid otherwise send a error
    /// </summary>
    /// <param name="type"></param>
    /// <param name="errormesage"></param>
    /// <returns>The consume token</returns>
    /// <exception cref="Error"></exception>
    public Token Consume(TokenType type, string errormesage)
    {
        if (Advance().Type == type)
            return Advance();
        throw new Error(errormesage + " after " + Previous().Lexeme + "  at  " + CurrentPosition.ToString());
    }

    /// <summary>
    /// Advances if it is a jump line
    /// </summary>
    public void Ignore()
    {
        while (Peek().Type == TokenType.LineChange)
            CurrentPosition++;
    }

    public VariableType VariableType(Token token)
    {
        VariableType type = new();
        switch (token.Lexeme)
        {
            case "Boolean":
                type = global::VariableType.Bool;
                break;

            case "String":
                type = global::VariableType.String;
                break;

            case "Number":
                type = global::VariableType.Number;
                break;
        }
        return type;
    }

    public Range FindRangeType(Token token)
    {
        switch (token.Lexeme)
        {
            case "Melee":
                return Range.Melee;
            case "Ranged":
                return Range.Ranged;
            case "Siege":
                return Range.Siege;
        }
        throw new Error("Not valid Range");
    }

}