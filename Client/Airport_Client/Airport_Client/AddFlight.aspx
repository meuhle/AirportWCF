<%@ Page Title="AddFlight" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddFlight.aspx.cs" Inherits="Airport_Client.AddFlight" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <p>
        <div>



            <asp:Label ID="DepLabel" runat="server" Text="Departure"></asp:Label><asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Calendar ID="depart" runat="server" autopostback="false"></asp:Calendar>
                </ContentTemplate>
            </asp:UpdatePanel>
            <input type="number" step="0.01" id="money" name="money"><br>
            Valore ricarica<br>
            <asp:DropDownList ID="path" runat="server"></asp:DropDownList>


            <asp:Button ID="ButtonAdd" runat="server" Text="Add Flight" OnClick="Add" />
        </div>
        <br />
        <br />
        <div>
            <div style="display: grid; grid-template-columns: 1fr; grid-gap: 20px;">
                <div>
                    Path
                <asp:GridView ID="rmgrid" runat="server" OnRowDeleting="OnRowDeleting" AutoGenerateColumns="false">
                    <SelectedRowStyle BackColor="LightCyan" ForeColor="DarkBlue" Font-Bold="true" />
                    <Columns>
                        <asp:BoundField HeaderText="ID" DataField="Id_Volo" />
                        <asp:BoundField HeaderText="Path" DataField="Tratta" />
                        <asp:BoundField HeaderText="Date" DataField="Data" />
                        <asp:BoundField HeaderText="Price" DataField="Prezzo" />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                    </Columns>
                </asp:GridView>
                </div>
            </div>
        </div>
        <asp:Button ID="goback" runat="server" Text="Home" OnClick="backpage"/>


    </p>
</asp:Content>
