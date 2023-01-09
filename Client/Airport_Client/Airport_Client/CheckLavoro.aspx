<%@ Page Title="CheckLavoro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckLavoro.aspx.cs" Inherits="Airport_Client.CheckLavoro" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <p>
        <div style="display: grid; grid-template-columns: 1fr ; grid-gap: 20px;">
            <div >
                Work
                <asp:GridView ID="work"  runat="server" AutoGenerateColumns="false">
                    <SelectedRowStyle BackColor="LightCyan" ForeColor="DarkBlue" Font-Bold="true" />
                    <Columns>
                         <asp:BoundField HeaderText="Flight" DataField="Volo" />
                        <asp:BoundField HeaderText="Data" DataField="Data" />
                        <asp:BoundField HeaderText="Role" DataField="Ruolo" />
                    </Columns>
                </asp:GridView>
                </div>
            </div>
                
           <asp:Button ID="goback" runat="server" Text="Home" OnClick="backpage"/>
            </p>
</asp:Content>
