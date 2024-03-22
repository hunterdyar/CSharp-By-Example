//Enum stands for 'enumeration type'. They are a named constants.  
//Behind the scenes, they get compiled to integers. 
//They are useful to represent a value or state that is non-numeric. Think "one-of-these-options".
public enum Suit
{
    Hearts,
    Spades,
    Clubs,
    Diamonds,
}

//You can explicitly define which integer will represent each enum value.  
//It is not neccesary to define every single value manually.
public enum Rank
{
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13,
    Ace = 1,
}
//---

//In this example, consider how this class, representing a single playing card, takes advantage of enums for code that is easy to read.
public class Card
{
    public Suit Suit;
    public Rank Rank;

    public bool MatchesColor(Suit otherSuit)
    {
        switch (otherSuit)
        {
            case Suit.Diamonds:
            case Suit.Hearts:
                return this.Suit == Suit.Diamonds || this.Suit == Suit.Hearts;
            case Suit.Clubs:
            case Suit.Spades:
                return this.Suit == Suit.Clubs || this.Suit == Suit.Spades;
            default:
                return false;
        }
    }

    public bool IsOneGreaterThan(Card otherCard)
    {
        return (int)Rank - (int)otherCard.Rank == 1;
    }

public bool CanKlondikeStackOn(Card otherCard)
{
    return !MatchesColor(otherCard.Suit) && IsOneGreaterThan(otherCard);
}

}