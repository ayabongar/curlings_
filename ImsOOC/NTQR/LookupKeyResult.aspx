<%@ Page Title="" Language="C#" MasterPageFile="~/CmsPages.master" AutoEventWireup="true" CodeFile="LookupKeyResult.aspx.cs" Inherits="NTQR_LookupKeyResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">Add Or Update Key Result lookups by Objectives</div>
        <div class="pageBody">
            <asp:ListView ID="ListView1" runat="server" DataKeyNames="Id" DataSourceID="SqlDataSource1" InsertItemPosition="LastItem" OnItemInserting="ListView1_ItemInserting" OnItemUpdating="ListView1_ItemUpdating">
                <AlternatingItemTemplate>
                    <tr>
                        <td>

                            <asp:LinkButton ID="EditButton" runat="server" CausesValidation="false" class="btn btn-xs btn-primary" CommandName="Edit" Text="Edit">
												<i class="ace-icon fa fa-pencil bigger-110"></i>	Edit
												
                            </asp:LinkButton>
                        </td>
                        <td style="visibility:hidden">
                            <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                        </td>
                        <td>
                            <asp:DropDownList ID="fk_StrategicObjective_IDDropDownList" Enabled="false" runat="server" SelectedValue='<%# Bind("fk_StrategicObjective_ID") %>'
                                DataSource="<%# dsObjectives %>" DataTextField="Name" DataValueField="Id" />
                        </td>
                        <td>
                            <asp:Label ID="NameLabel" runat="server" Text='<%# Eval("Name") %>' />
                        </td>
                        <td>
                            <asp:DropDownList ID="IsActive" Width="100px" Enabled="false" runat="server" SelectedValue='<%# Bind("IsActive") %>'
                                >
                               <asp:ListItem   Value="True" Text="Active" />
                                <asp:ListItem Value="False" Text="In Active" />
                            </asp:DropDownList>
                        </td>

                    </tr>
                </AlternatingItemTemplate>
                <EditItemTemplate>
                    <tr>
                        <td>
                            <nobr>
                                <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="false" class="btn btn-xs btn-primary" CommandName="Update" Text="Update">
                                    <i class="ace-icon fa fa-save bigger-110"></i>Update
												
                                </asp:LinkButton>
                                |<asp:LinkButton ID="CancelButton" runat="server" CausesValidation="false" class="btn btn-xs btn-primary" CommandName="Cancel" Text="Cancel">
                                    <i class="ace-icon fa fa-edit smaller-90"></i>Cancel
												
                                </asp:LinkButton>
                            </nobr>
                        </td>
                        <td style="visibility:hidden" >
                            <asp:Label ID="IdLabel" runat="server" Text='<%# Bind("Id") %>' />

                        </td>
                        <td>

                            <asp:DropDownList ID="fk_StrategicObjective_IDDropDownList" runat="server" SelectedValue='<%# Bind("fk_StrategicObjective_ID") %>'
                                DataSource="<%# dsObjectives %>" DataTextField="Name" DataValueField="Id" />


                        </td>
                        <td>
                            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                        </td>

                        <td>

                            <asp:DropDownList ID="IsActive" Enabled="True" runat="server" SelectedValue='<%# Bind("IsActive")  %>'
                                 >
                             <asp:ListItem   Value="True" Text="Active" />
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
                                <asp:LinkButton ID="InsertButton" runat="server" class="btn btn-xs btn-primary" CausesValidation="false" CommandName="Insert" Text="Insert">
                                    <i class="ace-icon fa fa-save bigger-110"></i>Insert
												
                                </asp:LinkButton>
                                |<asp:LinkButton ID="CancelButton" runat="server" class="btn btn-xs btn-primary" CausesValidation="false" CommandName="Cancel" Text="Clear">
                                    <i class="ace-icon fa fa-edit smaller-90"></i>Clear
												
                                </asp:LinkButton>
                            </nobr>

                        </td>
                        <td  style="visibility:hidden">&nbsp;</td>
                        <td>


                            <asp:DropDownList ID="fk_StrategicObjective_IDDropDownList" runat="server" SelectedValue='<%# Bind("fk_StrategicObjective_ID") %>'
                                DataSource="<%# dsObjectives %>" DataTextField="Name" DataValueField="Id" />

                        </td>
                        <td>
                            <asp:TextBox ID="NameTextBox" runat="server" Text='<%# Bind("Name") %>' />
                            <asp:RequiredFieldValidator ErrorMessage="Enter KR" ForeColor="Red" ControlToValidate="NameTextBox" runat="server" />
                        </td>

                          <td>

                            <asp:DropDownList ID="IsActiveDropDownList" Enabled="True" runat="server" SelectedValue='<%# Bind("IsActive")  %>'
                                 >
                                <asp:ListItem   Value="True" Text="Active" />
                                <asp:ListItem Value="False" Text="In Active" />
                            </asp:DropDownList>
                        </td>

                    </tr>
                </InsertItemTemplate>
                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:LinkButton ID="EditButton" runat="server" class="btn btn-xs btn-primary" CausesValidation="false" CommandName="Edit" Text="Edit">
												<i class="ace-icon fa fa-pencil bigger-110"></i>	Edit
												
                            </asp:LinkButton>
                        </td>
                        <td style="visibility:hidden">
                            <asp:Label ID="IdLabel" runat="server" Text='<%# Eval("Id") %>' />
                        </td>
                        <td>

                            <asp:DropDownList ID="fk_StrategicObjective_IDDropDownList" Enabled="false" runat="server" SelectedValue='<%# Eval("fk_StrategicObjective_ID") %>'
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
                                    <tr runat="server" style="background-color:steelblue;color:white;height:20px">
                                        <th runat="server"></th>
                                        <th runat="server" style="visibility:hidden">Id</th>
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
                            <td runat="server" style="text-align: center; background-color: #5D7B9D; font-family: Verdana, Arial, Helvetica, sans-serif; color: #FFFFFF">
                                <asp:DataPager ID="DataPager1" runat="server" PageSize="20">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                        <asp:NumericPagerField />
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowLastPageButton="True" ShowNextPageButton="False" ShowPreviousPageButton="False" />
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>
                <SelectedItemTemplate>
                    <tr>
                        <td>
                            <asp:Button ID="EditButton" runat="server" CommandName="Edit" Text="Edit" />
                        </td>
                        <td style="visibility:hidden">
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
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db %>" InsertCommand="INSERT INTO [dbo].[NTQ_Lookup_KeyResult]
           ([fk_StrategicObjective_ID]
           ,[Name]
           ,[timestamp]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[IsActive]
          )
     VALUES
           (@fk_StrategicObjective_ID,
           @Name,
           getdate(), 
           @CreatedBy,
            getdate(),
           @IsActive
         )"
                OldValuesParameterFormatString="original_{0}" SelectCommand="SELECT DISTINCT [Id], [fk_StrategicObjective_ID], [Name], [timestamp], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate],IsActive FROM [NTQ_Lookup_KeyResult] where fk_StrategicObjective_ID <> '' order by fk_StrategicObjective_ID,Name"
                UpdateCommand="UPDATE [dbo].[NTQ_Lookup_KeyResult]
   SET [fk_StrategicObjective_ID] = @fk_StrategicObjective_ID
      ,[Name] = @Name     
      ,[ModifiedBy] =@ModifiedBy
      ,[ModifiedDate] = @ModifiedDate
      ,[IsActive] = @IsActive
 WHERE Id = @Id"
                OnUpdated="SqlDataSource1_Updated" OnUpdating="SqlDataSource1_Updating" OnInserting="SqlDataSource1_Inserting">
                <InsertParameters>
                    <asp:Parameter Name="fk_StrategicObjective_ID" />
                    <asp:Parameter Name="Name" />
                    <asp:Parameter Name="timestamp" />
                    <asp:Parameter Name="CreatedBy" />  
                    <asp:Parameter Name="CreatedDate" />
                    <asp:Parameter Name="IsActive" />
                </InsertParameters>
                <UpdateParameters>
                    <asp:Parameter Name="fk_StrategicObjective_ID" />
                    <asp:Parameter Name="Name" />
                    <asp:Parameter Name="ModifiedBy" />
                    <asp:Parameter Name="ModifiedDate" />
                    <asp:Parameter Name="Id" />
                    <asp:Parameter Name="IsActive" />
                   
                </UpdateParameters>
            </asp:SqlDataSource>

            <asp:SqlDataSource ID="dsObjectives" runat="server" ConnectionString="<%$ ConnectionStrings:db %>" SelectCommand="SELECT [Id], [Name] FROM [NTQ_Lookup_StrategicObjective]"></asp:SqlDataSource>
        </div>
    </div>
</asp:Content>

