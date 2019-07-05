<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UNLookup.aspx.cs" Inherits="UNLookup.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
            font-family: Calibri;
        }
        .auto-style2 {
            text-align: center;
        }
        .auto-style5 {
            font-family: Calibri;
        }
        .auto-style6 {
            color: #FF0000;
        }
        .auto-style7 {
            color: #FF0000;
            font-family: Calibri;
        }
        .auto-style8 {
            font-family: Calibri;
            margin-left: 0px;
        }

        .auto-style9 {
            padding: 10px;
        }

        .auto-style10 {
            font-family: Calibri;
            margin-left: 4px;
        }

        .fieldset_class {
        border: 1px solid pink;
    }

        .auto-style11 {
            text-align: center;
            font-family: Calibri;
            font-size: large;
        }

        </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Image ID="trinrelogo" runat="server" ImageUrl="~/trinrelogo.png" CssClass="auto-style5" />
        </div>
        <p class="auto-style11">
            <strong>UN Consolidated List Search</strong></p>
        
        <fieldset class="fieldset_class">
            <legend class="auto-style5">Search Criteria</legend>
            <div class="auto-style9">
        <div class="auto-style2">
            <span class="auto-style5">&nbsp;</span>
            <asp:DropDownList ID="dd_type" runat="server" CssClass="auto-style10" Height="21px" Width="146px">
                <asp:ListItem>Select Client Type</asp:ListItem>
                <asp:ListItem>Individual</asp:ListItem>
                <asp:ListItem>Entity</asp:ListItem>
            </asp:DropDownList>
            <br />
            <asp:RequiredFieldValidator runat="server" ID="reqradio" ControlToValidate="dd_type" InitialValue="Select Client Type" ErrorMessage="Type is required" CssClass ="auto-style7" />
        </div>
        <p class="auto-style1">
            Enter Name <asp:TextBox ID="namesearch" runat="server" CssClass="auto-style8" Width="211px"></asp:TextBox>
        </p>
        <p class="auto-style1">
            <asp:RequiredFieldValidator runat="server" id="reqName" ControlToValidate="namesearch" ErrorMessage="Name is required" CssClass="auto-style6" />
            &nbsp;&nbsp;&nbsp; </p>
                <p class="auto-style1">
                    <span class="auto-style5">&nbsp;</span><asp:Button ID="btn_search" runat="server" CssClass="auto-style8" OnClick="btn_search_Click" Text="Search" Width="129px" />
                </p>
                </div>
        </fieldset>


        <fieldset class="fieldset_class">
            <legend class="auto-style5">UN Search Results</legend>
            <div class="auto-style9">
        <p>
            <asp:Label ID="lbl_namematch" runat="server" CssClass="auto-style5" Font-Bold="True" Text="Name Match"></asp:Label>
        </p>
        <p class="auto-style5">
            <asp:GridView ID="dt_namematch" runat="server">
            </asp:GridView>
        </p>
        <asp:Label ID="resultstext" runat="server" CssClass="auto-style5" Text="Label"></asp:Label>
        <br class="auto-style5" />
        <br class="auto-style5" />
        <asp:Label ID="lbl_aliasmatch" runat="server" CssClass="auto-style5" Font-Bold="True" Text="Alias Match"></asp:Label>
        <br class="auto-style5" />
        <asp:GridView ID="dt_aliasmatch" runat="server">
        </asp:GridView>
        <br class="auto-style5" />
        <asp:Label ID="aliastext" runat="server" CssClass="auto-style5" Text="Label"></asp:Label>
                </div>
        </fieldset><span class="auto-style5"> </span>
    </form>
    </body>
</html>
