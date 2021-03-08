<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="UserRequestService.aspx.cs" Inherits="Lab3.UserRequestService" %>
<%--Kirsi And Josh Coleman--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container col-6">
        <div class="card">
            <div class="card-header text-center">
                <asp:Label ID="lblRequestService" runat="server" Text="Request Service" Class="display-4 m-2"></asp:Label>
            </div>
            <div class="card-body">
                <asp:DropDownList ID="ddlServiceType" runat="server" AutoPostBack="true" class="dropdown form-control col-4 mb-3">
                    <asp:ListItem Text="Move" Value="M"></asp:ListItem>
                    <asp:ListItem Text="Auction" Value="A"></asp:ListItem>
                </asp:DropDownList>

                <div class="row">
                    <div class="col-2">
                        <asp:Label ID="lblserviceDeadline" runat="server" Text="Deadline "></asp:Label>
                    </div>
                    <div class="form-group col-5">
                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date" for="txtStartDate"></asp:Label>
                        <asp:TextBox ID="txtStartDate" runat="server" Placeholder="Start Date" class="form-control" TextMode="DateTimeLocal"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server"
                            ErrorMessage="RequiredFieldValidator" ControlToValidate="txtStartDate"
                            Text="Please Select a Start Date" ValidationGroup="CreateCustomer">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group col-5">
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date" for="txtEndDate"></asp:Label>
                        <asp:TextBox ID="txtEndDate" runat="server" Placeholder="End Date" class="form-control" TextMode="DateTimeLocal"></asp:TextBox>
                        <asp:CustomValidator ID="dateValidation" runat="server" Text="Start Date Must Be Before End Date" ErrorMessage="CustomValidator"
                            OnServerValidate="dateValidation_ServerValidate" ValidationGroup="CreateCustomer"></asp:CustomValidator>
                    </div>
                </div>

                <asp:TextBox ID="txtNoteBody" runat="server" TextMode="MultiLine" Placeholder="Customer Notes" class="form-control"></asp:TextBox>

                <div class="d-flex justify-content-around">
                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" Class="btn btn-primary" ValidationGroup="CreateCustomer" />
                </div>
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
