<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="NewAuction.aspx.cs" Inherits="Lab3.NewAuction" %>
<%--Kirsi And Josh Coleman 2/15/21--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container col-6">
        <div class="card">
            <div class="card-header text-center">
                <asp:Label ID="lblCreateNewCustomer" runat="server" Text="Schedule an Auction" Class="h3 m-2"></asp:Label>
            </div>

            <div class="card-body form-group">
                <asp:Label ID="outputLbl" runat="server" Text=""></asp:Label>

                <%--Customer Dropdown--%>
                

                <div class="row">
                    <div class="form-group col-6">
                        <asp:Label ID="lblStartDate" runat="server" Text="Start Date" for="txtStartDate"></asp:Label>
                        <asp:TextBox ID="txtStartDate" runat="server" Placeholder="Start Date" class="form-control" TextMode="DateTimeLocal"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvStartDate" runat="server"
                            ErrorMessage="RequiredFieldValidator" ControlToValidate="txtStartDate"
                            Text="Please Select a Start Date" ValidationGroup="CreateService">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="form-group col-6">
                        <asp:Label ID="lblEndDate" runat="server" Text="End Date" for="txtEndDate"></asp:Label>
                        <asp:TextBox ID="txtEndDate" runat="server" Placeholder="End Date" class="form-control" TextMode="DateTimeLocal"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvEndDate" runat="server"
                            ErrorMessage="RequiredFieldValidator" ControlToValidate="txtEndDate"
                            Text="Please Select an End Date" ValidationGroup="CreateService">
                        </asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="dateValidation" runat="server" Text="Start Date Must Be Before End Date" ErrorMessage="CustomValidator"
                            OnServerValidate="dateValidation_ServerValidate" ValidationGroup="CreateService"></asp:CustomValidator>
                    </div>
                </div>

                <asp:Label ID="Address" runat="server" Text="Auction Address"></asp:Label>
                <div class="form-group">
                    <asp:TextBox ID="txtAddress" runat="server" Placeholder="Address" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvAddress" runat="server"
                        ErrorMessage="RequiredFieldValidator" ControlToValidate="txtAddress"
                        Text="Please Enter An Address." ValidationGroup="CreateService">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="row form-group">
                    <div class="col-md-6">
                        <asp:TextBox ID="txtCity" runat="server" Placeholder="City" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCity" runat="server"
                            ErrorMessage="RequiredFieldValidator" ControlToValidate="txtCity"
                            Text="Please Enter A City." ValidationGroup="CreateService">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlState" runat="server" class="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvState" runat="server"
                            ErrorMessage="RequiredFieldValidator" ControlToValidate="ddlState"
                            Text="Please Select A State." ValidationGroup="CreateService">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-4">
                        <asp:TextBox ID="txtZipCode" runat="server" Placeholder="Zip" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvZipCode" runat="server"
                            ErrorMessage="RequiredFieldValidator" ControlToValidate="txtZipCode"
                            Text="Please Enter Zip Code." ValidationGroup="CreateService">
                        </asp:RequiredFieldValidator>
                    </div>
                </div>



                <asp:TextBox ID="txtNotes" runat="server" Placeholder="Auction Theme" TextMode="MultiLine" Class="form-control mb-2"></asp:TextBox>

                <div class="form-group">
                    <div class="d-flex justify-content-around">
                        <asp:Button ID="btnClear" runat="server" Text="Clear" class="btn btn-primary btn-lg" OnClick="btnClear_Click" CausesValidation="false" />
                        <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-primary  btn-lg" OnClick="btnSave_Click" ValidationGroup="CreateService" />
                        <asp:Button ID="btnPopulate" runat="server" Text="Populate" class="btn btn-secondary btn-lg" OnClick="btnPopulate_Click" CausesValidation="false" />
                    </div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
