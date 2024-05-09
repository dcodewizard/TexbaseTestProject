<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditCompany.aspx.cs" Inherits="WebApplication2.EditCompany" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Company</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Edit Company</h1>
            <table>
                <tr>
                    <td>Company Name:</td>
                    <td><asp:TextBox ID="txtCompanyName" runat="server"></asp:TextBox></td>
                    <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName" ForeColor="Red" ErrorMessage="Company Name is required"></asp:RequiredFieldValidator><br />
                </tr>
                <tr>
                    <td>Street:</td>
                    <td><asp:TextBox ID="txtStreet" runat="server"></asp:TextBox></td>
                    <asp:RequiredFieldValidator ID="rfvStreet" runat="server" ControlToValidate="txtStreet" ForeColor="Red" ErrorMessage="Street is required"></asp:RequiredFieldValidator><br />
                </tr>
                 <tr>
                 <td>City:</td>
                  <td><asp:TextBox ID="txtCity" runat="server"></asp:TextBox></td>
                   <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity" ForeColor="Red" ErrorMessage="City is required"></asp:RequiredFieldValidator>
                   </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="SaveCompany" />
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>