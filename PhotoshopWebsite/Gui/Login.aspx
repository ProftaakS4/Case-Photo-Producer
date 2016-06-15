<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="PhotoshopWebsite.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<link href="../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
<link href="../Bootstrap/Content/Custom.css" rel="stylesheet" />
<head runat="server">
    <title>Photoshop</title>
</head>

    <body>`
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
                        <h2 class="form-signin-heading" style="color: white;">
                            <asp:Literal runat="server" Text="<%$ Resources:LocalizedText, sign_in_title%>" />
                        </h2>
                        <label for="inputEmail" class="sr-only">
                            <asp:Literal runat="server" Text="<%$ Resources:LocalizedText, email%>" />
                        </label>
                        <asp:TextBox runat="server" type="email" ID="tbInputEmail" class="form-control" placeholder="<%$ Resources:LocalizedText, email%>"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                            ControlToValidate="tbInputEmail"
                            ValidationGroup="PersonalInfoGroup"
                            ErrorMessage="Enter Your Email"
                            runat="Server">
                        </asp:RequiredFieldValidator>
                        <label for="inputPassword" class="sr-only">
                            <asp:Literal runat="server" Text="<%$ Resources:LocalizedText, password%>" />
                        </label>
                        <asp:TextBox runat="server" type="password" ID="tbInputPassword" class="form-control" placeholder="<%$ Resources:LocalizedText, password%>"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                            ControlToValidate="tbInputPassword"
                            ValidationGroup="PersonalInfoGroup"
                            ErrorMessage="<%$ Resources:LocalizedText, error_enter_password%>"
                            runat="Server">
                        </asp:RequiredFieldValidator>
                        <div class="checkbox" style="margin-left:0px;">
                            <label style="color:white;">
                                <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged" /><asp:Literal runat="server" Text="<%$ Resources:LocalizedText, remember_me%>" /></label>
                        </div>
                        <asp:Button CausesValidation="true" ValidationGroup="PersonalInfoGroup" ID="BtnLogin" CssClass="btn btn-lg btn-primary btn-block" runat="server" OnClick="BtnLogin_Click" Text="<%$ Resources:LocalizedText, sign_in%>" />
                    </div>
                    <div class="col-lg-6">
                        <h2 class="form-signin-heading" style="margin-bottom: 30px; color: white;">
                            <asp:Literal runat="server" Text="<%$ Resources:LocalizedText, order_code_title%>" />
                        
                        </h2>
                        <asp:TextBox ID="tbInputCode" Style="margin-bottom: 0px;" type="text" runat="server" class="form-control" placeholder="<%$ Resources:LocalizedText, order_code%>"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                            ControlToValidate="tbInputCode"
                            ValidationGroup="PersonalInfoGroupCode"
                            ErrorMessage="<%$ Resources:LocalizedText, error_enter_code%>"
                            runat="Server">
                        </asp:RequiredFieldValidator>
                        <asp:Button CausesValidation="true" ValidationGroup="PersonalInfoGroupCode" ID="BtnCreateAccount" CssClass="btn btn-lg btn-primary btn-block" runat="server" Text="<%$ Resources:LocalizedText, create_account%>" OnClick="BtnCreateAccount_Click" style="margin-top:41px;" />
                    </div>
                </div>
        </form>
    </section>
</body>
</html>
