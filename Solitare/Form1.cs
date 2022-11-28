using System.Collections.Generic;
using System.Media;

namespace Solitare
{
    public partial class Form1 : Form
    {
        private static String[] suits = { "hearts", "diamonds", "spades", "clubs" };
        private static String[] ranks = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };

        private static List<String> finalClubs = new List<string>();
        private static List<String> finalSpades = new List<string>();
        private static List<String> finalHearts = new List<string>();
        private static List<String> finalDiamonds = new List<string>();
        private static List<String>[] finalList = new List<String>[] { finalClubs, finalSpades, finalHearts, finalDiamonds };

        private Deck d = new Deck(ranks, suits);
        private static List<Card> stock = new List<Card>();
        public static List<Card> row0 = new List<Card>();
        private static List<Card> row1 = new List<Card>();
        private static List<Card> row2 = new List<Card>();
        private static List<Card> row3 = new List<Card>();
        private static List<Card> row4 = new List<Card>();
        private static List<Card> row5 = new List<Card>();
        private static List<Card> row6 = new List<Card>();
        private static List<Card> row7 = new List<Card>();

        public static List<Card>[] master = new List<Card>[] { row1, row2, row3, row4, row5, row6, row7, row0 };

        private PictureBox stockBox = new PictureBox();
        private static PictureBox final1 = new PictureBox(); static PictureBox final2 = new PictureBox();
        private static PictureBox final3 = new PictureBox(); static PictureBox final4 = new PictureBox();
        private static Label label = new Label();
        public static PictureBox[] finalArr = { final1, final2, final3, final4 };
        public Form1() {
            InitializeComponent();
            d.shuffle();
        }
        private void Form1_Load(object sender, EventArgs e) {
            setTab();
            setStock();
            stockB.Image = Image.FromFile(@"../cards/back1.JPG");
            stockB.Click += StockBox_Click;
            label.Hide(); label.Location = new Point(700, 700); label.Text = "Congrats! You have beaten solitare click this to restart the game!";
            label.Height = 200; label.Width = 200; label.Click += Label_Click;

        }
        private void Label_Click(object? sender, EventArgs e)
        {
            Application.Restart();
        }
        private void StockBox_Click(object? sender, EventArgs e) {
            SoundPlayer simpleSound = new SoundPlayer(@"../cards/flip.wav");
            simpleSound.Play();
            if (stock.Count > 0)
            {
                for (int i = 0; i < Math.Min(3, stock.Count); i++)
                {
                    row0.Add(stock[0]);
                    row0[i].getPictureBox().Hide();
                    stock.RemoveAt(0);
                }
            }
            else {
                for (int i = 0; i < row0.Count; i++)
                { stock.Add(row0[i]); }
                row0.Clear();
                for (int i = 0; i < Math.Min(3, stock.Count); i++) {
                    row0.Add(stock[0]);
                    row0[i].getPictureBox().Hide();
                    stock.RemoveAt(0);
                }
            }

            refresh();
        }
        private void setTab() {
            Card card;
            int i = 1;
            int x = 320;
            int y = 30;

            foreach (List<Card> arr in master) {
                if (arr != row0)
                {
                    for (int numIterations = 0; numIterations < i; numIterations++)
                    {
                        card = d.deal();

                        arr.Add(card);
                        card.setIndex(i - 1);
                        PictureBox picBox = card.getPictureBox();
                        this.Controls.Add(picBox);

                        picBox.Location = new Point(x, y + (numIterations * 35));
                        picBox.BringToFront();
                        picBox.Show();
                    }
                    arr[arr.Count - 1].setToFace();
                    arr[arr.Count - 1].Moveable();
                    Card.pointArr.Add(new Point(x, y));
                    x += 165; i++;
                }
            }
            i = 0;
            foreach (List<String> str in finalList) {
                str.Add(suits[i]);
                foreach (String strRank in ranks) {
                    str.Add(strRank);
                }
                i++;
            }
        }
        public static void refresh() {
            int iterations = Math.Min(row0.Count, 3);
            for (int a = 0; a < row0.Count; a++)
                row0[a].notMoveable();
            for (int i = 0; i < iterations; i++) {
                int index = row0.Count - iterations + i;
                row0[index].getPictureBox().Location = new Point(15, 200 + (i * 35));
                row0[index].getPictureBox().BringToFront();
                row0[index].getPictureBox().Show();
                row0[index].getPictureBox().Refresh();
            }
            if (row0.Count != 0) { row0[row0.Count - 1].Moveable(); }

        }
        private void setStock() {
            row0.Clear();
            while (d.getSize() > 0) {
                Card c = d.deal();
                stock.Add(c);
                c.setToFace();
                c.setIndex(7);
                this.Controls.Add(c.getPictureBox());
            }
            stockBox.Image = Image.FromFile("../cards/back1.JPG");
            stockBox.Width = 150;
            stockBox.Height = 192;
            stockBox.Location = new Point(15, 15);
            for (int i = 0; i < finalArr.Length; i++)
            {
                finalArr[i].Height = 190;
                finalArr[i].Width = 130;
                finalArr[i].Location = new Point(1605, 30 + (200 * i));
                if (i == 0) { finalArr[0].Image = Image.FromFile("../cards/heartsCard.JPG"); }
                if (i == 1) { finalArr[1].Image = Image.FromFile("../cards/diamondsCard.JPG"); }
                if (i == 2) { finalArr[2].Image = Image.FromFile("../cards/spadesCard.JPG"); }
                if (i == 3) { finalArr[3].Image = Image.FromFile("../cards/clubsCard.JPG"); }
                this.Controls.Add(finalArr[i]);
            }
        }
        public static void pile(Card c) {
            for (int i = 0; i < finalList.Length; i++) {
                Boolean compat = finalList[i][0].Equals(c.getSuit()) && c.getRank().Equals(finalList[i][1]);
                if (compat)
                {
                    finalList[i].RemoveAt(1);
                    finalArr[i].Image = Image.FromFile(c.getFilePath());
                }
            }
            if (finalClubs.Count == 1 && finalHearts.Count == 1 && finalSpades.Count == 1 && finalDiamonds.Count == 1) { label.Show(); }
        }
        private void label1_Click(object sender, EventArgs e) { Application.Restart(); }
    }
}