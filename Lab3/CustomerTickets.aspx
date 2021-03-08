<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CustomerTickets.aspx.cs" Inherits="Lab3.CustomerTickets" EnableEventValidation="false" %>
<%--Kirsi And Josh Coleman 2/15/21--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container col-4">
        <div class="card">
            <div class="card-header">
                Search Results
            </div>
            <div class="card-body">
                <asp:GridView
                    ID="gvCustomerTicket"
                    runat="server"
                    DataKeyNames="serviceID"
                    AutoGenerateColumns="false"
                    OnRowDataBound="gvCustomerTicket_RowDataBound"
                    OnSelectedIndexChanged="gvCustomerTicket_SelectedIndexChanged"
                    class="table table-striped table-borderless">
                    <Columns>
                        <asp:BoundField DataField="serviceType" HeaderText="Service Type" />
                        <asp:BoundField DataField="ticketOpenDate" HeaderText="Open Date" />
                        <asp:BoundField DataField="ticketStatus" HeaderText="Status" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
