<%@ Page Title="CheckTicket" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckTicket.aspx.cs" Inherits="Airport_Client.CheckTicket" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <p>
        <div style="display: grid; grid-template-columns: 1fr 1fr; grid-gap: 20px; ">
            <div >
                Future Ticket
                <asp:GridView ID="future"  runat="server" AutoGenerateColumns="false">
                    <SelectedRowStyle BackColor="LightCyan" ForeColor="DarkBlue" Font-Bold="true" />
                    <Columns>
                         <asp:BoundField HeaderText="Passenger" DataField="Passaporto" />
                        <asp:BoundField HeaderText="Flight" DataField="CodVolo" />
                        <asp:BoundField HeaderText="Buyer" DataField="Buyer" />
                        <asp:BoundField HeaderText="Class" DataField="Classe" />
                        <asp:BoundField HeaderText="Price" DataField="Prezzo" />
                    </Columns>
                </asp:GridView>
                
            </div>
            <div style="text-align: right; width: 48%;">
                  Past Ticket
                <asp:GridView ID="past"  runat="server" AutoGenerateColumns="false">
                    <SelectedRowStyle BackColor="LightCyan" ForeColor="DarkBlue" Font-Bold="true" />
                    <Columns>
                         <asp:BoundField HeaderText="Passenger" DataField="Passaporto" />
                        <asp:BoundField HeaderText="Flight" DataField="CodVolo" />
                        <asp:BoundField HeaderText="Buyer" DataField="Buyer" />
                        <asp:BoundField HeaderText="Class" DataField="Classe" />
                        <asp:BoundField HeaderText="Price" DataField="Prezzo" />
                    </Columns>
                </asp:GridView>
        </div>
            <asp:Button ID="goback" runat="server" Text="Home" OnClick="backpage"/>
            </p>
</asp:Content>
