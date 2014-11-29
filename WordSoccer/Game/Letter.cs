using System;

namespace WordSoccer.Game
{
	public class Letter
	{
		private readonly int number;
		private char sign = ' ';
		private bool used;
		private Card.CardType cardType;
		private ILetterListener listener;

		public Letter(int number)
		{
			this.number = number;
			used = false;
		}

		public int GetNumber()
		{
			return number;
		}

		public char GetSign()
		{
			return sign;
		}

		public Letter SetSign(char sign)
		{
			this.sign = sign;

			OnChange();

			return this;
		}

		public bool IsUsed()
		{
			return used;
		}

		public Letter SetUsed(bool used)
		{
			if (IsDisabled() && used)
			{
				throw new Exception("Cannot use a disabled letter.");
			}

			this.used = used;

			OnChange();

			return this;
		}

		public bool IsDisabled()
		{
			return cardType == Card.CardType.RED;
		}

		public Card.CardType GetCardType()
		{
			return cardType;
		}

		public Letter SetCardType(Card.CardType cardType)
		{
			this.cardType = cardType;

			if (IsDisabled())
			{
				used = false;
			}

			OnChange();

			return this;
		}

		private void OnChange()
		{
			if (listener != null)
			{
				listener.OnChanged();
			}
		}

		public Letter SetListener(ILetterListener listener)
		{
			this.listener = listener;

			return this;
		}

		public interface ILetterListener
		{
			void OnChanged();
		}
	}
}