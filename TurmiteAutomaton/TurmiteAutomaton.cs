namespace TurmiteAutomaton
{
    public partial class TurmiteAutomaton : Form
    {
        private const int size = 400;
        private Bitmap canvas = new Bitmap(size, size);
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        private int x = size / 2;
        private int y = size / 2;
        private int direction = 0; // 0: up, 1: right, 2: down, 3: left
        private int[,] grid = new int[size, size];

        public TurmiteAutomaton()
        {
            this.Text = "Тьюрмиты Автомат";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            this.Paint += new PaintEventHandler(Draw);
            timer.Interval = 100;
            timer.Tick += new EventHandler(Update);
            timer.Start();

            InitializeComponent();
        }

        private void Update(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++) // Speed up the drawing
            {
                int state = grid[x, y];
                switch (state)
                {
                    case 0:
                        grid[x, y] = 1;
                        direction = (direction + 1) % 4; // turn right
                        break;
                    case 1:
                        grid[x, y] = 0;
                        direction = (direction + 3) % 4; // turn left
                        break;
                }

                canvas.SetPixel(x, y, state == 0 ? Color.Black : Color.White);

                switch (direction)
                {
                    case 0: y--; break;
                    case 1: x++; break;
                    case 2: y++; break;
                    case 3: x--; break;
                }

                if (x < 0) x = size - 1;
                if (y < 0) y = size - 1;
                if (x >= size) x = 0;
                if (y >= size) y = 0;
            }
            this.Invalidate();
        }

        private void Draw(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(canvas, 0, 0, size, size);
        }
    }
}
