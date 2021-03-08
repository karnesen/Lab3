<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="createNotes.aspx.cs" Inherits="Lab2.createNotes" %>
<%--Kirsi And Josh Coleman 2/15/21--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container col-6">
        <div class="card">
            <div class="card-header text-center">
                <asp:Label ID="lblCreateNewCustomer" runat="server" Text="Create New Note" Class="h3 m-2"></asp:Label>
            </div>
            <div class="card-body">
                <div class=" form-group">
                    <asp:DropDownList ID="ddlServices" runat="server" Class="dropdown form-control mb-3" ></asp:DropDownList>

                    <asp:TextBox ID="txtNoteTitle" runat="server" Placeholder="Note Title" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNoteTitle" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtNoteTitle" Text="Please create a title."></asp:RequiredFieldValidator>

                    <asp:TextBox ID="txtNoteBody" runat="server" TextMode="MultiLine" Placeholder="Note Body" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNoteBody" runat="server" ErrorMessage="RequiredFieldValidator" ControlToValidate="txtNoteBody" Text="Please enter note body."></asp:RequiredFieldValidator>

                </div>

                <div class="form-group">
                    <div class="d-flex justify-content-around">
                        <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-primary btn-lg" OnClick="btnClear_Click" CausesValidation="false"/>
                        <asp:Button ID="btnCreateNote" runat="server" Text="Create" OnClick="btnCreateNote_Click" class="btn btn-primary btn-lg" />
                        <asp:Button ID="btnPopulate" runat="server" Text="Populate" class="btn btn-secondary btn-lg" OnClick="btnPopulate_Click" CausesValidation="false"/>
                    </div>
                </div>
             
                <asp:Label ID="lblStatus" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
