using TokenClass;

public static class Reviewer
{
    public static Dictionary<string, object> VariablesOfEffect { get; set; } = new();

    public static TokenType CompositionCheck(this Action action, Dictionary<string, object> variables)
    {
        if (action is PrimitiveAtom primitiveAtom)
        {
            if (primitiveAtom.PrimitiveValue.Type == TokenType.VariableName)
            {
                if (variables.ContainsKey(primitiveAtom.PrimitiveValue.Lexeme))
                    return TypeVariable(variables[primitiveAtom.PrimitiveValue.Lexeme]);
                else throw new Error("Not declared variable");
            }
            else
                return primitiveAtom.PrimitiveValue.Type;
        }

        if (action is UnaryAction unaryAction)
        {
            if (unaryAction.ID.CompositionCheck(variables) is TokenType.NumberValue)
                return TokenType.NullValue;

            throw new Error("Expected a number value after a unary action");
        }

        if (action is ObjectAsignation objectAsignation)
        {

            if (objectAsignation.AssignType.Type == TokenType.Assign)
            {

            }
            return TokenType.NullValue;

        }

        if (action is InstructionExecution instruction)
        {
            //   instruction.FirstReference.CompositionCheck(variables);
            //  instruction.LaterReference.CompositionCheck(variables);

        }

        if (action is MethodCall methodCall)
        {
            if (!PosibleInstruction.Methods.Contains(methodCall.Instruction.Type))
                throw new Error("Not valid method");
            if (PosibleInstruction.EmptyMethods.Contains(methodCall.Instruction.Type))
                if (methodCall.Params != null)
                    throw new Error("This method dont spect any params");


        }


        if (action is WhileAction whileAction)
        {
            Dictionary<string, object> localvariables = new(variables);
            if (whileAction.Condition.CompositionCheck(localvariables) is not TokenType.BoleanValue)
                throw new Error("Wrong Implementation for WHILE");

            foreach (Action item in whileAction.Body)
                item.CompositionCheck(localvariables);

            return TokenType.NullValue;
            ;
        }

        if (action is ForeachAction foreachAction)
        {
            Dictionary<string, object> localvariables = new(variables);
            if (localvariables.ContainsKey(foreachAction.Reference))
                throw new Error("Wrong Implementation for FOR");

            if (localvariables.ContainsKey(foreachAction.Targets))
            {
                if (localvariables[foreachAction.Targets].GetType() != new List<Card>().GetType())
                    throw new Error("Wrong Implementation for FOR");
            }
            else throw new Error("Wrong Implementation for FOR");

            foreach (Action item in foreachAction.Body)
                item.CompositionCheck(localvariables);

            return TokenType.NullValue;

            throw new Error("Wrong Implementation for FOR");
        }


        throw new Error("Not implemented yet");
    }



    public static TokenType TypeVariable(object value)
    {
        switch (value)
        {
            case double:
                return TokenType.NumberValue;

            case string:
                return TokenType.WordValue;

            case bool:
                return TokenType.BoleanValue;

            case Player:
                return TokenType.PlayerValue;

            case Card:
                return TokenType.CardValue;

            case List<Card>:
                return TokenType.ListofCardValue;

            default:
                break;
        }
        throw new Error("Not valid type");
    }

    public static TokenType TypeForTokens(TokenType tokenType)
    {
        switch (tokenType)
        {
            case TokenType.Board:
            case TokenType.Field:
            case TokenType.FieldOfPlayer:
            case TokenType.Hand:
            case TokenType.HandOfPlayer:
            case TokenType.Deck:
            case TokenType.DeckOfPlayer:
            case TokenType.Graveyard:
            case TokenType.GraveyardOfPlayer:
            case TokenType.Find:
                return TokenType.ListofCardValue;

            case TokenType.Pop:
                return TokenType.Card;

            default:
                break;
        }
        throw new Error("Not valid type");

    }



    public static Object InstructionRecorre(this InstructionExecution instruction, object currentvalue)
    {

        //  if (instruction.FirstReference.CompositionCheck(VariablesOfEffect) == TokenType.Context)
        {

        }

        throw new Error("Not implemented");
    }




}