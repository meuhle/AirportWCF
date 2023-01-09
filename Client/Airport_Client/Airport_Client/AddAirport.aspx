<%@ Page Title="AddAirport" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddAirport.aspx.cs" Inherits="Airport_Client.AddAirport" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3> Add Airport value</h3>
    <p>
        <div>
        <!--<asp:TextBox ID="Code" runat="server" type="text"></asp:TextBox><br>Code<br>
        <asp:TextBox ID="State" runat="server" type="text"></asp:TextBox><br>State<br>
        <asp:TextBox ID="City" runat="server" type="text"></asp:TextBox><br>City<br>
        <asp:TextBox ID="Name" runat="server" type="text"></asp:TextBox><br>Name<br>
        <asp:TextBox ID="Lat" step="0.000001" runat="server" type="number"></asp:TextBox><br>Lat<br>
        <asp:TextBox ID="Lon" step="0.000001" runat="server" type="number"></asp:TextBox><br>Lon<br>-->

        <input id="code" type="text" name="code"/><br>Code<br>
        <input id="state" type="text" name="state" /><br>State<br>
        <input id="city" type="text" name="city"/><br>City<br>
        <input id="name" type="text" name="name" /><br>Name<br>
        <input id="lat" name="lat" step="0.000001" type="number" /><br>Lat<br>
        <input id="lon" name="lon" step="0.000001" type="number" /><br>Lon<br>
        <asp:Button ID="buttonadd" runat="server" Text="Add Path" OnClick="Add"  />
        </div>
        <br />
        <br />
        <div>
            <div style="display: grid; grid-template-columns: 1fr; grid-gap: 20px;">
            <div>
                Airports
                <asp:GridView ID="rmairport" runat="server" OnRowDeleting="OnRowDeleting" autogeneratecolumns="False">
                    <SelectedRowStyle BackColor="LightCyan" ForeColor="DarkBlue" Font-Bold="true" />
                    <Columns>
                        <asp:BoundField HeaderText="IATA Code" DataField="Codice" />
                        <asp:BoundField HeaderText="Name" DataField="Nome" />
                        <asp:BoundField HeaderText="City" DataField="Citta" />
                        <asp:BoundField HeaderText="State" DataField="Stato" />
                        <asp:BoundField HeaderText="Latitude" DataField="LAT" />
                        <asp:BoundField HeaderText="Longitude" DataField="LON" />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                    </Columns>
                </asp:GridView>
        </div>
                <asp:Button ID="goback" runat="server" Text="Home" OnClick="backpage"/>
    </p>
</asp:Content>
