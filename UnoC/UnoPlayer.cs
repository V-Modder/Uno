using System;
using System.Security.Permissions;
using Nito.Async.Sockets;
using System.Runtime.Serialization;

namespace UnoC
{
    [Serializable]
    public class UnoPlayer : IDisposable, ISerializable
    {
        string name;
        int cards;
        bool hasLastCard;
        SimpleServerChildTcpSocket socket;

        public static UnoPlayer EndMessage = new UnoPlayer("End");

        public UnoPlayer(SimpleServerChildTcpSocket s)
        {
            socket = s;
            hasLastCard = false;
        }

        public UnoPlayer(string Name)
        {
            this.name = Name;
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
