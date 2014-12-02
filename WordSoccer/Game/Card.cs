namespace WordSoccer.Game
{
	public class Card
	{
		private readonly CardType cardType;

		public Card(CardType cardType)
		{
			this.cardType = cardType;
		}

		public CardType GetCardType()
		{
			return cardType;
		}

		public enum CardType
		{
			RED, YELLOW, NONE
		}
	}
}