<%@ Page Title="AdminPage" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPage.aspx.cs" Inherits="Airport_Client.AdminPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Select a page</h3>
    <p>
        <div>
            <asp:Button ID="airportbutton" runat="server" Text="Airports Edit" OnClick="ra" />
            <asp:Button ID="pathbutton" runat="server" Text="Paths Edit" OnClick="rp"/>
            <asp:Button ID="flightsbutton" runat="server" Text="Flights Edit" OnClick="rf"/>
            <asp:Button ID="userbutton" runat="server" Text="Users Edit" OnClick="ru"/>
            <asp:Button ID="jobsbutton" runat="server" Text="Jobs Edit" OnClick="rj"/>
        </div>

    </p>
</asp:Content>
