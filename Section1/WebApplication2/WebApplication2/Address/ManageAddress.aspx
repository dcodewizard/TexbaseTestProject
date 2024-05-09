<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ManageAddress.aspx.cs" Inherits="WebApplication2.ManageAddress" %>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script type="text/javascript">
    function deleteAddress(addressId) {
        if (confirm("Are you sure you want to delete this address?")) {
            $.ajax({
                type: "POST",
                url: "ManageAddress.aspx/DeleteAddress",
                data: "{ 'addressId': '" + addressId + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        alert("Address deleted successfully.");
                        window.location.reload();
                    } else {
                        alert("No rows were affected. Address may not exist or there was an error or depenndency.");
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
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
                <div>
           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <h1>Manage Address</h1>
            <asp:GridView ID="gvCompanies" runat="server" AutoGenerateColumns="false" DataKeyNames="AddressID">
                <Columns>
                     <asp:BoundField DataField="Street" HeaderText="Street" />
                     <asp:BoundField DataField="City" HeaderText="City" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("AddressID") %>' OnClick="EditAddress">Edit</asp:LinkButton>
<%--                            <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("CompanyID") %>' OnClick="DeleteCompany">Delete</asp:LinkButton>--%>
                            <asp:LinkButton class="delete-btn" ID="lnkDelete" runat="server" Text="Delete" OnClientClick='<%# "deleteAddress(" + Eval("AddressID") + "); return false;" %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="btnAddAddress" runat="server" Text="Add Address" OnClick="AddAddress" />
        </div>
        </div>
    </form>
</body>
</html>
