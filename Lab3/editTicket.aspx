<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="editTicket.aspx.cs" Inherits="Lab2.editTicket" EnableEventValidation="false" %>

<%--Kirsi And Josh Coleman 2/15/21--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="text-center mb-4">
        <asp:Label ID="lblSelect" Text="Select Service" runat="server"></asp:Label>
        <asp:DropDownList
            ID="ddlServices"
            runat="server"
            DataSourceID="srcServices"
            DataTextField="ServiceOutput"
            DataValueField="serviceID"
            OnDataBound="ddlServices_DataBound">
        </asp:DropDownList>
        <asp:Button ID="btnSelect" runat="server" Text="Select" />
    </div>

    <asp:SqlDataSource
        ID="srcServices"
        ConnectionString="<%$ ConnectionStrings:Connect %>"
        SelectCommand="SELECT SERVICE.serviceType + '-' + CUSTOMER.firstName + ' ' + CUSTOMER.lastName as ServiceOutput, SERVICE.serviceID, CUSTOMER.customerID FROM CUSTOMER INNER JOIN
                  SERVICE ON CUSTOMER.customerID = SERVICE.customerID"
        runat="server"></asp:SqlDataSource>

    <div class="row justify-content-around">

        <div class=" col-5">
            <div class=" card mb-2">
                <div class="card-header">
                    <asp:Label ID="lblGrid" Text="Service Details" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <div class="card-body">
                    <asp:DetailsView
                        ID="dvMove"
                        DataKeyNames="serviceID"
                        DataSourceID="srcMoveData"
                        AutoGenerateEditButton="false"
                        AutoGenerateRows="false"
                        DefaultMode="ReadOnly"
                        class="detailsView"
                        runat="server">
                        <Fields>
                            <asp:BoundField DataField="serviceDeadlineStart" HeaderText="Deadline Start" />
                            <asp:BoundField DataField="serviceDeadlineEnd" HeaderText="Deadline End" />
                            <asp:BoundField DataField="serviceStartDate" HeaderText="Move Start" />
                            <asp:BoundField DataField="serviceCompletionDate" HeaderText="Move End" />
                            <asp:BoundField DataField="serviceCost" HeaderText="Cost" />
                            <asp:BoundField DataField="streetAddress" HeaderText="Pick Up Address" />
                            <asp:BoundField DataField="city" HeaderText="Pick Up City" />
                            <asp:BoundField DataField="state" HeaderText="Pick Up State" />
                            <asp:BoundField DataField="zipcode" HeaderText="Pick Up Zip" />
                            <asp:BoundField DataField="streetDestAddress" HeaderText="Drop Off Address" />
                            <asp:BoundField DataField="destCity" HeaderText="Drop Off City" />
                            <asp:BoundField DataField="destState" HeaderText="Drop Off State" />
                            <asp:BoundField DataField="destZipcode" HeaderText="Drop Off Zip" />
                            <asp:BoundField DataField="notes" HeaderText="Notes" />
                        </Fields>
                    </asp:DetailsView>

                    <asp:SqlDataSource
                        ID="srcMoveData"
                        ConnectionString="<%$ ConnectionStrings:Connect %>"
                        SelectCommand="SELECT SERVICE.serviceID, SERVICE.serviceDeadlineStart, SERVICE.serviceDeadlineEnd, SERVICE.serviceStartDate, SERVICE.serviceCompletionDate, 
                  SERVICE.serviceCost, MOVE.streetAddress, MOVE.city, MOVE.state, MOVE.zipcode, MOVE.streetDestAddress, MOVE.destCity, MOVE.destState, MOVE.destZipcode, MOVE.notes FROM SERVICE INNER JOIN
                  MOVE ON SERVICE.serviceID = MOVE.serviceID WHERE SERVICE.serviceID = @serviceID"
                        runat="server">
                        <SelectParameters>
                            <asp:ControlParameter
                                Name="serviceID"
                                Type="Int32"
                                ControlID="ddlServices" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                    <asp:DetailsView
                        ID="dvAuction"
                        DataKeyNames="serviceID"
                        DataSourceID="srcAuction"
                        AutoGenerateEditButton="false"
                        AutoGenerateRows="false"
                        DefaultMode="ReadOnly"
                        runat="server">
                        <Fields>
                            <asp:BoundField DataField="serviceDeadlineStart" HeaderText="Deadline Start" />
                            <asp:BoundField DataField="serviceDeadlineEnd" HeaderText="Deadline End" />
                            <asp:BoundField DataField="serviceStartDate" HeaderText="Move Start" />
                            <asp:BoundField DataField="serviceCompletionDate" HeaderText="Move End" />
                            <asp:BoundField DataField="serviceCost" HeaderText="Cost" />
                            <asp:BoundField DataField="streetAddress" HeaderText="Pick Up Address" />
                            <asp:BoundField DataField="city" HeaderText="Pick Up City" />
                            <asp:BoundField DataField="state" HeaderText="Pick Up State" />
                            <asp:BoundField DataField="zipcode" HeaderText="Pick Up Zip" />
                            <asp:BoundField DataField="notes" HeaderText="Notes" />
                        </Fields>
                    </asp:DetailsView>

                    <asp:SqlDataSource
                        ID="srcAuction"
                        ConnectionString="<%$ ConnectionStrings:Connect %>"
                        SelectCommand="SELECT SERVICE.serviceID, SERVICE.serviceDeadlineStart, SERVICE.serviceDeadlineEnd, SERVICE.serviceStartDate, SERVICE.serviceCompletionDate, SERVICE.serviceCost,
                  AUCTION.streetAddress, AUCTION.city, AUCTION.state, AUCTION.zipcode, AUCTION.notes FROM SERVICE INNER JOIN
                  AUCTION ON SERVICE.serviceID = AUCTION.serviceID WHERE (SERVICE.serviceID = @serviceID)"
                        runat="server">
                        <SelectParameters>
                            <asp:ControlParameter
                                Name="serviceID"
                                Type="Int32"
                                ControlID="ddlServices" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
            <div class="card">
                <div class="card-header">
                    <asp:Label ID="lblInventory" Text="Inventory" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <div class="card-body">
                    <asp:Button ID="btnAddInventory" runat="server" Text="Add Inventory" OnClick="btnAddInventory_Click" />
                    <asp:Button ID="btnAuctionInventory" runat="server" Text="Auction Inventory" OnClick="btnAuctionInventory_Click"/>
                    <asp:GridView 
                        ID="gvInventory"
                        runat="server"
                        DataSourceID="srcAuctionAssignment"
                        DataKeyNames="itemID"
                        AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="itemDescription" HeaderText="Item" />
                            <asp:BoundField DataField="auctionType" HeaderText="Auction Description" />
                            <asp:BoundField DataField="auctionStartDate" HeaderText="Auction Start" />
                        </Columns>
                    </asp:GridView>

                    <asp:SqlDataSource 
                        ID="srcAuctionAssignment" 
                        runat="server"
                        ConnectionString="<%$ ConnectionStrings:Connect %>"
                        SelectCommand="SELECT AUCTIONEVENT.auctionType, AUCTIONEVENT.auctionStartDate, INVENTORY.itemDescription, INVENTORY.itemID FROM atAUCTION INNER JOIN
                            AUCTIONEVENT ON atAUCTION.auctionEventID = AUCTIONEVENT.auctionEventID RIGHT OUTER JOIN
                            INVENTORY ON atAUCTION.itemID = INVENTORY.itemID WHERE Inventory.serviceID = @serviceID" >
                        <SelectParameters>
                            <asp:ControlParameter
                                Name="serviceID"
                                Type="Int32"
                                ControlID="ddlServices" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>
        </div>

        <div class="col-5">
            <div class="card mb-2">
                <div class="card-header">
                    <asp:Label ID="lblAssignment" Text="Ticket Holder History" runat="server" Font-Bold="true"></asp:Label>
                </div>


                <ul class="list-group">
                    <li class="list-group-item">
                        <asp:Label ID="lblCurrent" Text="Current Ticket Holder: " runat="server" Font-Bold="true"></asp:Label>
                        <br />
                        <asp:Label ID="lblChange" Text="Assign to: " runat="server" Font-Bold="true"></asp:Label>
                        <asp:DropDownList
                            ID="ddlEmployee"
                            DataSourceID="srcEmployee"
                            DataTextField="Name"
                            DataValueField="employeeID"
                            runat="server">
                        </asp:DropDownList>
                        <asp:TextBox ID="txtAssignTicket" PlaceHolder="Assignment Note" runat="server"></asp:TextBox>
                        <asp:SqlDataSource
                            ID="srcEmployee"
                            ConnectionString="<%$ ConnectionStrings:Connect %>"
                            SelectCommand="Select employeeID, firstName + ' ' + lastName as Name from Employee"
                            runat="server"></asp:SqlDataSource>
                        <asp:Button ID="btnAssign" runat="server" Text="Assign" OnClick="btnAssign_Click" />
                    </li>

                    <li class="list-group-item">
                        <asp:GridView
                            ID="gvHolder"
                            DataSourceID="srcHolder"
                            runat="server">
                        </asp:GridView>

                        <asp:SqlDataSource
                            ID="srcHolder"
                            ConnectionString="<%$ ConnectionStrings:Connect %>"
                            SelectCommand="SELECT EMPLOYEE.firstName + ' ' + EMPLOYEE.lastName as Employee, TICKETHOLDER.creationDate as Assigned, TICKETHOLDER.note 
                            FROM EMPLOYEE INNER JOIN TICKETHOLDER ON EMPLOYEE.employeeID = TICKETHOLDER.employeeID  
                            WHERE TICKETHOLDER.serviceTicketID = @serviceID
                            ORDER BY TICKETHOLDER.creationDate desc"
                            runat="server">
                            <SelectParameters>
                                <asp:ControlParameter
                                    Name="serviceID"
                                    Type="Int32"
                                    ControlID="ddlServices" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </li>
                </ul>
            </div>

            <div class="card mb-2">
                <div class="card-header">
                    <asp:Label ID="lblNotes" Text="Service Notes" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <div class="card-body">
                    <asp:GridView
                        ID="gvNotes"
                        DataSourceID="srcNotes"
                        OnRowDataBound="gvNotes_RowDataBound"
                        OnSelectedIndexChanged="gvNotes_SelectedIndexChanged"
                        runat="server">
                    </asp:GridView>

                    <asp:SqlDataSource
                        ID="srcNotes"
                        ConnectionString="<%$ ConnectionStrings:Connect %>"
                        SelectCommand="SELECT TICKETNOTE.ticketID as 'ID', TICKETNOTE.creationDate as 'Creation Date', EMPLOYEE.firstName + ' ' + EMPLOYEE.lastName as 'Created By', TICKETNOTE.noteTitle as 'Note Title' 
                            FROM TICKETNOTE INNER JOIN EMPLOYEE on TICKETNOTE.noteCreator = EMPLOYEE.employeeID WHERE serviceTicketID = @serviceID"
                        runat="server">
                        <SelectParameters>
                            <asp:ControlParameter
                                Name="serviceID"
                                Type="Int32"
                                ControlID="ddlServices" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <asp:Button ID="btnNewNote" runat="server" Text="Add Note" OnClick="btnNewNote_Click" Class="btn btn-dark" />
                </div>
            </div>

            <div class="card">
                <div class="card-header">
                    <asp:Label ID="lblWorkflow" Text="Service Workers" runat="server" Font-Bold="true"></asp:Label>
                </div>
                <div class="card-body">
                    <asp:GridView
                        ID="gvAssignmnets"
                        DataSourceID="srcAssignments"
                        runat="server">
                    </asp:GridView>

                    <asp:SqlDataSource
                        ID="srcAssignments"
                        ConnectionString="<%$ ConnectionStrings:Connect %>"
                        SelectCommand="SELECT EMPLOYEE.firstName as 'First Name', EMPLOYEE.lastName as 'Last Name', ASSIGNMENT.startDate 'Start Date', ASSIGNMENT.employeeRole as 'Role' FROM ASSIGNMENT INNER JOIN
                  EMPLOYEE ON ASSIGNMENT.employeeID = EMPLOYEE.employeeID WHERE ASSIGNMENT.serviceTicketID = @serviceID"
                        runat="server">
                        <SelectParameters>
                            <asp:ControlParameter
                                Name="serviceID"
                                Type="Int32"
                                ControlID="ddlServices" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </div>
            </div>

        </div>
    </div>

</asp:Content>
