<%@ Page Title="" Language="C#" MasterPageFile="PhotoshopMaster.Master" AutoEventWireup="true" CodeBehind="Mainstore.aspx.cs" Inherits="PhotoshopWebsite.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div ID="panel-body" class="panel-bodybefore">
        <div class='container' style="margin-left:0px; margin-right:0px;">
            <div class='row'>
              <form runat="server">
                <asp:Panel ID="pnlProduct" runat="server"></asp:Panel>    
              </form>                    
            </div>
        </div>
    </div>                  
</asp:Content>
