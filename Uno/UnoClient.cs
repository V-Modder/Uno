using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Nito.Async;
using Nito.Async.Sockets;
using UnoC;

namespace UnoClient
{
    public partial class UnoClient : Form
    {
        #region Class-Variables
        #if !DEBUG
        private SimpleClientTcpSocket client;
        #endif
        private bool isRunning;
        private bool bIsAdmin;
        private bool bHasEntered;
        private int iStarted = 0;
        private string playerName;
        private UnoCard stack;
        private List<UnoCard> cards;
        private List<UnoPlayer> players;
        private List<PictureBox> pictures;
        private PictureBox pcb_stack;
        private Point MouseDownLocation;
        private System.Drawing.Color[] col;
        #endregion

        #region Form-Control
        public UnoClient(string Address, string PlayerName, bool IsAdmin=false)
        {
            InitializeComponent();
            this.isRunning = true;
            this.bHasEntered = false;
            this.playerName = PlayerName;
            #if !DEBUG
            this.client = new SimpleClientTcpSocket();
            this.client.PacketArrived += new Action<AsyncResultEventArgs<byte[]>>(client_PacketArrived);
            this.client.ShutdownCompleted += new Action<AsyncCompletedEventArgs>(client_ShutdownCompleted);
            this.client.ConnectCompleted += new Action<AsyncCompletedEventArgs>(client_ConnectCompleted);
            this.client.ConnectAsync(System.Net.IPAddress.Parse(Address), 3456);
            #endif
            this.bIsAdmin = IsAdmin;
            this.cards = new List<UnoCard>();
            this.players = new List<UnoPlayer>();
            this.pictures = new List<PictureBox>();
            col = new System.Drawing.Color[10];
            col[0] = Color.LightBlue;
            col[1] = Color.LightYellow;
            col[2] = Color.LightCyan;
            col[3] = Color.LightGreen;
            col[4] = Color.LightSalmon;
            col[5] = Color.LightCoral;
            col[6] = Color.LightSeaGreen;
            col[7] = Color.LightGoldenrodYellow;
            col[8] = Color.LightCoral;
            col[9] = Color.Violet;
        }

        private void btn_recieve_Click(object sender, EventArgs e)
        {
            iStarted = 3;
            client.WriteAsync(Util.Serialize(UnoCard.GiveCard));
            btn_recieve.Enabled = false;
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            #if !DEBUG
            client.WriteAsync(Util.Serialize(UnoCard.StartRound));
            #endif
            btn_start.Visible = false;
        }

