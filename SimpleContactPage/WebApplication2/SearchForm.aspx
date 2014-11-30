<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchForm.aspx.cs" Inherits="WebApplication2.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Search For Contacts</title>
</head>
<body style="margin:0 0 0 0; background-color:beige;">
    
    <div style="margin:0px 0px 0px 0px; font-size: 28px; width:100%; background-color:bisque">
    <div style="margin-left:10px;">Contact Book</div>
    </div>
    <div style="margin-left:10%">
    <div style="margin-bottom: 10px;">
        <form action="SearchForm.aspx" method="post" >
            <div style="float:left;">
                Search For &nbsp; <input type="text" name="search"></input>
            </div>
        
            <div style="display:inline; float:left; margin-left:5px;">In
            <select id="SearchChoice" name="SearchChoice">
                <option value="1">Name</option>
                <option value="2">Email</option>
                <option value="3">Company</option>
                <option value="4">Title</option>
                <option value="5">Phone</option>
                <option value="6">Area Code</option>
                <option value="7">Fax</option>
            </select>
            </div>
                <input type="submit" ID="submit" value="Search" />
                <br />
            <div style="width:100px; display:inline; float:left;"></div>
        </form>
    </div>
    <div style="margin-top:15px;">
        <asp:Label id="searchdetails" runat="server"></asp:Label>
        <asp:Table ID="Table1" runat="server" width="500px">
            <asp:TableRow runat="server">
                <asp:TableCell runat="server" BorderStyle="None"><b>First Name</b></asp:TableCell>
                <asp:TableCell runat="server" BorderStyle="None"><b>Last Name</b></asp:TableCell>
                <asp:TableCell runat="server" BorderStyle="None"><b>Contact Page</b></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>

    </div>
</body>
</html>
