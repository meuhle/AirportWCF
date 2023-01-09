<%@ Page Title="Buy" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Buy.aspx.cs" Inherits="Airport_Client.Buy" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <p>
        <div style="display: grid; grid-template-columns: 1fr 1fr; grid-gap: 20px;">
            <div >
                <asp:GridView ID="oneway" AutoGenerateSelectButton="true" runat="server" AutoGenerateColumns="false">
                    <SelectedRowStyle BackColor="LightCyan" ForeColor="DarkBlue" Font-Bold="true" />
                    <Columns>
                         <asp:BoundField HeaderText="Path" DataField="Tratta" />
                        <asp:BoundField HeaderText="Price" DataField="Prezzo" />
                        <asp:BoundField HeaderText="Date" DataField="Data" />
                        <asp:BoundField HeaderText="Flight" DataField="Id_volo" />
                    </Columns>
                </asp:GridView>
                <asp:GridView ID="returngrid" AutoGenerateSelectButton="True" runat="server" AutoGenerateColumns="false">
                    <SelectedRowStyle BackColor="LightCyan" ForeColor="DarkBlue" Font-Bold="true" />
                    <Columns>
                         <asp:BoundField HeaderText="Path" DataField="Tratta" />
                        <asp:BoundField HeaderText="Price" DataField="Prezzo" />
                        <asp:BoundField HeaderText="Date" DataField="Data" />
                        <asp:BoundField HeaderText="Flight" DataField="Id_volo" />
                    </Columns>
                </asp:GridView>
                <asp:Button ID="Buttonbuy" runat="server" Text="Buy" OnClick="Buyticket" />
            </div>
            <div style="text-align: right; width: 48%;">
                <div>

                    <asp:CheckBox ID="economy" runat="server" OnCheckedChanged="eco" AutoPostBack="True" />
                    Economy
        <asp:CheckBox ID="business" runat="server" OnCheckedChanged="bus" AutoPostBack="True" />
                    Business
        <asp:CheckBox ID="first" runat="server" OnCheckedChanged="fir" AutoPostBack="True" />
                    First
                </div>
                <div>
                    <asp:CheckBox ID="forme" runat="server" OnCheckedChanged="fm" AutoPostBack="True" />
                    For Me
                    <asp:CheckBox ID="forother" runat="server" OnCheckedChanged="fo" AutoPostBack="True" />For Other
                    <asp:Button ID="viewbutton" runat="server" Text="+" OnClick="AddView" />
                </div>
            </div>
        </div>
        <br />
        <div id="inputview" runat="server" name="inputview" style="display: grid; grid-template-columns: 1fr 1fr; grid-gap: 20px; border: 1px solid black; height: 220px">
            <div style="display: grid; grid-template-columns: 1fr 1fr; grid-gap: 20px; border: 1px solid black; height: 220px">
                <div>
                    <input type="text" id="passport" name="passport">
                    Passport<br />
                    <input type="text" id="name" name="name">
                    Name<br />
                    <input type="text" id="surname" name="surname">
                    Surname<br />
                    <br />
                    <br />
                    <asp:Button ID="addbutton" runat="server" Text="Add User" OnClick="AddUser" AutoPostBack="true" />
                </div>
                <div>

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" Style="position: absolute">
                        <ContentTemplate>
                            <asp:Calendar ID="Calendar1" runat="server" CellPadding="4"
                                BorderColor="#999999" Font-Names="Verdana" Font-Size="8pt"
                                Height="180px" ForeColor="Black" DayNameFormat="FirstLetter"
                                Width="200px" BackColor="White" autopostback="false">
                                <TodayDayStyle ForeColor="Black" BackColor="#CCCCCC"></TodayDayStyle>
                                <SelectorStyle BackColor="#CCCCCC"></SelectorStyle>
                                <NextPrevStyle VerticalAlign="Bottom"></NextPrevStyle>
                                <DayHeaderStyle Font-Size="7pt" Font-Bold="True" BackColor="#CCCCCC"></DayHeaderStyle>
                                <SelectedDayStyle Font-Bold="True" ForeColor="White" BackColor="#666666"></SelectedDayStyle>
                                <TitleStyle Font-Bold="True" BorderColor="Black" BackColor="#999999"></TitleStyle>
                                <WeekendDayStyle BackColor="LightSteelBlue"></WeekendDayStyle>
                                <OtherMonthDayStyle ForeColor="#808080"></OtherMonthDayStyle>
                            </asp:Calendar>
                            Birthdate
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div style="border: 1px solid red;">
                <asp:GridView ID="passengerview" runat="server" autogeneratecolumns="False" OnRowDeleting="OnRowDeleting">
                    <Columns>
                        <asp:BoundField HeaderText="Nome" DataField="Nome" />
                        <asp:BoundField HeaderText="Cognome" DataField="Cognome" />
                        <asp:BoundField HeaderText="Passaporto" DataField="Passaporto" />
                        <asp:BoundField HeaderText="Nascita" DataField="Nascita" />
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" />
                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </p>
</asp:Content>
