using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;
public partial class Sub_Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Session["RoleID"] == null || Session["UserID"] == null)
            {
                Util.ShowMessage("用户登录超时，请重新登录！", "Login.aspx");
            }
            else
            {
                int RoleID = Convert.ToInt16(Session["RoleID"].ToString());
                if (RoleID > 1)
                {
                    Util.ShowMessage("对不起，你无权访问该页面！", "User_Center.aspx");

                }
                else
                {
                    IDLabel.Text = Request.QueryString["ID"].ToString();

                    if (!String.IsNullOrEmpty(Request["ID"]))
                    {
                        SubName.Focus();
                        MyInitForUpdate();
                    }
                }
            }
     }         
    }


    private void MyInitForUpdate()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Cats order by Orders desc";
            conn.Open();
            SqlDataReader rd1 = cmd.ExecuteReader();
            CatName.DataSource = rd1;
            CatName.DataValueField = "ID";
            CatName.DataTextField = "CatName";
            CatName.DataBind();
            rd1.Close();


            string sql = "select * from Subs order by valid desc,Orders desc;select * from Subs where ID = @ID";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt16(Request["ID"]));
            SqlDataReader rd = cmd.ExecuteReader();
           // string CatID = rd["CatID"].ToString();
            rd.NextResult();
            if (rd.Read())
            {
                SubName.Text = rd["SubName"].ToString();
                Description.Text = rd["Description"].ToString();
                string CatID = rd["CatID"].ToString();
                if (CatName.Items.FindByValue(CatID) != null)
                {
                    CatName.ClearSelection();
                    CatName.Items.FindByValue(CatID).Selected = true;
                }

            }

            //读取单选框所选择的信息
            int valid = Convert.ToInt32(rd["Valid"]);
            if (valid == 1)
            {
                true1.Checked = true;
            }
            else
            {
                false1.Checked = true;
            }

            rd.Close();
            conn.Close();

        }
        }
    
            
    



  /*  protected void MyDataBind()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            string sql = "select * from Cats order by valid desc,Orders desc;";
            cmd.CommandText = sql;
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();

            rd.Close();
            conn.Close();
        }
    }
    
    */


    private int DoUpdate(int finished)
    {
        int i = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            StringBuilder sb = new StringBuilder("Update [Subs] set SubName = @SubName,Description = @Description,Valid=@Valid, CatID=@CatID,CatName=@CatName where ID=@SubID ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);

            StringBuilder sb2 = new StringBuilder("Update [Articles] set SubName = @SubName2,CatID=@CatID2,CatName=@CatName2 where SubID=@SubID2");
            SqlCommand cmd2 = new SqlCommand(sb2.ToString(), conn);

            cmd2.Parameters.AddWithValue("@SubName2", SubName.Text);
            cmd2.Parameters.AddWithValue("@SubID2", IDLabel.Text);
            cmd2.Parameters.AddWithValue("@CatID2", CatName.SelectedValue);
            cmd2.Parameters.AddWithValue("@CatName2", CatName.SelectedItem.Text);

            cmd.Parameters.AddWithValue("@SubID", IDLabel.Text);
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
            cmd.Parameters.AddWithValue("@SubName", SubName.Text);
            cmd.Parameters.AddWithValue("@Description", Description.Text);
            cmd.Parameters.AddWithValue("@CatID", CatName.SelectedValue);
            cmd.Parameters.AddWithValue("@CatName", CatName.SelectedItem.Text);

            conn.Open();
            cmd2.ExecuteNonQuery();
            i = cmd.ExecuteNonQuery();
            if (i == 1)
            {
                Response.Write("<script language='javascript'> alert('操作成功');window.location='Cat_Man.aspx';</script>");
            }
            else
            {
                Response.Write("<script language='javascript'> alert('操作失败，请重试！');window.location='Cat_Man.aspx';</script>");
            }
        }
      
    
        return i;
    }



    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("Cat_Man.aspx");
    }
    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        int i;
        i = DoUpdate(1);
        
        if (i == 1)
        {
            ErrorLabel.Text = "信息修改成功！";
        }
        else
        {
            ErrorLabel.Text = "信息修改失败，请重试！";
        }

    }
}
