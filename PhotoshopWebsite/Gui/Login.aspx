<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PhotoshopWebsite.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
<link href="../Bootstrap/Content/Custom.css" rel="stylesheet" />
<head runat="server">
    <title>Photoshop</title>
</head>

    <body>
    <section id="home" data-type="background" data-speed="10">
            <script type="text/javascript">
         function setBackGround (){
             var random = Math.floor((Math.random() * 4) + 1);
             document.getElementById("home").style.backgroundImage = 'url("Images/background/background' + random + '.jpg")';
         };
         setBackGround();
    </script>
        <form id="form1" runat="server">
            <div class="container" style="max-width: 600px; max-height: 236px; padding-top: 50px;">
                <div class="row">
                    <div class="col-lg-6">
                        <h2 class="form-signin-heading" style="color: white;">Please sign in</h2>
                        <label for="inputEmail" class="sr-only">Email address</label>
                        <asp:TextBox runat="server" type="email" ID="tbInputEmail" class="form-control" placeholder="Email address"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            ControlToValidate="tbInputEmail"
                            ValidationGroup="PersonalInfoGroup"
                            ErrorMessage="Enter Your Email"
                            runat="Server">
                        </asp:RequiredFieldValidator>
                        <label for="inputPassword" class="sr-only">Password</label>
                        <asp:TextBox runat="server" type="password" ID="tbInputPassword" class="form-control" placeholder="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                            ControlToValidate="tbInputPassword"
                            ValidationGroup="PersonalInfoGroup"
                            ErrorMessage="Enter Your Password"
                            runat="Server">
                        </asp:RequiredFieldValidator>
                        <div class="checkbox">
                            <label>
                                <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" />Remember me
                            </label>
                        </div>
                        <asp:Button CausesValidation="true" ValidationGroup="PersonalInfoGroup" ID="BtnLogin" CssClass="btn btn-lg btn-primary btn-block" runat="server" OnClick="BtnLogin_Click" Text="Sign in" />
                    </div>
                    <div class="col-lg-6">
                        <h2 class="form-signin-heading" style="margin-bottom: 30px; color: white;">Or insert your order code</h2>
                        <asp:TextBox ID="tbInputCode" Style="margin-bottom: 0px;" type="password" runat="server" class="form-control" placeholder="Code"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                            ControlToValidate="tbInputCode"
                            ValidationGroup="PersonalInfoGroupCode"
                            ErrorMessage="Enter Your Code"
                            runat="Server">
                        </asp:RequiredFieldValidator>
                        <asp:Button CausesValidation="true" ValidationGroup="PersonalInfoGroupCode" ID="BtnCreateAccount" CssClass="btn btn-lg btn-primary btn-block" runat="server" Text="Create Account" OnClick="BtnCreateAccount_Click" />
                    </div>
                </div>
        </form>
    </section>
</body>
</html>
