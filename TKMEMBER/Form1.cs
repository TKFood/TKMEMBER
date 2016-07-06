using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace TKMEMBER
{
    public partial class Form1 : Form
    {
        DataSet dsMember = new DataSet();

        public Form1()
        {
            InitializeComponent();

        }

        #region FUNTION
        private void button1_Click(object sender, EventArgs e)
        {
            Search();
        }

        #endregion

        #region SERACH
        public void Search()
        {
            try
            {
                SqlConnection sqlConn = new SqlConnection();
                SqlCommand sqlComm = new SqlCommand();
                StringBuilder sbSql = new StringBuilder();
                SqlDataAdapter adapter = new SqlDataAdapter();
                SqlCommandBuilder sqlCmdBuilder = new SqlCommandBuilder();
                SqlTransaction tran;
                SqlCommand cmd = new SqlCommand();
                

                if(!string.IsNullOrEmpty(textBox1.Text.ToString()))
                {
                    sqlConn = new SqlConnection("server=192.168.1.102;database=TKFOODDB;uid=sa;pwd=chi");
                    sbSql.Clear();
                    sbSql.AppendFormat("SELECT  [ID],[Cname],[Mobile1],[OldCardID],[Telphone],[Email],[Address],[Sex],[Birthday] FROM [TKFOODDB].[dbo].[Member] WHERE OldCardID LIKE '{0}%' ", textBox1.Text.ToString());

                    adapter = new SqlDataAdapter(@"" + sbSql, sqlConn);
                    sqlCmdBuilder = new SqlCommandBuilder(adapter);

                    sqlConn.Open();
                    dsMember.Clear();
                    adapter.Fill(dsMember, "TEMPdsMember");
                    sqlConn.Close();
                    label1.Text = dsMember.Tables["TEMPdsMember"].Rows.ToString();

                    if(dsMember.Tables["TEMPdsMember"].Rows.Count == 0)
                    {
                        label1.Text = "0 find nothing";
                    }
                    else
                    {
                        label1.Text = dsMember.Tables["TEMPdsMember"].Rows.Count.ToString();

                        dataGridView1.DataSource= dsMember.Tables["TEMPdsMember"];
                    }
                }
                else
                {
                    
                }
               

               
            }
            catch
            {

            }
            finally
            {
               
            }
        }


        #endregion

        #region GRIDVIEW

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if(dsMember.Tables["TEMPdsMember"].Rows.Count >= 1)
            {
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textBox5.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
                textBox6.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
                textBox7.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
                textBox8.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                textBox9.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();

            }
            else
            {
                textBox2.Text = "";
            }
            
        }

        #endregion
    }
}
