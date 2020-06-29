<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="Book.aspx.cs" Inherits="TestTask.Book" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="Style.css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:GridView ID="PhoneBook"
            runat="server"
            AutoGenerateColumns="false"
            ShowFooter="true"
            DataKeyNames="Id"
            OnRowCommand="PhoneBook_RowCommand"
            OnRowDeleting="PhoneBook_RowDeleting"
            BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" OnSelectedIndexChanged="PhoneBook_SelectedIndexChanged"
            
            >
            <FooterStyle BackColor="White" ForeColor="#000066" />
            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
            <RowStyle ForeColor="#000066" />
            <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#007DBB" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#00547E" />

            <Columns>
                <asp:TemplateField HeaderText="Name">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Name") %>' runat="server"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtName" Text='<%# Eval("Name") %>' runat="server"/>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtNameFooter" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Surname">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Surname") %>' runat="server"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSurname" Text='<%# Eval("Surname") %>' runat="server"/>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtSurnameFooter" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Phone">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Phone") %>' runat="server"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPhone" Text='<%# Eval("Phone") %>' runat="server"/>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtPhoneFooter" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <asp:Label Text='<%# Eval("Email") %>' runat="server"/>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEmail" Text='<%# Eval("Email") %>' runat="server"/>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtEmailFooter" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button Text="X" CommandName="Delete" runat="server"/>
                    </ItemTemplate>
                    <EditItemTemplate>

                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:Button Text="+" CommandName="AddNew" runat="server"/>
                    </FooterTemplate>
                </asp:TemplateField>
            </Columns>

        </asp:GridView>
        <br />
        <asp:Label ID="successMesage" Text="" runat="server" ForeColor="Green"/>
        <br />
        <asp:Label ID="errorMessage" Text="" runat="server" ForeColor="Red"/>
    </form>
</body>
</html>
