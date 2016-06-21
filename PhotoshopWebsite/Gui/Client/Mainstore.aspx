<%@ Page Title="" Language="C#" MasterPageFile="PhotoshopMaster.Master" AutoEventWireup="true" CodeBehind="Mainstore.aspx.cs" Inherits="PhotoshopWebsite.WebForm1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../../Bootstrap/Scripts/jquery.min.js"></script>
    <script src="../../Bootstrap/Scripts/jquery.Jcrop.js"></script>
    <link href="../../Bootstrap/Content/jquery.Jcrop.css" rel="stylesheet" />
    <script type="text/javascript">

        $(function () {

            $('.cropbox').Jcrop({
                onChange: showPreview,
                onChange: getCoords,
                onSelect: getCoords,
                onSelect: showPreview,
                onRelease: hidePreview
            });

            var $preview = $('.preview');
            var $tracker = document.getElementsByClassName("jcrop-tracker");
            var height = window.getComputedStyle($tracker, null).getPropertyValue("height");
            var width = window.getComputedStyle($tracker, null).getPropertyValue("width");

            function showPreview(coords) {
                if (parseInt(coords.w) > 0) {
                    var rx = 100 / coords.w;
                    var ry = 100 / coords.h;
                }
            }

            function hidePreview() {
                $preview.stop().fadeOut('fast');
            }
            function getCoords(coords) {

                $('#head_input_X').val(1.2 * coords.x);
                $('#head_input_Y').val(1.2 * coords.y);
                $('#head_input_W').val(1.2 * coords.w);
                $('#head_input_H').val(1.2 * coords.h);
                //$('#head_input_height').val(height);
                //$('#head_input_width').val(width);
            }
        });
    </script>
    <input id="input_X" type="hidden" runat="server" />
    <input id="input_Y" type="hidden" runat="server" />
    <input id="input_W" type="hidden" runat="server" />
    <input id="input_H" type="hidden" runat="server" />
    <%-- <input id="head_input_height" type="hidden" runat="server" />
    <input id="head_input_width" type="hidden" runat="server" />--%>
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
