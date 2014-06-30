using System;
using System.Security.Permissions;
using Nito.Async.Sockets;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Security.Cryptography;

namespace UnoC
{
    [Serializable]
    public class UnoPlayer : IEquatable<UnoPlayer>, IDisposable, ISerializable
    {
        string name;
        int cards;
        bool hasLastCard;
        SimpleServerChildTcpSocket socket;

        public static UnoPlayer EndMessage = new UnoPlayer("End", 9999);

        public UnoPlayer(SimpleServerChildTcpSocket s)
        {
            socket = s;
            hasLastCard = false;
        }

        public UnoPlayer(string Name, int Cards=0)
        {
            this.name = Name;
            this.cards = Cards;
        }

        public void Dispose()
        {
        }

        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", name, typeof(string));
            info.AddValue("cards", cards, typeof(int));
        }

        public UnoPlayer(SerializationInfo info, StreamingContext context)
        {
            name = (string)info.GetValue("name", typeof(string));
            cards = (int)info.GetValue("cards", typeof(int));
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        
        public int Cards
        {
            get { return cards; }
            set { cards = value; }
        }

        public bool Equals(UnoPlayer other)
        {
            if ((Object)other == null)
                return false;

            return (this.name == other.name && this.cards == other.cards);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            UnoPlayer UnoCPlayerObj = obj as UnoPlayer;
            if ((Object)UnoCPlayerObj == null)
                return false;
            else
                return (this.name == UnoCPlayerObj.name && this.cards == UnoCPlayerObj.cards);
        }

        public static bool operator ==(UnoPlayer a, UnoPlayer b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            // Return true if the fields match:
            return (a.name == b.name && a.cards == b.cards);
        }

        public static bool operator !=(UnoPlayer a, UnoPlayer b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return this.name.GetHashCode();
        }

        public SimpleServerChildTcpSocket Socket
        {
            get { return socket; }
            set { socket = value; }
        }

        public bool HasLastCard
        {
            get { return hasLastCard; }
            set { hasLastCard = value; }
        }
    }
}
