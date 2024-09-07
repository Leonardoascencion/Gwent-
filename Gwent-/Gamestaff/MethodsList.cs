
using System.Security.Cryptography.X509Certificates;

/// <summary>
/// Metodos reservados para las listas de cartas en todo el juego
/// </summary>
public static class MethodsList
{

    /// <summary>
    /// Devuelven todas las cartas iguales a la carta predicate
    /// Sigue la definicion de carta del selector
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    public static List<Card> Find(this List<Card> cards, Card predicate)
    {
        List<Card> aux = new();
        foreach (var card in cards)
        {
            if (card.NameComparer(predicate))
                if (card.FactionComparer(predicate))
                    if (card.TypeComparer(predicate))
                        if (card.PowerComparer(predicate))
                            if (card.RangeComparer(predicate))
                                if (card.OwnerComparer(predicate))
                                    aux.Add(card);
        }
        return aux;
    }


    /// <summary>
    /// Agrega una carta al top de la lista
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="card"></param>
    public static void Push(this List<Card> cards, Card card) => cards.Add(card);

    /// <summary>
    /// Agrega una carta al fondo de la lista
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="card"></param>
    public static void SendBottom(this List<Card> cards, Card card) => cards.Insert(0, card);

    /// <summary>
    /// Quita la carta q esta al tope de la lista
    /// </summary>
    /// <param name="cards"></param>
    /// <returns>Devuelve la carta al tope de la lista</returns>
    public static Card Pop(this List<Card> cards)
    {
        Card card = new();
        if (cards.Count > 0)
        {
            card = cards.Last();
            cards.Remove(card);
        }
        return card;

    }

    /// <summary>
    /// Remueve la carta de la lista
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="card"></param>
    /// <returns></returns>
    public static void Remove(this List<Card> cards, Card card) => cards.Remove(card);


    /// <summary>
    /// Mezcla la lista
    /// </summary>
    /// <param name="cards"></param>
    public static void Shuffle(this List<Card> cards)
    {
        List<Card> aux = new();
        Random random = new();

        for (int i = 0; i < cards.Count; i++)
            aux.Add(cards[random.Next(cards.Count - i)]);

        cards = new(aux);
    }

    ////////////METODOS PROPIOS


    /// <summary>
    /// El cambio de mente clasico
    /// </summary>
    /// <param name="cards"></param>
    /// <param name="card"></param>
    /// <param name="player"></param>
    public static void MindControl(this List<Card> cards, Card card, Player player)
    {
        foreach (Card card1 in cards)
            if (card == card1)
                card1.Owner = player;
    }


}