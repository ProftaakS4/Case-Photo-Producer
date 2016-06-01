<%@ Page Title="" Language="C#" MasterPageFile="~/Gui/Photographer/PhotographerMaster.Master" AutoEventWireup="true" CodeBehind="Pictures.aspx.cs" Inherits="PhotoshopWebsite.Gui.Photographer.Pictures" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Bootstrap/Scripts/jquery.min.js"></script>
    <script src="../../Bootstrap/Scripts/jquery.Jcrop.js"></script>
    <link href="../../Bootstrap/Content/jquery.Jcrop.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="panel-body" class="panel-bodybefore">
        <div class='container' style="margin-left: 0px; margin-right: 0px;">
            <div class='row'>
                <asp:Panel ID="pnlProduct" runat="server"></asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
