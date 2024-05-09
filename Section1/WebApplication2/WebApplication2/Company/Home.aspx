<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="WebApplication2.Home" %>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script type="text/javascript">
    function deleteCompany(companyId) {
        if (confirm("Are you sure you want to delete this company?")) {
            $.ajax({
                type: "POST",
                url: "Home.aspx/DeleteCompany",
                data: "{ 'companyId': '" + companyId + "' }",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    if (response.d) {
                        alert("Company deleted successfully.");
                        window.location.reload();
                    } else {
                        alert("No rows were affected. Company may not exist or there was an error.");
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
<head>
    <title>Manage Companies</title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <h1>Companies and Contacts</h1>
            <asp:GridView ID="gvCompanies" runat="server" AutoGenerateColumns="false" DataKeyNames="CompanyID">
                <Columns>
                    <asp:BoundField DataField="CompanyID" HeaderText="Company ID" />
                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                     <asp:BoundField DataField="Street" HeaderText="Street" />
                     <asp:BoundField DataField="City" HeaderText="City" />
                    <asp:TemplateField HeaderText="Actions">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Eval("CompanyID") %>' OnClick="EditCompany">Edit</asp:LinkButton>
                            <asp:LinkButton class="delete-btn" ID="lnkDelete" runat="server" Text="Delete" OnClientClick='<%# "deleteCompany(" + Eval("CompanyID") + "); return false;" %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <asp:Button ID="btnAddCompany" runat="server" Text="Add Company" OnClick="AddCompany" />
        </div>
    </form>
</body>
</html>