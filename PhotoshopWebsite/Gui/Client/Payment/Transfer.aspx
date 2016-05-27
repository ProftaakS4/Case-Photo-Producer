<%@ Page Language="C#" MasterPageFile="../PhotoshopMaster.Master" AutoEventWireup="true" CodeBehind="Transfer.aspx.cs" Inherits="PhotoshopWebsite.Gui.Client.Payment.Transfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="panel-body" class="panel-bodybefore">
        <div class='container' style="margin-left: 13px;">
            <div class='row'>                
                <h3>Account Info</h3>
                <span>
                        <asp:Label class="label label-default" ID="Firstname" runat="server" Text="Firstname :"></asp:Label>
                        <asp:TextBox class="form-control" ID="tbFirstname" runat="server"></asp:TextBox>                           
                    </span><br />
                    <span>
                        <asp:Label class="label label-default" ID="Lastname" runat="server" Text="Lastname :"></asp:Label>
                        <asp:TextBox class="form-control" ID="tbLastname" runat="server"></asp:TextBox>                           
                    </span>  <br />
                    <span>
                        <asp:Label class="label label-default" ID="Streetname" runat="server" Text="Streetname :"></asp:Label>
                        <asp:TextBox class="form-control" ID="tbStreetname" runat="server"></asp:TextBox> 
                    </span>  <br />      
                    <span>
                        <asp:Label class="label label-default" ID="Housenumber" runat="server" Text="Housenumber :"></asp:Label>
                        <asp:TextBox class="form-control" ID="tbHousenumber" runat="server"></asp:TextBox>       
                    </span>  <br />
                    <span>
                        <asp:Label class="label label-default" ID="Zipcode" runat="server" Text="Zipcode :"></asp:Label>
                        <asp:TextBox class="form-control" ID="tbZipcode" runat="server"></asp:TextBox>       
                    </span>  <br />
                    <span>
                    <asp:Label class="label label-default" ID="City" runat="server" Text="City :"></asp:Label>
                    <asp:TextBox class="form-control" ID="tbCity" runat="server"></asp:TextBox>       
                    </span>  <br />
                    <span>
                        <asp:Label class="label label-default" ID="PhoneNumber" runat="server" Text="PhoneNumber :"></asp:Label>
                        <asp:TextBox class="form-control" ID="tbPhoneNumber" runat="server"></asp:TextBox>   
                    </span>  <br />    
                    <span>
                        <asp:Label class="label label-default" ID="IBAN" runat="server" Text="IBAN :"></asp:Label>
                        <asp:TextBox class="form-control" ID="tbIBAN" runat="server"></asp:TextBox>      
                    </span>  <br /> 
                    <span>
                        <asp:Label class="label label-default" ID="EMail" runat="server" Text="EMail :"></asp:Label>
                        <asp:TextBox class="form-control" ID="tbEMail" runat="server"></asp:TextBox>   
                    </span>  <br /> 
                <h3>Order Info</h3>
                <asp:Panel ID="pnlOrderInfo" runat="server"></asp:Panel>
                <h3>Confirm order and shipping address</h3> 
        </div>
    </div>
    
</asp:Content>