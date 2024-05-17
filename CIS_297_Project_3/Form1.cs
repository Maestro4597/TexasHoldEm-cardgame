using System.Security.Policy;
using TexasHoldEm;




namespace CIS_297_Project_3
{


    public partial class Form1 : Form
    {

        Player One = new Player("Mike", 100);
        Player Two = new Player("John", 100);

       

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
           
            player1TotalDollarAmount.Text = One.ToString();
            player2TotalDollarAmount.Text = Two.ToString();

            Deck FirstDeck = new Deck();


            Table Initial;
            Initial = new Table();
            Initial.AddPlayer(One);
            Initial.AddPlayer(Two);
            Initial.Deal();


            textBox1.Text = One.Hand[0] + " And " + One.Hand[1];

            textBox2.Text = Two.Hand[0] + " And " + Two.Hand[1];



            textBox3.Text = Convert.ToString(Initial.CommunityCards[0]);
            textBox4.Text = Convert.ToString(Initial.CommunityCards[1]);
            textBox5.Text = Convert.ToString(Initial.CommunityCards[2]);
            textBox6.Text = Convert.ToString(Initial.CommunityCards[3]);
            textBox7.Text = Convert.ToString(Initial.CommunityCards[4]);

           

           
        }

        private void player1CallButton_Click(object sender, EventArgs e)
        {

        }

        private void player1FoldButton_Click(object sender, EventArgs e)
        {

        }

        private void player1CheckButton_Click(object sender, EventArgs e)
        {

        }

        private void player1BetButton_Click(object sender, EventArgs e)
        {

        }

        private void player2CallButton_Click(object sender, EventArgs e)
        {

        }

        private void player2FoldButton_Click(object sender, EventArgs e)
        {

        }

        private void player2CheckButton_Click(object sender, EventArgs e)
        {

        }

        private void player2BetButton_Click(object sender, EventArgs e)
        {

        }
        private void player1BetAmountTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void player2BetAmountTextBox_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

       

        }

        private void player1Card1_Click(object sender, EventArgs e)
        {


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void player1TotalDollarAmount_Click(object sender, EventArgs e)
        {

        }

        private void player2TotalDollarAmount_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void flopCard1_Click(object sender, EventArgs e)
        {


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {


        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void player2CallButton_Click_1(object sender, EventArgs e)
        {


        }
    }
}