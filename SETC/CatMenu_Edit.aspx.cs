using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;

public partial class CatMenu_Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LabelUserID.Text = Request.QueryString["ID"];
            CatMenuName.Focus();
            if (!String.IsNullOrEmpty(Request["ID"]))
            {
                using (SqlConnection conn = new DB().GetConnection())
                {
                    SqlCommand cmd = conn.CreateCommand();
                    string sql = "select * from CatMenu order by valid desc;select * from CatMenu where ID = @ID";
                    cmd.CommandText = sql;
                    cmd.Parameters.AddWithValue("@ID", Convert.ToInt16(Request["ID"]));
                    conn.Open();
                    SqlDataReader rd = cmd.ExecuteReader();
                    rd.NextResult();


                    if (rd.Read())
                    {

                        CatMenuName.Text = rd["CatMenuName"].ToString();
                        Href.Text = rd["Href"].ToString();
                        Orders.Text = rd["Orders"].ToString();

                        //读取单选框所选择的信息
                        int valid = Convert.ToInt32(rd["Valid"]);
                        if (valid == 1 )
                        {
                            true1.Checked = true;
                        }
                        else
                        {
                            false1.Checked = true;
                        }
                        

                    }
                    rd.Close();
                    conn.Close();
                }
            }
            
            //using (sqlconnection conn = new db().getconnection())
            //{
            //    string sql = "select * from Cats order by ID desc";
            //    SqlCommand cmd = new SqlCommand(sql, conn);
            //    conn.Open();
            //    SqlDataReader rd = cmd.ExecuteReader();
            //    cmd.Parameters.AddWithValue("@ID", Label1.Text);
            //    rd = cmd.ExecuteReader();
            //    if (rd.Read())
            //    {
            //        int valid = Convert.ToInt32(rd["Valid"]);
            //        if (valid == 1)
            //        {
            //            true1.Checked = true;
            //        }
            //        else
            //        {
            //            false1.Checked = true;
            //        }
        }
    }




   
    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("CatMenu_Man.aspx");
    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        int i;
        using (SqlConnection conn = new DB().GetConnection())
        {

            string sql = "Update [CatMenu] set CatMenuName = @CatMenuName,Href = @Href,Orders = @Orders,Valid=@Valid where ID=@CatMenuID";
           
            SqlCommand cmd = new SqlCommand(sql, conn);
            
            
            string radiobuttonvalue = "";
            if (true1.Checked)
            {
                radiobuttonvalue = true1.Text;
            }
            else if (false1.Checked)
            {
                radiobuttonvalue = false1.Text;
            }
            cmd.Parameters.AddWithValue("@Valid", radiobuttonvalue);
            
          
            cmd.Parameters.AddWithValue("@CatMenuName", CatMenuName.Text);
            cmd.Parameters.AddWithValue("@Href", Href.Text);
            cmd.Parameters.AddWithValue("@Orders", Orders.Text);
            cmd.Parameters.AddWithValue("@CatMenuID", LabelUserID.Text);

            conn.Open();
           
            i = cmd.ExecuteNonQuery();
            conn.Close();
            if (i == 1)
            {
                Response.Write("<script language='javascript'> alert('操作成功');window.location='CatMenu_Man.aspx';</script>");
            }
            else
            {
                Response.Write("<script language='javascript'> alert('操作失败，请重试！');window.location='CatMenu_Man.aspx';</script>");
            }
        }
    }
}
