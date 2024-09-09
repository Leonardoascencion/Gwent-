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
    public List<Card> DeadCards { get; set; } = new();

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


public class Board
{
    public List<Card> AllCards { get; set; } = new();

    public Board(Player player)
    {

        AllCards.AddRange(player.Hand.Hand);
        AllCards.AddRange(player.Grave.DeadCards);
        AllCards.Add(player.Lider.Lider);
        AllCards.AddRange(player.Deck.PlayerDeck);
        AllCards.AddRange(player.CombatField.MeleeZone);
        AllCards.AddRange(player.CombatField.RangedZone);
        AllCards.AddRange(player.CombatField.SiegeZone);
        AllCards.Add(player.CombatField.BuffMelee);
        AllCards.Add(player.CombatField.BuffRanged);
        AllCards.Add(player.CombatField.BuffSiege);

    }
    public Board(Player player1, Player player2)
    {
        AllCards.AddRange(player1.Hand.Hand);
        AllCards.AddRange(player1.Grave.DeadCards);
        AllCards.Add(player1.Lider.Lider);
        AllCards.AddRange(player1.Deck.PlayerDeck);
        AllCards.AddRange(player1.CombatField.MeleeZone);
        AllCards.AddRange(player1.CombatField.RangedZone);
        AllCards.AddRange(player1.CombatField.SiegeZone);
        AllCards.Add(player1.CombatField.BuffMelee);
        AllCards.Add(player1.CombatField.BuffRanged);
        AllCards.Add(player1.CombatField.BuffSiege);

        AllCards.AddRange(player2.Grave.DeadCards);
        AllCards.AddRange(player2.Hand.Hand);
        AllCards.Add(player2.Lider.Lider);
        AllCards.AddRange(player2.Deck.PlayerDeck);
        AllCards.AddRange(player2.CombatField.MeleeZone);
        AllCards.AddRange(player2.CombatField.RangedZone);
        AllCards.AddRange(player2.CombatField.SiegeZone);
        AllCards.Add(player2.CombatField.BuffMelee);
        AllCards.Add(player2.CombatField.BuffRanged);
        AllCards.Add(player2.CombatField.BuffSiege);
    }
}

public class Field
{
    public List<Card> FieldCards { get; set; } = new();

    public Field(Player player)
    {
        FieldCards.Add(player.Lider.Lider);
        FieldCards.AddRange(player.CombatField.MeleeZone);
        FieldCards.AddRange(player.CombatField.RangedZone);
        FieldCards.AddRange(player.CombatField.SiegeZone);
        FieldCards.Add(player.CombatField.BuffMelee);
        FieldCards.Add(player.CombatField.BuffRanged);
        FieldCards.Add(player.CombatField.BuffSiege);
    }
    public Field(Player player1, Player player2)
    {
        FieldCards.Add(player1.Lider.Lider);
        FieldCards.AddRange(player1.CombatField.MeleeZone);
        FieldCards.AddRange(player1.CombatField.RangedZone);
        FieldCards.AddRange(player1.CombatField.SiegeZone);
        FieldCards.Add(player1.CombatField.BuffMelee);
        FieldCards.Add(player1.CombatField.BuffRanged);
        FieldCards.Add(player1.CombatField.BuffSiege);

        FieldCards.Add(player2.Lider.Lider);
        FieldCards.AddRange(player2.CombatField.MeleeZone);
        FieldCards.AddRange(player2.CombatField.RangedZone);
        FieldCards.AddRange(player2.CombatField.SiegeZone);
        FieldCards.Add(player2.CombatField.BuffMelee);
        FieldCards.Add(player2.CombatField.BuffRanged);
        FieldCards.Add(player2.CombatField.BuffSiege);
    }
}
