<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddContacts.aspx.cs" Inherits="WebApplication2.AddContacts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Contact</title>
</head>
<body>
    <form id="form1" runat="server">
           <div>
        <h1>Add Contact</h1>
        <table>
            <tr>
                <td>Contact Name:</td>
                <td><asp:TextBox ID="txtContactName" runat="server"></asp:TextBox></td>
                <asp:RequiredFieldValidator ID="rfvContactName" runat="server" ControlToValidate="txtContactName" ForeColor="Red" ErrorMessage="Contact Name is required"></asp:RequiredFieldValidator><br/>
            </tr>
             <tr>
                 <td>Email:</td>
                 <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
                 <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Email is required"></asp:RequiredFieldValidator><br/>
                 <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ForeColor="Red" ErrorMessage="Invalid email format" ValidationExpression="\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b"></asp:RegularExpressionValidator><br/>
             </tr>
            <tr>
                <td>Title</td>
              <td><asp:TextBox ID="txtTitle" runat="server"></asp:TextBox></td>
              <asp:RequiredFieldValidator ID="rfvTitle" runat="server" ControlToValidate="txtTitle" ForeColor="Red" ErrorMessage="Title is required"></asp:RequiredFieldValidator><br/>
             </tr>
            <tr>
                <td>Street:</td>
                <td><asp:TextBox ID="txtStreet" runat="server"></asp:TextBox></td>
                <asp:RequiredFieldValidator ID="rfvStreet" runat="server" ControlToValidate="txtStreet" ForeColor="Red" ErrorMessage="Street is required"></asp:RequiredFieldValidator><br />
            </tr>
             <tr>
                 <td>City:</td>
                 <td><asp:TextBox ID="txtCity" runat="server"></asp:TextBox></td>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCity" ForeColor="Red" ErrorMessage="City is required"></asp:RequiredFieldValidator>
             </tr>
            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="SaveContact" />
                </td>
            </tr>
        </table>
    </div>
</form>
</body>
</html>

