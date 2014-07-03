using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace UnoC
{
    public enum Colors { Red, Green, Blue, Yellow, Black };
    public enum SpecialCards { Skip = 10, Turn, TakeTwo, ChangeColor, ChangeColorPlusFour };

    public class UnoCards
    {
        private List<UnoCard> cards;

        public UnoCards()
        {
            cards = new List<UnoCard>();
            this.Init();
        }

        public void Init()
        {
            this.cards.Clear();
            for (int i = 0; i <= 12; i++)
            {
                this.cards.Add(new UnoCard(i, Colors.Blue));
                this.cards.Add(new UnoCard(i, Colors.Green));
                this.cards.Add(new UnoCard(i, Colors.Red));
                this.cards.Add(new UnoCard(i, Colors.Yellow));
                if (i != 0)
                {
                    this.cards.Add(new UnoCard(i, Colors.Blue));
                    this.cards.Add(new UnoCard(i, Colors.Green));
                    this.cards.Add(new UnoCard(i, Colors.Red));
                    this.cards.Add(new UnoCard(i, Colors.Yellow));
                }
            }

            for (int i = 0; i < 4; i++)
            {
                this.cards.Add(new UnoCard((int)SpecialCards.ChangeColor, Colors.Black));
                this.cards.Add(new UnoCard((int)SpecialCards.ChangeColorPlusFour, Colors.Black));
            }
            this.cards.Shuffle();
        }

        public UnoCard GetNext()
        {
            UnoCard c = this.cards[0];
            this.cards.RemoveAt(0);
            return c;
        }

        public bool isEmpty()
        {
            if (this.cards.Count == 0)
                return true;
            else
                return false;
        }

        public void ReInit(UnoCard[] card)
        {
            this.cards.AddRange(card);
            this.cards.Shuffle();
        }
    }

    [Serializable]
    public class UnoCard : IEquatable<UnoCard>, IDisposable, ISerializable
    {
        private int number;
        private Colors color;

        public static UnoCard GiveCard = new UnoCard(20, Colors.Black);
        public static UnoCard StartRound = new UnoCard(21, Colors.Black);
        public static UnoCard EndRound = new UnoCard(22, Colors.Black);
        public static UnoCard Uno = new UnoCard(23, Colors.Black);
        public static UnoCard Won = new UnoCard(24, Colors.Black);

        public UnoCard(int num, Colors col)
        {
            this.number = num;
            this.color = col;
        }

        public void Dispose()
        {
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("number", this.number, typeof(int));
            info.AddValue("color", this.color, typeof(Colors));
        }

        public UnoCard(SerializationInfo info, StreamingContext context)
        {
            this.number = (int)info.GetValue("number", typeof(int));
            this.color = (Colors)info.GetValue("color", typeof(int));
        }

        public int Number
        {
            get { return this.number; }
            //set { this.number = value; }
        }

        public Colors Color
        {
            get { return this.color; }
            set 
            {
                if (this.color == Colors.Black)
                    this.color = value; 
            }
        }

        public bool Equals(UnoCard other)
        {
            if ((Object)other == null)
                return false;

            return (this.number == other.number && this.color == other.color);
        }

        public Image GetImage()
        {
            ResourceManager rm = UnoC.Properties.Resources.ResourceManager;
            string s = "";
            if (this.color == Colors.Black && this.number >= 13)
                s = this.color.ToString() + "_" + (this.number - 13).ToString();
            else
                s = this.color.ToString() + "_" + this.number.ToString();
            return (Bitmap)rm.GetObject(s);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            UnoCard UnoCardObj = obj as UnoCard;
            if ((Object)UnoCardObj == null)
                return false;
            else
                return (this.number == UnoCardObj.number && this.color == UnoCardObj.color);
        }

        public override int GetHashCode()
        {
            return this.number.GetHashCode() + this.color.GetHashCode();
        }

        public static bool operator ==(UnoCard card1, UnoCard card2)
        {
            if ((object)card1 == null || ((object)card2) == null)
                return Object.Equals(card1, card1);

            return card1.Equals(card2);
        }

        public static bool operator !=(UnoCard card1, UnoCard card2)
        {
            if (card1 == null || card2 == null)
                return !Object.Equals(card1, card2);

            return !(card1.Equals(card2));
        }
    }

    static class MyExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}