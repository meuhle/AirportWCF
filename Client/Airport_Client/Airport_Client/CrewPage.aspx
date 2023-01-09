<%@ Page Title="AdminPage" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrewPage.aspx.cs" Inherits="Airport_Client.CrewPage" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3></h3>
    <p>
        <div>
            <asp:Button ID="checkwork" runat="server" Text="My Jobs" OnClick="jobs" />
            
        </div>

    </p>
</asp:Content>
