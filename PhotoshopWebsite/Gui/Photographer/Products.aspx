<%@ Page Title="" Language="C#" MasterPageFile="~/Gui/Photographer/PhotographerMaster.Master" AutoEventWireup="true" CodeBehind="Products.aspx.cs" Inherits="PhotoshopWebsite.Gui.Photographer.Selection" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div ID="panel-body" class="panel-bodybefore">
        <div class='container' style="margin-left:13px;">
            <div class='row'>
                <asp:Panel ID="pnlProducts" runat="server"></asp:Panel>   
            </div>
        </div>
    </div>
</asp:Content>
