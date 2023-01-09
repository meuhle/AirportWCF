<%@ Page Title="Ricarica" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Ricarica.aspx.cs" Inherits="Airport_Client.Ricarica" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <p>
        <div>
        <input type="number" step="0.01"  id="money"name="money"><br>Valore ricarica<br>
        
        <asp:Button ID="Buttontopup" runat="server" Text="Top Up" OnClick="Topup" />
            </div>
        <asp:Button ID="goback" runat="server" Text="Button" OnClick="backpage"/>

    </p>
</asp:Content>