        private void UnoCard_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                PictureBox p = (PictureBox)sender;
                p.Location = new Point(p.Location.X + 10, p.Location.Y + 10);
                p.Size = new Size(100, 142);
                MouseDownLocation = e.Location;
            }
        }

        private void UnoCard_MouseMove(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                p.Left = e.X + p.Left - MouseDownLocation.X;
                p.Top = e.Y + p.Top - MouseDownLocation.Y;
                if (this.PointToClient(System.Windows.Forms.Control.MousePosition).X >= pcb_stack.Location.X &&
                    this.PointToClient(System.Windows.Forms.Control.MousePosition).X < (pcb_stack.Location.X + pcb_stack.Size.Width) &&
                    this.PointToClient(System.Windows.Forms.Control.MousePosition).Y >= pcb_stack.Location.Y &&
                    this.PointToClient(System.Windows.Forms.Control.MousePosition).Y < (pcb_stack.Location.Y + pcb_stack.Size.Height) &&
                    !this.bHasEntered)
                {
                    bHasEntered = true;
                    Point delta = new Point();
                    delta.X = pcb_stack.Location.X - p.Location.X;
                    delta.Y = pcb_stack.Location.Y - p.Location.Y;
                    Cursor.Position = new Point(Cursor.Position.X + delta.X, Cursor.Position.Y + delta.Y);
                    this.Refresh();
                }
                else
                    bHasEntered = false;
            }
        }

        private void UnoCard_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (p.Location != pcb_stack.Location)
            {
                ResetCards(p);
            }
            else
            {
                int i = pictures.IndexOf(p);
                if (cards[i].Color == stack.Color || cards[i].Number == stack.Number || cards[i].Color == Colors.Black)
                {
                    stack = cards[i];
                    cards.RemoveAt(i);
                    this.Controls.Remove(p);
                    pictures.Remove(p);
                    if (stack.Color == Colors.Black)
                    {
                        Uno.ChooseBox cb = new Uno.ChooseBox();
                        cb.ShowDialog();
                        stack.Color = cb.Choose;
                    }
                    #if !DEBUG
                    client.WriteAsync(Util.Serialize(stack));
                    #endif
                }
                ResetCards(p);
            }

        }

        private void UnoCard_MouseEnter(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            p.Location = new Point(p.Location.X - 10, p.Location.Y - 10);
            p.Size = new Size(120, 162);
            p.BringToFront();
        }

        private void UnoCard_MouseLeave(object sender, EventArgs e)
        {
            PictureBox p = (PictureBox)sender;
            if (p.Location != pcb_stack.Location)
            {
                p.Location = new Point(p.Location.X + 10, p.Location.Y + 10);
                p.Size = new Size(100, 142);
                ResetCards((PictureBox)sender);
            }
        }

        private void UnoClient_Load(object sender, EventArgs e)
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            pcb_stack = new PictureBox();
            pcb_stack.Location = new Point(12, 170);
            pcb_stack.Size = new Size(94, 142);
            pcb_stack.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(pcb_stack);

            btn_start.Visible = bIsAdmin;

            #if DEBUG
            UnoCards c = new UnoCards();
            cards.Add(c.GetNext());
            cards.Add(c.GetNext());
            cards.Add(c.GetNext());
            cards.Add(c.GetNext());
            stack = c.GetNext();
            pcb_stack.Image = stack.GetImage();
            RefreshDisplay();
            #endif
        }

        private void UnoClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            isRunning = false;
            #if !DEBUG
            client.AbortiveClose();
            #endif
            e.Cancel = false;
        }
        #endregion

        #region Network-Control
        private void client_ConnectCompleted(AsyncCompletedEventArgs e)
        {
            try
            {
                client.WriteAsync(Util.Serialize(new UnoPlayer(this.playerName)));
            }
            catch (Exception ex)
            {
                int i = 0;
            }
        }

        private void client_PacketArrived(AsyncResultEventArgs<byte[]> e)
        {
            switch (iStarted)
            {
                case 0:
                    iStarted = 1;
                    stack = (UnoCard)Util.Deserialize(e.Result);
                    break;
                case 1:
                    using (UnoCard msgC = (UnoCard)Util.Deserialize(e.Result))
                    {
                        if (msgC != null)
                        {
                            if (msgC == UnoCard.EndRound)
                            {
                                players.Clear();
                                iStarted = 2;
                            }
                            else
                                cards.Add(msgC);
                        }
                    }
                    break;
                case 2:
                    using (UnoPlayer msgP = (UnoPlayer)Util.Deserialize(e.Result))
                    {
                        if (msgP != null)
                        {
                            if (msgP == UnoPlayer.EndMessage)
                            {
                                iStarted = 0;
                                RefreshDisplay();
                                btn_recieve.Enabled = true;
                            }
                            else
                                players.Add(msgP);
                        }
                    }
                    break;
                case 3:
                    using (UnoCard msgC = (UnoCard)Util.Deserialize(e.Result))
                    {
                        if (msgC != null)
                        {
                            cards.Add(msgC);
                            RefreshDisplay();
                        }
                    }
                    iStarted = 0;
                    break;
            }
        }

        private void client_ShutdownCompleted(AsyncCompletedEventArgs e)
        {
        }
        #endregion

        #region Class-Methods
        private void RefreshDisplay()
        {
            int y = pictures.Count;
            for (int x = y; x < cards.Count; x++)
            {
                PictureBox p = new PictureBox();
                p.Location = new Point(((y + x) * 35) + 12, 12);
                p.Size = new Size(94, 142);
                p.Image = cards[x].GetImage();
                p.SizeMode = PictureBoxSizeMode.StretchImage;
                p.MouseDown += new MouseEventHandler(UnoCard_MouseDown);
                p.MouseMove += new MouseEventHandler(UnoCard_MouseMove);
                p.MouseUp += new MouseEventHandler(UnoCard_MouseUp);
                p.MouseEnter += new EventHandler(UnoCard_MouseEnter);
                p.MouseLeave += new EventHandler(UnoCard_MouseLeave);
                this.Controls.Add(p);
                p.BringToFront();
                pictures.Add(p);
            }
            ResetCards(pictures[0]);
            txt_players.Text = "";
            for (int i = 0; i < players.Count;i++)
            {
                if (players[i].Cards == 1)
                    txt_players.AppendText((players[i].Name + "\t" + players[i].Cards.ToString()).PadRight(50, ' ') + Environment.NewLine, Color.Red);
                else
                    txt_players.AppendText((players[i].Name + "\t" + players[i].Cards.ToString()).PadRight(50, ' ') + Environment.NewLine, Color.Black);
                txt_players.Select(txt_players.GetFirstCharIndexFromLine(i), txt_players.Lines[i].Length);
                txt_players.SelectionBackColor = col[i];
                txt_players.Select(txt_players.GetFirstCharIndexFromLine(i), players[i].Name.Length);
                if (players[i].Name == this.playerName && players[i].Cards == this.cards.Count)
                    txt_players.SelectionFont = new Font(txt_players.SelectionFont.FontFamily, txt_players.SelectionFont.Size, FontStyle.Underline | FontStyle.Bold);
                txt_players.ScrollToCaret();
            }
            txt_players.Select(0, 0);
            this.Refresh();
        }

        private void ResetCards(PictureBox index)
        {
            int startindex = 0;
            if (pictures.IndexOf(index) >= 0)
                startindex = pictures.IndexOf(index);
            for (int i = startindex; i < pictures.Count; i++)
            {
                pictures[i].Location = new Point((i * 35) + 12, 12);
                pictures[i].BringToFront();
            }
            pcb_stack.Image = stack.GetImage();
        }

        public bool IsRunning
        {
            get { return isRunning; }
        }
        #endregion
    }
}
