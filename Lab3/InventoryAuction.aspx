<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="InventoryAuction.aspx.cs" Inherits="Lab3.InventoryAuction" %>
<%--Kirsi And Josh Coleman 2/15/21--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container col-6">
        <div class="card">
            <div class="card-header text-center">
                <asp:Label ID="lblViewNote" runat="server" Text="Add Inventory to Auction" Class="h3 m-2"></asp:Label>
            </div>
            <div class="card-body">
                <div class="d-flex justify-content-around">
                    <asp:Label ID="lblSelect" Text="Select Auction" runat="server"></asp:Label>
                    <asp:DropDownList
                        ID="ddlAuctions"
                        runat="server"
                        DataSourceID="srcAuctions"
                        DataTextField="AuctionOutput"
                        DataValueField="auctionEventID">
                    </asp:DropDownList>


                    <asp:SqlDataSource
                        ID="srcAuctions"
                        ConnectionString="<%$ ConnectionStrings:Connect %>"
                        SelectCommand="SELECT auctionType + ' ' + CAST(auctionStartDate as varchar) as AuctionOutput, auctionEventID from AUCTIONEVENT"
                        runat="server"></asp:SqlDataSource>

                    <asp:DropDownList
                        ID="ddlInventory"
                        runat="server"
                        DataTextField="AuctionOutput"
                        DataValueField="inventoryID">
                    </asp:DropDownList>

                    <asp:Button ID="btnAssign" runat="server" Text="Add to Auction" OnClick="btnAssign_Click"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
