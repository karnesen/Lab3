<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Assign.aspx.cs" Inherits="Lab1.WebForm1" %>
<%--Kirsi And Josh Coleman 2/15/21--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container col-6">
        <div class="card">
            <div class="card-header text-center">
                <asp:Label ID="lblAssignToService" runat="server" Text="Assign to Service" Class="h3 m-2"></asp:Label>
            </div>

            <div class="card-body form-group">
                <div class="form-group">
                    <asp:Label ID="lblServices" runat="server" Text="Services" for="ddlServices"></asp:Label>
                    <asp:DropDownList ID="ddlServices" runat="server" class="dropdown"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"
                        ControlToValidate="ddlServices" Text="Select a Service" ValidationGroup="Assign"></asp:RequiredFieldValidator>
                </div>

                <div class="form-group">
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged"> 
                        <asp:ListItem Text="Equipment" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Employee" Value="2"></asp:ListItem>
                    </asp:RadioButtonList>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblEmployees" runat="server" Text="Employee" for="ddlEmployees" Visible="false"></asp:Label>
                    <asp:DropDownList ID="ddlEmployees" runat="server" class="dropdown" Visible="false"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvEmployee" runat="server" ErrorMessage="RequiredFieldValidator"
                        ControlToValidate="ddlEmployees" Text="Select an Employee" ValidationGroup="Assign"></asp:RequiredFieldValidator>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblEquipment" runat="server" Text="Equipment" for="ddlEquipment" Visible="false"></asp:Label>
                    <asp:DropDownList ID="ddlEquipment" runat="server" class="dropdown" Visible="false"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="rfvEquipment" runat="server" ErrorMessage="RequiredFieldValidator"
                        ControlToValidate="ddlEmployees" Text="Select Equipment" ValidationGroup="Assign"></asp:RequiredFieldValidator>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblNotes" runat="server" Text="Notes" Visible="false"></asp:Label>
                    <asp:TextBox ID="txtNotes" runat="server" Visible="false"></asp:TextBox>
                </div>

                <div class="form-group">
                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date" for="txtStartDate" Visible="false"></asp:Label>
                        <asp:TextBox ID="txtStartDate" runat="server" class="form-control" TextMode="Date" Visible="false"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server"
                            ErrorMessage="RequiredFieldValidator" ControlToValidate="txtStartDate"
                            Text="Please Enter A Start Date." ValidationGroup="Assign" Enabled="false">
                        </asp:RequiredFieldValidator>
                    </div>

                <asp:Label ID="outputLbl" runat="server" Text=""></asp:Label>

                <div class="form-group">
                    <div class="d-flex justify-content-around">
                        <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-primary btn-lg" OnClick="btnClear_Click" CausesValidation="false"/>
                        <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary  btn-lg" OnClick="btnSave_Click" ValidationGroup="Assign"/>
                        <asp:Button ID="btnPopulate" runat="server" Text="Populate" class="btn btn-secondary btn-lg" OnClick="btnPopulate_Click" CausesValidation="false"/>
                    </div>
                </div>
            </div>
        </div>
        
    </div>
</asp:Content>
