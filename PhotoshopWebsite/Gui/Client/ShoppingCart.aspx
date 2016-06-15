<%@ Page Title="" Language="C#" MasterPageFile="PhotoshopMaster.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="PhotoshopWebsite.Gui.ShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <script type="text/javascript">
    function isReferenceNumberAvailable()
{    
        var rbuttons = document.getElementsByName("rate");
    
        for (var elem in rbuttons)
        {
            if (rbuttons[elem].checked)
            {
                alert(rbuttons[elem].value);  // got the element which is checked      
                if (rbuttons[elem].checked) {
                    document.getElementById("myModalLabel").innerHTML = rbuttons[elem].value;
                        var bank = document.getElementById("paymentMenu");
                        alert(bank[type].value)
                }
                else if (rbuttons[elem].value == "no")
                    alert("don't create ");
            }
        }
    }
    $(function () {

        $(".dropdown-menu li a").click(function () {

            $("#dropdown:first-child").text($(this).text());
            $("#dropdown:first-child").val($(this).text());

        });

    });
       </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="panel-body" class="panel-bodybefore">
        <div class='container' style="margin-left: 13px;">
            <div class='row'>
                <asp:Panel ID="pnlProduct" runat="server"></asp:Panel>
                <h4 style="font-weight: bold"><asp:Literal runat="server" Text="<%$ Resources:LocalizedText, choose_payment_method%>" /></h4>
                <div class="well" style="background-color: #96C8FA; width: 600px; height: 100%; padding-right: 30px; padding-left: 30px;">
                    <div class="row">
                        <div class="col-md-12" style='background-color: white; border-radius: 7px; height: 50px; padding: 10px; margin-bottom: 15px;'>
                            <input type="radio" id="rbPaymentTransfer" name="rate" value="paymentTransfer"> 

                            <asp:Image ID="imgPaymentTransfer" ImageUrl="http://www.glerups.nl/media/wysiwyg/infortis/ultimo/custom/overboeking.jpg" runat="server" Class="img-responsive" Style="width: 86px; height: 35px; margin-left: 30px; display: inline" />
                            <asp:Label runat="server" Text="<%$ Resources:LocalizedText, bank_transfer%>" Style="display: inline"></asp:Label>
                        </div>
                        <div class="col-md-12" style='background-color: white; border-radius: 7px; height: 50px; padding: 10px; margin-bottom: 15px;'>
                            <input type="radio" id="rbPaypal" name="rate" value="payPal"> 
                            <asp:ImageButton  onclick="btnPaypal_Click1" ID="btnPaypal" src="../Images/Payment/Paypal.jpg"  runat="server" CssClass="img-responsive" Style="width: 53px; height: 35px; margin-left: 20px; display: inline" />
                        </div>
                        <div class="col-md-12" style='background-color: white; border-radius: 7px; height: 100px; padding: 10px; margin-bottom: 15px;'>
                            <input type="radio" id="rbIdeal" name="rate" value="ideal"> 
                            <asp:Image ID="imgPaymentIdeal" ImageUrl="../Images/Payment/ideal.jpg" runat="server" CssClass="img-responsive" Style="width: 42px; height: 35px; margin-left: 20px; display: inline" />
                            <asp:Label ID="lblIdeal" runat="server" Text="<%$ Resources:LocalizedText, ideal%>" Font-Bold="true" Style="display: inline"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text="<%$ Resources:LocalizedText, ideal_description%>" Font-Bold="false" Style="display: inline; color: darkseagreen"></asp:Label>
                                 <div class="dropdown">
                                    <button id="dropdown" class="btn btn-default dropdown-toggle" type="button"  data-toggle="dropdown"><asp:Literal runat="server" Text="<%$ Resources:LocalizedText, choose_bank_prompt%>" />
                                    <span class="caret"></span></button>
                                      <ul class="dropdown-menu" role="menu" aria-labelledby="menu1">
                                      <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Rabobank</a></li>
                                      <li role="presentation"><a role="menuitem" tabindex="-1" href="#">SnsBank</a></li>
                                      <li role="presentation"><a role="menuitem" tabindex="-1" href="#">ABNamro</a></li>
                                    </ul>
                                  </div>
                        </div>
                        <div class="col-md-12" style='background-color: white; border-radius: 7px; height: 50px; padding: 10px; margin-bottom: 15px;'>
                            <input type="radio" id="rbPaymentGoogle" name="rate" value="google"> 
                            <asp:Image ID="imgPaymentGoogle" src="../Images/Payment/google_checkout.png" runat="server" CssClass="img-responsive" Style="width: 97px; height: 32px; margin-left: 20px; display: inline" />
                        </div>
                        <div class="col-md-12" style='background-color: white; border-radius: 7px; height: 50px; padding: 10px; margin-bottom: 15px;'>
                            <input type="radio" id="rbPaymentOgone" name="rate" value="ogone"> 
                            <asp:Image ID="imgPaymentOgone" ImageUrl="https://tctechcrunch2011.files.wordpress.com/2012/07/87407v3-max-250x250.jpg" runat="server" CssClass="img-responsive" Style="width: 74px; height: 35px; margin-left: 20px; display: inline" />
                        </div>
                    </div>
                </div>
                <div class="row" style="background-color: #3B9BC4; width: 600px; padding-left: 5px; margin-left: 1px; border-radius: 7px;">
                    <div class="col-md-12" style="border-radius: 7px; height: 50px; padding: 10px;">
                        <button type='button' id='btnOrder' class='btn btn-primary' style="background-color:#1E8C1B" data-toggle='modal'  onclick="isReferenceNumberAvailable();" data-target='#myModal'><asp:Literal runat="server" Text="<%$ Resources:LocalizedText, place_order%>" /></button>
                    </div>
                    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="myModalLabel"></h4>
                                </div>
                                <div class="modal-body">
                                    ...
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-default" data-dismiss="modal"><asp:Literal runat="server" Text="<%$ Resources:LocalizedText, close%>" /></button>
                                    <button type="button" class="btn btn-primary"><asp:Literal runat="server" Text="<%$ Resources:LocalizedText, save_changes%>" /></button>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
