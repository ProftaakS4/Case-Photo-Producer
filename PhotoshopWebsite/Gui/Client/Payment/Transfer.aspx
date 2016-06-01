<%@ Page Language="C#" MasterPageFile="PaymentMasterNested.master" AutoEventWireup="true" CodeBehind="Transfer.aspx.cs" Inherits="PhotoshopWebsite.Gui.Client.Payment.Transfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="nestedHeadContent" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="nestedContent" runat="server">
    <div id="panel-body" class="panel-bodybefore">
        <div class='container' style="margin-left: 13px;">
            <asp:Panel ID="pnlTransfer" runat="server"></asp:Panel>
        </div>
    </div>
</asp:Content>
