<%@ Page Title="AddUser" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="Airport_Client.AddUser" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Insert your data here</h3>
    <p>
        <div>
        <input type="text" id="passport" name="passport"><br>Passport<br>
        <input type="text" id="name" name="name"><br>Name<br>
        <input type="text" id="surname" name="surname"><br>Surname<br>
        <!--<input type="text" id="birthdate" name="birthdate"><br>BirthDate<br>-->
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Calendar ID="birthdate" runat="server" autopostback ="false"></asp:Calendar>
                <br>BirthDate<br>
             </ContentTemplate>
        </asp:UpdatePanel>
        
        <input type="text" id="mail" name="mail"><br>Mail<br>
        <input type="text" id="password" name="password"><br>Password<br>
        <asp:Button ID="Button1" runat="server" Text="Register" OnClick="Registra" />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
        <asp:CheckBox ID="crew" runat="server" OnCheckedChanged="Crew" AutoPostBack="True" />
            Crew
        <asp:CheckBox ID="admin" runat="server" OnCheckedChanged="Admin" AutoPostBack="True" />
            Admin
                </ContentTemplate>
        </asp:UpdatePanel>
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
                        <asp:BoundField HeaderText="Name" DataField="Nome" />
                        <asp:BoundField HeaderText="Surname" DataField="Cognome" />
                        <asp:BoundField HeaderText="Mail" DataField="Mail" />
                        <asp:BoundField HeaderText="BirthDate" DataField="Nascita" />
                        <asp:BoundField HeaderText="Credit" DataField="Credito" />
                        <asp:BoundField HeaderText="Type" DataField="Tipo" />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                    </Columns>
                </asp:GridView>
                </div>
                </div>
        </div>
        <asp:Button ID="goback" runat="server" Text="Home" OnClick="backpage"/>
    </p>
</asp:Content>
