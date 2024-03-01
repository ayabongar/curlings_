<%@ Page Title="" Language="C#" MasterPageFile="~/CmsPages.master" AutoEventWireup="true" CodeFile="AddUser.aspx.cs" Inherits="NTQR_AddUser" %>

<%@ Register Src="../Admin/UserSelector.ascx" TagName="UserSelector" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.9.1.js"></script>
    <script src="../Scripts/webservices.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">



    <div class="panel panel-primary">
        <div class="panel-heading">
            ADD USERS
        </div>
        <div class="panel-body">
            <div class="pageBody">



                <fieldset>
                    <legend>Add New User</legend>
                    <table style="width: 60%; text-align: left;">

                        <tr>
                            <td style="padding-left: 15px;" >
                                <strong>Search User (SID/Name/Surname):</strong>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 15px; max-width: 300px" colspan="2">
                                <uc1:UserSelector ID="UserSelector1" runat="server" class="width-20"  />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 15px;" >
                                <strong>Crop and Upload your Signature in .png format:</strong>
                            </td>
                        </tr>
                        <tr id="trSignature" runat="server" colspan="2">
                            <td>
                                <asp:FileUpload runat="server" ID="fileUpload" Width="425px" />
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 15px;">
                                <strong>Select User Role:</strong>
                            </td>
                             <td style="padding-left: 15px;" id="tdUnits" runat="server">
                                <strong>Units: <span style="color: red">*</span></strong>
                            </td>
                        </tr>
                        <tr >
                            <td style="padding-left: 15px;vertical-align:top" >
                                
                                <asp:CheckBoxList runat="server" ID="chkRoles" >
                                </asp:CheckBoxList>
                            </td>
                            <td style="padding-left: 15px;" colspan="2">
                               
                                   <asp:CheckBoxList ID="drpUnits" Width="300px"  runat="server" ValidationGroup="popup" >
                                            </asp:CheckBoxList>
                                           


                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" CssClass="buttons" OnClick="Save" Text="Add Process User" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="buttons" Text="Cancel"
                                    OnClick="btnCancel_Click" />
                        </tr>
                    </table>
                </fieldset>


            </div>
        </div>
    </div>

</asp:Content>

