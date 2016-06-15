<%@ Page Title="" Language="C#" MasterPageFile="PhotoshopMaster.Master" AutoEventWireup="true" CodeBehind="ShoppingCart.aspx.cs" Inherits="PhotoshopWebsite.Gui.ShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        //global variables
        var userresult;
        var orderresult;
        var orderName = "Photo Shop Order";
        var totalAmount;

        //Onload function getting client and shipping data from the Webservice
        window.onload = function (e) {
            e.preventDefault();

            sendRequest("WebService.asmx/getUserData", "OnSuccessUser", "POST");
            sendRequest("WebService.asmx/getOrderData", "OnSuccessOrder", "POST");
        };

        //Sends requests to the webService based on the parameteres given in the onload function
        function sendRequest(Url, successmethod, requesttype) {
            var type;
            if (successmethod == "OnSuccessUser") {
                type = OnSuccessUser;
            }
            else {
                type = OnSuccessOrder;
            }
            $.ajax({
                type: requesttype,
                async: true,
                //getUserData is my webmethod   
                url: Url,
                contentType: "application/json; charset=utf-8",
                dataType: "json", // dataType is json format
                success: type,
                error: OnErrorCall
            });

            //Different fucntions setting the global variables
            function OnSuccessUser(response) {
                window.userresult = JSON.parse(response.d);
                console.log(response.d);
            }
            //Different fucntions setting the global variables
            function OnSuccessOrder(response) {
                window.orderresult = JSON.parse(response.d);
                console.log(response.d);
            }

            function OnErrorCall(response) { console.log(response.error); }
        }



        //Function generating the modal content based upon the radiobutton checked. 
        function generateModal(method, orderName, totalAmount) {

            document.getElementById("myModalLabel").innerHTML = method;
            document.getElementById("Amountlabel").innerHTML = "Price: €" + totalAmount;
            document.getElementById("IBAN").setAttribute("value", userresult["IBAN"]);
            document.getElementById("Firstname").setAttribute("value", userresult["Firstname"]);
            document.getElementById("Lastname").setAttribute("value", userresult["Lastname"]);
            document.getElementById("Streetname").setAttribute("value", userresult["Streetname"]);
            document.getElementById("Housenumber").setAttribute("value", userresult["Housenumber"]);
            document.getElementById("City").setAttribute("value", userresult["City"]);
            document.getElementById("Phonenumber").setAttribute("value", userresult["Phonenumber"]);
            document.getElementById("IBAN").setAttribute("value", userresult["IBAN"]);
            document.getElementById("Emailaddress").setAttribute("value", userresult["Emailaddress"]);


        }

        //Function triggered by the  "Place order" button, checking which button is checked and launching the modal
        function isReferenceNumberAvailable() {

            var rbuttons = document.getElementsByName("rate");
            for (var elem in rbuttons) {
                if (rbuttons[elem].checked) {
                    var totalAmount = document.getElementById("ContentPlaceHolder1_tdTotalAmount").textContent;
                    totalAmount = totalAmount.replace("€", "");
                    totalAmount = totalAmount.replace(",", ".");

                    if (rbuttons[elem].value == "payPal") {

                        window.location.assign("https://www.paypal.com/us/cgi-bin/webscr?cmd=_xclick&business=stanniez%40live%2enl&item_name=" + orderName + "&currency_code=EUR&amount=" + totalAmount);
                    }
                    var method = document.getElementById("myModalLabel").innerHTML = rbuttons[elem].value;
                    generateModal(method, orderName, totalAmount);
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
    <%-- Page body --%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="panel-body" class="panel-bodybefore">
        <div class='container' style="margin-left: 13px;">
            <div class='row'>
                <asp:Panel ID="pnlProduct" runat="server"></asp:Panel>
                <h4 style="font-weight: bold">Kies uw betaalmethode:</h4>
                <div class="well" style="background-color: #96C8FA; width: 600px; height: 100%; padding-right: 30px; padding-left: 30px;">
                    <div class="row">
                        <div class="col-md-12" style='background-color: white; border-radius: 7px; height: 50px; padding: 10px; margin-bottom: 15px;'>
                            <input type="radio" id="rboverboeking" name="rate" value="overboeking">

                            <asp:Image ID="imgPaymentTransfer" ImageUrl="http://www.glerups.nl/media/wysiwyg/infortis/ultimo/custom/overboeking.jpg" runat="server" Class="img-responsive" Style="width: 86px; height: 35px; margin-left: 30px; display: inline" />
                            <asp:Label ID="Label8" runat="server" Text="- Over boeking" Font-Bold="true" Style="display: inline"></asp:Label>
                            <asp:Label ID="Label9" runat="server" Text="Regel uw betaling via bankoverschrijving." Font-Bold="false" Style="display: inline; float: right; color: darkseagreen"></asp:Label>

                        </div>
                        <div class="col-md-12" style='background-color: white; border-radius: 7px; height: 50px; padding: 10px; margin-bottom: 15px;'>
                            <input type="radio" id="rbPaypal" name="rate" value="payPal">
                            <asp:ImageButton OnClick="btnPaypal_Click1" ID="btnPaypal" src="../Images/Payment/Paypal.jpg" runat="server" CssClass="img-responsive" Style="width: 53px; height: 35px; margin-left: 20px; display: inline" />
                            <asp:Label ID="Label6" runat="server" Text="- Paypal Nederland" Font-Bold="true" Style="display: inline"></asp:Label>
                            <asp:Label ID="Label7" runat="server" Text="Veilig online betalen of betaald worden" Font-Bold="false" Style="display: inline; float: right; color: darkseagreen"></asp:Label>
                        </div>
                        <div class="col-md-12" style='background-color: white; border-radius: 7px; height: 50px; padding: 10px; margin-bottom: 15px;'>
                            <input type="radio" id="rbIdeal" name="rate" value="ideal">
                            <asp:Image ID="imgPaymentIdeal" ImageUrl="../Images/Payment/ideal.jpg" runat="server" CssClass="img-responsive" Style="width: 42px; height: 35px; margin-left: 20px; display: inline" />
                            <asp:Label ID="lblIdeal" runat="server" Text="- Ideal" Font-Bold="true" Style="display: inline"></asp:Label>
                            <asp:Label ID="Label1" runat="server" Text="De Meest gebruikte betaal methode van Nederland" Font-Bold="false" Style="display: inline; float: right; color: darkseagreen"></asp:Label>

                        </div>
                        <div class="col-md-12" style='background-color: white; border-radius: 7px; height: 50px; padding: 10px; margin-bottom: 15px;'>
                            <input type="radio" id="rbPaymentGoogle" name="rate" value="google">
                            <asp:Image ID="imgPaymentGoogle" src="../Images/Payment/google_checkout.png" runat="server" CssClass="img-responsive" Style="width: 97px; height: 32px; margin-left: 20px; display: inline" />
                            <asp:Label ID="Label2" runat="server" Text="- Google Checkout" Font-Bold="true" Style="display: inline"></asp:Label>
                            <asp:Label ID="Label3" runat="server" Text="Fast checkout through google" Font-Bold="false" Style="display: inline; float: right; color: darkseagreen"></asp:Label>
                        </div>
                        <div class="col-md-12" style='background-color: white; border-radius: 7px; height: 50px; padding: 10px; margin-bottom: 15px;'>
                            <input type="radio" id="rbPaymentOgone" name="rate" value="ogone">
                            <asp:Image ID="imgPaymentOgone" ImageUrl="https://tctechcrunch2011.files.wordpress.com/2012/07/87407v3-max-250x250.jpg" runat="server" CssClass="img-responsive" Style="width: 74px; height: 35px; margin-left: 20px; display: inline" />
                            <asp:Label ID="Label4" runat="server" Text="- Ogone" Font-Bold="true" Style="display: inline"></asp:Label>
                            <asp:Label ID="Label5" runat="server" Text="An ingenico company" Font-Bold="false" Style="display: inline; float: right; color: darkseagreen"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class="row" style="background-color: #3B9BC4; width: 600px; padding-left: 5px; margin-left: 1px; border-radius: 7px;">
                    <div class="col-md-12" style="border-radius: 7px; height: 50px; padding: 10px;">
                        <button type='button' id='btnOrder' class='btn btn-primary' style="background-color: #1E8C1B" data-toggle='modal' onclick="isReferenceNumberAvailable();" data-target='#myModal'>Place order</button>
                    </div>


                    <%-- Modal --%>
                    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <h4 class="modal-title" id="myModalLabel"></h4>
                                </div>
                                <div class="modal-body">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="dropdown">
                                                <button id="dropdown" class="btn btn-default dropdown-toggle" type="button" data-toggle="dropdown">
                                                    Kies uw Bank
                                    <span class="caret"></span>
                                                </button>
                                                <ul class="dropdown-menu" role="menu" aria-labelledby="menu1">
                                                    <li role="presentation"><a role="menuitem" tabindex="-1" href="#">Rabobank</a></li>
                                                    <li role="presentation"><a role="menuitem" tabindex="-1" href="#">SnsBank</a></li>
                                                    <li role="presentation"><a role="menuitem" tabindex="-1" href="#">ABNamro</a></li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div class="col-lg-6">
                                            <h3>Personal information</h3>
                                            <input type="text" class="input-group" id="Firstname" readonly />
                                            <input type="text" class="input-group" id="Lastname" readonly />
                                            <input type="text" class="input-group" id="Streetname" readonly />
                                            <input type="text" class="input-group" id="Housenumber" readonly />
                                            <input type="text" class="input-group" id="City" readonly />
                                            <input type="text" class="input-group" id="Phonenumber" readonly />
                                            <input type="text" class="input-group" id="IBAN" readonly />
                                            <input type="text" class="input-group" id="Emailaddress" readonly />

                                        </div>
                                        <div class="col-lg-6">
                                            <h3>Order information</h3>
                                        </div>

                                    </div>
                                    <div class="modal-footer">
                                        <label class="x" id="Amountlabel" style="float: left"></label>
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Go back</button>
                                        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" class="btn btn-success" Text="Submit" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
</asp:Content>
