<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="Lab2.HomePage" EnableEventValidation="false" %>

<%--Kirsi And Josh Coleman 2/15/21--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container col-10">
        <asp:Label ID="lblHome" runat="server" Text="Welcome to the movers r Us home page!"></asp:Label>
        <div class="card-group">
            <div class="card p-3">
                                <asp:Label ID="lblWork" runat="server" Text=" Your Current Assignments"></asp:Label>
                <asp:GridView
                    ID="gvWork"
                    runat="server"
                    DataKeyNames="serviceTicketID"
                    OnRowDataBound="gvWork_RowDataBound"
                    OnSelectedIndexChanged="gvWork_SelectedIndexChanged"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Note" HeaderText="Assignment Note" />
                        <asp:BoundField DataField="Assigned On:" HeaderText="Assigned On" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="Label1" runat="server" Text="Select a ticket assignment for more information." Font-Italic="true"></asp:Label>
            </div>

            <div class="card p-3">
                <asp:Label ID="Label3" runat="server" Text="New Customers"></asp:Label>
                <asp:GridView
                    ID="gvNewCustomers"
                    runat="server"
                    DataSourceID="srcNewCustomers"
                    DataKeyNames="userID"
                    OnRowDataBound="gvNewCustomers_RowDataBound"
                    OnSelectedIndexChanged="gvNewCustomers_SelectedIndexChanged"
                    AutoGenerateColumns="false">
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Customer" />
                        <asp:BoundField DataField="serviceType" HeaderText="Service Type" />
                        <asp:BoundField DataField="dateRequested" HeaderText="Date Requested" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="Label4" runat="server" Text="Select a customer for more information." Font-Italic="true"></asp:Label>

                <asp:SqlDataSource 
                    ID="srcNewCustomers" 
                    runat="server"
                    ConnectionString="<%$ ConnectionStrings:AUTH %>"
                    SelectCommand="SELECT serviceRequest.serviceRequestID, Person.FirstName + ' ' + Person.LastName AS Name, serviceRequest.serviceType, Person.userID, serviceRequest.dateRequested FROM Person INNER JOIN
                            serviceRequest ON Person.UserID = serviceRequest.UserID WHERE(serviceRequest.requestStatus = '1')"
                ></asp:SqlDataSource>
            </div>
        </div>
    </div>
</asp:Content>
