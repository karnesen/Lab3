<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="Lab3.UserLogin" %>
<%--Kirsi And Josh Coleman--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="container col-4">
            <div class="card p-3">
                <strong class="text-center">Login</strong><br />

                <div class="form-group">
                    <asp:TextBox ID="txtUsername" class="form-control" PlaceHolder="Username" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:TextBox ID="txtPassword" runat="server" class="form-control" PlaceHolder="Password" TextMode="Password"></asp:TextBox>
                </div>

                <asp:Button ID="btnLogin" runat="server" OnClick="btnLogin_Click" Text="Login" Class="btn btn-primary" />
                <br />
                <asp:Label ID="lblStatus" runat="server"></asp:Label>

                <asp:LinkButton ID="lnkCreate" runat="server" OnClick="lnkCreate_Click">Create User</asp:LinkButton>
            </div>
        </div>

    </div>
</asp:Content>
