<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="PhotoshopWebsite.Gui.Client.CreateAccount" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../Bootstrap/Content/bootstrap.css" rel="stylesheet" />
    <link href="../../Bootstrap/Content/Custom.css" rel="stylesheet" />
    <title></title>
</head>
<body>
       <div id="wrapper">
           
           <div class="jumbotron" style="border: initial; border-color:white; max-height:150px;">
  <h1 style="color:white;">Account creation page</h1>      
  <p style="opacity:0.8; color:white;">Welcome to the account creation page! Please fill in the fields below</p>
</div>

            <div ID="panel-body" class="panel-body">
   <div class='container-create-account'>
                    <form runat="server">
                    <div class='row'>
                        <div class='col-md-6';>
                            <asp:Label class="label label-default" ID="Firstname" runat="server" Text="Firstname :"></asp:Label>
                            <asp:TextBox class="form-control" ID="tbFirstname" runat="server" required></asp:TextBox>                           
                            </div>
                        </div>
                        <div class='row'>
                        <div class='col-md-6' ;>
                            <asp:Label class="label label-default" ID="Lastname" runat="server" Text="Lastname :"></asp:Label>
                            <asp:TextBox class="form-control" ID="tbLastname" runat="server" required></asp:TextBox>                           
                            
                            </div>
                        </div>
                        <div class='row'>
                        <div class='col-md-6' ;>
                            <asp:Label class="label label-default" ID="Streetname" runat="server" Text="Streetname :"></asp:Label>
                             <asp:TextBox class="form-control" ID="tbStreetname" runat="server" required></asp:TextBox>       
                            </div>
                        </div>
                        <div class='row'>
                        <div class='col-md-6' ;>
                            <asp:Label class="label label-default" ID="Housenumber" runat="server" Text="Housenumber :"></asp:Label>
                             <asp:TextBox class="form-control" ID="tbHousenumber" runat="server" required></asp:TextBox>       
                            </div>
                        </div>
                        <div class='row'>
                        <div class='col-md-6' ;>
                            <asp:Label class="label label-default" ID="Zipcode" runat="server" Text="Zipcode :"></asp:Label>
                             <asp:TextBox class="form-control" ID="tbZipcode" runat="server" required></asp:TextBox>       
                            </div>
                        </div>
                        <div class='row'>
                        <div class='col-md-6' ;>
                            <asp:Label class="label label-default" ID="City" runat="server" Text="City :"></asp:Label>
                             <asp:TextBox class="form-control" ID="tbCity" runat="server" required></asp:TextBox>       
                            </div>
                        </div>
                        <div class='row'>
                        <div class='col-md-6' ;>
                            <asp:Label class="label label-default" ID="PhoneNumber" runat="server" Text="PhoneNumber :"></asp:Label>
                            <asp:TextBox class="form-control" ID="tbPhoneNumber" runat="server" required></asp:TextBox>       
                            </div>
                        </div>
                         <div class='row'>
                        <div class='col-md-6' ;>
                            <asp:Label class="label label-default" ID="IBAN" runat="server" Text="IBAN :"></asp:Label>
                             <asp:TextBox class="form-control" ID="tbIBAN" runat="server" required></asp:TextBox>       
                            </div>
                        </div>
                         <div class='row'>
                        <div class='col-md-6' ;>
                            <asp:Label class="label label-default" ID="Email" runat="server" Text="Email :"></asp:Label>
                             <asp:TextBox class="form-control" ID="tbEMail" runat="server" required></asp:TextBox>       
                            </div>
                        </div>
                         <div class='row'>
                        <div class='col-md-6' ;>
                            <asp:Label class="label label-default" ID="Password" runat="server" Text="Password :"></asp:Label>
                             <asp:TextBox class="form-control" ID="tbPassword" runat="server" required></asp:TextBox>       
                            </div>
                        </div>
                          <div class='row'>
                        <div class='col-md-6' ;>
                            <asp:Label class="label label-default" ID="Label1" runat="server" Text="LoginCode :"></asp:Label>
                             <asp:TextBox class="form-control" ID="tbloginCode" runat="server" required></asp:TextBox>       
                            </div>
                        </div>
                         <div class='row'>
                        <div class='col-md-6' ;>
                        <asp:Button class="btn btn-default" ID="btnCreate" runat="server" Text="Create" OnClick="btnCreate_Click" />
                               </div>
                        </div>
                                    </div>
                     </div>
                </form>                      
</body>
</html>
