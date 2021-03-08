<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="noteDetails.aspx.cs" Inherits="Lab2.noteDetails" %>
<%--Kirsi And Josh Coleman 2/15/21--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="container col-6">
        <div class="card">
            <div class="card-header text-center">
                <asp:Label ID="lblViewNote" runat="server" Text="View Note" Class="h3 m-2"></asp:Label>
            </div>
            <div class="card-body">
                    <asp:TextBox ID="txtNoteTitle" runat="server" Placeholder="Note Title" class="form-control" ReadOnly="true"></asp:TextBox>
                    <asp:TextBox ID="txtNoteBody" runat="server" TextMode="MultiLine" Placeholder="Note Body" class="form-control" ReadOnly="true"></asp:TextBox>
            </div>
        </div>
    </div>
</asp:Content>
