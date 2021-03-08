<%@ Page Title="" Language="C#" MasterPageFile="~/CustomerMaster.Master" AutoEventWireup="true" CodeBehind="NewUser.aspx.cs" Inherits="Lab3.NewUser" %>
<%--Kirsi And Josh Coleman--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container col-6">
        <div class="card">
            <div class="card-header">
                New Account
            </div>
            <div class="card-body">

                <div class="form-group">
                    <asp:TextBox ID="txtFirstName" runat="server" Placeholder="First Name" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server"
                        ErrorMessage="RequiredFieldValidator" ControlToValidate="txtFirstName"
                        Text="Please Enter A First Name." ValidationGroup="CreateCustomer">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="form-group">
                    <asp:TextBox ID="txtLastName" runat="server" Placeholder="Last Name" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server"
                        ErrorMessage="RequiredFieldValidator" ControlToValidate="txtLastName"
                        Text="Please Enter A Last Name." ValidationGroup="CreateCustomer">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="form-group">
                    <asp:TextBox ID="txtUsername" runat="server" Placeholder="email" class="form-control" TextMode="Email"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                        ErrorMessage="RequiredFieldValidator" ControlToValidate="txtUsername"
                        Text="Please Enter An Email." ValidationGroup="CreateCustomer">
                    </asp:RequiredFieldValidator>
                    <asp:CustomValidator ID="cvCheckUniqueCustomer" runat="server"
                            ErrorMessage="CustomValidator" OnServerValidate="cvCheckUniqueCustomer_ServerValidate"
                            ValidationGroup="CreateCustomer" Text="This Email is already in use"></asp:CustomValidator>
                </div>

                <div class="form-group">
                    <asp:TextBox ID="txtPassword" runat="server" Placeholder="Password" TextMode="Password" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server"
                        ErrorMessage="RequiredFieldValidator" ControlToValidate="txtPassword"
                        Text="Please Create A Password." ValidationGroup="CreateCustomer">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="row form-group">
                    <div class="col-md 9">
                        <asp:TextBox ID="txtPhoneNumber" runat="server" Placeholder="Phone Number" class="form-control" TextMode="Phone"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvPhoneNumber" runat="server"
                            ErrorMessage="RequiredFieldValidator" ControlToValidate="txtPhoneNumber"
                            Text="Please Enter A Phone Number." ValidationGroup="CreateCustomer">
                        </asp:RequiredFieldValidator>
                    </div>
                    <div class="col-md 2">
                        <asp:DropDownList ID="ddlPhoneNumberType" runat="server" class="dropdown form-control col-4 mb-1">
                            <asp:ListItem>Home</asp:ListItem>
                            <asp:ListItem>Cell</asp:ListItem>
                            <asp:ListItem>Work</asp:ListItem>
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RrfvPhoneType" runat="server"
                            ErrorMessage="RequiredFieldValidator" ControlToValidate="ddlPhoneNumberType"
                            Text="Please Select Phone Type." ValidationGroup="CreateCustomer">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>


                <div class="form-group">
                    <asp:TextBox ID="txtAddress" runat="server" Placeholder="Address" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server"
                        ErrorMessage="RequiredFieldValidator" ControlToValidate="txtAddress"
                        Text="Please Enter An Address." ValidationGroup="CreateCustomer">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="row form-group">
                    <div class="col-md-6">
                        <asp:TextBox ID="txtCity" runat="server" Placeholder="City" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCity" runat="server"
                            ErrorMessage="RequiredFieldValidator" ControlToValidate="txtCity"
                            Text="Please Enter A City." ValidationGroup="CreateCustomer">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlState" runat="server" class="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvState" runat="server"
                            ErrorMessage="RequiredFieldValidator" ControlToValidate="ddlState"
                            Text="Please Select A State." ValidationGroup="CreateCustomer">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-4">
                        <asp:TextBox ID="txtZipCode" runat="server" Placeholder="Zip" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvZipCode" runat="server"
                            ErrorMessage="RequiredFieldValidator" ControlToValidate="txtZipCode"
                            Text="Please Enter Zip Code." ValidationGroup="CreateCustomer">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="d-flex justify-content-around">
                    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" Class="btn btn-primary" ValidationGroup="CreateCustomer" />
                </div>
                <asp:Label ID="lblStatus" runat="server"></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
