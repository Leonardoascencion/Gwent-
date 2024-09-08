using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using TokenClass;

class Parser
{
    public readonly List<Token> Tokens = new();
    public int CurrentPosition { get; set; } = 0;
    public Parser(List<Token> tokens) => Parse();

    /// <summary>
    /// the thing that will parse everything
    /// </summary>
    public void Parse()
    {
        while (!IsAtEnd())
        {
            if (Match(TokenType.Effect))
                EffectScope.AddEffect(ParsesEffect().Name, ParsesEffect());
            else
            if (Match(TokenType.Card))
                CardScope.AddCard(ParsesCard().Name, ParsesCard());
            else
                throw new Error("Not valid imput");
        }
    }

    #region Creation of Effect
    ///FALTA LA CONSTRUCCION DE LOS ARBOLES PARA DETERMINAR Q STRING SE QUIERE PARA EL NOMBRE Y FALTA LA CREACION DEL METODO Q ENTRA EN EL ACTION Y DEVUELVE EL ARBOL Q DEBE EJECUTAR
    ///FALTA LA CONSTRUCCION DE LOS ARBOLES PARA DETERMINAR Q STRING SE QUIERE PARA EL NOMBRE Y FALTA LA CREACION DEL METODO Q ENTRA EN EL ACTION Y DEVUELVE EL ARBOL Q DEBE EJECUTAR
    ///FALTA LA CONSTRUCCION DE LOS ARBOLES PARA DETERMINAR Q STRING SE QUIERE PARA EL NOMBRE Y FALTA LA CREACION DEL METODO Q ENTRA EN EL ACTION Y DEVUELVE EL ARBOL Q DEBE EJECUTAR


    /// <summary>
    /// For the struck of a effect definition
    /// </summary>
    /// <returns>The effect create of a valid source</returns>
    /// <exception cref="Error">IF has anithing that do not follow the documentation of how create a effect in this project</exception>
    public Effect ParsesEffect()
    {
        Effect effect = new();

        Advance();
        Consume(TokenType.LCurly, "Expected Left Curly ( { )");
        Ignore();

        #region NameDefinition

        Consume(TokenType.Name, "Expected the Name declaration");
        Consume(TokenType.Colon, "Expected asignator :");
        ///AKI VA EL METODO Q CALCULA EL VALOR DESPUES DEL IGUAL
        ///AKI VA EL METODO Q CALCULA EL VALOR DESPUES DEL IGUAL
        ///AKI VA EL METODO Q CALCULA EL VALOR DESPUES DEL IGUAL
        Consume(TokenType.WordValue, "Expected Value Name");
        effect.Name = Previous().Lexeme;

        if (EffectScope.Effects.ContainsKey(effect.Name))
            throw new Error("The current Name has been taken");

        #endregion

        Consume(TokenType.Comma, "Expected Comma");
        Consume(TokenType.LineChange, "Not expected more implementation for Name");
        Ignore();

        #region Params

        if (Peek().Type == TokenType.Params)
        {
            Advance();
            Consume(TokenType.Colon, "Expected asignator :");
            Ignore();

            Consume(TokenType.LCurly, "Expected Left Curly ( { )");
            Ignore();

            while (!Match(TokenType.RCurly))
            {
                Ignore();
                Consume(TokenType.VariableName, "Expected a variable name declaration");
                string name = Previous().Lexeme;

                Consume(TokenType.Colon, "Expected asignator :");

                if (Peek().Type != TokenType.Number || Peek().Type != TokenType.String || Peek().Type != TokenType.Bool)
                    throw new Error("Expected a Variable Identificator in the effect " + effect.Name);
                VariableType variableType = FindVariableType(Peek());

                if (effect.Variables.ContainsKey(name))
                    throw new Error("Already declare variable int the param of the effect " + effect.Name);
                else
                    effect.Variables.Add(name, variableType);

                Advance();

                if (Peek().Type == TokenType.Comma)
                    Advance();
                else break;
            }
            Ignore();
            Consume(TokenType.RCurly, "Expected Right Curly ( } )");
            Consume(TokenType.Comma, "Expected a comma");
            Ignore();
        }

        #endregion

        #region Action or the Effect of the effect


        Consume(TokenType.Action, "Expected a Action to execute");
        Consume(TokenType.Colon, "Expected : declaration");
        Consume(TokenType.LParen, "Expected left parentesis ( ( )");
        Consume(TokenType.Targets, "Expected targets call");
        Consume(TokenType.Comma, "Expected a comma");
        Consume(TokenType.Context, "Expected Context call");
        Consume(TokenType.RParen, "Expected right parentesis");
        Consume(TokenType.Arrow, "Expected Arrow");
        Consume(TokenType.LCurly, "Expected Left Curly");
        Ignore();

        while (Peek().Type != TokenType.RCurly)
        {
            //AKI VA EL METODO Q SE ENCARGA DE CONSTRUIR EL METODO Q EJECUTA EL CUERPO Y GUARDA EL AST CORRESPONDIENTE                            
            //AKI VA EL METODO Q SE ENCARGA DE CONSTRUIR EL METODO Q EJECUTA EL CUERPO Y GUARDA EL AST CORRESPONDIENTE                                
            //AKI VA EL METODO Q SE ENCARGA DE CONSTRUIR EL METODO Q EJECUTA EL CUERPO Y GUARDA EL AST CORRESPONDIENTE                                
            //AKI VA EL METODO Q SE ENCARGA DE CONSTRUIR EL METODO Q EJECUTA EL CUERPO Y GUARDA EL AST CORRESPONDIENTE                                    
            Ignore();
        }

        #endregion

        Advance();
        Ignore();

        Consume(TokenType.RCurly, "Expected Right Curly");
        Ignore();

        return effect;
    }

