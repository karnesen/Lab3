<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="Lab2.LoginPage" %>
<%--Kirsi And Josh Coleman 2/15/21--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container col-4">
        <div class="card p-3">
            <asp:Label ID="Label1" runat="server" Text="Log in" Class="text-center h3 m-2"></asp:Label>
            <div class="form-group">
                <asp:TextBox ID="txtUsername" class="form-control" PlaceHolder="Username" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:TextBox ID="txtPassword" runat="server" class="form-control" PlaceHolder="Password" TextMode="Password"></asp:TextBox>
            </div>
            <div class="text-center">
                <asp:Button ID="btnLogin" runat="server" Class="btn btn-primary" OnClick="btnLogin_Click" Text="Log in" />
            </div>
            <asp:Label ID="lblLoginMessage" runat="server" Text=""></asp:Label>

        </div>
    </div>
</asp:Content>
