using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Text;
using System.IO;

public partial class SubMenu_Edit : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            IDLabel.Text = Request.QueryString["ID"].ToString();

            if (!String.IsNullOrEmpty(Request["ID"]))
            {
                SubMenuName.Focus();
                MyInitForUpdate();
            }
        }
    }


    private void MyInitForUpdate()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from CatMenu order by Orders asc";
            conn.Open();
            SqlDataReader rd1 = cmd.ExecuteReader();
            CatMenuName.DataSource = rd1;
            CatMenuName.DataValueField = "ID";
            CatMenuName.DataTextField = "CatMenuName";
            CatMenuName.DataBind();
            rd1.Close();


            string sql = "select * from SubMenu order by Orders asc,ID desc;select * from SubMenu where ID = @ID";
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@ID", Convert.ToInt16(Request["ID"]));
            SqlDataReader rd = cmd.ExecuteReader();
            // string CatID = rd["CatID"].ToString();
            rd.NextResult();
            if (rd.Read())
            {
                SubMenuName.Text = rd["SubMenuName"].ToString();
                Orders.Text = rd["Orders"].ToString();
                Href.Text = rd["Href"].ToString();
                string CatMenuID = rd["CatMenuID"].ToString();
                if (CatMenuName.Items.FindByValue(CatMenuID) != null)
                {
                    CatMenuName.ClearSelection();
                    CatMenuName.Items.FindByValue(CatMenuID).Selected = true;
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
            StringBuilder sb = new StringBuilder("Update [SubMenu] set SubMenuName = @SubMenuName,Valid=@Valid,Href=@Href, CatMenuID=@CatMenuID,CatMenuName=@CatMenuName,Orders=@Orders where ID=@SubMenuID ");
            SqlCommand cmd = new SqlCommand(sb.ToString(), conn);

           // StringBuilder sb2 = new StringBuilder("Update [Articles] set SubName = @SubName2,CatID=@CatID2,CatName=@CatName2 where SubID=@SubID2");
            //SqlCommand cmd2 = new SqlCommand(sb2.ToString(), conn);

           // cmd2.Parameters.AddWithValue("@SubMenuName2", SubMenuName.Text);
           // cmd2.Parameters.AddWithValue("@SubMenuID2", IDLabel.Text);
           // cmd2.Parameters.AddWithValue("@CatMenuID2", CatMenuName.SelectedValue);
           // cmd2.Parameters.AddWithValue("@CatMenuName2", CatMenuName.SelectedItem.Text);

            cmd.Parameters.AddWithValue("@SubMenuID", IDLabel.Text);
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
            cmd.Parameters.AddWithValue("@SubMenuName", SubMenuName.Text);
            cmd.Parameters.AddWithValue("@Orders", Orders.Text);
            cmd.Parameters.AddWithValue("@Href", Href.Text);
            cmd.Parameters.AddWithValue("@CatMenuID", CatMenuName.SelectedValue);
            cmd.Parameters.AddWithValue("@CatMenuName", CatMenuName.SelectedItem.Text);

            conn.Open();
            //cmd2.ExecuteNonQuery();
            i = cmd.ExecuteNonQuery();
            if (i == 1)
            {
                Response.Write("<script language='javascript'> alert('操作成功');window.location='CatMenu_Man.aspx';</script>");
            }
            else
            {
                Response.Write("<script language='javascript'> alert('操作失败，请重试！');window.location='CatMenu_Man.aspx';</script>");
            }
        }


        return i;
    }



    protected void Button4_Click(object sender, EventArgs e)
    {
        Response.Redirect("CatMenu_Man.aspx");
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