    #endregion


    #region Creation of Card

    /// <summary>
    /// For the struck of a card definition
    /// </summary>
    /// <returns>The card create of a valid source</returns>
    /// <exception cref="Error">IF has anithing that do not follow the documentation of how create a card in this project</exception>
    public Card ParsesCard()
    {
        Card card = new();
        Dictionary<string, bool> DefinedPropetys = new Dictionary<string, bool>()
        {
            {"Name",false},
            {"Type",false},
            {"Faction",false},
            {"Power",false},
            {"Range",false}
        };

        Advance();
        Consume(TokenType.LCurly, "Expected Left Curly ( { )");
        Ignore();

        #region Propetys Definition
        ///IMPLEMENTACION DE PROPIEDADES BASICAS DE LAS CARTAS CASI COMPLETAMENTE FUNCIONAL FALTA LOS ARBOLES 
        while (Peek().Type != TokenType.OnActivation)
        {
            Ignore();

            if (Peek().Type == TokenType.Type)
            {
                if (DefinedPropetys["Type"])
                    throw new Error("Type has been already declared");

                Advance();
                Consume(TokenType.Colon, "Expected a declarator :");
                Consume(TokenType.WordValue, "Expected a Type Name");
                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO                
                card.Type = Previous().Lexeme;

                if (!CardType.Types.Contains(card.Type))
                    throw new Error("Not defined Type");

                Consume(TokenType.Comma, "Expected a comma");
                Ignore();

                DefinedPropetys["Type"] = true;
                continue;
            }

            if (Peek().Type == TokenType.Name)
            {
                if (DefinedPropetys["Name"])
                    throw new Error("Name has been already declared");

                Advance();
                Consume(TokenType.Colon, "Expected a declarator :");
                Consume(TokenType.WordValue, "Expected a valid Name Value");
                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRIGN DEVUELTO POR EL AST FORMADO
                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRIGN DEVUELTO POR EL AST FORMADO
                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRIGN DEVUELTO POR EL AST FORMADO            
                card.Name = Previous().Lexeme;

                if (CardScope.Cards.ContainsKey(card.Name))
                    throw new Error("Name already has been taken");

                Consume(TokenType.Comma, "Expected a comma");
                Ignore();

                DefinedPropetys["Name"] = true;
                continue;
            }

            if (Peek().Type == TokenType.Faction)
            {
                if (DefinedPropetys["Faction"])
                    throw new Error("Faction has been already declared");

                Advance();
                Consume(TokenType.Colon, "Expected a declarator :");
                Consume(TokenType.WordValue, "Expected a Faction to belong");
                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRIGN DEVUELTO POR EL AST FORMADO
                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRIGN DEVUELTO POR EL AST FORMADO
                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRIGN DEVUELTO POR EL AST FORMADO   
                card.Faction = Previous().Lexeme;

                Consume(TokenType.Comma, "Expected a comma");
                Ignore();

                DefinedPropetys["Faction"] = true;
                continue;
            }

            if (Peek().Type == TokenType.Power)
            {
                if (DefinedPropetys["Power"])
                    throw new Error("Power has been already declared");

                Advance();
                Consume(TokenType.Colon, "Expected a declarator :");
                Consume(TokenType.NumberValue, "Expected a Number Value");
                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRIGN DEVUELTO POR EL AST FORMADO
                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRIGN DEVUELTO POR EL AST FORMADO
                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRIGN DEVUELTO POR EL AST FORMADO   
                double.TryParse(Previous().Lexeme, out double result);
                card.Power = result;

                Consume(TokenType.Comma, "Expected a comma");
                Ignore();

                DefinedPropetys["Power"] = true;
                continue;
            }

            if (Peek().Type == TokenType.Range)
            {

                if (DefinedPropetys["Range"])
                    throw new Error("Range has been already declared");

                Advance();
                Consume(TokenType.Colon, "Expected a declarator :");
                Ignore();
                Consume(TokenType.RBracket, "Expected the start of the collection of Ranges ( [ )");
                Ignore();

                int i = 0;
                do
                {
                    if (i > 3)
                        throw new Error("Not accepted more than 3 Range declaration");

                    Consume(TokenType.WordValue, "Expected the Range asignation");
                    ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
                    ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
                    ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO   
                    string range = Previous().Lexeme;
                    card.Range.Add(FindRangeType(range));
                    Ignore();
                    i++;

                    if (Peek().Type == TokenType.Comma)
                    {
                        Advance();
                        Ignore();
                    }
                } while (Peek().Type != TokenType.RBracket);

                Consume(TokenType.Comma, "Expected a comma");
                Ignore();

                DefinedPropetys["Range"] = true;
                continue;
            }
        }

        if (DefinedPropetys.ContainsValue(false))
            if (card.Type != "Oro" && card.Type != "Plata")
            {
                DefinedPropetys["Power"] = true;
                if (DefinedPropetys.ContainsValue(false))
                    throw new Error("Missing a propety for delcare");
            }
            else
                throw new Error("Missing a propety for delcare");

        #endregion





        #region OnActivation Definition

        card.OnActivation = new OnActivation();

        Consume(TokenType.OnActivation, "Expected OnActivation Command");
        Consume(TokenType.Colon, "Expected declarator :");
        Ignore();


        Consume(TokenType.LBracket, "Expected the start of the collection of Comands of OnActivation ( [ )");
        Ignore();

        while (Peek().Type != TokenType.RBracket)
        {
            Consume(TokenType.LCurly, "Expected start of the effect declaration ( { )");
            Ignore();

            Consume(TokenType.Effect, "Expected effect start");
            Consume(TokenType.Colon, "Expected a declartor :");

            if (Peek().Type == TokenType.WordValue)
            {
                string name = "";
                ///AKI TAMBIEN HAY Q CALCULAR EL VALOR DEL AST Q DEBE DEVOLVER UN STRING
                if (UnicEffect(Peek().Lexeme))
                    throw new Error("The effect do not exist");

                Effect effect = SearchEffect(name);
                card.OnActivation.Effects.Add(effect);
                Advance();
                Ignore();

                if (Peek().Type == TokenType.LCurly)
                {
                    Advance();
                    while (Peek().Type != TokenType.RCurly)
                    {
                        if (effect.Variables.Count != 0)
                        {
                            int i = effect.Variables.Count;
                            card.OnActivation.ParamasOfEffect = new() { { effect, new List<Tuple<Token, object>>() } };
                            while (i > 0)
                            {
                                Consume(TokenType.VariableName, "Expected the name of the param of the effect");
                                string nameofvariable = Previous().Lexeme;
                                ///AKI HAY Q CALCULAR TMB EL VALOR DEL AST PARA EL NOMBRE
                                Token variable = Previous();
                                Consume(TokenType.Colon, "Expected declarator :");

                                ///ESTO ESTA MAL COMPRUEBA SI ES BOOL O STRING O NUMBER CUANDO DEBERIA DECIR SI Q ES EL VALOR
                                if (CorrectParmas(nameofvariable, Peek(), effect))
                                    card.OnActivation.ParamasOfEffect[effect].Add(new(variable, Peek().Lexeme));

                                Advance();

                                if (i - 1 > 0)
                                    Consume(TokenType.Comma, "Expected more param implementation");

                                Ignore();

                                i--;
                            }
                        }


                        ///Accion a realizar con los parametrso no tengo idea ahora mismo
                        /// YA ESTA HECHO
                        ///  RRRRRRRRR  EEEEEEEE CCCCCCCCCC OOOOOOOOO RRRRRRRRR  DDDDDDDD AAAAAAAA RRRRRRRRR   
                        ///  RRR   RRR  EEE      CCCC   CC  OOO   OOO RRR   RRR  DD   DDD AA    AA RRR   RRR
                        ///  RRR  RRR   EEEEE    CC         OOO   OOO RRR  RRR   DD   DDD AAA  AAA RRR  RRR
                        ///  RRR RRR    EEEEE    CC         OOO   OOO RRR RRR    DD   DDD AAAAAAAA RRR RRR
                        ///  RRR   RRR  EEE      CCCC   CC  OOO   OOO RRR   RRR  DD   DDD AAA  AAA RRR   RRR
                        ///  RRR    RRR EEEEEEEE CCCCCCCCCC OOOOOOOOO RRR    RRR DDDDDDDD AAA  AAA RRR    RRR

                        Ignore();
                    }
                }
            }
            else
                if (Peek().Type == TokenType.LCurly)
            {
                {
                    Advance();
                    Ignore();

                    Consume(TokenType.Name, "Expected Name declaration");
                    Consume(TokenType.Colon, "Expected declarator :");
                    string name = "";
                    Consume(TokenType.WordValue, "Expected a Name");
                    ///AKI TMB HAY Q CONSTRUIR EL AST Y EVALUARLO Y Q DEVUELVA UN STRING

                    Effect effect = SearchEffect(name);
                    card.OnActivation.Effects.Add(effect);

                    if (UnicEffect(Previous().Lexeme))
                        throw new Error("The effect do not exist");

                    Consume(TokenType.Comma, "Expected a comma");
                    Ignore();

                    if (effect.Variables.Count != 0)
                    {
                        int i = effect.Variables.Count;
                        card.OnActivation.ParamasOfEffect = new() { { effect, new List<Tuple<Token, object>>() } };

                        while (i > 0)
                        {
                            Consume(TokenType.VariableName, "Expected the name of the param of the effect");
                            string nameofvariable = Previous().Lexeme;
                            ///AKI HAY Q CALCULAR TMB EL VALOR DEL AST PARA EL NOMBRE
                            Token variable = Previous();
                            Consume(TokenType.Colon, "Expected declarator :");

                            ///ESTO ESTA MAL COMPRUEBA SI ES BOOL O STRING O NUMBER CUANDO DEBERIA DECIR SI Q ES EL VALOR
                            if (CorrectParmas(nameofvariable, Peek(), effect))
                                card.OnActivation.ParamasOfEffect[effect].Add(new(variable, Peek().Lexeme));

                            Advance();
                            if (i - 1 > 0)
                                Consume(TokenType.Comma, "Expected a comma");
                            Ignore();

                            i--;
                        }
                    }
                }
                Consume(TokenType.RCurly, "Expected the close of the effect declaration ( } )");
            }
            else throw new Error("Not Valid declaration");

            ///No necesariamente tiene q haber una coma asi q ahora hago un iff con un peek()
            Consume(TokenType.Comma, "Expected a comma");
            Ignore();

            Consume(TokenType.Selector, "Expected a Selector comand");

            card.OnActivation.Selector = new Selector();

            Consume(TokenType.Colon, "Expected declarator :");
            Consume(TokenType.LCurly, "Expected the start of the definition ( { )");
            Ignore();

            Consume(TokenType.Source, "Expected the start of the definition for Source");
            Consume(TokenType.Colon, "Expected declarator :");
            //String nameofsourcecontext = "";
            //METODO PARA CALCULAR EL VALOR DEL ARRAY lo igualo al name q esta en la linea de arriba
            //  card.OnActivation.Selector.Context = SourceFinder(nameofsourcecontext);
            Consume(TokenType.Comma, "Expected a comma ,");
            Ignore();



            if (Peek().Type == TokenType.Single)
            {
                Advance();
                Consume(TokenType.Colon, "Expected declarator :");
                Consume(TokenType.VariableName, "Expected a bolean value or a boolean variable");
                ///AKI VA EL CALCULO DE PARA COMPROBAR SI ES UN BOOLENAO O NO Y DEVOLVERLO EN CASO DE Q SEA
                Consume(TokenType.Comma, "Expected a comma ,");
                Ignore();
            }

            Consume(TokenType.Predicate, "Expected the start of the definition for Predicate");
            Consume(TokenType.Colon, "Expected declarator :");
            Consume(TokenType.LParen, "Expected start of definition of the variable for predicate action ( ( )");
            Consume(TokenType.VariableName, "Expected a valid variable definition for the predicate");

            ///DUDA SOBRE COMO TRATAR ESTO BIEN TENGO LA IDEA DE COGER Y LAS PROPIEDADES Q SE CAMBIARON Q SEAN LAS Q ME IMPORTA TOCAR PERO NOSE
            card.OnActivation.Selector.Predicate.Name = Previous().Lexeme;

            Consume(TokenType.RParen, "Expected end of definition of the variable for predicate action");
            Consume(TokenType.Arrow, "Expected the indicator for the start of the predicate definition");

            if (Peek().Lexeme != card.OnActivation.Selector.Predicate.Name)
                throw new Error("Not the type defined");

            Consume(TokenType.Point, "Expected the cast of the propety");
            switch (Peek().Type)
            {

                case TokenType.Type:
                    ///Expected implementation for the modifaied propety for the predicate of selector
                    break;

                case TokenType.Name:
                    ///Expected implementation for the modifaied propety for the predicate of selector
                    break;

                case TokenType.Power:
                    ///Expected implementation for the modifaied propety for the predicate of selector
                    break;

                case TokenType.Faction:
                    ///Expected implementation for the modifaied propety for the predicate of selector
                    break;

                case TokenType.Range:
                    ///Expected implementation for the modifaied propety for the predicate of selector
                    break;

                ///ESTABA ARREGLANDO COMO PINCHA LO DE LAS PROPIEDADES EN UN LLAMADO DE EFECTO CREO Q YA IGUAL REVISAR
                ///HACER LA PINCHA PARA CADA PROPIEDAD SE QUEDA PA DESPUES NO VAYA A SER Q ESTO ME LO PUEDA AHORRAR DE ALGUNA MANERA

                default:
                    throw new Error("Not valid propety");
            }
            Ignore();

            Consume(TokenType.RCurly, "Expected end of definition for the Selector ( } )");
            while (Peek().Type != TokenType.RCurly)
            {
                if (Peek().Type != TokenType.Comma)
                    break;

                Advance();
                Consume(TokenType.PostAction, "Expected the start of definition for PostAction or a miss comma has ben written");
                ///EMPEZARIA AKI LA DEFINICION PARA POST ACTION Y TODO ESO PERO LO HARE DESPUES DE TERMINAR DE DEFINIR EL CICLO PARA LOS EFECTOS NORMALES

                Consume(TokenType.RCurly, "Expected end of definition for the Selector ( } )");
                Ignore();
            }

            Consume(TokenType.RCurly, "Expected end of definition for the Selector ( } )");

            ///PARA LAS CARTAS CON MAS EFECTOS LO Q HARE SERA VOLVER A EJECUTAR EL METODO Q CALCULA EL EFECTO Y YA EZEPEACE



        }
        #endregion

        Advance();
        Ignore();
        Consume(TokenType.RCurly, "Expected the end of the card declaration ( } )");

        return card;
    }
    #endregion




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

