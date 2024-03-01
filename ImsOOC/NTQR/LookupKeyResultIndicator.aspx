﻿<%@ Page Title="" Language="C#" MasterPageFile="~/CmsPages.master" AutoEventWireup="true" CodeFile="LookupKeyResultIndicator.aspx.cs" Inherits="NTQR_LookupKeyResultIndicator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">Add Or Update Key Result Indicator lookup by Objectives</div>
        <div class="pageBody">
            <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1" DataKeyNames="Id" InsertItemPosition="LastItem" OnItemEditing="ListView1_ItemEditing">
                <AlternatingItemTemplate>
                    <tr style="background-color: #DCDCDC;">
                        <td>
                            <asp:LinkButton ID="EditButton" runat="server" class="btn btn-xs btn-primary" CausesValidation="false" CommandName="Edit" Text="Edit">
												<i class="ace-icon fa fa-pencil bigger-110"></i>	Edit
												
                            </asp:LinkButton>
                        </td>
                        <td>
                            <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                        </td>
                        <td>

                            <asp:DropDownList ID="fk_StrategicObjective_IDDropDownList" runat="server" Enabled="false" SelectedValue='<%# Eval("fk_StrategicObjective_ID") %>'
                                DataSource="<%# dsObjectives %>" DataTextField="Name" DataValueField="Id" />
                        </td>
                        <td>
                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                        </td>
                        <td>
                            <asp:DropDownList ID="IsActive" Width="100px" Enabled="false" runat="server" SelectedValue='<%# Bind("IsActive") %>'>
                                <asp:ListItem Value="True" Text="Active" />
                                <asp:ListItem Value="False" Text="In Active" />
                            </asp:DropDownList>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <EditItemTemplate>
                    <tr style="background-color: #008A8C; color: #FFFFFF;">
                        <td>
                            <nobr>
                                <asp:LinkButton ID="UpdateButton" runat="server" class="btn btn-xs btn-primary"  CausesValidation="false" CommandName="Update" Text="Update">
                                    <i class="ace-icon fa fa-save bigger-110"></i>Update
												
                                </asp:LinkButton>
                                |<asp:LinkButton ID="CancelButton" runat="server" class="btn btn-xs btn-primary" CausesValidation="false"  CommandName="Cancel" Text="Cancel">
                                    <i class="ace-icon fa fa-edit smaller-90"></i>Cancel
												
                                </asp:LinkButton>
                            </nobr>
                        </td>
                        <td>
                            <asp:Label ID="IdLabel1" runat="server" Text='<%# Eval("Id") %>' />
                        </td>
                        <td>
                            <asp:DropDownList ID="fk_StrategicObjective_IDDropDownList" runat="server" SelectedValue='<%# Bind("fk_StrategicObjective_ID") %>'
                                DataSource="<%# dsObjectives %>" DataTextField="Name" DataValueField="Id" />
                        </td>
                        <td>
                            <asp:TextBox ID="Name1TextBox" runat="server" Text='<%# Bind("Name") %>' />
                           
                
                        </td>
                        <td>

                            <asp:DropDownList ID="IsActive" Enabled="True" runat="server" SelectedValue='<%# Bind("IsActive")  %>'>
                                <asp:ListItem Value="True" Text="Active" />
                                <asp:ListItem Value="False" Text="In Active" />
                            </asp:DropDownList>
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
                            <nobr>
                                <asp:LinkButton ID="InsertButton" runat="server" class="btn btn-xs btn-primary" CommandName="Insert" Text="Insert">
                                    <i class="ace-icon fa fa-save bigger-110"></i>Insert
												
                                </asp:LinkButton>
                                |<asp:LinkButton ID="CancelButton" runat="server" class="btn btn-xs btn-primary" CommandName="Cancel" Text="Clear">
                                    <i class="ace-icon fa fa-edit smaller-90"></i>Clear
												
                                </asp:LinkButton>
                            </nobr>

                        </td>
                        <td>&nbsp;</td>
                        <td>

                            <asp:DropDownList ID="fk_StrategicObjective_IDDropDownList" runat="server" SelectedValue='<%# Bind("fk_StrategicObjective_ID") %>'
                                DataSource="<%# dsObjectives %>" DataTextField="Name" DataValueField="Id" />
                        </td>
                        <td>
                            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                                 <asp:RequiredFieldValidator ErrorMessage="Enter KR Indicator" ForeColor="Red" ControlToValidate="NameTextBox" runat="server" />
                   
                        </td>
                        <td>

                            <asp:DropDownList ID="IsActiveDropDownList" Enabled="True" runat="server" SelectedValue='<%# Bind("IsActive")  %>'>
                                <asp:ListItem Value="True" Text="Active" />
                                <asp:ListItem Value="False" Text="In Active" />
                            </asp:DropDownList>
                        </td>

                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr style="background-color: whitesmoke; color: #000000;">
                        <td>
                            <asp:LinkButton ID="EditButton" runat="server" class="btn btn-xs btn-primary"  CausesValidation="false"  CommandName="Edit" Text="Edit">
												<i class="ace-icon fa fa-pencil bigger-110"></i>	Edit
												
                            </asp:LinkButton>
                        </td>
                        <td>
                            <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                        </td>
                        <td>
                            <asp:DropDownList ID="fk_StrategicObjective_IDDropDownList" Enabled="false"  runat="server" SelectedValue='<%# Eval("fk_StrategicObjective_ID") %>'
                                DataSource="<%# dsObjectives %>" DataTextField="Name" DataValueField="Id" />
                        </td>
                        <td>
                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                        </td>

                         <td>
                            <asp:DropDownList ID="IsActive" Width="100px" Enabled="false"  runat="server" SelectedValue='<%# Bind("IsActive") %>'
                                >
                               <asp:ListItem   Value="True"  Text="Active" />
                                <asp:ListItem Value="False" Text="In Active" />
                            </asp:DropDownList>
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
                                        <th runat="server">Id</th>
                                        <th runat="server">StrategicObjective</th>
                                        <th runat="server">Name</th>
                                         <th runat="server">Is Active</th>
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
                            <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                        </td>
                        <td>
                            <asp:Label ID="fk_StrategicObjective_IDLabel" runat="server" Text='<%# Eval("fk_StrategicObjective_ID") %>' />
                        </td>
                        <td>
                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                        </td>

                    </tr>
                </SelectedItemTemplate>
            </asp:ListView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConflictDetection="CompareAllValues" ConnectionString="<%$ ConnectionStrings:db %>"
                DeleteCommand="DELETE FROM [NTQ_Lookup_KeyResultIndicator] WHERE [Id] = @original_Id AND (([fk_StrategicObjective_ID] = @original_fk_StrategicObjective_ID) OR ([fk_StrategicObjective_ID] IS NULL AND @original_fk_StrategicObjective_ID IS NULL)) AND (([Name] = @original_Name) OR ([Name] IS NULL AND @original_Name IS NULL)) AND (([timestamp] = @original_timestamp) OR ([timestamp] IS NULL AND @original_timestamp IS NULL)) AND (([CreatedBy] = @original_CreatedBy) OR ([CreatedBy] IS NULL AND @original_CreatedBy IS NULL)) AND (([CreatedDate] = @original_CreatedDate) OR ([CreatedDate] IS NULL AND @original_CreatedDate IS NULL)) AND (([ModifiedBy] = @original_ModifiedBy) OR ([ModifiedBy] IS NULL AND @original_ModifiedBy IS NULL)) AND (([ModifiedDate] = @original_ModifiedDate) OR ([ModifiedDate] IS NULL AND @original_ModifiedDate IS NULL))"
                InsertCommand="INSERT INTO [NTQ_Lookup_KeyResultIndicator] ([fk_StrategicObjective_ID], [Name], [timestamp], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate],[IsActive]) VALUES (@fk_StrategicObjective_ID, @Name, @timestamp, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate,@IsActive)" OldValuesParameterFormatString="original_{0}"
                SelectCommand="SELECT * FROM [NTQ_Lookup_KeyResultIndicator] order by fk_StrategicObjective_ID,Name"
                UpdateCommand="UPDATE [NTQ_Lookup_KeyResultIndicator] SET [fk_StrategicObjective_ID] = @fk_StrategicObjective_ID, [Name] = @Name, [timestamp] = @timestamp, [CreatedBy] = @CreatedBy, [CreatedDate] = @CreatedDate, [ModifiedBy] = @ModifiedBy, [ModifiedDate] = @ModifiedDate,[IsActive]=@IsActive WHERE [Id] = @original_Id AND (([fk_StrategicObjective_ID] = @original_fk_StrategicObjective_ID) OR ([fk_StrategicObjective_ID] IS NULL AND @original_fk_StrategicObjective_ID IS NULL)) AND (([Name] = @original_Name) OR ([Name] IS NULL AND @original_Name IS NULL)) AND (([timestamp] = @original_timestamp) OR ([timestamp] IS NULL AND @original_timestamp IS NULL)) AND (([CreatedBy] = @original_CreatedBy) OR ([CreatedBy] IS NULL AND @original_CreatedBy IS NULL)) AND (([CreatedDate] = @original_CreatedDate) OR ([CreatedDate] IS NULL AND @original_CreatedDate IS NULL)) AND (([ModifiedBy] = @original_ModifiedBy) OR ([ModifiedBy] IS NULL AND @original_ModifiedBy IS NULL)) AND (([ModifiedDate] = @original_ModifiedDate) OR ([ModifiedDate] IS NULL AND @original_ModifiedDate IS NULL))" OnUpdating="SqlDataSource1_Updating">
                <DeleteParameters>
                    <asp:Parameter Name="original_Id" Type="Int32" />
                    <asp:Parameter Name="original_fk_StrategicObjective_ID" Type="Int32" />
                    <asp:Parameter Name="original_Name" Type="String" />
                    <asp:Parameter Name="original_timestamp" Type="DateTime" />
                    <asp:Parameter Name="original_CreatedBy" Type="Int32" />
                    <asp:Parameter Name="original_CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="original_ModifiedBy" Type="Int32" />
                    <asp:Parameter Name="original_ModifiedDate" Type="DateTime" />
                </DeleteParameters>
                <InsertParameters>
                    <asp:Parameter Name="fk_StrategicObjective_ID" Type="Int32" />
                    <asp:Parameter Name="Name" Type="String" />
                    <asp:Parameter Name="timestamp" Type="DateTime" />
                    <asp:Parameter Name="CreatedBy" Type="Int32" />
                    <asp:Parameter Name="CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="ModifiedBy" Type="Int32" />
                    <asp:Parameter Name="ModifiedDate" Type="DateTime" />
                     <asp:Parameter Name="IsActive" Type="Boolean" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="fk_StrategicObjective_ID" Type="Int32" />
                    <asp:Parameter Name="Name1" Type="String" />
                    <asp:Parameter Name="timestamp" Type="DateTime" />
                    <asp:Parameter Name="CreatedBy" Type="Int32" />
                    <asp:Parameter Name="CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="ModifiedBy" Type="Int32" />
                    <asp:Parameter Name="ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="original_Id" Type="Int32" />
                    <asp:Parameter Name="original_fk_StrategicObjective_ID" Type="Int32" />
                    <asp:Parameter Name="original_Name" Type="String" />
                    <asp:Parameter Name="original_timestamp" Type="DateTime" />
                    <asp:Parameter Name="original_CreatedBy" Type="Int32" />
                    <asp:Parameter Name="original_CreatedDate" Type="DateTime" />
                    <asp:Parameter Name="original_ModifiedBy" Type="Int32" />
                    <asp:Parameter Name="original_ModifiedDate" Type="DateTime" />
                    <asp:Parameter Name="IsActive" Type="Boolean" />
                </UpdateParameters>
            </asp:SqlDataSource>

            <asp:SqlDataSource ID="dsObjectives" runat="server" ConnectionString="<%$ ConnectionStrings:db %>" SelectCommand="SELECT [Id], [Name] FROM [NTQ_Lookup_StrategicObjective]"></asp:SqlDataSource>
        </div>
    </div>
</asp:Content>

