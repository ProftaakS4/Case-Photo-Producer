<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PhotoshopWebsite.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
<head runat="server">
    <title></title>
</head>
<body style="background-color:#eee">
    <form id="form1" runat="server">
   <div class="container" style="max-width:300px; max-height:236px; padding-top: 50px;">

        <h2 class="form-signin-heading">Please sign in
        </h2>
        <label for="inputEmail" class="sr-only">Email address</label>
        <asp:TextBox  runat="server" type="email" id="tbInputEmail" class="form-control" placeholder="Email address" required autofocus></asp:TextBox>
        <label for="inputPassword" class="sr-only">Password</label>
        <asp:TextBox  runat="server" type="password" id="tbInputPassword" class="form-control" placeholder="Password" required ></asp:TextBox>
        <div class="checkbox">
          <label>
              <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" />Remember me</asp:CheckBox>
          </label>           
        </div>
        <asp:Button ID="BtnLogin" CssClass="btn btn-lg btn-primary btn-block" runat="server" OnClick="BtnLogin_Click" Text="Button" />
        </div>        
    </form>
</body>
</html>
