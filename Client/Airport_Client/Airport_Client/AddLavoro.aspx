<%@ Page Title="AddLavoro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddLavoro.aspx.cs" Inherits="Airport_Client.AddLavoro" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Select option for job</h3>
    <p>
        <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
            <asp:DropDownList id="flight" runat="server" AutoPostBack="True"></asp:DropDownList>
            <asp:DropDownList id="pass" runat="server" AutoPostBack="True"></asp:DropDownList>
            <asp:DropDownList id="role" runat="server" AutoPostBack="True"></asp:DropDownList>
            
            
                </ContentTemplate>
        </asp:UpdatePanel>
            <asp:Button ID="buttonadd" runat="server" Text="Add Work" OnClick="Add" />
            </div>
        <br />
        <br />
        <div>
            <div style="display: grid; grid-template-columns: 1fr; grid-gap: 20px;">
            <div>
                Path
                <asp:GridView ID="rmgrid" runat="server" OnRowDeleting="OnRowDeleting" autogeneratecolumns="false">
                    <SelectedRowStyle BackColor="LightCyan" ForeColor="DarkBlue" Font-Bold="true" />
                    <Columns>
                         <asp:BoundField HeaderText="Passport" DataField="Passaporto" />
                        <asp:BoundField HeaderText="Role" DataField="Ruolo" />
                        <asp:BoundField HeaderText="Flight" DataField="Volo" />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                    </Columns>
                </asp:GridView>
                </div>
                </div>
        </div>
        <asp:Button ID="goback" runat="server" Text="Home" OnClick="backpage"/>
    </p>
</asp:Content>
