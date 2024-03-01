<%@ Page Title="" Language="C#" MasterPageFile="~/CmsPages.master" AutoEventWireup="true" CodeFile="NotificationDates.aspx.cs" Inherits="NTQR_NotificationDates" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
        <div class="panel panel-primary">
        <div class="panel-heading">Update Email Notifications lookup</div>
        <div class="pageBody">
    <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1" DataKeyNames="ID" >
        <AlternatingItemTemplate>
            <tr style="background-color: #FFF8DC;">
                <td>
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                </td>
               
                <td>
                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                </td>

                 <td>
                  <%--  <asp:Label ID="Q1DateLabel" runat="server" Text='<%# Eval("Q1Date", "{0:MMM d, yyyy}") %>' />--%>
                    <asp:HyperLink runat="server" ID="hl" NavigateUrl='<%# "NTQ_ReportViewer.aspx?Id="+ Eval("Id") +"&q=" + 1 %>' Text='<%# Eval("Q1Date", "{0:MMM d, yyyy}") %>'></asp:HyperLink>
                </td>
                <td>
                    <%--<asp:Label ID="Q2DateLabel" runat="server" Text='<%# Eval("Q2Date", "{0:MMM d, yyyy}") %>' />--%>
                     <asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl='<%# "NTQ_ReportViewer.aspx?Id="+ Eval("Id") +"&q=" + 2 %>' Text='<%# Eval("Q2Date", "{0:MMM d, yyyy}") %>'></asp:HyperLink>
                </td>
                <td>
                    <%--<asp:Label ID="Q3DateLabel" runat="server" Text='<%# Eval("Q3Date", "{0:MMM d, yyyy}") %>' />--%>
                    <asp:HyperLink runat="server" ID="HyperLink2" NavigateUrl='<%# "NTQ_ReportViewer.aspx?Id="+ Eval("Id") +"&q=" + 3 %>' Text='<%# Eval("Q3Date", "{0:MMM d, yyyy}") %>'></asp:HyperLink>
                </td>
                <td>
                    <%--<asp:Label ID="Q4DateLabel" runat="server" Text='<%# Eval("Q4Date", "{0:MMM d, yyyy}") %>' />--%>
                    <asp:HyperLink runat="server" ID="HyperLink3" NavigateUrl='<%# "NTQ_ReportViewer.aspx?Id="+ Eval("Id") +"&q=" + 4 %>' Text='<%# Eval("Q4Date", "{0:MMM d, yyyy}") %>'></asp:HyperLink>
                </td>
               
                <td>
                    <asp:Label ID="EmailMocksLabel" runat="server" Text='<%# Eval("EmailMocks") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="isActiveCheckBox" runat="server" Checked='<%# Eval("isActive") %>' Enabled="false" />
                </td>
               
                <td>
                    <asp:CheckBox ID="isProdCheckBox" runat="server" Checked='<%# Eval("isProd") %>' Enabled="false" />
                </td>

            </tr>
        </AlternatingItemTemplate>
        <EditItemTemplate>
            <tr style="background-color: #008A8C; color: #FFFFFF;">
                <td>
                    <asp:Button ID="UpdateButton" runat="server" CommandName="Update" Text="Update" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Cancel" />
                </td>
                <td>
                    <asp:Label ID="IDLabel1" runat="server" Text='<%# Eval("ID") %>' />
                </td>
               
                <td>
                    <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                </td>

                <td>
                    <asp:TextBox ID="Q1DateTextBox" runat="server" Text='<%# Bind("Q1Date", "{0:MMM d, yyyy}") %>' />
                     <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="yyyy-MM-dd"
                                        Enabled="True" TargetControlID="Q1DateTextBox">
                                    </asp:CalendarExtender>
                </td>
                <td>
                    <asp:TextBox ID="Q2DateTextBox" runat="server" Text='<%# Bind("Q2Date", "{0:MMM d, yyyy}") %>' />
                       <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="yyyy-MM-dd"
                                        Enabled="True" TargetControlID="Q2DateTextBox">
                                    </asp:CalendarExtender>
                </td>
                <td>
                    <asp:TextBox ID="Q3DateTextBox" runat="server" Text='<%# Bind("Q3Date", "{0:MMM d, yyyy}") %>' />
                     <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="yyyy-MM-dd"
                                        Enabled="True" TargetControlID="Q3DateTextBox">
                                    </asp:CalendarExtender>
                </td>
                <td>
                    <asp:TextBox ID="Q4DateTextBox" runat="server" Text='<%# Bind("Q4Date", "{0:MMM d, yyyy}") %>' />
                    <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="yyyy-MM-dd"
                                        Enabled="True" TargetControlID="Q4DateTextBox">
                                    </asp:CalendarExtender>
                </td>
             
                <td>
                    <asp:TextBox ID="EmailMocksTextBox" runat="server" Text='<%# Bind("EmailMocks") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="isActiveCheckBox" runat="server" Checked='<%# Bind("isActive") %>' />
                </td>
                
                <td>
                    <asp:CheckBox ID="isProdCheckBox" runat="server" Checked='<%# Bind("isProd") %>' />
                </td>

            </tr>
        </EditItemTemplate>
        <EmptyDataTemplate>
            <table runat="server" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px;">
                <tr>
                    <td>No data was returned.</td>
                </tr>
            </table>
        </EmptyDataTemplate>
        <InsertItemTemplate>
            <tr style="">
                <td>
                    <asp:Button ID="InsertButton" runat="server" CommandName="Insert" Text="Insert" />
                    <asp:Button ID="CancelButton" runat="server" CommandName="Cancel" Text="Clear" />

                </td>
                <td>&nbsp;</td>
               
                <td>
                    <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                </td>
                <td>
                    <asp:TextBox ID="Q1DateTextBox" runat="server" Text='<%# Bind("Q1Date", "{0:MMM d, yyyy}") %>' />
                          <asp:CalendarExtender ID="CalendarExtender5" runat="server" Format="yyyy-MM-dd"
                                        Enabled="True" TargetControlID="Q1DateTextBox">
                                    </asp:CalendarExtender>
                <td>
                    <asp:TextBox ID="Q2DateTextBox" runat="server" Text='<%# Bind("Q2Date", "{0:MMM d, yyyy}") %>' />
                     <asp:CalendarExtender ID="CalendarExtender6" runat="server" Format="yyyy-MM-dd"
                                        Enabled="True" TargetControlID="Q2DateTextBox">
                                    </asp:CalendarExtender>
                </td>
                <td>
                    <asp:TextBox ID="Q3DateTextBox" runat="server" Text='<%# Bind("Q3Date", "{0:MMM d, yyyy}") %>' />
                     <asp:CalendarExtender ID="CalendarExtender7" runat="server" Format="yyyy-MM-dd"
                                        Enabled="True" TargetControlID="Q3DateTextBox">
                                    </asp:CalendarExtender>
                </td>
                <td>
                    <asp:TextBox ID="Q4DateTextBox" runat="server" Text='<%# Bind("Q4Date", "{0:MMM d, yyyy}") %>' />
                     <asp:CalendarExtender ID="CalendarExtender8" runat="server" Format="yyyy-MM-dd"
                                        Enabled="True" TargetControlID="Q4DateTextBox">
                                    </asp:CalendarExtender>
                </td>
              
                <td>
                    <asp:TextBox ID="EmailMocksTextBox" runat="server" Text='<%# Bind("EmailMocks") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="isActiveCheckBox" runat="server" Checked='<%# Bind("isActive") %>' />
                </td>
               
                <td>
                    <asp:CheckBox ID="isProdCheckBox" runat="server" Checked='<%# Bind("isProd") %>' />
                </td>
            </tr>
        </InsertItemTemplate>
        <ItemTemplate>
            <tr style="background-color: #DCDCDC; color: #000000;">
                <td>
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                </td>
               
                <td>
                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                </td>



                <td>
                  <%--  <asp:Label ID="Q1DateLabel" runat="server" Text='<%# Eval("Q1Date", "{0:MMM d, yyyy}") %>' />--%>
                    <asp:HyperLink runat="server" ID="hl" NavigateUrl='<%# "NTQ_ReportViewer.aspx?Id="+ Eval("Id") +"&q=" + 1 %>' Text='<%# Eval("Q1Date", "{0:MMM d, yyyy}") %>'></asp:HyperLink>
                </td>
                <td>
                    <%--<asp:Label ID="Q2DateLabel" runat="server" Text='<%# Eval("Q2Date", "{0:MMM d, yyyy}") %>' />--%>
                     <asp:HyperLink runat="server" ID="HyperLink1" NavigateUrl='<%# "NTQ_ReportViewer.aspx?Id="+ Eval("Id") +"&q=" + 2 %>' Text='<%# Eval("Q2Date", "{0:MMM d, yyyy}") %>'></asp:HyperLink>
                </td>
                <td>
                    <%--<asp:Label ID="Q3DateLabel" runat="server" Text='<%# Eval("Q3Date", "{0:MMM d, yyyy}") %>' />--%>
                    <asp:HyperLink runat="server" ID="HyperLink2" NavigateUrl='<%# "NTQ_ReportViewer.aspx?Id="+ Eval("Id") +"&q=" + 3 %>' Text='<%# Eval("Q3Date", "{0:MMM d, yyyy}") %>'></asp:HyperLink>
                </td>
                <td>
                    <%--<asp:Label ID="Q4DateLabel" runat="server" Text='<%# Eval("Q4Date", "{0:MMM d, yyyy}") %>' />--%>
                    <asp:HyperLink runat="server" ID="HyperLink3" NavigateUrl='<%# "NTQ_ReportViewer.aspx?Id="+ Eval("Id") +"&q=" + 4 %>' Text='<%# Eval("Q4Date", "{0:MMM d, yyyy}") %>'></asp:HyperLink>
                </td>
                
                <td>
                    <asp:Label ID="EmailMocksLabel" runat="server" Text='<%# Eval("EmailMocks") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="isActiveCheckBox" runat="server" Checked='<%# Eval("isActive") %>' Enabled="false" />
                </td>
               
                <td>
                    <asp:CheckBox ID="isProdCheckBox" runat="server" Checked='<%# Eval("isProd") %>' Enabled="false" />
                </td>



            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table runat="server">
                <tr runat="server">
                    <td runat="server">
                        <table id="itemPlaceholderContainer" runat="server" border="1" style="background-color: #FFFFFF; border-collapse: collapse; border-color: #999999; border-style: none; border-width: 1px; font-family: Verdana, Arial, Helvetica, sans-serif;">
                            <tr runat="server" style="background-color: #DCDCDC; color: #000000;">
                                <th runat="server"></th>
                                <th runat="server">ID</th>
                             
                                <th runat="server">Description</th>

                                <th runat="server">Q1Date</th>
                                <th runat="server">Q2Date</th>
                                <th runat="server">Q3Date</th>
                                <th runat="server">Q4Date</th>                                
                                <th runat="server">EmailMocks</th>
                                <th runat="server">isActive</th>
                                
                                <th runat="server">isProd</th>

                            </tr>
                            <tr id="itemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server">
                    <td runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;">
                        <asp:DataPager ID="DataPager1" runat="server">
                            <Fields>
                                <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True" />
                            </Fields>
                        </asp:DataPager>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <SelectedItemTemplate>
            <tr style="background-color: #008A8C; font-weight: bold; color: #FFFFFF;">
                <td>
                    <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                </td>
                <td>
                    <asp:Label ID="IDLabel" runat="server" Text='<%# Eval("ID") %>' />
                </td>
               
                <td>
                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                </td>

                <td>
                    <asp:Label ID="Q1DateLabel" runat="server" Text='<%# Eval("Q1Date") %>' />
                </td>
                <td>
                    <asp:Label ID="Q2DateLabel" runat="server" Text='<%# Eval("Q2Date") %>' />
                </td>
                <td>
                    <asp:Label ID="Q3DateLabel" runat="server" Text='<%# Eval("Q3Date") %>' />
                </td>
                <td>
                    <asp:Label ID="Q4DateLabel" runat="server" Text='<%# Eval("Q4Date") %>' />
                </td>
             
                <td>
                    <asp:Label ID="EmailMocksLabel" runat="server" Text='<%# Eval("EmailMocks") %>' />
                </td>
                <td>
                    <asp:CheckBox ID="isActiveCheckBox" runat="server" Checked='<%# Eval("isActive") %>' Enabled="false" />
                </td>
               
                <td>
                    <asp:CheckBox ID="isProdCheckBox" runat="server" Checked='<%# Eval("isProd") %>' Enabled="false" />
                </td>

            </tr>
        </SelectedItemTemplate>
    </asp:ListView>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db %>"
        DeleteCommand="DELETE FROM [NTQR_ServiceConfig] WHERE [ID] = @original_ID"
        InsertCommand="INSERT INTO [NTQR_ServiceConfig] ([Description], [Q1Date], [Q2Date], [Q3Date], [Q4Date], [AnnualDate], [EmailMocks], [isActive], [testEmail], [isProd]) VALUES (@Description, @Q1Date, @Q2Date, @Q3Date, @Q4Date, @AnnualDate, @EmailMocks, @isActive, @testEmail, @isProd)" OldValuesParameterFormatString="original_{0}"
        SelectCommand="SELECT * FROM [NTQR_ServiceConfig] where id <> 2"
        UpdateCommand="UPDATE [NTQR_ServiceConfig] SET [Description] = @Description, [Q1Date] = @Q1Date, [Q2Date] = @Q2Date, [Q3Date] = @Q3Date, [Q4Date] = @Q4Date,  [EmailMocks] = @EmailMocks, [isActive] = @isActive,  [isProd] = @isProd WHERE [ID] = @original_ID" >
        <DeleteParameters>
            <asp:Parameter Name="original_ID" Type="Int32" />
        </DeleteParameters>
        <InsertParameters>
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="Q1Date" DbType="Date" />
            <asp:Parameter Name="Q2Date" DbType="Date" />
            <asp:Parameter Name="Q3Date" DbType="Date" />
            <asp:Parameter DbType="Date" Name="Q4Date" />
            <asp:Parameter DbType="Date" Name="AnnualDate" />
            <asp:Parameter Name="EmailMocks" Type="String" />
            <asp:Parameter Name="isActive" Type="Boolean" />
            <asp:Parameter Name="testEmail" Type="String" />
            <asp:Parameter Name="isProd" Type="Boolean" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="Description" Type="String" />
            <asp:Parameter Name="Q1Date" DbType="Date" />
            <asp:Parameter Name="Q2Date" DbType="Date" />
            <asp:Parameter Name="Q3Date" DbType="Date" />
            <asp:Parameter Name="Q4Date" DbType="Date" />
            
            <asp:Parameter Name="EmailMocks" Type="String" />
            <asp:Parameter Name="isActive" Type="Boolean" />
         
            <asp:Parameter Name="isProd" Type="Boolean" />
            <asp:Parameter Name="original_ID" Type="Int32" />
        </UpdateParameters>
    </asp:SqlDataSource>

</div>
            </div>
</asp:Content>

