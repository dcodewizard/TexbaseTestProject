<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageContacts.aspx.cs" Inherits="WebApplication2.ManageContacts" %>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script type="text/javascript">
    function deleteContact(contactId) {
        if (confirm("Are you sure you want to delete this company?")) {
            $.ajax({
                type: "POST",
                url: "ManageContacts.aspx/DeleteContact",
                data: "{ 'contactId': '" + contactId + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        alert("Contact deleted successfully.");
                        window.location.reload();
                    } else {
                        alert("No rows were affected. Contact may not exist or there was an error.");
                    }
                },
                error: function (xhr, status, error) {
                    alert("Error: " + error);
                }
            });
        }
    }
</script>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Contacts</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <h1>Manage Contacts</h1>
            <asp:GridView ID="gvCompanies" runat="server" AutoGenerateColumns="false" DataKeyNames="ContactID">
                <Columns>
                    <asp:BoundField DataField="ContactID" HeaderText="Contact ID" />
                    <asp:BoundField DataField="ContactName" HeaderText="Contact Name" />
                     <asp:BoundField DataField="Email" HeaderText="Email" />
                     <asp:BoundField DataField="Title" HeaderText="Title" />
                     <asp:BoundField DataField="Street" HeaderText="Street" />
                     <asp:BoundField DataField="City" HeaderText="City" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("ContactID") %>' OnClick="EditContact">Edit</asp:LinkButton>
<%--                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("CompanyID") %>' OnClick="DeleteCompany">Delete</asp:LinkButton>--%>
                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" OnClientClick='<%# "deleteContact(" + Eval("ContactID") + "); return false;" %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="btnAddCompany" runat="server" Text="Add Contact" OnClick="AddContact" />
        </div>
        </div>
    </form>
</body>
</html>
