using Point = System.Drawing.Point;

namespace Solitare
{

    public class Card
    {

        private String suit;
        private String rank;
        private Boolean flipped;
        private String filePath;
        private PictureBox picBox = new PictureBox();
        private int xPos, yPos;
        private int originalX, originalY;

        public Card(String cardRank, String cardSuit)
        {
            rank = cardRank;
            suit = cardSuit;
            filePath = "../cards/" + rank + suit + ".JPG";

            flipped = false;
            ;
            picBox.Height = 172;
            picBox.Width = 130;
            picBox.Hide();

            picBox.Image = Image.FromFile(@"../cards/back1.JPG");

        }
        public Card() { }


        public PictureBox getPictureBox()
        {
            return picBox;
        }

        public String getFilePath()
        {
            return filePath;
        }
        public void Moveable()
        {
            picBox.MouseDown += PicBox_MouseDown;
            picBox.MouseMove += PicBox_MouseMove;
            picBox.MouseUp += PicBox_MouseUp;
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

        public Boolean isFlipped()
        {
            return flipped;
        }

        public void setFlip(Boolean param)
        {
            flipped = param;
        }

        public String toString()
        {
            return rank + " of " + suit;
        }

        Point dragPoint = Point.Empty;
        bool dragging = false;
        bool mouseDown = false;

        private void PicBox_MouseUp(object? sender, MouseEventArgs e)
        {
            dragging = false;
            if (Math.Abs(picBox.Location.X - 320) < 82 && Math.Abs(picBox.Location.Y - 140) < 150)
            {
                picBox.Location = new Point(320, Form1.master[0].ElementAt(Form1.master[0].Count() - 1).getPictureBox().Location.Y - 35);
            }
            else
            {
                picBox.Location = new Point(originalX, originalY);
            }

        }
        private void PicBox_MouseDown(object? sender, MouseEventArgs e)
        {
            originalX = picBox.Location.X;
            originalY = picBox.Location.Y;
            if (e.Button == MouseButtons.Left)
            {
                dragging = true;
                xPos = e.X;
                xPos = e.Y;

            }
        }
        private void PicBox_MouseMove(object? sender, MouseEventArgs e)
        {
            Control c = sender as Control;
            if (dragging && c != null)
            {
                c.Top = e.Y + c.Top - yPos;
                c.Left = e.X + c.Left - xPos;
            }
        }

    }

}