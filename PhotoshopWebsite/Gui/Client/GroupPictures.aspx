<%@ Page Title="" Language="C#" MasterPageFile="~/Gui/Client/PhotoshopMaster.Master" AutoEventWireup="true" CodeBehind="GroupPictures.aspx.cs" Inherits="PhotoshopWebsite.Gui.Client.GroupPictures" %>

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
                onRelease: hidePreview,
                aspectRatio: 1
            });

            var $preview = $('.preview');
            var $width = document.getElementsByClassName('jcrop-tracker').offSetWidth();

            function showPreview(coords) {
                if (parseInt(coords.w) > 0) {
                    var rx = 100 / coords.w;
                    var ry = 100 / coords.h;

                    $preview.css({
                        width: Math.round(rx * 300) + 'px',
                        height: Math.round(ry * 177) + 'px',
                        marginLeft: '-' + Math.round(rx * coords.x) + 'px',
                        marginTop: '-' + Math.round(ry * coords.y) + 'px'
                    }).show();
                }
            }

            function hidePreview() {
                $preview.stop().fadeOut('fast');
            }
            function getCoords(coords) {

                $('#head_input_X').val(coords.x);
                $('#head_input_Y').val(coords.y);
                $('#head_input_W').val(coords.w);
                $('#head_input_H').val(coords.h);
            }
        });
    </script>
    <input id="input_X" type="hidden" runat="server" />
    <input id="input_Y" type="hidden" runat="server" />
    <input id="input_W" type="hidden" runat="server" />
    <input id="input_H" type="hidden" runat="server" />
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

