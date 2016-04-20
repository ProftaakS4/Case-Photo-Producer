<%@ Page Title="" Language="C#" MasterPageFile="PhotoshopMaster.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="PhotoshopWebsite.Gui.ShoppingCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div ID="panel-body" class="panel-bodybefore">
        <div class='container' style="margin-left:13px;">
            <div class='row'>
                <asp:Panel ID="pnlProduct" runat="server"></asp:Panel>   
            </div>
        </div>
<form>
<form name="_xclick" action="https://www.paypal.com/us/cgi-bin/webscr" method="post" runat="server">
<input type="hidden" name="cmd" value="_xclick" />
<input type="hidden" name="business" value="stanniez@live.nl" />
<input type="hidden" name="currency_code" value="EUR" />
<input type="hidden" name="item_name" value="1.00" />
<input type="hidden" id="Amount" name="amount" value="Fotoo" placeholder="ENTER UR PRICE HERE" />
<input type="image" src="http://www.paypalobjects.com/en_US/i/btn/btn_buynow_LG.gif" border="0" name="submit" alt="Make payments with PayPal - it's fast, free and secure!" />
</form>
    </form>
    </div>
</asp:Content>
