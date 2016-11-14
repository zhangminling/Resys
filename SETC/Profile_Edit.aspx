<%@ Page Title="" Language="C#" MasterPageFile="~/User.master" AutoEventWireup="true" CodeFile="Profile_Edit.aspx.cs" Inherits="Profile_Edit" ValidateRequest="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <!-- Make sure the path to CKEditor is correct. -->
    <script type="text/javascript" src="ckeditor2/ckeditor.js"></script>
    <%--插入资源用的js代码--%>
    <script type="text/javascript">
        editor.addCommand('insertTimestamp', {
            exec: function showMyDialog(e) {
                var str = 'width=980,height=650,left=' + ((screen.width - 900) / 2) + ',top=' + ((screen.height - 650) / 2) + ',scrollbars=yes,scrolling=no,location=no,toolbar=no，titlebar=no,status=no,menubar=no';
                //var w = window.open('File_Browse.aspx', 'MyWindow', str);
            }
        });


        editor.ui.addButton('Timestamp', {
            label: 'Insert Image',
            command: 'insertTimestamp',
            toolbar: 'insert'
        });
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="col-md-12" id="ErrorPanel" runat="server" visible="false" style="text-align:center;">
        <h1>你尚未开通空间，是否前往开通</h1>
        <asp:Button ID="GoBtn" runat="server" Text="前往开通" CssClass="btn btn-info btn-lg" Width="180" OnClick="GoBtn_Click" />
    </div>
    <script src="ckeditor201507_1/plugins/templates/templates/default.js"></script>
    <div id="MainContent" runat="server">
        <div id="CurrentPosition">当前位置：<a href="Profile_Edit.aspx">信息修改</a></div>
        <p>&nbsp;</p>
        <div class="col-lg-12">
            <asp:Button ID="menu_1" CssClass="btn btn-success" runat="server" Text="个人信息" OnClick="btnIntroduction_Click" />&nbsp;&nbsp;
                <asp:Button ID="menu_2" CssClass="btn btn-defaul" runat="server" Text="修改头像" OnClick="btnIntroduction_Click" />&nbsp;&nbsp;
                <asp:Button ID="menu_3" CssClass="btn btn-default" runat="server" Text="编写简介" OnClick="btnIntroduction_Click" />&nbsp;&nbsp;
                <asp:Button ID="menu_4" CssClass="btn btn-default" runat="server" Text="个人经历" OnClick="btnIntroduction_Click" Visible="false" />
        </div>
        <div >
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0"> 
                <asp:View ID="View1" runat="server">
                    <div class="col-md-6 col-sm-6 col-xs-12">
                        <div class="well with-header  with-footer">
                            <div class="header bordered-blue">添加个人信息：</div>
                            <form class="form">
                                <asp:Label ID="LabelSno" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:Label ID="LabelName" runat="server" Text="" Visible="false"></asp:Label>
                                <div class="form-group">
                                    <asp:Label ID="LabelTrueName1" runat="server" Text="姓名" Width="150px"></asp:Label>
                                    <asp:TextBox ID="TrueName1" runat="server" MaxLength="30" CssClass="TextBox"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LabelEmail1" runat="server" Text="邮箱" Width="150px"></asp:Label>
                                    <asp:TextBox ID="Email" runat="server" MaxLength="30" CssClass="TextBox"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LabelTel" runat="server" Text="电话号码" Width="150px"></asp:Label>
                                    <asp:TextBox ID="Tel" runat="server" MaxLength="30" CssClass="TextBox"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LabelSex" runat="server" Text="性别" Width="150px"></asp:Label>
                                    <asp:DropDownList ID="SexDDL" runat="server" CssClass="btn btn-default">
                                        <asp:ListItem Value="1">男</asp:ListItem>
                                        <asp:ListItem Value="0">女</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LabelClass1" runat="server" Text="班级" Width="150px"></asp:Label>
                                    <asp:DropDownList ID="ClassesDDL" runat="server" CssClass="btn btn-default"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <asp:Label ID="LabelStdUnion" runat="server" Text="学生会" Width="150px"></asp:Label>
                                    <%--<asp:DropDownList ID="StdUnionDDL" runat="server" CssClass="btn btn-default"></asp:DropDownList>--%>
                                    <p>&nbsp;</p>
                                    <asp:CheckBoxList ID="StdUnionCBL" runat="server" RepeatColumns="4" CellPadding="10" CellSpacing="10" RepeatDirection="Horizontal" Width="50%"></asp:CheckBoxList>
                                    <p>&nbsp;</p>
                                </div>
                                <div class="form-group">
                                    <p>座右铭</p>
                                    <asp:TextBox ID="txtmotto" runat="server" TextMode="MultiLine" Columns="50" Rows="4" CssClass="TextBoxMultiLine" MaxLength="140"></asp:TextBox>
                                    <p></p>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="当前状态，不超过75个字符！" ControlToValidate="txtmotto" ValidationExpression=".{0,75}"> </asp:RegularExpressionValidator>
                                </div>
                                <p></p>
                                <asp:Label ID="ErrorLabel" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                                <p></p>
                                <asp:Button ID="btnAlter" runat="server" Text=" 修改信息 " CssClass="btn btn-success" OnClick="btnAlter_Click" />
                                &nbsp;
                    <asp:Button ID="btnCancel" runat="server" Text=" 取消修改 " CssClass="btn btn-default" OnClick="btnCancel_Click" />
                            </form>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div class="col-lg-6 col-sm-6 col-xs-12">
                        <div class="well with-header  with-footer">
                        <div class="header bordered-blue">设置个人头像：</div>
                            <p>
                                <asp:Image ID="Image1" runat="server"  Width="230" Height="230" ImageUrl="~/images/users/1.png" /><br />
                                <asp:Button ID="ButCutImage" runat="server" Text="上传头像" CssClass="btn btn-success" OnClick="ButCutImage_Click"  />
                            </p>
                            <asp:Button ID="Btn_back" runat="server" Text=" 返回首页 " CssClass="btn btn-success" OnClick="Btn_back_Click" />
                        </div>
                    </div>                   
                </asp:View>
                <asp:View ID="View3" runat="server">
                    <div class="col-lg-6 col-sm-6 col-xs-12">
                        <div class="well with-header  with-footer">
                            <div class="header bordered-blue">添加自我介绍：</div>
                            <div id="visits" class="tab-pane active animated fadeInUp">
                                <div class="row">
                                    <div class="col-lg-12 chart-container" style="height: 710px;">
                                        <div id="dashboard-chart-visits" class="chart chart-lg no-margin">
                                            <asp:HiddenField ID="HiddenField1" runat="server" />
                                            <asp:TextBox ID="Editor1" runat="server" TextMode="MultiLine" />
                                            <script type="text/javascript">
                                                var editor = CKEDITOR.replace('<%= Editor1.ClientID %>', { height: "600px" });
                                        </script>
                                            <p>&nbsp;</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <p>&nbsp;</p>
                            <asp:Button ID="Btn_Ok" runat="server" Text="确定" CssClass="btn btn-success" OnClick="Btn_Ok_Click" />
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View4" runat="server">
                    <div class="col-lg-6 col-sm-6 col-xs-12">
                        <div class="well with-header  with-footer">
                        <div class="header bordered-blue">添加个人经历：</div>
                            <asp:Button ID="AddExperience" runat="server" Text="新增经历" CssClass="btn btn-success" OnClick="AddExperience_Click" />
                            <p>&nbsp;</p>
                            <asp:Repeater ID="RepeaterExp" runat="server">
                                <ItemTemplate>
                                    <div style="background-color:lightgray;width:30%">
                                        <p><%# Eval("SchoolName") %></p>
                                        <p><%# Eval("PreviousPosts") %></p>
                                        <p><%# Eval("Year1") %>年<%# Eval("Month1") %>—<%# Eval("Year2") %>年<%# Eval("Month2") %>| <%# Eval("Province") %>  <%# Eval("City") %></p>
                                        <p><%# Eval("SchoolDescription") %></p>
                                        <p>&nbsp;</p>
                                        <asp:Button ID="Btn1" runat="server" Text="修改" CssClass="btn btn-default" />
                                        <asp:Button ID="Btn2" runat="server" Text="删除"  CssClass="btn btn-danger"/>
                                    </div>                                    
                                </ItemTemplate>
                            </asp:Repeater>
                            <p>&nbsp;</p>
                        <asp:Panel ID="PanelExperience" runat="server" Visible="false">
                            <div class="form-group">
                            <span class="text-primary">学校名称：</span>
                            <p>&nbsp;</p>
                            <span class="input-icon icon-right">
                                <asp:TextBox ID="SchoolName" class="form-control" runat="server"></asp:TextBox>
                                <i class="fa fa-users darkorange"></i>
                            </span>
                        </div> 
                        <div class="form-group">
                            <span class="text-primary">曾任职务：</span>
                            <p>&nbsp;</p>
                            <span class="input-icon icon-right">
                                <asp:TextBox ID="Job" class="form-control" runat="server"></asp:TextBox>
                                <i class="fa fa-user darkorange"></i>
                            </span>
                        </div>
                        <div class="form-group">
                            <span class="text-primary">学校地区：</span>
                            <p>&nbsp;</p>
                            <p>
                                <asp:TextBox ID="Province" runat="server" Width="70px"></asp:TextBox>省<asp:TextBox ID="City" runat="server" Width="100px"></asp:TextBox>市
                            </p>
                        </div>
                        <div class="form-group">
                            <span class="text-primary">在校时长：</span>
                            <p>&nbsp;</p>
                            <span class="input-icon">
                                <asp:TextBox ID="Year1" CssClass="btn btn-default" runat="server" Width="100px"></asp:TextBox>
                                <asp:DropDownList ID="MonthDDL1" runat="server">
                                    <asp:ListItem>1月</asp:ListItem>
                                    <asp:ListItem>2月</asp:ListItem>
                                    <asp:ListItem>3月</asp:ListItem>
                                    <asp:ListItem>4月</asp:ListItem>
                                    <asp:ListItem>5月</asp:ListItem>
                                    <asp:ListItem>6月</asp:ListItem>
                                    <asp:ListItem>7月</asp:ListItem>
                                    <asp:ListItem>8月</asp:ListItem>
                                    <asp:ListItem>9月</asp:ListItem>
                                    <asp:ListItem>10月</asp:ListItem>
                                    <asp:ListItem>11月</asp:ListItem>
                                    <asp:ListItem>12月</asp:ListItem>
                                </asp:DropDownList>—
                                <asp:TextBox ID="Year2" CssClass="btn btn-default" runat="server" Width="100px"></asp:TextBox>
                                <asp:DropDownList ID="MonthDDL2" runat="server">
                                    <asp:ListItem>1月</asp:ListItem>
                                    <asp:ListItem>2月</asp:ListItem>
                                    <asp:ListItem>3月</asp:ListItem>
                                    <asp:ListItem>4月</asp:ListItem>
                                    <asp:ListItem>5月</asp:ListItem>
                                    <asp:ListItem>6月</asp:ListItem>
                                    <asp:ListItem>7月</asp:ListItem>
                                    <asp:ListItem>8月</asp:ListItem>
                                    <asp:ListItem>9月</asp:ListItem>
                                    <asp:ListItem>10月</asp:ListItem>
                                    <asp:ListItem>11月</asp:ListItem>
                                    <asp:ListItem>12月</asp:ListItem>

                                </asp:DropDownList>
                                <asp:Label ID="zhijin" runat="server" Text="至今" Font-Size="Large" Visible="false"></asp:Label>
                            </span>
                            <span>
                                <asp:CheckBox ID="CheckBox1" CssClass="btn btn-default" runat="server" Text="仍然在读" OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="True" />
                            </span>
                        </div>
                        <div class="form-group">
                            <span class="text-primary">在校描述：</span>
                            <p>&nbsp;</p>
                            <span class="input-icon icon-right">
                                <asp:TextBox ID="SchoolDescription" TextMode="MultiLine" class="form-control" Columns="40" Rows="5" MaxLength="300" runat="server"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="当前状态，不超过300个字符！" ControlToValidate="SchoolDescription" ValidationExpression=".{0,300}"> </asp:RegularExpressionValidator>
                                <i class="glyphicon glyphicon-briefcase darkorange"></i>
                            </span>
                            <p class="help-block">请输入你对学校的描述，不超过300个字符！（可不填）</p>
                        </div>
                        <asp:Button ID="BtnSava" runat="server" Text="保存" CssClass="btn btn-default" OnClick="BtnSava_Click" />
                        </asp:Panel>                       
                        </div>
                    </div>                      
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</asp:Content>

