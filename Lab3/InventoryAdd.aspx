<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="InventoryAdd.aspx.cs" Inherits="Lab1.AddInventory" %>
<%--Kirsi And Josh Coleman 2/15/21--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container col-6">
        <div class="card">
            <div class="card-header text-center">
                <asp:Label ID="lblAddInventory" runat="server" Text="Add Inventory" Class="h3 m-2"></asp:Label>
            </div>

            <div class="card-body form-group">

                <div class="form-group">
                    <asp:Label ID="lblServices" runat="server" Text="Services" for="ddlServices"></asp:Label>
                    <asp:DropDownList ID="ddlServices" runat="server" class="dropdown" AutoPostBack="true" OnSelectedIndexChanged="ddlServices_SelectedIndexChanged"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"
                        ControlToValidate="ddlServices" Text="Select a Service" ValidationGroup="inventory"></asp:RequiredFieldValidator>
                </div>


                <div class="form-group">
                    <asp:TextBox ID="txtItemName" runat="server" Placeholder="Item Name" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvItemName" runat="server"
                        ErrorMessage="RequiredFieldValidator" ControlToValidate="txtItemName"
                        Text="Please Enter An Item Name." ValidationGroup="inventory">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="form-group">
                    <asp:TextBox ID="txtItemValuation" runat="server" Placeholder="Item Valuation" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvItemValuation" runat="server"
                        ErrorMessage="RequiredFieldValidator" ControlToValidate="txtItemValuation"
                        Text="Please Enter An Item Valuation." ValidationGroup="inventory">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="valuationDecimal" runat="server" ErrorMessage="CompareValidator" Operator="DataTypeCheck"
                        ControlToValidate="txtItemValuation" Type="Currency" ValidationGroup="inventory" Text="Invalid Currency Input"></asp:CompareValidator>
                </div>

                <div class="form-group">
                    <asp:Label ID="lblCurrentItems" runat="server" Text="Current Items in Inventory" For="txtCurrentItems"></asp:Label>
                    <asp:TextBox ID="txtCurrentItems" runat="server" TextMode="MultiLine" Placeholder="No Items In Inventory" class="form-control mb-2" ReadOnly="True"> </asp:TextBox>
                </div>

                <asp:Label ID="outputLbl" runat="server" Text=""></asp:Label>

                <div class="form-group">
                    <div class="d-flex justify-content-around">
                        <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-primary btn-lg" OnClick="btnClear_Click" CausesValidation="false" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary  btn-lg" OnClick="btnSave_Click" ValidationGroup="inventory" />
                        <asp:Button ID="btnPopulate" runat="server" Text="Populate" class="btn btn-secondary btn-lg" OnClick="btnPopulate_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
