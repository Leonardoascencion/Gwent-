using System.Globalization;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using TokenClass;

class Parser
{
    public readonly List<Token> Tokens = Lexer.Tokens;
    public int CurrentPosition { get; set; } = 0;
    public Action? actionfortest { get; set; }
    public Parser() => Parse();

    /// <summary>
    /// the thing that will parse everything in the source
    /// </summary>
    public void Parse()
    {

        while (!IsAtEnd())
        {
            System.Console.WriteLine("y aki");
            if (Match(TokenType.Effect))
                EffectScope.AddEffect(ParsesEffect().Name, ParsesEffect());
            else
            if (Match(TokenType.Card))
                CardScope.AddCard(ParsesCard().Name, ParsesCard());
            else
            if (Peek().Type == TokenType.VariableName && Peek().Lexeme == "prueba")
            {
                Advance();
                Ignore();
                System.Console.WriteLine("empezo");
                Pruebaparse();
            }
            else
                throw new Error("Not valid imput");
            Advance();
        }
        System.Console.WriteLine("termino");
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


        ///TODAVIA FALTA LA IMPLEMENTACION PARA DETERMINAR EL VALOR DE VARIABLES
        ///TODAVIA FALTA LA IMPLEMENTACION PARA DETERMINAR EL VALOR DE VARIABLES
        ///TODAVIA FALTA LA IMPLEMENTACION PARA DETERMINAR EL VALOR DE VARIABLES

        if (EffectScope.Effects.ContainsKey(effect.Name))
            throw new Error("The current Name has been taken");

        Consume(TokenType.Comma, "Expected Comma");
        Consume(TokenType.LineChange, "Not expected more implementation for Name");
        Ignore();

        #endregion

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

                if (effect.Params.ContainsKey(name))
                    throw new Error("Already declare variable int the param of the effect " + effect.Name);


                effect.Params.Add(name, variableType);
                effect.Variables.Add(name, FindTypeVariable(variableType));
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
        Consume(TokenType.RParen, "Expected right parentesis ( ) )");
        Consume(TokenType.Arrow, "Expected Arrow  ( => )");
        Consume(TokenType.LCurly, "Expected Left Curly ( { )");
        Ignore();

        effect.Variables.Add("targets", new List<Card>());

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
        Dictionary<string, object> Variables = new();
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
        ///ESTA SEPARACION ES PARA Q SE VEA BONITO EN LA BARRA LATERAL
        ///ESTA SEPARACION ES PARA Q SE VEA BONITO EN LA BARRA LATERAL
        ///ESTA SEPARACION ES PARA Q SE VEA BONITO EN LA BARRA LATERAL
        ///ESTA SEPARACION ES PARA Q SE VEA BONITO EN LA BARRA LATERAL
        ///ESTA SEPARACION ES PARA Q SE VEA BONITO EN LA BARRA LATERAL
        ///ESTA SEPARACION ES PARA Q SE VEA BONITO EN LA BARRA LATERAL
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

            Consume(TokenType.Effect, "Expected Effect start");
            Consume(TokenType.Colon, "Expected a declartor :");

            Effect effect = new();

            ///ME DI CUENTA Q AKI TENGO Q PASAR EL METODO Q CALCULA LO Q HALLA Y PREGUNTARLE SI ES UN STRING
            if (Peek().Type == TokenType.WordValue)
            {
                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO    
                string nameofeffect = Peek().Lexeme;

                if (!EffectScope.Effects.ContainsKey(nameofeffect))
                    throw new Error("The effect do not exist");

                effect = EffectScope.Effects[nameofeffect];
                card.OnActivation.Effects.Add(effect);
                card.OnActivation.ParamasOfEffect.Add(effect, new Dictionary<string, object>());

                Advance();
                Consume(TokenType.Colon, "Expected a declartor :");
                Ignore();
                Consume(TokenType.LCurly, "Expected start of the effect declaration ( { )");
                Ignore();
            }
            else if (Peek().Type == TokenType.LCurly)
            {
                {
                    Advance();
                    Ignore();

                    Consume(TokenType.Name, "Expected Name declaration");
                    Consume(TokenType.Colon, "Expected declarator :");

                    ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
                    ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
                    ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
                    string nameofeffect = Peek().Lexeme;

                    if (!EffectScope.Effects.ContainsKey(nameofeffect))
                        throw new Error("The effect do not exist");

                    effect = EffectScope.Effects[nameofeffect];
                    card.OnActivation.Effects.Add(effect);
                    card.OnActivation.ParamasOfEffect.Add(effect, new Dictionary<string, object>());

                    Consume(TokenType.Comma, "Expected a comma");
                    Ignore();
                }
            }
            else throw new Error("Not Valid declaration for effect");

            if (effect.Params.Count != 0)
            {
                int i = effect.Params.Count;
                card.OnActivation.ParamasOfEffect = new();
                while (i > 0)
                {
                    Consume(TokenType.VariableName, "Expected the name of the param of the effect");
                    string variablename = Previous().Lexeme;
                    Consume(TokenType.Colon, "Expected a declartor :");

                    ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
                    ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
                    ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO    
                    object variablevalue = Peek().Lexeme;

                    if (CorrectParmas(variablename, variablevalue, effect))
                        card.OnActivation.ParamasOfEffect[effect].Add(variablename, variablevalue);

                    Consume(TokenType.Comma, "Expected more params");
                    Ignore();
                    i--;
                }
            }

            Consume(TokenType.RCurly, "Expected end of the effect declaration ( } )");
            Consume(TokenType.Comma, "Expected a comma");
            Ignore();

            Consume(TokenType.Selector, "Expected Selector definition");
            card.OnActivation.Selectors.Add(SelectorConstructor(card, false));
            Consume(TokenType.Comma, "Expected a comma ( , )");

            #region PostAction

            List<PostAction> totalpostaction = new();
            Selector selectorpostaction = new();
            Effect effectpostaction = new();
            card.OnActivation.PostActions.Add(effect, new List<PostAction>());


            while (Peek().Type != TokenType.RCurly)
            {
                Consume(TokenType.PostAction, "Expected the start of definition for PostAction or a miss comma has ben written");
                Consume(TokenType.Colon, "Expected declarator :");
                Ignore();

                PostAction postAction = new();

                Consume(TokenType.LCurly, "Expected start of definition for the Selector ( { )");
                Ignore();

                Consume(TokenType.Type, "Expected definition for the effect of postaction");
                Consume(TokenType.Colon, "Expected declarator :");

                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
                ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO  
                string effectforpostaction = Previous().Lexeme;

                if (!EffectScope.Effects.ContainsKey(effectforpostaction))
                    throw new Error("The effect do not exist");

                effectpostaction = EffectScope.Effects[effectforpostaction];
                postAction.Effect = effectpostaction;

                int j = effectpostaction.Params.Count;
                if (j != 0)
                {
                    postAction.Params = new();
                    while (j > 0)
                    {
                        Consume(TokenType.VariableName, "Expected the name of the param of the effect");
                        string variablename = Previous().Lexeme;
                        Consume(TokenType.Colon, "Expected a declartor :");

                        ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
                        ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
                        ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO    
                        object variablevalue = Peek().Lexeme;

                        if (CorrectParmas(variablename, variablevalue, effectpostaction))
                            postAction.Params.Add(variablename, variablevalue);

                        Consume(TokenType.Comma, "Expected more params");
                        Ignore();
                        j--;
                    }
                }

                if (Peek().Type == TokenType.Selector)
                    selectorpostaction = SelectorConstructor(card, true);
                else
                if (totalpostaction.Count == 0)
                    postAction.Selector = card.OnActivation.Selectors.Last();
                else
                    postAction.Selector = totalpostaction.Last().Selector;

                Consume(TokenType.Comma, "Expected a comma ( , )");
                Ignore();
                totalpostaction.Add(postAction);


                card.OnActivation.PostActions[effect].Add(postAction);
            }

            for (int i = 0; i < card.OnActivation.PostActions[effect].Count; i++)
            {
                Consume(TokenType.RCurly, "Expected end of definition for the element of Onactivation ( } )");
                Ignore();
            }

            Consume(TokenType.RCurly, "Expected end of definition for the element of Onactivation ( } )");

            if (Peek().Type == TokenType.Comma)
                Advance();

            Ignore();

            #endregion

        }
        #endregion

