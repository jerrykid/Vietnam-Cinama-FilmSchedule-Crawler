<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PCRSchedule._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Process Film Schedule</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Button runat="server" ID="btnGetSchedule" Text="Get Film Schedule" OnClick="btnGetSchedule_Click"  />
    <asp:Label runat="server" ID="lblMessage" Text=""   />
    <br /><br /><br />
    <asp:Label runat="server" ID="Label1" Text="List File From Sever"   />
    <div runat="server" id="divListSchedule" >
    
    </div>
    </form>
</body>
</html>
