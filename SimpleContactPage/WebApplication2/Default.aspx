<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication2.WebForm2" %>

<!DOCTYPE html>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Home Page</title>
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

    <div style="font-size:12px">
        <div style="font-size: 20px">Welcome To Your Address Book!</div>
        <div style="margin-left:20px">
                Search for an existing contact using our search bar, or add a new contact by clicking <a href="CreateContact.aspx" >here!</a>
        </div>
    </div>
    </div>
</body>
</html>
