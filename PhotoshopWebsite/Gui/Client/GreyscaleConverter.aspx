<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GreyscaleConverter.aspx.cs" Inherits="PhotoshopWebsite.Gui.Client.GreyscaleConverter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Grey Scale Converter</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <center>
            <h1>
                Image Converter</h1>
            Select Image:<asp:FileUpload ID="FileUpload1" runat="server" />
            <br />
            <asp:Button ID="btnUpload" runat="server" OnClick="btnUpload_Click" Text="Upload" />
            <br />
            <asp:Button ID="btnGreyScale" runat="server" OnClick="btnGreyScale_Click" Text="Convert To Grey Scale" />
            <asp:Button ID="btnSepia" runat="server" OnClick="btnSepia_Click" Text="Convert To Sepia" />
            <br />
            <asp:Image ID="Img" runat="server" Height="400px" Width="500px" />
            <br />
        </center>
        </form>
    </div>
    </body>