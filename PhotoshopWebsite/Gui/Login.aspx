<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PhotoshopWebsite.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
<head runat="server">
    <title></title>
</head>
<body style="background-color:#eee">
    <form id="form1" runat="server">
   <div class="container" style="max-width:600px; max-height:236px; padding-top: 50px;">
       <div class="row">
           <div class="col-lg-6">
               <h2 class="form-signin-heading">Please sign in</h2>
        <label for="inputEmail" class="sr-only">Email address</label>
        <asp:TextBox  runat="server" type="email" id="tbInputEmail" class="form-control" placeholder="Email address" required autofocus></asp:TextBox>
        <label for="inputPassword" class="sr-only">Password</label>
        <asp:TextBox  runat="server" type="password" id="tbInputPassword" class="form-control" placeholder="Password" required ></asp:TextBox>
        <div class="checkbox">
          <label>
              <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" />Remember me</asp:CheckBox>
          </label>           
        </div>
        <asp:Button ID="BtnLogin" CssClass="btn btn-lg btn-primary btn-block" runat="server" OnClick="BtnLogin_Click" Text="Sign in" />     
        </div>
            <div class="col-lg-6">
               <h2 class="form-signin-heading">Or insert your order code</h2>
             <asp:TextBox ID="tbInputCode" type="password" runat="server" class="form-control" placeholder="Code" required style="margin-bottom: 41px;"></asp:TextBox>     
            <asp:Button ID="BtnCreateAccount" CssClass="btn btn-lg btn-primary btn-block" runat="server" Text="Create Account" OnClick="BtnCreateAccount_Click" />              
           </div>
            </div>
    </form>
</body>
</html>
