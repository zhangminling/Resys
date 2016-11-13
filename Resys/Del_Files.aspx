<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Del_Files.aspx.cs" Inherits="Del_Files" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style type="text/css">
/* 文件夹样式 */    
        #folders div{
            margin:20px 20px;
            padding:20px 20px;
            border:1px solid #ccc;
            width:60px;
            height:60px;
            float:left;
        }
        .divHover{            
            cursor:pointer;            
            background-color:#73aef7;
            color:#fff;
        }
        #folders div p{
            text-align:center;
        } 
/*具体类型文件夹的样式表*/       
        #AllFolders a
        {
            margin:20px 20px;
            padding:20px 20px;
            border:1px solid #ccc;
            width:60px;
            height:60px;
            float:left; 
            text-align:center;
            }
        a:hover
        {
           cursor:pointer;            
            background-color:#73aef7;
            color:#fff;
            }  
            
        a
        {
            text-decoration:none;
            }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <!--panel1是根目录文件夹-->
    <asp:Panel ID="Panel1" runat="server">
         <div id="folders">
              <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                      <span style="float:left; margin-left:20px">
                      <input id="Checkbox1" type="checkbox" value='<%# Eval("FolderType") %>' runat="server" style="float:left" checked="checked"/>
                      <div><p><%# Eval("FolderType")%></p></div>
                      <p>创建时间：<%# Convert.ToDateTime(Eval("CDT")).ToString("yyyy-MM-dd")%></p>
                      </span>
                    </ItemTemplate>
                    
               </asp:Repeater>   
            </div>
            
    </asp:Panel>
    <!--panel2是二级目录文件夹-->
    <asp:Panel ID="Panel2" runat="server">
     <div id="AllFolders">
             <asp:Repeater ID="Repeater2" runat="server">
                <ItemTemplate>
                   <span style="float:left; margin-left:20px;">
                      <input id="Checkbox2" type="checkbox" value='<%# Eval("ID") %>' runat="server" style="float:left" />
                      <a href="File_Browse.aspx?type=<%# Eval("FolderType") %>&folderName=<%# Eval("FolderName") %>"><%# Eval("FolderName") %></a>
                      <p>创建时间：<%# Convert.ToDateTime(Eval("CDT")).ToString("yyyy-MM-dd")%></p>
                     </span>
                    </ItemTemplate>
                     
               </asp:Repeater> 
            </div>
            
    </asp:Panel>
    <!--panel3是具体图片缩略图-->
    <asp:Panel ID="Panel3" runat="server">
    <div id="AllFiles">
             <asp:Repeater ID="Repeater3" runat="server">
                <ItemTemplate>
                   <span style="float:left; margin-left:20px;">
                      <input id="Checkbox3" type="checkbox" value='<%# Eval("ID") %>' runat="server" style="float:left" />
                      <img src='<%# Eval("FilePath") %>' alt=""  width="200px" />
                      <p>创建时间：<%# Convert.ToDateTime(Eval("CDT")).ToString("yyyy-MM-dd")%></p>
                     </span>
                    </ItemTemplate>
                     
               </asp:Repeater> 
           </div>
    </asp:Panel>
   <div><p> <asp:Button ID="Button1" runat="server" Text="确定删除" onclick="Button1_Click" />&nbsp; &nbsp; 
       <asp:Button ID="Button2" runat="server" Text="返回" onclick="Button2_Click" /></p></div>
</asp:Content>