    #region Methods to recorre the tokens list
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
    /// Advances while it is stand in a jump line
    /// </summary>
    public void Ignore()
    {
        while (Peek().Type == TokenType.LineChange)
            CurrentPosition++;
    }
    #endregion


    /// <summary>
    /// Cheks the token for the type available to a variable
    /// </summary>
    /// <param name="token">The token to analize</param>
    /// <returns>Return the type accord to the type of the variable(example string return VariableType.String)</returns>
    /// <exception cref="Error">Throw an error if the type do not match with the defined in this lengauge</exception>
    public VariableType FindVariableType(Token token)
    {
        switch (token.Type)
        {
            case TokenType.Bool:
                return VariableType.Boolean;
            case TokenType.String:
                return VariableType.String;
            case TokenType.Number:
                return VariableType.Number;
        }
        throw new Error("Not defined type");
    }

    /// <summary>
    /// Cheks the token for the type of range available to a card
    /// </summary>
    /// <param name="token">The token to analize</param>
    /// <returns>Return the type accord to the type of the variable(example Melee return Range.Melee)</returns>
    /// <exception cref="Error">Throw an error if the type do not match with the defined in this lengauge</exception>
    public Range FindRangeType(string range)
    {
        switch (range)
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

    /// <summary>
    /// Cheks if the effect already exist
    /// </summary>
    /// <param name="name"></param>
    /// <returns>True if the effect do not exist in the context and false if the effect exist</returns>
    public bool UnicEffect(string name)
    {
        foreach (var item in EffectScope.Effects)
            if (item.Key == name)
                return false;
        return true;
    }



    /// <summary>
    /// Search the effect in the Effect Scope
    /// </summary>
    /// <param name="name">The name of the required effect</param>
    /// <returns>The effect founded if it not exist throw an error</returns>
    /// <exception cref="Error"></exception>
    public Effect SearchEffect(string name)
    {
        if (EffectScope.Effects.ContainsKey(name))
            return EffectScope.Effects.GetValueOrDefault(name, new Effect());

        throw new Error("Effect Error 404");
    }

    /// <summary>
    /// Checks if the effect calls have the same params has the definition
    /// </summary>
    /// <param name="namevariable">The name of the variable</param>
    /// <param name="type">The type of the variable</param>
    /// <param name="effect">The effect to check the match</param>
    /// <returns>True if it have the same paramas propetys the call and the definition</returns>
    public bool CorrectParmas(string namevariable, Token type, Effect effect)
    {
        if (effect.Variables.ContainsKey(namevariable))
            if (effect.Variables.Contains(new KeyValuePair<string, VariableType>(namevariable, FindVariableType(type))))
                return true;
            else throw new Error("Incorrect type of variable");
        return false;
    }

    /// <summary>
    /// Search the Card in the Scope of Cards
    /// </summary>
    /// <param name="name">The name of the required card</param>
    /// <returns>The Card founded if it not exist throw an error</returns>
    /// <exception cref="Error"></exception>
    public Card CardFinder(string name)
    {
        if (CardScope.Cards.ContainsKey(name))
            return CardScope.Cards.GetValueOrDefault(name, new Card());

        throw new Error("Not defined name for the source");
    }




    //EMPEZO LO CHIDO A CREAR ARBOLES
    //
    //IDEA PARA EL SCOPE
    //CREO UNA LISTA DE TUPLAS DE VARIABLES CON SU VALOR Q SERIA UN OBJET
    //ESTA LISTA SERIA GLOBAL Y TODOS TENDRIAN ACCESO A ELLA
    // PERO LA LSITA FUNCIONA COMO UNA PILA
    //AGREGO COSAS DE IZQUIERDA A DERECHA DONDE LO DE MAS A LA IZQUIERDA ES LO MAS ANTIGUO
    //Y SERA LO ULTIMO EN ACCEDER, SI HAY DOS VARIABLES CON EL MISMO NOMBRE PODRIA HACER ALGO COMO Q SE GUARDE LA POSICION
    //DE DONDE EN LA LISTA SERIA UN NUEVO CONTEXTO
    //PERO BASICAMENTE SI QUISIERA ACCEDER A UNA VARIABLE SOLO RECORRERIA LA LISTA DE DERECHA A IZQUIERDA Y LA PRIMERA 
    //VARIABLE Q TENGA EL MISMO NOMBRE ACCEDO AL VALOR

    //VOLVIENDO A LA IDEA DE DICCIONARIO DE NOMBRE VALOR SE ME ACABA DE OCURRIR COMO IMPLEMENTARLO
    //SENCILLO
    //YA TENGO EL DICCIONARIO ESTATICO DE VARIABLES 
    //SOLO TENGO Q DECIR Q EN CADA CONTEXTO CREAR UN DICCIONARIO NUEVO CON LOS ELEMENTOS DEL ANTERIOR
    //Y A TODOS LOS METODOS LES PASO ESE DICCIOARIO NUEVO
    //EL PROBLEMA CREO Q ESTA SI EL CODIGO BASE CUBRIRA TODAS LAS POSIBLES COMBINACIONES DE CODIGO Q PUEDAN PASAR 
    //ESA ES MI UNICA PREOCUPACION

    //YASE Q HACER
    //LOS UNICOS Q NECESITAN LLEVAR UN CONTEXT SON LOS WHILE LOS FOR Y LOS ACTION(EFECTOS DE CARTAS)
    //SI ESTOS SON LOS UNICOS Q LO NECESITAN LO UNICO Q TENGO Q HACER ES Q ELLOS TENGAN ESA PROPIEDAD
    //LES PONGO A CADA UNO UNA PROPIEDAD DE UN CONTEXT(DICCIONARIO DE VARIABLES) Y CON ESO DEBO RESOLVER EL PROBLEMA
    //A CADA UNO LES PASO COMO PARAMETRO UN CONTEXT PARA PODER CREARLO 
    //Y CON ESO DEBE ARREGLARSE

    //SIGO TENIENDO PROBLEMAS CON LOS METODOS RESERVADOS


    //           AAA               SSSSSSSSSSSS     TTTTTTTTTTTTTTT                      
    //         AAA AAA          SSSSSSSS            TTTTT TTT TTTTT     
    //        AAA   AAA           SSSSSSS           TTT   TTT   TTT       
    //      AAA     AAA              SSSSSSS        TT    TTT    TT            
    //      AAA       AAA                SSSSSSS    T     TTT     T             
    //     AAAAAAAAAAAAAAA             SSSSSSS            TTT             
    //    AAA           AAA     SSSSSSSSSSSS              TTT          

    /*    

public string StringDeterminate()
{
    List<Token> Operation = new();
    while (Peek().Type == TokenType.Comma || Peek().Type == TokenType.LineChange)
    {
        if (IsAtEnd())
            throw new Error("Not valid operation");
        Operation.Add(Peek());
        Advance();
    }
    return CalculateString(Operation);
}

 */

    public object Evaluate(object expected)
    {

        List<Token> tokens = new();
        while (Peek().Type != TokenType.Comma || Peek().Type != TokenType.LineChange || Peek().Type != TokenType.RCurly)
        {
            if (IsAtEnd())
                throw new Error("Wrong implementation of operation");

            tokens.Add(Peek());
            Advance();
        }

        Action operation = Calculate(tokens);

        if (operation is BinaryAction)
        {
            //((BinaryAction)operation)


        }
        else if (operation is UnaryAction)
        {

        }
        throw new Error("");
    }

    //5 + 4 devolver 9 CReo q funciona Panga revisar
    //esta operacion tiene q ser recursiva
    public Token ReturnValueBinary(Action action)
    {
        if (action is Atom variable)
            return variable.Id;

        if (action is BinaryAction binaryAction)

            switch (binaryAction.Operator.Type)
            {
                case TokenType.Plus:
                case TokenType.Minus:
                case TokenType.Multiply:
                case TokenType.Divide:
                case TokenType.Pow:
                    {
                        if (!double.TryParse(ReturnValueBinary(binaryAction.Left).Lexeme, out double left))
                            throw new Error("Expected a number");

                        if (!double.TryParse(ReturnValueBinary(binaryAction.Right).Lexeme, out double right))
                            throw new Error("Expected a number");

                        double.TryParse(binaryAction.Operator.Lexeme, out double previousvalue);

                        switch (binaryAction.Operator.Type)
                        {
                            case TokenType.More:
                                previousvalue = left + right;
                                break;

                            case TokenType.Minus:
                                previousvalue = left - right;
                                break;

                            case TokenType.Multiply:
                                previousvalue = left * right;
                                break;

                            case TokenType.Divide:
                                if (right == 0)
                                    throw new Error("Not accepted division by 0");
                                previousvalue = left / right;
                                break;

                            case TokenType.Pow:
                                previousvalue = Math.Pow(left, right);
                                break;

                            default:
                                throw new Error("Wrong implementation");
                        }
                    }
                    break;

                case TokenType.More:
                case TokenType.MoreEq:
                case TokenType.Less:
                case TokenType.LessEq:
                    {
                        if (ReturnValueBinary(binaryAction.Left).Type == TokenType.NumberValue || ReturnValueBinary(binaryAction.Right).Type == TokenType.NumberValue)
                        {
                            if (!double.TryParse(ReturnValueBinary(binaryAction.Left).Lexeme, out double left))
                                throw new Error("Expected a number");

                            if (!double.TryParse(ReturnValueBinary(binaryAction.Right).Lexeme, out double right))
                                throw new Error("Expected a number");

                            switch (binaryAction.Operator.Type)
                            {
                                case TokenType.More:
                                    binaryAction.Operator.Lexeme = (left > right).ToString();
                                    break;
                                case TokenType.MoreEq:
                                    binaryAction.Operator.Lexeme = (left >= right).ToString();
                                    break;
                                case TokenType.Less:
                                    binaryAction.Operator.Lexeme = (left < right).ToString();
                                    break;
                                case TokenType.LessEq:
                                    binaryAction.Operator.Lexeme = (left <= right).ToString();
                                    break;
                            }
                        }
                        break;
                    }

                case TokenType.Equal:
                    if (ReturnValueBinary(binaryAction.Left).Lexeme == ReturnValueBinary(binaryAction.Right).Lexeme)
                        binaryAction.Operator.Lexeme = "true";
                    else
                        binaryAction.Operator.Lexeme = "false";
                    break;

                case TokenType.Or:
                    if (ReturnValueBinary(binaryAction.Left).Lexeme == "true" || "true" == ReturnValueBinary(binaryAction.Right).Lexeme)
                        binaryAction.Operator.Lexeme = "true";
                    else
                        binaryAction.Operator.Lexeme = "false";
                    break;

                case TokenType.And:
                    if (ReturnValueBinary(binaryAction.Left).Lexeme == "true" && "true" == ReturnValueBinary(binaryAction.Right).Lexeme)
                        binaryAction.Operator.Lexeme = "true";
                    else
                        binaryAction.Operator.Lexeme = "false";
                    break;
            }

        return ((BinaryAction)action).Operator;
    }

    public Token ReturnValueUnary(UnaryAction action)
    {
        double.TryParse(action.ID.Lexeme, out double number);
        if (action.ID.Type == TokenType.NumberValue)
            switch (action.Operation.Type)
            {
                case TokenType.Increment:
                    action.ID.Lexeme = (number + 1).ToString();
                    break;
                case TokenType.Decrement:
                    action.ID.Lexeme = (number - 1).ToString();
                    break;
                default:
                    throw new Error("Wrong definition for a unary operation");
            }
        else if (action.ID.Type == TokenType.VariableName)
        {
            //AKI ETOY TRABADO
            //AKI ETOY TRABADO
            //AKI ETOY TRABADO
            //AKI ETOY TRABADO
            //AKI ETOY TRABADO
            //AKI ETOY TRABADO
            //AKI ETOY TRABADO
            //AKI ETOY TRABADO
            //EL LIO ESE CON Q HAGO LA PINCHA DE SCOPE DE VARIABLES PQ ESO ES LO Q PIENSO HACER
        }
        else
            throw new Error("Not accepted a unary expresion with a operation whitaout a number o variable");

        return action.ID;
    }










    /// <summary>
    /// Parses the operation line searching in order all the posibles operation
    /// </summary>
    /// <param name="tokens"></param>
    /// <returns>The AST with the operation line ready to execute</returns>
    /// <exception cref="Error"></exception>
    public Action Calculate(List<Token> tokens)
    {
        if (tokens.Count == 1)
            return new Atom(tokens[0]);

        for (int i = 0; i < tokens.Count; i++)
            if (AndOrOperators(tokens, i))
                return new BinaryAction(Calculate(SubList(tokens, 0, i - 1)), tokens[i], Calculate(SubList(tokens, 0, i + 1)));
        for (int i = 0; i < tokens.Count; i++)
            if (LogicalOperator(tokens, i))
                return new BinaryAction(Calculate(SubList(tokens, 0, i - 1)), tokens[i], Calculate(SubList(tokens, 0, i + 1)));
        for (int i = 0; i < tokens.Count; i++)
            if (LessImportantOperator(tokens, i))
                return new BinaryAction(Calculate(SubList(tokens, 0, i - 1)), tokens[i], Calculate(SubList(tokens, 0, i + 1)));
        for (int i = 0; i < tokens.Count; i++)
            if (MediumImportantOperator(tokens, i))
                return new BinaryAction(Calculate(SubList(tokens, 0, i - 1)), tokens[i], Calculate(SubList(tokens, 0, i + 1)));
        for (int i = 0; i < tokens.Count; i++)
            if (MoreImportantOperator(tokens, i))
                return new BinaryAction(Calculate(SubList(tokens, 0, i - 1)), tokens[i], Calculate(SubList(tokens, 0, i + 1)));

        for (int i = 0; i < tokens.Count; i++)
            if (IncreDecrement(tokens, i))
            {
                if (tokens[i - 1].Type != TokenType.VariableName || tokens.Count != 2)
                    throw new Error("Not valid Operation");
            }
            else return new UnaryAction(tokens[i - 1], tokens[i]);

        throw new Error("Not valid Operation");
    }


    /// <summary>
    /// Exctracts from a list of tokens one sub list of tokens         
    /// </summary>
    /// <param name="tokens">The token list to extract the sub list</param>
    /// <param name="start">The position where begin the extraction of the original list </param>
    /// <param name="end">The position where end the extraction of the original list</param>
    /// <returns>Return the sub list from start to end</returns>
    /// <exception cref="Error"></exception>
    public List<Token> SubList(List<Token> tokens, int start, int end)
    {
        List<Token> tokens1 = new();

        if (start < 0 || start > tokens.Count || end < 0 || end > tokens.Count)
            throw new Error("Wrong implementation of operation");

        for (int i = start; i < end; i++)
            tokens1.Add(tokens[i]);

        return tokens1;
    }

    /// <summary>
    /// Says if in the position it is a logical operator
    /// </summary>
    /// <returns>True if it is in a logical operator false otherwhise</returns>
    public bool LogicalOperator(List<Token> tokens, int position)
    {
        if (tokens[position].Type == TokenType.Equal)
            return true;
        if (tokens[position].Type == TokenType.LessEq)
            return true;
        if (tokens[position].Type == TokenType.MoreEq)
            return true;
        if (tokens[position].Type == TokenType.Less)
            return true;
        if (tokens[position].Type == TokenType.More)
            return true;
        return false;
    }

    /// <summary>
    /// Method for the less important of the list of operation (+ and -)
    /// </summary>
    /// <param name="n"></param>
    /// <returns>True if the position have a less important operator</returns>
    public bool LessImportantOperator(List<Token> tokens, int n)
    {
        if (tokens[n].Type == TokenType.Plus || tokens[n].Type == TokenType.Minus)
            return true;
        return false;
    }

    /// <summary>
    /// Method for the medium important of the list of operator (* and /)
    /// </summary>
    /// <param name="n"></param>
    /// <returns>True if the position have a medium important operator</returns>
    public bool MediumImportantOperator(List<Token> tokens, int n)
    {
        if (tokens[n].Type == TokenType.Multiply || tokens[n].Type == TokenType.Divide)
            return true;
        return false;
    }

    /// <summary>
    /// Method for the more important of the list of operator (^)
    /// </summary>
    /// <param name="n"></param>
    /// <returns>True if in the position have a more important operator</returns>
    public bool MoreImportantOperator(List<Token> tokens, int n)
    {
        if (tokens[n].Lexeme == "^")
            return true;
        return false;
    }

    /// <summary>
    /// Method for the boolean operators (&& and ||)
    /// </summary>
    /// <param name="n"></param>
    /// <returns>True if in the position have a bolean operator</returns>
    public bool AndOrOperators(List<Token> tokens, int n)
    {
        if (tokens[n].Type == TokenType.And || tokens[n].Type == TokenType.Or)
            return true;
        return false;
    }

    /// <summary>
    /// Method for the increment and decrement operators (++ and --)
    /// </summary>
    /// <param name="n"></param>
    /// <returns>True if in the position have a increment or decrement operator</returns>
    public bool IncreDecrement(List<Token> tokens, int n)
    {
        if (tokens[n].Type == TokenType.Increment || tokens[n].Type == TokenType.Decrement)
            return true;
        return false;
    }






}