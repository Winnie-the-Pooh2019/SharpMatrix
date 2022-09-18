using System;
using System.Text.RegularExpressions;

namespace Matrix
{
    public partial class Form1 : Form
    {
        private TextBox[,] textBoxes;
        private Button randomButton;
        private Button mix;
        private Matrix matrix;

        public Form1()
        {
            InitializeComponent();

            textBoxes = new TextBox[5, 5];
            randomButton = this.Controls.Find("random", true)[0] as Button;
            mix = this.Controls.Find("mixButton", true)[0] as Button;
            matrix = new Matrix(5, 5);

            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                {
                    textBoxes[i, j] = this.Controls.Find($"textBox{i * 5 + j + 1}", true)[0] as TextBox;
                }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void RandomPush(object sender, EventArgs e)
        {
            matrix.GenerateArray();
            clear();
            
            for (int i = 0; i < matrix.Array.GetLength(0); i++)
                for (int j = 0; j < matrix.Array.GetLength(1); j++)
                    textBoxes[i, j].Text = matrix.Array[i, j].ToString();
        }

        private bool checkFields()
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (!Regex.IsMatch(textBoxes[i, j].Text, @"^\d+$"))
                        return false;

            return true;
        }

        private void clear()
        {
            for (int i = 0; i < 5; i++)
                for (int j = 0; j < 5; j++)
                    if (i + j + 1 != 5)
                        textBoxes[i, j].BackColor = Color.White;
        }

        private void SwapNumbers(object sender, EventArgs e)
        {
            if (!checkFields())
            {
                MessageBox.Show("Incorrect input format");
                return;
            }
            clear();

            (int h1, int w1) = matrix.FindMaxLeft();
            (int h2, int w2) = matrix.FindMaxRight();

            textBoxes[h1, w1].BackColor = Color.YellowGreen;
            textBoxes[h2, w2].BackColor = Color.YellowGreen;

            (matrix.Array[h1, w1], matrix.Array[h2, w2]) = (matrix.Array[h2, w2], matrix.Array[h1, w1]);

            (textBoxes[h2, w2].Text, textBoxes[h1, w1].Text) = (textBoxes[h1, w1].Text, textBoxes[h2, w2].Text);
        }
    }
    class Matrix
    {
        public int[,] Array { get; private set; }

        public Matrix(int height, int width)
        {
            Array = new int[height, width];
        }

        public (int, int) FindMaxLeft()
        {
            (int maxh, int maxw) = (0, 0);

            for (int i = 0; i < Array.GetLength(0) - 1; i++)
            {
                for (int j = 0; j < Array.GetLength(1) - i - 1; j++)
                {
                    // System.Console.Write(Array[i, j] + " ");
                    if (Array[i, j].CompareTo(Array[maxh, maxw]) == 1)
                    {
                        maxh = i;
                        maxw = j;
                    }
                }
                // System.Console.WriteLine();
            }

            return (maxh, maxw);
        }

        public (int, int) FindMaxRight()
        {
            (int maxh, int maxw) = (1, Array.GetLength(1) - 1);

            for (int i = 1; i < Array.GetLength(0); i++)
            {
                for (int j = Array.GetLength(1) - 1; j > Array.GetLength(1) - i - 1; j--)
                {
                    if (Array[i, j].CompareTo(Array[maxh, maxw]) == 1)
                    {
                        maxh = i;
                        maxw = j;
                    }
                }
            }

            return (maxh, maxw);
        }

        public void GenerateArray()
        {
            var rnd = new Random();

            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    Array[i, j] = rnd.Next() % 100;
                }
            }
        }

        public override string ToString()
        {
            var sb = new System.Text.StringBuilder();

            for (int i = 0; i < Array.GetLength(0); i++)
            {
                for (int j = 0; j < Array.GetLength(1); j++)
                {
                    sb.Append(Array[i, j] + " ");
                }
                sb.Append("\n");
            }

            return sb.ToString();
        }
    }
}