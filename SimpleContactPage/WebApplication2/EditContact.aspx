<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditContact.aspx.cs" Inherits="WebApplication2.AddNewContact" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Contact</title>
</head>
<body style="margin:0 0 0 0; background-color:beige;">
    
    <div style="margin:0px 0px 0px 0px; font-size: 28px; width:100%; background-color:bisque">
    <div style="margin-left:10px;">Contact Book</div>
    </div>
    <div style="margin-left:10%">
    <div style="margin-bottom: 10px;">
        <form action="SearchForm.aspx" method="post" >
            <div style="float:left;">
                Search For &nbsp; <input type="text" name="Search"></input>
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
                <input type="submit" ID="submit" Text="Search" />
                <br />
            <div style="width:100px; display:inline; float:left;"></div>
        </form>
    </div>

    <div style="font-size:17px; margin-top:20px;">
        <form method="post" action="EditContact.aspx" id="editcontact" runat="server">
            <div id="container">
            <asp:HiddenField runat="server" id="indexvalue" value="" />

            <div id="labels" style="display:inline; float:left;">
                <div id='label' style="margin-top:5px; margin-bottom:0px;">First Name:</div>
                <div id='label' style="margin-top:5px; margin-bottom:0px;">Last Name:</div>
                <div id='label' style="margin-top:5px; margin-bottom:0px;">Company:</div>
                <div id='label' style="margin-top:5px; margin-bottom:0px;">Title:</div>
            </div>

            <div id="textbox" style="display:inline; float:left;">
                <asp:TextBox style='margin-left:20px' ID="firstname" runat="server"></asp:TextBox> <br />
                <asp:TextBox style='margin-left:20px' ID="lastname" runat="server"></asp:TextBox> <br />
                <asp:TextBox style='margin-left:20px' ID="company" runat="server"></asp:TextBox> <br />
                <asp:TextBox style='margin-left:20px' ID="title" runat="server"></asp:TextBox> <br />
            </div>
            <div id="filler" style="clear: both; width:100%; height:100%; background-color:red; display:block;"></div>
            </div>
            
            
           <div style="clear: both; margin-top:15px; display:inline; float:left; margin-left:20px;">
                <div>
                 <asp:Label style="display:block;" runat="server" ID="label1">Emails</asp:Label>
                <asp:TextBox runat="server" ID="email" OnClick="button1_Click"/>
                </div>
                <div><asp:Button UseSubmitBehavior=false runat="server" ID="button2" Text="Add" OnClick="email_add"/>
                    <asp:Button runat="server" ID="button3" Text="Remove" OnClick="button3_Click"/>
                </div>
                <asp:ListBox style="margin-top:95px" ID="emails" SelectionMode="Single" runat="server" Width="181px">
                </asp:ListBox>

            </div>
            <div style="margin-top:15px; display:inline; float:left; margin-left:20px;">
                <asp:Label style="display:block;" runat="server" ID="label2">Addresses</asp:Label>
                <asp:TextBox style="display:block;" runat="server" ID="street" />
                <asp:Label runat="server" ID="label5">Suite</asp:Label>
                <asp:Label style="margin-left:40px;" runat="server" ID="label6">City</asp:Label>
                <div style="display:block;" ></div>
                <asp:TextBox runat="server" ID="suite" Width="50px"/>
                <asp:TextBox runat="server" ID="city" width="100px"/>
                <div style="display:block;" ></div>
                <asp:Label runat="server" ID="label4">State</asp:Label>
                <asp:Label style="margin-left:75px;" runat="server" ID="label7">Zip</asp:Label>
                <div style="display:block;" ></div>
                <asp:TextBox runat="server" ID="state" Width="100px"/>
                <asp:TextBox runat="server" ID="zip" width="50px"/>

                <div><asp:Button runat="server" ID="addaddress" Text="Add" OnClick="address_add" />
                    <asp:Button runat="server" ID="removeaddress" Text="Remove" OnClick="address_remove" />
                </div>
                <asp:ListBox ID="addresses" SelectionMode="Single" runat="server" Width="181px">
                </asp:ListBox>
            </div>
            <div style="margin-top:15px; display:inline; float: left; margin-left:20px;">
                <div>
                 <asp:Label style="display:block;" runat="server" ID="label3">Numbers</asp:Label>
                 <asp:TextBox runat="server" ID="areacode" Width="31px"/>
                 <asp:TextBox runat="server" ID="phonenumber" />
                 <div style="display:block;" ></div>
                <asp:Label runat="server" ID="label9">Type</asp:Label>
                <asp:dropdownlist id="numberType" runat="server">
                    <asp:ListItem>Phone</asp:ListItem>
                    <asp:ListItem>Fax</asp:ListItem>
                </asp:dropdownlist>
                </div>
                <div><asp:Button runat="server" ID="addphone" Text="Add" OnClick="numbers_add"/>
                    <asp:Button runat="server" ID="removephone" Text="Remove" OnClick="number_remove"/>
                </div>
                <asp:ListBox style="margin-top:70px" ID="numbers" SelectionMode="Single" runat="server" Width="181px">
                </asp:ListBox>
            </div>
            <asp:Button runat="server" style="margin-top:230px; margin-left:30px;"  ID="submitbutton" Text="Update Contact" OnClick="submitbutton_Click"/>
            <div id="filler" style="clear: both; width:100%; height:100%; background-color:red; display:block;"></div>
            </div>
        <!-- Regular Expressions -->
        <asp:regularexpressionvalidator style="color:red;" ValidationExpression="^\d{3}$" ControlToValidate="areacode" runat="server" errormessage="Area code must be 3 numeric digits."></asp:regularexpressionvalidator> 
        <asp:regularexpressionvalidator style="color:red;" ValidationExpression="^\d{7}$" ControlToValidate="phonenumber" runat="server" errormessage="Phone number must be 7 numeric digits."></asp:regularexpressionvalidator> 
        </form>
    </div>

    </div>
    <div runat="server" id="testy"></div>
</body>
</html>
