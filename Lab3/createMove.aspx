<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="createMove.aspx.cs" Inherits="Lab1.createService" %>
<%--Kirsi And Josh Coleman 2/15/21--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container col-6">
        <div class="card">
            <div class="card-header text-center">
                <asp:Label ID="lblCreateNewCustomer" runat="server" Text="New Move Request" Class="h3 m-2"></asp:Label>
            </div>

            <div class="card-body form-group">
                <asp:Label ID="outputLbl" runat="server" Text=""></asp:Label>

                <div class="form-group">
                    <asp:Label ID="lblCustomers" runat="server" Text="Customer" for="ddlCustomer"></asp:Label>
                    <asp:DropDownList ID="ddlCustomer" runat="server" class="dropdown"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="RequiredFieldValidator"
                        ControlToValidate="ddlCustomer" Text="Select a Customer"></asp:RequiredFieldValidator>
                </div>

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
                <asp:CustomValidator ID="checkNotOverlappingService" runat="server" ErrorMessage="CustomValidator" ValidationGroup="CreateService"
                    OnServerValidate="checkNotOverlappingService_ServerValidate" Text="This Overlaps with a Current Move for this customer"></asp:CustomValidator>


                <div class="form-group">
                    <asp:Label ID="lblServiceCost" runat="server" Text="Service Cost" for="txtServiceCost"></asp:Label>
                    <asp:TextBox ID="txtServiceCost" runat="server" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvServiceCost" runat="server"
                        ErrorMessage="RequiredFieldValidator" ControlToValidate="txtServiceCost"
                        Text="Please Enter a Service Cost." ValidationGroup="CreateService">
                    </asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="compareDouble" runat="server" ErrorMessage="CompareValidator"
                        ControlToValidate="txtServiceCost" Text="Invalid Cost" ValidationGroup="CreateService"
                        Operator="DataTypeCheck" Type="Currency"></asp:CompareValidator>
                </div>

                <asp:Label ID="originAddress" runat="server" Text="Origin Address"></asp:Label>
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

                <asp:Label ID="lblDestinationAddress" runat="server" Text="Destination Address"></asp:Label>
                <div class="form-group">
                    <asp:TextBox ID="txtDestinationAddress" runat="server" Placeholder="Address" class="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvDestinationAddress" runat="server"
                        ErrorMessage="rfvDestinationAddress" ControlToValidate="txtDestinationAddress"
                        Text="Please Enter An Address." ValidationGroup="CreateService">
                    </asp:RequiredFieldValidator>
                </div>

                <div class="row form-group">
                    <div class="col-md-6">
                        <asp:TextBox ID="txtDestinationCity" runat="server" Placeholder="City" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfcDestinationCity" runat="server"
                            ErrorMessage="RequiredFieldValidator" ControlToValidate="txtDestinationCity"
                            Text="Please Enter A City." ValidationGroup="CreateService">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-2">
                        <asp:DropDownList ID="ddlDestinationState" runat="server" class="form-control"></asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvDestinationState" runat="server"
                            ErrorMessage="RequiredFieldValidator" ControlToValidate="ddlDestinationState"
                            Text="Please Select A State." ValidationGroup="CreateService">
                        </asp:RequiredFieldValidator>
                    </div>

                    <div class="col-md-4">
                        <asp:TextBox ID="txtDestinationZip" runat="server" Placeholder="Zip" class="form-control"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvDestinationZip" runat="server"
                            ErrorMessage="RequiredFieldValidator" ControlToValidate="txtDestinationZip"
                            Text="Please Enter Zip Code." ValidationGroup="CreateService">
                        </asp:RequiredFieldValidator>
                    </div>

                </div>

                <asp:TextBox ID="txtNotes" runat="server" Placeholder="Notes or Employee Accomodations" TextMode="MultiLine" Class="form-control mb-2"></asp:TextBox>




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
