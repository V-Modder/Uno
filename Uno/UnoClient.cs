using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Nito.Async;
using Nito.Async.Sockets;
using UnoC;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

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
        private bool bEnableUI;
        private bool bFirstRound;
        private bool bHasCard;
        private int iStarted;
        private int iTakenCards;
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
            this.Icon = Uno.Properties.Resources.uno;
            this.iStarted = 0;
            this.iTakenCards = 0;
            this.isRunning = true;
            this.bHasEntered = false;
            this.playerName = PlayerName;
            this.bEnableUI = false;
            this.bFirstRound = true;
            this.bHasCard = false;
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
            this.Text += " - " + this.playerName;
            this.col = new System.Drawing.Color[10];
            this.col[0] = Color.LightBlue;
            this.col[1] = Color.LightYellow;
            this.col[2] = Color.LightCyan;
            this.col[3] = Color.LightGreen;
            this.col[4] = Color.LightSalmon;
            this.col[5] = Color.LightCoral;
            this.col[6] = Color.LightSeaGreen;
            this.col[7] = Color.LightGoldenrodYellow;
            this.col[8] = Color.LightCoral;
            this.col[9] = Color.Violet;
        }

        private void btn_endround_Click(object sender, EventArgs e)
        {
            client.WriteAsync(Util.Serialize(UnoCard.EndRound));
            bEnableUI = false;
            RefreshDisplay();
        }

        private void btn_recieve_Click(object sender, EventArgs e)
        {
            bHasCard = true;
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

        private void dgv_player_SelectionChanged(object sender, EventArgs e)
        {
            dgv_player.ClearSelection();
        }

        private void UnoCard_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && bEnableUI)
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
            if (e.Button == System.Windows.Forms.MouseButtons.Left && bEnableUI)
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
                    if (lbl_warnplustwo.Visible && cards[i].Number == (int)UnoC.SpecialCards.TakeTwo && i < cards.Count - iTakenCards)
                    {
                        client.WriteAsync(Util.Serialize(UnoCard.PlusTwoBack));
                        for (int ii = cards.Count - 1; ii >= cards.Count - iTakenCards; ii--)
                        {
                            client.WriteAsync(Util.Serialize(cards[ii]));
                            cards.RemoveAt(i);
                            this.Controls.Remove(pictures[i]);
                            pictures.RemoveAt(i);
                        }
                        client.WriteAsync(Util.Serialize(UnoCard.EndRound));
                    }
                    bEnableUI = false;
                    stack = cards[i];
                    if (stack.Color == Colors.Black)
                    {
                        Uno.ChooseBox cb = new Uno.ChooseBox();
                        cb.ShowDialog();
                        stack.Color = cb.Choose;
                    }
                    cards.RemoveAt(i);
                    this.Controls.Remove(p);
                    pictures.Remove(p);
                    
                    #if !DEBUG
                    client.WriteAsync(Util.Serialize(stack));
                    #endif
                    lbl_warnplustwo.Visible = false;
                }
                //ResetCards(p);
                RefreshDisplay();
            }

        }

        private void UnoCard_MouseEnter(object sender, EventArgs e)
        {
            if (bEnableUI)
            {
                PictureBox p = (PictureBox)sender;
                p.Location = new Point(p.Location.X - 10, p.Location.Y - 10);
                p.Size = new Size(120, 162);
                p.BringToFront();
            }
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
                    bHasCard = false;
                    if (!bFirstRound)
                        bEnableUI = true;
                    else
                        bFirstRound = false;
                    if (stack.Number == (int)UnoC.SpecialCards.TakeTwo)
                        lbl_warnplustwo.Visible = true;
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
                            {
                                iTakenCards++;
                                cards.Add(msgC);
                            }
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
                                btn_start.Visible = false;
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
                            for (int i = 0; i < players.Count; i++)
                                if (players[i].Name == this.playerName && players[i].Cards == this.cards.Count)
                                    players[i].Cards++;
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
            dgv_player.Rows.Clear();
            for (int i = 0; i < players.Count;i++)
            {
                dgv_player.Rows.Add(players[i].Name, players[i].Cards.ToString());
                if (players[i].Cards == 1)
                    dgv_player.Rows[i].DefaultCellStyle.ForeColor = Color.Red;

                dgv_player.Rows[i].DefaultCellStyle.BackColor = col[i];

                if (players[i].Name == this.playerName && players[i].Cards == this.cards.Count)
                    dgv_player.Rows[i].Cells[0].Style.Font = new Font(dgv_player.Font, FontStyle.Bold | FontStyle.Underline);
            }
            if (stack.Number < (int)UnoC.SpecialCards.ChangeColor)
                pcb_stack.Image = stack.GetImage();
            else
            {
                Color ColorChoose = Color.Black;
                UnoCard black = new UnoCard(stack.Number, Colors.Black);
                switch (stack.Color)
                {
                    case Colors.Blue:
                        ColorChoose = ToColor(0xFF5555FF);
                        break;
                    case Colors.Green:
                        ColorChoose = ToColor(0xFF00AA00);
                        break;
                    case Colors.Red:
                        ColorChoose = ToColor(0xFFFF5555);
                        break;
                    case Colors.Yellow:
                        ColorChoose = ToColor(0xFFFFAA00);
                        break;
                }
                Bitmap b = (Bitmap)black.GetImage();
                FloodFill(b, 45, 100, ColorChoose);
                FloodFill(b, 135, 300, ColorChoose);
                pcb_stack.Image = b;
            }
            
            pcb_green.Visible = bEnableUI;
            btn_recieve.Enabled = bEnableUI && !bHasCard;
            btn_endround.Enabled = bEnableUI;
            this.Refresh();
        }

        private Color ToColor(uint argb)
        {
            return Color.FromArgb((byte)((argb & -16777216) >> 0x18),
                                  (byte)((argb & 0xff0000) >> 0x10),
                                  (byte)((argb & 0xff00) >> 8),
                                  (byte)(argb & 0xff));
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
            if (stack.Number < (int)UnoC.SpecialCards.ChangeColor)
                pcb_stack.Image = stack.GetImage();
            else
            {
                Color ColorChoose = Color.Black;
                UnoCard black = new UnoCard(stack.Number, Colors.Black);
                switch (stack.Color)
                {
                    case Colors.Blue:
                        ColorChoose = ToColor(0xFF5555FF);
                        break;
                    case Colors.Green:
                        ColorChoose = ToColor(0xFF00AA00);
                        break;
                    case Colors.Red:
                        ColorChoose = ToColor(0xFFFF5555);
                        break;
                    case Colors.Yellow:
                        ColorChoose = ToColor(0xFFFFAA00);
                        break;
                }
                Bitmap b = (Bitmap)black.GetImage();
                FloodFill(b, 45, 100, ColorChoose);
                FloodFill(b, 135, 300, ColorChoose);
                pcb_stack.Image = b;
            }
        }

        void FloodFill(Bitmap bitmap, int x, int y, Color color)
        {
            BitmapData data = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
            int[] bits = new int[data.Stride / 4 * data.Height];
            Marshal.Copy(data.Scan0, bits, 0, bits.Length);

            LinkedList<Point> check = new LinkedList<Point>();
            int floodTo = color.ToArgb();
            int floodFrom = bits[x + y * data.Stride / 4];
            bits[x + y * data.Stride / 4] = floodTo;

            if (floodFrom != floodTo)
            {
                check.AddLast(new Point(x, y));
                while (check.Count > 0)
                {
                    Point cur = check.First.Value;
                    check.RemoveFirst();

                    foreach (Point off in new Point[] {
                new Point(0, -1), new Point(0, 1), 
                new Point(-1, 0), new Point(1, 0)})
                    {
                        Point next = new Point(cur.X + off.X, cur.Y + off.Y);
                        if (next.X >= 0 && next.Y >= 0 &&
                            next.X < data.Width &&
                            next.Y < data.Height)
                        {
                            if (bits[next.X + next.Y * data.Stride / 4] == floodFrom)
                            {
                                check.AddLast(next);
                                bits[next.X + next.Y * data.Stride / 4] = floodTo;
                            }
                        }
                    }
                }
            }
            Marshal.Copy(bits, 0, data.Scan0, bits.Length);
            bitmap.UnlockBits(data);
        }

        public bool IsRunning
        {
            get { return isRunning; }
        }
        #endregion
    }
}
