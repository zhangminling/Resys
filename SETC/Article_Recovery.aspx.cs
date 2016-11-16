﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

public partial class Article_Recovery : System.Web.UI.Page
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
                if (Convert.ToInt16(Session["RoleID"]) >0)
                {
                    Util.ShowMessage("对不起，你无权访问该页面！", "User_Center.aspx");
                }
                else
                {
                    if (Request.QueryString["IDS"] != null)
                    {
                        IDSLabel.Text = Request.QueryString["IDS"].ToString();
                        MyInit();
                    }
                }
            }
        }
    }

    private void MyInit()
    {
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "select * from Articles where ID in (" + IDSLabel.Text + ") order by ID desc";
            conn.Open();
            SqlDataReader rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();
            conn.Close();
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        int i = 0;
        using (SqlConnection conn = new DB().GetConnection())
        {
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = "Update Articles set Valid=1 where ID in (" + IDSLabel.Text + ") ";
            conn.Open();
            i = cmd.ExecuteNonQuery();
            cmd.Dispose();

            cmd.CommandText = "select * from Articles where Valid =0 and ID in (" + IDSLabel.Text + ") order by ID desc";
            SqlDataReader rd = cmd.ExecuteReader();
            GridView1.DataSource = rd;
            GridView1.DataBind();
            rd.Close();
            conn.Close();

        }
        if (i > 0)
        {
            ResultLabel.Text = "成功恢复" + i + "篇文章！";
            ResultLabel.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            ResultLabel.Text = "操作失败，请重试！";
            ResultLabel.ForeColor = System.Drawing.Color.Red;
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Article_Recycle.aspx");
    }





}