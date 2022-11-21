namespace Solitare
{
    public partial class Form1 : Form
    {
        private static String[] suits = { "hearts", "diamonds", "spades", "clubs" };
        private static String[] ranks = { "ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "jack", "queen", "king" };

        private Deck d = new Deck(ranks, suits);
        private static List<Card> stock = new List<Card>();
        private static List<Card> row0 = new List<Card>();
        private static List<Card> row1 = new List<Card>();
        private static List<Card> row2 = new List<Card>();
        private static List<Card> row3 = new List<Card>();
        private static List<Card> row4 = new List<Card>();
        private static List<Card> row5 = new List<Card>();
        private static List<Card> row6 = new List<Card>();
        private static List<Card> row7 = new List<Card>();

        public static List<Card>[] master = new List<Card>[] { row1, row2, row3, row4, row5, row6, row7 };
        private PictureBox stockBox = new PictureBox();

        public Form1()
        {
            InitializeComponent();
            d.shuffle();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            setTab();
            setStock();

            Stock.Image = Image.FromFile(@"../cards/back1.JPG");
            stockBox.Click += StockBox_Click;

        }

        private void StockBox_Click(object? sender, EventArgs e)
        {
            for (int i = 0; i < Math.Min(3, stock.Count); i++)
            {
                row0.Add(stock[0]);
                stock.RemoveAt(0);
            }

            deal3();
        }
        private void setTab()
        {
            Card card;
            int i = 1;
            int x = 320;
            int y = 30;

            foreach (List<Card> arr in master)
            {
                for (int numIterations = 0; numIterations < i; numIterations++)
                {
                    card = d.deal();

                    arr.Add(card);
                    PictureBox picBox = card.getPictureBox();
                    this.Controls.Add(picBox);
                    picBox.Location = new Point(x, y + (numIterations * 35));
                    picBox.BringToFront();
                    picBox.Show();
                }
                arr[arr.Count - 1].setToFace();
                arr[arr.Count - 1].Moveable();
                x += 165;
                i++;
            }
        }
        private void deal3()
        {
            for (int i = 0; i < 3; i++)
            {
                this.Controls.Add(row0[row0.Count - 3 + i].getPictureBox());
                row0[row0.Count - 3 + i].getPictureBox().Location = new Point(15, 200 + (i * 35));
                row0[row0.Count - 3 + i].getPictureBox().BringToFront();

                row0[row0.Count - 3 + i].setToFace();

                row0[row0.Count - 3 + i].getPictureBox().Show();
                row0[row0.Count - 3 + i].getPictureBox().Refresh();

                if (i == 2) { row0[row0.Count - 3 + i].Moveable(); }
            }
        }
        private void setStock()
        {
            while (d.getSize() > 0)
            {
                Card c = d.deal();
                stock.Add(c);
                this.Controls.Add(c.getPictureBox());
            }
            stockBox.Image = Image.FromFile("../cards/back1.JPG");
            stockBox.Width = 130;
            stockBox.Height = 172;
            stockBox.Location = new Point(15, 15);
        }

    }
}