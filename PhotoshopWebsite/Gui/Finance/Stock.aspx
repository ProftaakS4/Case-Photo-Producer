<%@ Page Title="" Language="C#" MasterPageFile="~/Gui/Finance/FinancialAdmin.Master" AutoEventWireup="true" CodeBehind="Stock.aspx.cs" Inherits="PhotoshopWebsite.Gui.Stock" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div ID="panel-body" class="panel-bodybefore">
        <div class='container' style="margin-left:13px;">
            <div class='row'>
                <asp:Panel ID="pnlCodes" runat="server"></asp:Panel>   
            </div>
        </div>
    </div>
</asp:Content>
