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

        SqlConnection sqlConn = new SqlConnection();
        SqlCommand sqlComm = new SqlCommand();
        StringBuilder sbSql = new StringBuilder();
        SqlDataAdapter adapter = new SqlDataAdapter();
        SqlCommandBuilder sqlCmdBuilder = new SqlCommandBuilder();
        SqlTransaction tran;
        SqlCommand cmd = new SqlCommand();
        DataSet dsMember = new DataSet();
        int result;

        public class Member
        {
            public string ID { set; get; }
            public string Cname { set; get; }
            public string Mobile1 { set; get; }
            public string Telphone { set; get; }
            public string Email { set; get; }
            public string Address { set; get; }
            public string Sex { set; get; }
            public string Birthday { set; get; }
        }

        List<Member> list_Member = new List<Member>();
        
        public Form1()
        {
            InitializeComponent();

        }

        #region FUNTION
       
        #endregion

        #region SERACH
        public void Search()
        {
            try
            {                

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

        public void MemberUpdate()
        {
            try
            {
                sqlConn = new SqlConnection("server=192.168.1.102;database=TKFOODDB;uid=sa;pwd=chi");
               
                sqlConn.Close();
                sqlConn.Open();
                tran = sqlConn.BeginTransaction();

                sbSql.Clear();
                //sbSql.Append("UPDATE Member SET Cname='009999',Mobile1='009999',Telphone='',Email='',Address='',Sex='',Birthday='' WHERE ID='009999'");
                
                sbSql.AppendFormat("  UPDATE Member SET Cname='{1}',Mobile1='{2}',Telphone='{3}',Email='{4}',Address='{5}',Sex='{6}' WHERE ID='{0}' ", list_Member[0].ID.ToString(), list_Member[0].Cname.ToString(), list_Member[0].Mobile1.ToString(), list_Member[0].Telphone.ToString(), list_Member[0].Email.ToString(), list_Member[0].Address.ToString(), list_Member[0].Sex.ToString());
                //sbSql.AppendFormat("  UPDATE Member SET Cname='{1}',Mobile1='{2}' WHERE ID='{0}' ", list_Member[0].ID.ToString(), list_Member[0].Cname.ToString(), list_Member[0].Mobile1.ToString());

                cmd.Connection = sqlConn;
                cmd.CommandTimeout = 60;
                cmd.CommandText = sbSql.ToString();
                cmd.Transaction = tran;
                result = cmd.ExecuteNonQuery();

                if (result == 0)
                {
                    tran.Rollback();    //交易取消
                }
                else
                {
                    tran.Commit();      //執行交易
                }
                sqlConn.Close();
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

        #region BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            list_Member.Clear();
            list_Member.Add(new Member() { ID = dataGridView1.CurrentRow.Cells[0].Value.ToString(), Cname = textBox2.Text.ToString(), Mobile1 = textBox3.Text.ToString(), Telphone = textBox4.Text.ToString(), Email = textBox9.Text.ToString(), Address = textBox7.Text.ToString(), Sex = textBox5.Text.ToString(), Birthday = textBox6.Text.ToString() });

            MemberUpdate();
        }
        #endregion
    }
}
