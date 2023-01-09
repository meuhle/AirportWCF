<%@ Page Title="Register" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Airport_Client.Register" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Insert your data here</h3>
    <p>
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

    </p>
</asp:Content>
