<%@ Page Title="AdminPage" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserPage.aspx.cs" Inherits="Airport_Client.UserPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Insert your data here</h3>
    <p>
        <div>
            <asp:Button ID="checkticket" runat="server" Text="My Ticket" OnClick="ticket" />
            <asp:Button ID="topupbutton" runat="server" Text="Topup" OnClick="topup" />
            <asp:Button ID="home" runat="server" Text="Home" OnClick="gohome"/>
            
        </div>

    </p>
</asp:Content>
