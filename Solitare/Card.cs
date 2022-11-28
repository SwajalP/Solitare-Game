using System.Media;
using Control = System.Windows.Forms.Control;
using Point = System.Drawing.Point;

namespace Solitare
{
    public class Card
    {
        public static List<Point> pointArr = new List<Point>();
        List<Card> appendArr = new List<Card>();
        private String suit, rank, filePath;
        private Boolean flipped;
        private PictureBox picBox = new PictureBox();
        private int xPos, yPos, originalX, originalY, listIndex;
        private Point dragPoint = Point.Empty;
        private bool dragging = false, mouseDown = false, isBlack;
        private int pointValue;
        public Card(String cardRank, String cardSuit)
        {
            rank = cardRank; suit = cardSuit;
            filePath = "../cards/" + rank + suit + ".JPG";

            flipped = false;
            picBox.Height = 172; picBox.Width = 130;
            picBox.Hide();

            picBox.Image = Image.FromFile(@"../cards/back1.JPG");

            if (rank.Equals("ace")) { pointValue = 1; }
            else if (rank.Equals("king")) { pointValue = 13; }
            else if (rank.Equals("queen")) { pointValue = 12; }
            else if (rank.Equals("jack")) { pointValue = 11; }
            else { pointValue = Int32.Parse(rank); }

            isBlack = suit.Equals("clubs") || suit.Equals("spades");
        }
        public Card() { }


        public PictureBox getPictureBox()
        {
            return picBox;
        }
        public int getPointValue()
        {
            return pointValue;
        }
        public String getFilePath()
        {
            return filePath;
        }

        public void values(String ra)
        {
            if (ra.Equals("ace")) { pointValue = 1; }
            else if (ra.Equals("king")) { pointValue = 13; }
            else if (ra.Equals("queen")) { pointValue = 12; }
            else if (ra.Equals("jack")) { pointValue = 11; }
            else { pointValue = Int32.Parse(rank); }
        }
        public void Moveable()
        {
            flipped = true;
            picBox.MouseDown += PicBox_MouseDown;
            picBox.MouseMove += PicBox_MouseMove;
            picBox.MouseUp += PicBox_MouseUp;
        }

        public void notMoveable()
        {
            picBox.MouseDown -= PicBox_MouseDown;
            picBox.MouseMove -= PicBox_MouseMove;
            picBox.MouseUp -= PicBox_MouseUp;
        }

        public void setToFace()
        {
            picBox.Image = Image.FromFile(filePath);
        }
        public void isVisible()
        {
            picBox.Visible = true;
            picBox.Show();
            picBox.Refresh();
        }

        public String getSuit()
        {
            return suit;
        }

        public String getRank()
        {
            return rank;
        }
        public int getIndex()
        {
            return listIndex;
        }

        public void setIndex(int i)
        {
            listIndex = i;
        }
        public Boolean isFlipped()
        {
            return flipped;
        }

        public void setFlip(Boolean param)
        {
            flipped = param;
        }
        private Boolean areCompatible(Card c)
        {
            return this.isBlack != c.isBlack && this.pointValue + 1 == c.pointValue;
        }

        private void PicBox_MouseUp(object? sender, MouseEventArgs e)
        {
            dragging = false; mouseDown = false;
            int x, y; Boolean radius = false;
            Boolean comparsion = true;
            for (int i = 0; i < Form1.finalArr.Length; i++)
            {
                radius = Math.Abs(picBox.Location.X - Form1.finalArr[i].Location.X) < 82 &&
                                Math.Abs(picBox.Location.Y - Form1.finalArr[i].Location.Y) < 140;
                if (appendArr.Count == 1 && radius)
                {
                    Form1.pile(this); Form1.master[this.listIndex].Remove(this); picBox.Hide();
                    if (this.listIndex != 7 && Form1.master[this.listIndex].Count - 1 > 0)
                    {
                        SoundPlayer simpleSound = new SoundPlayer(@"../cards/flip.wav");
                        simpleSound.Play();
                        Form1.master[this.listIndex][Form1.master[this.listIndex].Count - 1].Moveable();
                        Form1.master[this.listIndex][Form1.master[this.listIndex].Count - 1].setToFace();
                    }
                    Form1.refresh();
                }
            }
            for (int i = 0; i < pointArr.Count; i++)
            {
                x = pointArr[i].X; y = pointArr[i].Y;
                try
                {
                    radius = Math.Abs(picBox.Location.X - x) < 82 &&
                    Math.Abs(picBox.Location.Y - Form1.master[i][Form1.master[i].Count - 1].getPictureBox().Location.Y) < 140 && listIndex != i;
                    comparsion = radius && areCompatible(Form1.master[i][Form1.master[i].Count() - 1]) || radius && Form1.master[i].Count() < 1;
                }
                catch
                {
                    radius = Math.Abs(picBox.Location.X - x) < 82 &&
                    Math.Abs(picBox.Location.Y - 30) < 140 && listIndex != i;
                    comparsion = radius;
                }
                if (comparsion)
                {
                    foreach (Card card in appendArr)
                    {
                        Form1.refresh();
                        Form1.master[card.listIndex].Remove(card);
                        if (Form1.master[card.listIndex].Count > 0 && !Form1.master[card.listIndex][Form1.master[card.listIndex].Count - 1].isFlipped())
                        {
                            SoundPlayer simpleSound = new SoundPlayer(@"../cards/flip.wav");
                            simpleSound.Play();
                            Form1.master[card.listIndex][Form1.master[card.listIndex].Count - 1].Moveable();
                            Form1.master[card.listIndex][Form1.master[card.listIndex].Count - 1].setToFace();
                        }
                        if (Form1.master[i].Count > 0)
                        {
                            card.getPictureBox().Location = new Point(x, Form1.master[i][Form1.master[i].Count() - 1].getPictureBox().Location.Y + 35);
                        }
                        else { card.getPictureBox().Location = new Point(x, 30); }
                        card.getPictureBox().BringToFront();
                        card.setIndex(i);
                        Form1.master[i].Add(card);
                    }
                    Form1.refresh();

                    break;
                }
            }
            if (!radius || !comparsion)
            {
                picBox.Location = new Point(originalX, originalY);
                for (int i = 0; i < appendArr.Count; i++)
                {
                    appendArr[i].getPictureBox().Location = new Point(originalX, originalY + (35 * i));
                }
            }
            appendArr.Clear();
        }
        private void PicBox_MouseDown(object? sender, MouseEventArgs e)
        {
            originalX = picBox.Location.X;
            originalY = picBox.Location.Y;
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                xPos = e.X;
                yPos = e.Y;
            }
        }
        private void PicBox_MouseMove(object? sender, MouseEventArgs e)
        {
            Control c = sender as Control;
            if (dragging && c != null)
            {
                c.Top = e.Y + c.Top - yPos;
                c.Left = e.X + c.Left - xPos;

                int index = Form1.master[listIndex].IndexOf(this);
                for (int i = index; i < Form1.master[listIndex].Count && i != -1; i++)
                {
                    if (appendArr.IndexOf(Form1.master[listIndex][i]) < 0)
                    {
                        appendArr.Add(Form1.master[listIndex][i]);
                    }
                }
                for (int i = 1; i < appendArr.Count; i++)
                {
                    appendArr[i].getPictureBox().Location = new Point(appendArr[0].getPictureBox().Location.X,
                        appendArr[0].getPictureBox().Location.Y + (i * 35));
                }
            }
        }
    }
}
    