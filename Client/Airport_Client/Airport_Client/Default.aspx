<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Airport_Client._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div>
                    <br />
                        <div style="text-align: left; display: inline-block; width: auto;">
                            <asp:Label ID="Label2" runat="server" Text="From"></asp:Label>
                            
                            <asp:DropDownList id="Airport1" runat="server" AutoPostBack="True" ></asp:DropDownList>
                        </div>
                        <div id="todiv" style="text-align: left; display: inline-block; width: auto;">
                            <asp:Label ID="Label1" runat="server" Text="To"></asp:Label>
                           
                            <asp:DropDownList id="Airport2" runat="server" AutoPostBack="True" ></asp:DropDownList>
                        </div>
                   
                    <div>
                        <div id="depcal"style="text-align: left; display: inline-block; width: auto;">
                            <asp:Label ID="DepLabel" runat="server" Text="Departure"></asp:Label>
                            <asp:Calendar ID="depart" runat="server" autopostback="false"></asp:Calendar>
                        </div>
                        <div id="retdiv" runat="server" style="text-align: left; display: inline-block; width: auto;">
                            <br>
                            Return<br>
                            <asp:Calendar ID="returndate" runat="server" autopostback="false"></asp:Calendar>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div>
            
            <asp:CheckBox ID="oneway" runat="server" OnCheckedChanged="Oneway" AutoPostBack="True" />
            One Way
        <asp:CheckBox ID="lucky" runat="server" OnCheckedChanged="Lucky" AutoPostBack="True" />
            Lucky
        <asp:CheckBox ID="bestprice" runat="server" OnCheckedChanged="BestPrice" AutoPostBack="True" />
            Best Price
        </div>
        <asp:Button ID="search" runat="server" Text="Search" OnClick="Search" />
</asp:Content>

