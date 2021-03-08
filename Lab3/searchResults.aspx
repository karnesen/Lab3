<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="searchResults.aspx.cs" Inherits="Lab3.searchResults" EnableEventValidation="false" %>
<%--Kirsi And Josh Coleman--%>
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
                    ID="gvCustomer"
                    runat="server"
                    DataKeyNames="customerID"
                    AutoGenerateColumns="false"
                    OnRowDataBound="gvCustomer_RowDataBound"
                    OnSelectedIndexChanged="gvCustomer_SelectedIndexChanged"
                    class="table table-striped table-borderless">
                    <Columns>
                        <asp:BoundField DataField="firstName" HeaderText="Customer" />
                        <asp:BoundField DataField="lastName" HeaderText=" " />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>
