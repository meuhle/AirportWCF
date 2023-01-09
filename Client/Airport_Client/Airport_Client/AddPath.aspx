<%@ Page Title="AddPath" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddPath.aspx.cs" Inherits="Airport_Client.AddPath" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3> Select two airport for a new path</h3>
    <p>
        <div>
        <asp:DropDownList id="dep" runat="server" AutoPostBack="True" ></asp:DropDownList>
        <asp:DropDownList id="arr" runat="server" AutoPostBack="True" ></asp:DropDownList>
        <asp:Button ID="buttonadd" runat="server" Text="Add Path" OnClick="Add" />
            </div>
        <br />
        <br />
        <div>
             <div style="display: grid; grid-template-columns: 1fr; grid-gap: 20px;">
            <div>
                Path
                <asp:GridView ID="rmgrid" runat="server" OnRowDeleting="OnRowDeleting" autogeneratecolumns="False">
                    <SelectedRowStyle BackColor="LightCyan" ForeColor="DarkBlue" Font-Bold="true" />
                    <Columns>
                         <asp:BoundField HeaderText="ID" DataField="Id_Tratta" />
                        <asp:BoundField HeaderText="Departure" DataField="Partenza" />
                        <asp:BoundField HeaderText="Arrival" DataField="Destinazione" />
                        <asp:BoundField HeaderText="Distance" DataField="Distance" />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                    </Columns>
                </asp:GridView>
                </div>
                 </div>
        </div>
        <asp:Button ID="goback" runat="server" Text="Home" OnClick="backpage"/>
    </p>
</asp:Content>
