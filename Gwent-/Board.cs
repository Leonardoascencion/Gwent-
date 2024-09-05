/* 
public class Board
{
    public Zones zones;
}

public class Zones
{
    public static CombatField Field1 { get; set; } = new();
    public static CombatField Field2 { get; set; } = new();
    public static Graveyard Grave1 { get; set; } = new();
    public static Graveyard Grave2 { get; set; } = new();
    public static PlayerHand Hand1 { get; set; } = new();
    public static PlayerHand Hand2 { get; set; } = new();
    public static LiderZone Lider1 { get; set; } = new();
    public static LiderZone Lider2 { get; set; } = new();
}
 */



public class PlayerHand
{
    public List<Card> Hand { get; set; } = new();
    public List<Card> OverStack { get; set; } = new();

    public void Draw(Card card)
    {
        if (Hand.Count < 10)
            Hand.Add(card);
        else
            OverStack.Add(card);
    }
}

public class Graveyard
{
    List<Card> DeadCards { get; set; } = new();

    public void Kill(Card card) => DeadCards.Add(card);
}

public class LiderZone
{
    public Card Lider { get; set; } = new Card();

    public void Invoke(Card card)
    {
        if (card.Type == "Lider")
            Lider = card;
    }
}

public static class Climate
{
    public static List<Card> Climates { get; set; } = new();

    public static List<Card> Descart { get; set; } = new();

    public static void ChangeWeter(Card card)
    {
        if (card.Type == "Clima")
            Climates.Add(card);
        else
            Descart.Add(card);
    }
}

public class CombatField
{
    public double TotalPower { get; set; } = 0;

    public List<Card> MeleeZone { get; set; } = new();
    public List<Card> RangedZone { get; set; } = new();
    public List<Card> SiegeZone { get; set; } = new();

    public Card BuffMelee { get; set; } = new Card();
    public Card BuffRanged { get; set; } = new Card();
    public Card BuffSiege { get; set; } = new Card();

    public void InvokeMelee(Card card)
    {
        foreach (var range in card.Range)
            if (range.ToString() == "Melee")
                MeleeZone.Add(card);
        Refresh();
    }

    public void InvokeRanged(Card card)
    {
        foreach (var range in card.Range)
            if (range.ToString() == "Range")
                MeleeZone.Add(card);
        Refresh();
    }

    public void InvokeSiege(Card card)
    {
        foreach (var range in card.Range)
            if (range.ToString() == "Siege")
                MeleeZone.Add(card);
        Refresh();
    }

    public void UgradeMelee(Card card)
    {
        if (card.Type == "Aumento")
            BuffMelee = card;
        Refresh();
    }

    public void UgradeRanged(Card card)
    {
        if (card.Type == "Aumento")
            BuffMelee = card;
        Refresh();
    }

    public void UgradeSiege(Card card)
    {
        if (card.Type == "Aumento")
            BuffMelee = card;
        Refresh();
    }

    public void Refresh()
    {
        foreach (Card card in MeleeZone) TotalPower += card.Power;
        foreach (Card card in RangedZone) TotalPower += card.Power;
        foreach (Card card in SiegeZone) TotalPower += card.Power;
    }
}

public class Deck
{
    public List<Card> PlayerDeck { get; set; } = new List<Card>();

    public void CreateDeck(List<Card> cards)
    {
        Random random = new Random();
        int positon = 0;
        foreach (Card card in cards)
        {
            positon = random.Next(cards.Count);
            PlayerDeck.Add(cards[positon]);
        }
    }
}