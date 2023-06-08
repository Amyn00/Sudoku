namespace Sudoku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var frm2 = new Form2();
            frm2.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                VisitLink();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void VisitLink()
        {
            this.linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo("https://youtu.be/58KdzVeR9aA") {  UseShellExecute = true  });
        }
    }
}