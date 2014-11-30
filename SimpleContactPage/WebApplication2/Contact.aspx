<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="WebApplication2.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contact</title>
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

    <div runat="server" id="testdiv" style="font-size:12px">
        <div runat="server" id="name" style="font-size: 28px">Name Goes Here</div>

        <div runat="server" id="title" style="margin-left:10px; margin-right:20px; float:left;">Title Goes Here</div>
        <div runat="server" id="company" style="margin-left:10px; margin-right:20px; float:left;">Company Goes Here</div>
        <div style="display:block;clear:both;"></div>

        <div style="margin-left:30px;margin-top:15px;">
            <div id='email_label' runat='server' style="font-size: 18px;">Emails</div>
            <div runat="server" id="emails">
                863 CR 3141 E, Cleveland, Texas, 77327<br />
                619 W Main St #1809, Bellville, Texas, 77237
            </div>
        </div>

        <div style="margin-left:30px;display:block;clear:both;margin-top:15px;">
            <div id='phone_label' runat='server' style="font-size: 18px;">Phone Numbers</div>
            <div runat="server" id="phonenumbers">
                863 CR 3141 E, Cleveland, Texas, 77327<br />
                619 W Main St #1809, Bellville, Texas, 77237
            </div>
        </div>

         <div style="margin-left:30px;display:block;clear:both;margin-top:15px;">
            <div id='address_label' runat='server' style="font-size: 18px;">Addresses</div>
            <div runat="server" id="addresses">
                863 CR 3141 E, Cleveland, Texas, 77327<br />
                619 W Main St #1809, Bellville, Texas, 77237
            </div>
        </div>

         <div style="margin-left:30px;display:block;clear:both;margin-top:15px;">
            <div id='fax_label' runat='server' style="font-size: 18px;">Faxes</div>
            <div runat="server" id="faxes">
                863 CR 3141 E, Cleveland, Texas, 77327<br />
                619 W Main St #1809, Bellville, Texas, 77237
            </div>
        </div>
        <a id="editcontactlink" runat="server" href="">Edit Contact</a>
        </div>
</body>
</html>
