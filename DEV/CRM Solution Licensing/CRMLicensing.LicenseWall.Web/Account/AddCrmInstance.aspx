<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddCrmInstance.aspx.cs" Inherits="CRMLicensing.LicenseWall.Web.Account.AddCrmInstance" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Manage you licenses</h1>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT * FROM [CrmOrganizations] WHERE ([UserId] = @UserId)" OnSelecting="SqlDataSource1_Selecting">
        <SelectParameters>
            <asp:Parameter Name="UserId" Type="Object" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" DeleteCommand="DELETE FROM [CrmOrganizations] WHERE [Id] = @Id" InsertCommand="INSERT INTO [CrmOrganizations] ([Id], [OrganisationId], [UserId], [OrgName]) VALUES (@Id, @OrganisationId, @UserId, @OrgName)" OnInserting="SqlDataSource2_Inserting" SelectCommand="SELECT * FROM [CrmOrganizations]" UpdateCommand="UPDATE [CrmOrganizations] SET [OrganisationId] = @OrganisationId, [UserId] = @UserId, [OrgName] = @OrgName WHERE [Id] = @Id">
        <DeleteParameters>
            <asp:Parameter Name="Id" Type="Int32" />
        </DeleteParameters>
        <FilterParameters>
            <asp:ControlParameter ControlID="GridView1" Name="OrganisationId" PropertyName="SelectedValue" Type="Object" />
        </FilterParameters>
        <InsertParameters>
            <asp:Parameter Name="Id" Type="Int32" />
            <asp:Parameter Name="OrganisationId" Type="Object" />
            <asp:Parameter Name="UserId" Type="Object" />
            <asp:Parameter Name="OrgName" Type="String" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="OrganisationId" Type="Object" />
            <asp:Parameter Name="UserId" Type="Object" />
            <asp:Parameter Name="OrgName" Type="String" />
            <asp:Parameter Name="Id" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>
    <asp:GridView ID="GridView1" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="Id" DataSourceID="SqlDataSource1" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:CommandField ShowSelectButton="True" />
            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
            <asp:BoundField DataField="OrganisationId" HeaderText="OrganisationId" SortExpression="OrganisationId" />
            <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" />
            <asp:BoundField DataField="OrgName" HeaderText="OrgName" SortExpression="OrgName" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>

    <asp:DetailsView ID="DetailsView1" runat="server" AllowPaging="True" AutoGenerateRows="False" DataKeyNames="Id" DataSourceID="SqlDataSource2" Height="50px" Width="125px">
        <Fields>
            <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id" />
            <asp:BoundField DataField="OrganisationId" HeaderText="OrganisationId" SortExpression="OrganisationId" />
            <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" />
            <asp:BoundField DataField="OrgName" HeaderText="OrgName" SortExpression="OrgName" />
            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" ShowInsertButton="True" />
        </Fields>
    </asp:DetailsView>

</asp:Content>
