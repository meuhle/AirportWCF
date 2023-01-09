<%@ Page Title="LoginCrew" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LoginCrew.aspx.cs" Inherits="Airport_Client.LoginCrew" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Insert data</h3>
    <p>
        <input type="text" id="passport" name="passport"><br>Passport<br>
        
        <asp:Button ID="ButtonLogin" runat="server" Text="Login" OnClick="Log" />

    </p>
</asp:Content>
