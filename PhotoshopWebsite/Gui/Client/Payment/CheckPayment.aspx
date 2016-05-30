<%@ Page Language="C#" MasterPageFile="../PhotoshopMaster.Master" AutoEventWireup="true" CodeBehind="CheckPayment.aspx.cs" Inherits="PhotoshopWebsite.Gui.Client.Payment.CheckPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content  ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">   
 <div id="panel-body" class="panel-bodybefore">
        <div class='container' style="margin-left: 13px;">
            <div class='row'>
                <asp:Panel ID="pnlWait" runat="server"></asp:Panel>                
            </div>
        </div>        
    </div>    
</asp:Content>
