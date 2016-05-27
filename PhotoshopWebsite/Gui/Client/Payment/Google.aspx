<%@ Page Language="C#" MasterPageFile="../PhotoshopMaster.Master" AutoEventWireup="true" CodeBehind="Google.aspx.cs" Inherits="PhotoshopWebsite.Gui.Client.Payment.Google" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="panel-body" class="panel-bodybefore">
        <div class='container' style="margin-left: 13px;">
            <div class='row'>
                <asp:Panel ID="pnlProductInfo" runat="server"></asp:Panel>
                <asp:Panel ID="pnlUserInfo" runat="server"></asp:Panel>
                <asp:Panel ID="pnlPaymentInfo" runat="server"></asp:Panel>
            </div>
        </div>        
    </div>
    
</asp:Content>
