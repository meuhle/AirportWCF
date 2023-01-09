<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Airport_Client.Login" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Insert data</h3>
    <p>
        <input type="text" id="mail" name="mail"><br>Mail<br>
        <input type="text" id="password" name="password"><br>Password<br>
        
        <asp:Button ID="ButtonLogin" runat="server" Text="Login" OnClick="Log" />

    </p>
</asp:Content>