        Advance();
        Ignore();
        Consume(TokenType.RCurly, "Expected the end of the card declaration ( } )");

        return card;
    }

    /// <summary>
    /// Empieza donde se encontro el token selector y continua a partir de ahi analizando q este escrita correctamente la estructura del codigo
    /// </summary>
    /// <param name="card"></param>
    /// <returns>El selector formado de las lineas de codigo</returns>
    public Selector SelectorConstructor(Card card, bool ispostaction = false)
    {
        Consume(TokenType.Colon, "Expected a declartor :");
        Consume(TokenType.LCurly, "Expected start of the selector declaration ( { )");
        Ignore();

        Selector selector = new Selector();

        Consume(TokenType.Source, "Expected the start of the definition for Source");
        Consume(TokenType.Colon, "Expected declarator :");

        ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
        ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
        ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO  
        string nameofsource = Previous().Lexeme;

        if (nameofsource == "parent")
        {
            if (ispostaction)
            {
                if (card.OnActivation.PostActions.ContainsKey(card.OnActivation.Effects.Last()))
                {
                    if (card.OnActivation.PostActions[card.OnActivation.Effects.Last()].Count == 0)
                        selector.Source = card.OnActivation.Selectors.Last().FinalSource;
                    else
                        selector.Source = card.OnActivation.PostActions[card.OnActivation.Effects.Last()].Last().Selector.FinalSource;
                }
                else
                    selector.Source = card.OnActivation.Selectors.Last().FinalSource;
            }
            else throw new Error("Only valid parent source in postaction");
        }
        else
            selector.Source = SourceFinder(nameofsource, card.Owner);

        Consume(TokenType.Comma, "Expected a comma ,");
        Ignore();

        if (Peek().Type == TokenType.Single)
        {
            Advance();
            Consume(TokenType.Colon, "Expected declarator :");
            Consume(TokenType.VariableName, "Expected a bolean value or a boolean variable");

            ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
            ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO
            ///AKI VA EL METODO PARA CALCULAR EL VALOR DEL STRING DEVUELTO POR EL AST FORMADO  
            object singlevalue = Previous();
            ///METODO PARA SABER SI ES UN BOOLEANO

            if (singlevalue is bool condition)
                selector.Single = condition;

            Consume(TokenType.Comma, "Expected a comma ( , )");
            Ignore();
        }

        Consume(TokenType.Predicate, "Expected the start of the definition for Predicate");
        Consume(TokenType.Colon, "Expected declarator :");
        Consume(TokenType.LParen, "Expected start of definition of the variable for predicate action ( ( )");
        Consume(TokenType.VariableName, "Expected a valid variable definition for the predicate");
        Consume(TokenType.RParen, "Expected end of definition of the variable for predicate action ( ) )");
        Consume(TokenType.Arrow, "Expected the indicator for the start of the predicate definition ( => ) ");


        List<Card> predicateresultlist = new();
        Card predicate = new();

        ///AKI VA UN METODO ESPECIAL PARA CALCULAR LA ENTRADA DESCRITA EN LAS LINEAS SIGUIENTES DEL CODIGO FUENTE
        ///AKI VA UN METODO ESPECIAL PARA CALCULAR LA ENTRADA DESCRITA EN LAS LINEAS SIGUIENTES DEL CODIGO FUENTE
        ///AKI VA UN METODO ESPECIAL PARA CALCULAR LA ENTRADA DESCRITA EN LAS LINEAS SIGUIENTES DEL CODIGO FUENTE

        ///TENGO DE IDEA ALGO COMO UN FOREACH CARTA EN LA SOURCE REALIZAR ESTE METODO DIFERENTE PARA CADA CARTA
        ///TENGO DE IDEA ALGO COMO UN FOREACH CARTA EN LA SOURCE REALIZAR ESTE METODO DIFERENTE PARA CADA CARTA
        ///TENGO DE IDEA ALGO COMO UN FOREACH CARTA EN LA SOURCE REALIZAR ESTE METODO DIFERENTE PARA CADA CARTA

        Ignore();

        Consume(TokenType.RCurly, "Expected end of definition for the Selector ( } )");

        selector.FinalSource = predicateresultlist;
        ///AKI VA UN METODO Q DEVUELVE UN OBJECT, LE HAGO UN : object is List<CARD>, Y CONTINUO

        return selector;
    }

    #endregion

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
    public void Advance()
    {
        if (!IsAtEnd())
            CurrentPosition++;
        else
            throw new Error("Invalid source");
    }

    /// <summary>
    /// Consume the current token if have the type specifaid otherwise send a error
    /// </summary>
    /// <param name="type"></param>
    /// <param name="errormesage"></param>
    /// <exception cref="Error"></exception>
    public void Consume(TokenType type, string errormesage)
    {
        if (Peek().Type == type)
            Advance();
        else
            throw new Error(errormesage + " after " + Previous().Lexeme + "  at  " + CurrentPosition.ToString());
    }

    /// <summary>
    /// Advances while it is stand in a jump line
    /// </summary>
    public void Ignore()
    {
        while (Peek().Type == TokenType.LineChange || IsAtEnd())
            CurrentPosition++;
    }
    #endregion

    #region Helper Method For Parse
    /// <summary>
    /// Cheks the value object for the type admisible of a variable
    /// </summary>
    /// <param name="value"></param>
    /// <returns>The variable type acord to the value of the object</returns>
    /// <exception cref="Error"></exception>
    public VariableType FindTypeValue(object value)
    {
        switch (value)
        {
            case bool:
                return VariableType.Boolean;

            case string:
                return VariableType.String;

            case double:
            case int:
                return VariableType.Number;

            default:
                break;
        }
        throw new Error("Not defined type");
    }

    /// <summary>
    /// For validate the params diccionary
    /// </summary>
    /// <param name="token"></param>
    /// <returns>The type accord to the token declaration (token type string returns string)</returns>
    /// <exception cref="Error"></exception>
    public VariableType FindVariableType(Token token)
    {
        switch (token.Type)
        {
            case TokenType.Bool:
                return VariableType.Boolean;

            case TokenType.Number:
                return VariableType.Number;

            case TokenType.String:
                return VariableType.String;

            default:
                throw new Error("Not defined type");
        }
    }

    /// <summary>
    /// Cheks the variabletype for a primitive value
    /// </summary>
    /// <param name="variableType"></param>
    /// <returns>The primitive value accord to the variable type (ejemp number primitive value is 0)</returns>
    /// <exception cref="Error"></exception>
    public object FindTypeVariable(VariableType variableType)
    {
        switch (variableType)
        {
            case VariableType.Number:
                return 0;

            case VariableType.Boolean:
                return false;

            case VariableType.String:
                return "";

            default:
                throw new Error("No va a pasar esto");
        }

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
    /// Checks if the effect calls have the same params has the definition
    /// </summary>
    /// <param name="namevariable">The name of the variable</param>
    /// <param name="type">The type of the variable</param>
    /// <param name="effect">The effect to check the match</param>
    /// <returns>True if it have the same paramas propetys the call and the definition</returns>
    public bool CorrectParmas(string variablename, object variablevalue, Effect effect)
    {
        if (effect.Params.ContainsKey(variablename))
            if (effect.Params[variablename] == FindTypeValue(variablevalue))
                return true;

        throw new Error("Incorrect definition of variable name or type");
    }

    /// <summary>
    /// Metodo sencillo para validar la source
    /// </summary>
    /// <param name="nameofsource"></param>
    /// <param name="owner"></param>
    /// <returns>La source correspondiente a la entrada, si no se encuentra entre las definidas lanza un error, la source parent no es admisible</returns>
    /// <exception cref="Error"></exception>
    public List<Card> SourceFinder(string nameofsource, Player owner)
    {
        switch (nameofsource)
        {
            case "board":
                return Context.Board();

            case "field":
                return owner.FieldofPlayer();

            case "deck":
                return owner.Deck.PlayerDeck;
            case "hand":
                return owner.Hand.Hand;

            case "otherField":
                if (owner == Context.Player1)
                    return Context.Player2.FieldofPlayer();
                else
                    return Context.Player1.FieldofPlayer();

            case "otherDeck":
                if (owner == Context.Player1)
                    return Context.Player2.Deck.PlayerDeck;
                else
                    return Context.Player1.Deck.PlayerDeck;

            case "otherHand":
                if (owner == Context.Player1)
                    return Context.Player2.Hand.Hand;
                else
                    return Context.Player1.Hand.Hand;

            default:
                throw new Error("Not valid source");
        }

    }

    /// <summary>
    /// Un metodo sencillo para saber si se llego al final de una linea de operacion o asignacion
    /// </summary>
    /// <returns>True si se encuentra algunas de los tokens q terminan una operacion( ejemplo ; o , o ) )</returns>
    public bool OperationEnd()
    {
        if (IsAtEnd())
            throw new Error("Not valid operation operation at end");

        if (Peek().Type == TokenType.Comma || Peek().Type == TokenType.LineChange || Peek().Type == TokenType.RCurly || Peek().Type != TokenType.Semicolon)
            return true;

        return false;
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

        for (int i = start; i <= end; i++)
            tokens1.Add(tokens[i]);

        return tokens1;
    }

    #endregion


    /// <summary>
    /// Method for body construction
    /// </summary>
    /// <returns>The body of all action to execute</returns>
    public List<Action> BodyConstructor()
    {
        List<Action> body = new();
        while (Peek().Type != TokenType.RCurly)
        {
            Ignore();
            body.Add(FinalFunctionMethod());
            Consume(TokenType.Semicolon, "Espera un semicolon aki es el error");
            Ignore();
        }
        Advance();
        return body;
    }


    ////EN TEORIA DEBE FUNCIONAR (TODO ESTA IMPLEMENTADO)
    public Action FinalFunctionMethod()
    {
        switch (Peek().Type)
        {
            case TokenType.While:
                Advance();
                return ForConstructor();

            case TokenType.For:
                Advance();
                return WhileConstructor();

            default:
                Action action = PrimitiveAction();
                if (PosibleInstruction.EndOfLeftTerm.Contains(Peek().Type))
                {
                    Token token = Peek();

                    if (Peek().Type == TokenType.Decrement || Peek().Type == TokenType.Increment)
                    {
                        Advance();
                        return new UnaryAction(action, token);
                    }
                    Advance();
                    return new ObjectAsignation(action, token, FinalFunctionMethod());
                }
                if (PosibleInstruction.BynariOperations.Contains(Peek().Type))
                {
                    BinaryAction binaryAction = new();
                    binaryAction.Terms.Add(action);
                    binaryAction.Operators.Add(Peek().Type);
                    System.Console.WriteLine("agregue el operador " + Peek().Type);
                    Advance();
                    binaryAction.Complete(FinalFunctionMethod());
                    return binaryAction;
                }
                return action;
        }
    }

    ///HAY Q HACERLE UN PAR DE PRUEBAS A VER SI CREA BIEN LOS ARBOLES
    ///EN TEORIA DEBE FUNCIONAR
    /// <summary>
    /// El metodo encargado de crear el arbol para cualquier instruccion(no le importa si esta completamente bien escrito)
    /// </summary>
    /// <returns>El arbol q contiene una instruccion</returns>
    /// <exception cref="Error"></exception>
    public Action PrimitiveAction()
    {
        if (!PosibleInstruction.ValidInstruction.Contains(Peek().Type))
            throw new Error("Not valid instruction");

        System.Console.WriteLine("voy a agregar  " + Peek().Type + " " + Peek().Lexeme);
        PrimitiveAtom primitiveAtom = new(Peek());
        Advance();

        if (primitiveAtom.PrimitiveValue.Type == TokenType.Find)
        {
            Consume(TokenType.LParen, "Expected star of method call");
            Consume(TokenType.LParen, "Expected star of method call");
            Token reference = Peek();
            Consume(TokenType.VariableName, "Expected a variable declaration for target");
            Consume(TokenType.RParen, "Expected end of method call");
            Consume(TokenType.Arrow, "Expected a arrow");
            Predicate predicate = new(reference, FinalFunctionMethod());
            MethodCall methodCall = new(primitiveAtom.PrimitiveValue);
            Consume(TokenType.RParen, "Expected end of method call");
            methodCall.Params = predicate;

            if (Peek().Type == TokenType.LBracket)
            {
                Advance();
                Consume(TokenType.NumberValue, "Expected a number value for index");
                methodCall.Indexation(Previous().Lexeme);
                Consume(TokenType.RBracket, "Expected end of indexation");
            }

            if (ElmentLimit()) return methodCall;

            if (ElementJump()) return methodCall;

            if (Peek().Type == TokenType.Point)
            {
                Advance();
                return new InstructionExecution(methodCall, PrimitiveAction());
            }

            throw new Error("Not valid implementation for Find");

        }


        if (Peek().Type == TokenType.Point)
            return PointCase(primitiveAtom);
        if (PosibleInstruction.Methods.Contains(primitiveAtom.PrimitiveValue.Type))
            if (Peek().Type == TokenType.LParen)
            {

                Advance();
                MethodCall methodCall = new MethodCall(primitiveAtom.PrimitiveValue);
                if (PosibleInstruction.EmptyMethods.Contains(primitiveAtom.PrimitiveValue.Type))
                    if (Peek().Type == TokenType.RParen)
                    {
                        Advance();
                    }
                    else
                        throw new Error("This is not a valid empty method ");
                else
                {
                    methodCall.ParamsCreation(PrimitiveAction());
                    Consume(TokenType.RParen, "Expected end of method call");
                }

                if (Peek().Type == TokenType.LBracket)
                {
                    Advance();
                    Consume(TokenType.NumberValue, "Expected a number value for index");
                    methodCall.Indexation(Previous().Lexeme);
                    Consume(TokenType.RBracket, "Expected end of indexation");
                }
                if (Peek().Type == TokenType.Point)
                {
                    Advance();
                    if (methodCall.Params == null)
                        return new InstructionExecution(primitiveAtom, PrimitiveAction());

                    return new InstructionExecution(methodCall, PrimitiveAction());
                }
                if (methodCall.Params == null)
                    return primitiveAtom;

                return methodCall;
            }
            else
                throw new Error("Expected cast of params of method ");

        if (Peek().Type == TokenType.LBracket)
        {
            if (!PosibleInstruction.IsaList.Contains(primitiveAtom.PrimitiveValue.Type))
                throw new Error("Only can index in a list");

            MethodCall methodCall = new(primitiveAtom.PrimitiveValue);
            Advance();
            Consume(TokenType.NumberValue, "Expected a number value for index");
            methodCall.Indexation(Previous().Lexeme);
            Consume(TokenType.RBracket, "Expected end of indexation");

            if (Peek().Type == TokenType.Point)
            {
                Advance();
                return new InstructionExecution(methodCall, PrimitiveAction());
            }

            return methodCall;
        }

        if (ElmentLimit() || ElementJump()) return primitiveAtom;

        ///ESTO CREO Q PUEDO USARLO PARA OPTIMIZAR DESPUES
        /*         if (PosibleInstruction.MethodCallInstruction.Contains(primitiveAtom.PrimitiveValue.Type))
                {
                    Consume(TokenType.LParen, "Expected star of method call");
                    Consume(TokenType.RParen, "Expected end of method call");
                    return primitiveAtom;
                }

                if (PosibleInstruction.MethodCallParams.Contains(primitiveAtom.PrimitiveValue.Type))
                {
                    Consume(TokenType.LParen, "Expected star of method call");
                    ListIndexCase listIndexCase = new ListIndexCase(primitiveAtom.PrimitiveValue, PrimitiveAction());
                    Consume(TokenType.RParen, "Expected end of method call");
                    if (Peek().Type == TokenType.LBracket)
                    {
                        Advance();
                        // arreglar esto listIndexCase.Indexation(PrimitiveLeftAction());
                        Consume(TokenType.RBracket, "Expected end of indexation");
                    }
                    return listIndexCase;
                }

                if (PosibleInstruction.ValidInstruction.Contains(Peek().Type))
                {
                    PrimitiveAtom primitiveAtom1 = new PrimitiveAtom(Peek());
                    Advance();

                    if (PosibleInstruction.EndOperation.Contains(Peek().Type) || PosibleInstruction.EndOfLeftTerm.Contains(Peek().Type) || PosibleInstruction.BynariOperations.Contains(Peek().Type))
                        return primitiveAtom1;

                    if (Peek().Type == TokenType.Comma)
                        return new MethodExecution(primitiveAtom1, PrimitiveAction());

                    if (Peek().Type == TokenType.LBracket)
                    {
                        Advance();
                        PrimitiveAction();
                    }

                } */

        throw new Error("Invalid source");
    }

    public Action PointCase(PrimitiveAtom primitiveAtom)
    {
        Advance();
        return new InstructionExecution(primitiveAtom, PrimitiveAction());
    }



    /// <summary>
    /// Constructor for while instruction
    /// </summary>
    /// <returns></returns>
    public WhileAction WhileConstructor()
    {
        Consume(TokenType.LParen, "Expected star of method call");
        Action condition = PrimitiveAction();
        Consume(TokenType.RParen, "Expected end of method call");

        if (Peek().Type == TokenType.LCurly)
            return new WhileAction(condition, BodyConstructor());
        else
            return new WhileAction(condition, new List<Action>() { PrimitiveAction() });
    }

    /// <summary>
    /// Constructor to for instructions
    /// </summary>
    /// <returns></returns>
    public ForeachAction ForConstructor()
    {
        Token targetname = Peek();
        Consume(TokenType.VariableName, "Expected a variable declaration for target");
        Consume(TokenType.In, "Expected in word");
        Token targetsname = Peek();
        Consume(TokenType.VariableName, "Expected a variable declaration for targets");

        if (Peek().Type == TokenType.LCurly)
            return new ForeachAction(targetname.Lexeme, targetsname.Lexeme, BodyConstructor());
        else
            return new ForeachAction(targetname.Lexeme, targetsname.Lexeme, new List<Action>() { PrimitiveAction() });
    }

    /// <summary>
    /// Metodo para saber si llego al final de una construccion de elemento ) ] 
    /// </summary>
    /// <returns></returns>
    public bool ElementJump()
    {
        if (PosibleInstruction.EndOperation.Contains(Peek().Type)) return true;
        else return false;
    }

    /// <summary>
    /// Metodo para saber si llego al separador entre dos elementos + = ;
    /// </summary>
    /// <returns></returns>
    public bool ElmentLimit()
    {
        if (Peek().Type == TokenType.Semicolon) return true;
        if (PosibleInstruction.BynariOperations.Contains(Peek().Type)) return true;
        if (PosibleInstruction.EndOfLeftTerm.Contains(Peek().Type)) return true;
        return false;

    }


    public void Pruebaparse()
    {
        List<Action> actions = BodyConstructor();
        System.Console.WriteLine(actions.Count);
        System.Console.WriteLine("YES YES YES");
        foreach (Action action in actions)
        {
            LectorPrueba(action);
        }
    }

    public void LectorPrueba(Action action)
    {
        System.Console.WriteLine(" Q ES ESTE ACTION");
        System.Console.WriteLine(action);
        if (action is PrimitiveAtom primitiveAtom)
        {
            System.Console.WriteLine(primitiveAtom.PrimitiveValue.Lexeme);
        }
        if (action is MethodCall methodCall)
        {
            System.Console.WriteLine(methodCall.Instruction.Lexeme + " instruccion a ejecutar");
            if (methodCall.Index != null)
                System.Console.WriteLine(methodCall.Index + " valor del index");
            if (methodCall.Params != null)
            {
                System.Console.WriteLine("valor del parametro");
                LectorPrueba(methodCall.Params);
            }
        }
        if (action is InstructionExecution instructionExecution)
        {
            System.Console.WriteLine("A LA IZQUIERDA");
            LectorPrueba(instructionExecution.FirstReference);
            System.Console.WriteLine("A LA DERECHA");
            LectorPrueba(instructionExecution.LaterReference);
        }
        if (action is BinaryAction binaryAction)
        {
            System.Console.WriteLine("SIZE OF THE TINHG");
            System.Console.WriteLine(binaryAction.Terms.Count);
            System.Console.WriteLine(binaryAction.Operators.Count);
            for (int i = 0; i < binaryAction.Operators.Count; i++)
            {
                System.Console.WriteLine("Q OPERADOR ERES");
                System.Console.WriteLine(binaryAction.Operators[i]);
                LectorPrueba(binaryAction.Terms[i]);
                LectorPrueba(binaryAction.Terms[i + 1]);
            }
        }
    }
}