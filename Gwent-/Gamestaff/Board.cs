

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
        AllCards.Add(player.LiderOfPlayer.Lider);
        AllCards.AddRange(player.PlayerDeck.PlayerDeck);
        AllCards.AddRange(player.PlayerField.MeleeZone);
        AllCards.AddRange(player.PlayerField.RangedZone);
        AllCards.AddRange(player.PlayerField.SiegeZone);
        AllCards.Add(player.PlayerField.BuffMelee);
        AllCards.Add(player.PlayerField.BuffRanged);
        AllCards.Add(player.PlayerField.BuffSiege);

    }
    public Board(Player player1, Player player2)
    {
        AllCards.AddRange(player1.Hand.Hand);
        AllCards.AddRange(player1.Grave.DeadCards);
        AllCards.Add(player1.LiderOfPlayer.Lider);
        AllCards.AddRange(player1.PlayerDeck.PlayerDeck);
        AllCards.AddRange(player1.PlayerField.MeleeZone);
        AllCards.AddRange(player1.PlayerField.RangedZone);
        AllCards.AddRange(player1.PlayerField.SiegeZone);
        AllCards.Add(player1.PlayerField.BuffMelee);
        AllCards.Add(player1.PlayerField.BuffRanged);
        AllCards.Add(player1.PlayerField.BuffSiege);

        AllCards.AddRange(player2.Grave.DeadCards);
        AllCards.AddRange(player2.Hand.Hand);
        AllCards.Add(player2.LiderOfPlayer.Lider);
        AllCards.AddRange(player2.PlayerDeck.PlayerDeck);
        AllCards.AddRange(player2.PlayerField.MeleeZone);
        AllCards.AddRange(player2.PlayerField.RangedZone);
        AllCards.AddRange(player2.PlayerField.SiegeZone);
        AllCards.Add(player2.PlayerField.BuffMelee);
        AllCards.Add(player2.PlayerField.BuffRanged);
        AllCards.Add(player2.PlayerField.BuffSiege);
    }
}

public class Field
{
    public List<Card> FieldCards { get; set; } = new();

    public Field(Player player)
    {
        FieldCards.Add(player.LiderOfPlayer.Lider);
        FieldCards.AddRange(player.PlayerField.MeleeZone);
        FieldCards.AddRange(player.PlayerField.RangedZone);
        FieldCards.AddRange(player.PlayerField.SiegeZone);
        FieldCards.Add(player.PlayerField.BuffMelee);
        FieldCards.Add(player.PlayerField.BuffRanged);
        FieldCards.Add(player.PlayerField.BuffSiege);
    }
    public Field(Player player1, Player player2)
    {
        FieldCards.Add(player1.LiderOfPlayer.Lider);
        FieldCards.AddRange(player1.PlayerField.MeleeZone);
        FieldCards.AddRange(player1.PlayerField.RangedZone);
        FieldCards.AddRange(player1.PlayerField.SiegeZone);
        FieldCards.Add(player1.PlayerField.BuffMelee);
        FieldCards.Add(player1.PlayerField.BuffRanged);
        FieldCards.Add(player1.PlayerField.BuffSiege);

        FieldCards.Add(player2.LiderOfPlayer.Lider);
        FieldCards.AddRange(player2.PlayerField.MeleeZone);
        FieldCards.AddRange(player2.PlayerField.RangedZone);
        FieldCards.AddRange(player2.PlayerField.SiegeZone);
        FieldCards.Add(player2.PlayerField.BuffMelee);
        FieldCards.Add(player2.PlayerField.BuffRanged);
        FieldCards.Add(player2.PlayerField.BuffSiege);
    }
}
