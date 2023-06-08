using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Sudoku
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        //__________________________________________________________________

        public bool isDone(int[,] board, int row, int col, int num)
        {
            //check if the number is unique within the rows 
            for (int i = 0; i < board.GetLength(0); i++)
                if (board[row, i] == num) // If the number occurs return false
                    return false;

            //check if the number is unique within the columns 
            for (int i = 0; i < board.GetLength(0); i++)
                if (board[i, col] == num) // If the number occurs return false
                    return false;

            //check if there is a match within the box (3x3).
            int squareroot = (int)Math.Sqrt(board.GetLength(0));
            int rowStart = row - row % squareroot;
            int colStart = col - col % squareroot;

            for (int i = rowStart; i < rowStart + squareroot; i++)
                for (int j = colStart; j < colStart + squareroot; j++)
                    if (board[i, j] == num)
                        return false; //not 'safe'

            // if there is no match, it is 'safe'
            return true;
        }
        //__________________________________________________________________
        public bool solve(int[,] board, int n)
        {
            int row = 0;
            int col = 0;

            bool isEmpty = true; //box is empty by default

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++) //two for loops to 'scan' the whole board
                {
                    if (board[i, j] == 0) //check if the box is already filled in
                    {
                        row = i; //will be needed later for check
                        col = j; //will be needed later for check

                        // Still empty squares in the game.

                        isEmpty = false; //box is no longer empty
                        break;
                    }
                }
                if (!isEmpty) //if the box is already filled in proceed to the next box
                {
                    break;
                }
            }

            // completely resolved
            if (isEmpty) //if the suduku is completely resolved return true
            {
                return true;
            }


            for (int num = 1; num <= n; num++)
            {
                if (isDone(board, row, col, num)) //check if 'safe'
                {
                    
                    board[row, col] = num; //finally fill in the box
                    
                    if (solve(board, n)) //if the board is completely resolved return true (use the function recursively)
                    {
                        listBox1.Items.Add("Feld[" + row + "," + col + "] : " + num);
                        return true;
                        
                    }
                    else
                    {
                        board[row, col] = 0; // not good, replace.

                    }
                }
                
            }
        
            return false;
        }
        //__________________________________________________________________

        public void printSolving(int[,] board, int N)
        {
            listBox1.Items.Add("Anzeigetafel.....");
            //Print solution on screen.
            System.Windows.Forms.TextBox[,] sudokubtn2 = new System.Windows.Forms.TextBox[9, 9]
            { { Cube19, Cube29, Cube39, Cube49, Cube59, Cube69, Cube79, Cube89, Cube99 }
              ,{ Cube18, Cube28, Cube38, Cube48, Cube58, Cube68, Cube78, Cube88, Cube98 }
              ,{ Cube17, Cube27, Cube37, Cube47, Cube57, Cube67, Cube77, Cube87, Cube97 }
              ,{ Cube16, Cube26, Cube36, Cube46, Cube56, Cube66, Cube76, Cube86, Cube96 }
              ,{ Cube15, Cube25, Cube35, Cube45, Cube55, Cube65, Cube75, Cube85, Cube95 }
              ,{ Cube14, Cube24, Cube34, Cube44, Cube54, Cube64, Cube74, Cube84, Cube94 }
              ,{ Cube13, Cube23, Cube33, Cube43, Cube53, Cube63, Cube73, Cube83, Cube93 }
              ,{ Cube12, Cube22, Cube32, Cube42, Cube52, Cube62, Cube72, Cube82, Cube92 }
              ,{ Cube11, Cube21, Cube31, Cube41, Cube51, Cube61, Cube71, Cube81, Cube91 }
            };
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    sudokubtn2[i, j].Text = board[i, j].ToString();
                }
            }
        }

        //__________________________________________________________________


        private void textboxes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Add("Anfang...");
                System.Windows.Forms.TextBox[,] sudokubtn2 = new System.Windows.Forms.TextBox[9, 9]
                { { Cube19, Cube29, Cube39, Cube49, Cube59, Cube69, Cube79, Cube89, Cube99 }
                  ,{ Cube18, Cube28, Cube38, Cube48, Cube58, Cube68, Cube78, Cube88, Cube98 }
                  ,{ Cube17, Cube27, Cube37, Cube47, Cube57, Cube67, Cube77, Cube87, Cube97 }
                  ,{ Cube16, Cube26, Cube36, Cube46, Cube56, Cube66, Cube76, Cube86, Cube96 }
                  ,{ Cube15, Cube25, Cube35, Cube45, Cube55, Cube65, Cube75, Cube85, Cube95 }
                  ,{ Cube14, Cube24, Cube34, Cube44, Cube54, Cube64, Cube74, Cube84, Cube94 }
                  ,{ Cube13, Cube23, Cube33, Cube43, Cube53, Cube63, Cube73, Cube83, Cube93 }
                  ,{ Cube12, Cube22, Cube32, Cube42, Cube52, Cube62, Cube72, Cube82, Cube92 }
                  ,{ Cube11, Cube21, Cube31, Cube41, Cube51, Cube61, Cube71, Cube81, Cube91 }
                };

                for (int x = 0; x < 9; x++)
                {
                    for (int y = 0; y < 9; y++)
                    {

                        if (sudokubtn2[x, y].Text == "")
                        { sudokubtn2[x, y].Text = "0"; }

                            sudokubtn2[x, y].ReadOnly = true;
                    }

                }
                button4.Enabled = true;
                this.button2.Enabled = false;
            }
            catch (Exception ex)
            {
               listBox1.Items.Add(ex.Message);
            }

        }


        private void Form2_Load(object sender, EventArgs e)
        {
            button4.Enabled = false;
            listBox1.Items.Add("Willkommen beim Sudoku-Lösen");
            listBox1.Items.Add("Bitte füllen Sie Sudoku aus");

            System.Windows.Forms.TextBox[,] sudokuload = new System.Windows.Forms.TextBox[9, 9]
             { { Cube19, Cube29, Cube39, Cube49, Cube59, Cube69, Cube79, Cube89, Cube99 }
              ,{ Cube18, Cube28, Cube38, Cube48, Cube58, Cube68, Cube78, Cube88, Cube98 }
              ,{ Cube17, Cube27, Cube37, Cube47, Cube57, Cube67, Cube77, Cube87, Cube97 }
              ,{ Cube16, Cube26, Cube36, Cube46, Cube56, Cube66, Cube76, Cube86, Cube96 }
              ,{ Cube15, Cube25, Cube35, Cube45, Cube55, Cube65, Cube75, Cube85, Cube95 }
              ,{ Cube14, Cube24, Cube34, Cube44, Cube54, Cube64, Cube74, Cube84, Cube94 }
              ,{ Cube13, Cube23, Cube33, Cube43, Cube53, Cube63, Cube73, Cube83, Cube93 }
              ,{ Cube12, Cube22, Cube32, Cube42, Cube52, Cube62, Cube72, Cube82, Cube92 }
              ,{ Cube11, Cube21, Cube31, Cube41, Cube51, Cube61, Cube71, Cube81, Cube91 }
             };
            
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    sudokuload[x, y].KeyPress += new KeyPressEventHandler(textboxes_KeyPress);
                    sudokuload[x, y].MaxLength = 1;
                    sudokuload[x, y].TextAlign = HorizontalAlignment.Center;
                    sudokuload[x, y].Multiline = true;
                    sudokuload[x, y].Size= new System.Drawing.Size(40, 40);
                    sudokuload[x, y].Font=new Font("Calibri", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

                }
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
                System.Windows.Forms.TextBox[,] sudokusolve = new System.Windows.Forms.TextBox[9, 9]
                 { { Cube19, Cube29, Cube39, Cube49, Cube59, Cube69, Cube79, Cube89, Cube99 }
              ,{ Cube18, Cube28, Cube38, Cube48, Cube58, Cube68, Cube78, Cube88, Cube98 }
              ,{ Cube17, Cube27, Cube37, Cube47, Cube57, Cube67, Cube77, Cube87, Cube97 }
              ,{ Cube16, Cube26, Cube36, Cube46, Cube56, Cube66, Cube76, Cube86, Cube96 }
              ,{ Cube15, Cube25, Cube35, Cube45, Cube55, Cube65, Cube75, Cube85, Cube95 }
              ,{ Cube14, Cube24, Cube34, Cube44, Cube54, Cube64, Cube74, Cube84, Cube94 }
              ,{ Cube13, Cube23, Cube33, Cube43, Cube53, Cube63, Cube73, Cube83, Cube93 }
              ,{ Cube12, Cube22, Cube32, Cube42, Cube52, Cube62, Cube72, Cube82, Cube92 }
              ,{ Cube11, Cube21, Cube31, Cube41, Cube51, Cube61, Cube71, Cube81, Cube91 }
                 };
                int[,] t = new int[9, 9];
                for (int i = 0; i < 9; i++)
                {
                    for (int j = 0; j < 9; j++)
                    {

                        t[i, j] = int.Parse(sudokusolve[i, j].Text);

                    }
                }

                int N = t.GetLength(0); //length of the board

                if (solve(t, N))
                    printSolving(t, N);
                else
                    listBox1.Items.Add("Keine Lösung");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            button4.Enabled = false;
            listBox1.Items.Clear();
            listBox1.Items.Add("Willkommen beim Sudoku-Lösen");
            listBox1.Items.Add("Bitte füllen Sie Sudoku aus");

            System.Windows.Forms.TextBox[,] sudokuneu = new System.Windows.Forms.TextBox[9, 9]
            { { Cube19, Cube29, Cube39, Cube49, Cube59, Cube69, Cube79, Cube89, Cube99 }
              ,{ Cube18, Cube28, Cube38, Cube48, Cube58, Cube68, Cube78, Cube88, Cube98 }
              ,{ Cube17, Cube27, Cube37, Cube47, Cube57, Cube67, Cube77, Cube87, Cube97 }
              ,{ Cube16, Cube26, Cube36, Cube46, Cube56, Cube66, Cube76, Cube86, Cube96 }
              ,{ Cube15, Cube25, Cube35, Cube45, Cube55, Cube65, Cube75, Cube85, Cube95 }
              ,{ Cube14, Cube24, Cube34, Cube44, Cube54, Cube64, Cube74, Cube84, Cube94 }
              ,{ Cube13, Cube23, Cube33, Cube43, Cube53, Cube63, Cube73, Cube83, Cube93 }
              ,{ Cube12, Cube22, Cube32, Cube42, Cube52, Cube62, Cube72, Cube82, Cube92 }
              ,{ Cube11, Cube21, Cube31, Cube41, Cube51, Cube61, Cube71, Cube81, Cube91 }
            };

            for(int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    sudokuneu[x, y].Text = "";
                    sudokuneu[x, y].ReadOnly = false;
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}

