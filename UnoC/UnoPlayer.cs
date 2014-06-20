using System;
using Nito.Async.Sockets;
using System.Runtime.Serialization;

namespace UnoC
{
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

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Use the AddValue method to specify serialized values.
            info.AddValue("name", name, typeof(string));
            info.AddValue("cards", cards, typeof(int));
        }

        // The special constructor is used to deserialize values.
        public UnoPlayer(SerializationInfo info, StreamingContext context)
        {
            // Reset the property value using the GetValue method.
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
