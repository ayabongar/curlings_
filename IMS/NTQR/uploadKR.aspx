<%@ Page Title="" Language="C#" MasterPageFile="~/CmsPages.master" AutoEventWireup="true" CodeFile="uploadKR.aspx.cs" Inherits="NTQR_uploadKR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="panel panel-primary">
        <div class="panel-heading">Upload TID Image</div>
        <div class="pageBody">
            <div class="form-group has-info" runat="server">
                <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Strategic Objective: <span style="color: red">*</span></label>

                <div class="col-xs-12 col-sm-10">


                    <asp:DropDownList ID="drpStrategicObjective" class="width-100" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:CompareValidator ErrorMessage="StrategicObjective is a required field" ValueToCompare="-99999" ForeColor="Red" ControlToValidate="drpStrategicObjective" runat="server" Operator="NotEqual" SetFocusOnError="True" />




                </div>

            </div>
            <div class="form-group has-info" runat="server">
                <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">TID Description: <span style="color: red">*</span></label>

                <div class="col-xs-12 col-sm-10">


                    <asp:TextBox ID="txtKRO" class="width-100" runat="server"> </asp:TextBox>
                    <asp:RequiredFieldValidator ErrorMessage="description is a required field" ForeColor="Red" ControlToValidate="txtKRO" runat="server" SetFocusOnError="True" />




                </div>

            </div>

            <div class="form-group has-info" runat="server">
                <label style="font-weight: bold" for="inputWarning" class="col-xs-12 col-sm-2 control-label no-padding-right">Upload KRO Image(.png format): <span style="color: red">*</span></label>

                <div class="col-xs-12 col-sm-10">


                    <asp:FileUpload runat="server" ID="fileUpload" Width="425px" /><asp:Button runat="server"  class="btn btn-xs btn-primary"  ID="btnAddFile" Text="Upload File" OnClick="UploadFile" />



                    <br />
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" OnUpdating="SqlDataSource1_Updating" ConnectionString="<%$ ConnectionStrings:db %>" SelectCommand="SELECT [Id], [fk_StrategicObjective_ID], [IsActive], [Description], [Name] FROM [NTQ_Lookup_TID] WHERE fk_StrategicObjective_ID =@drpStrategicObjective ORDER BY [timestamp] DESC"
                        UpdateCommand="UPDATE NTQ_Lookup_TID SET IsActive = @IsActive, Description=@Description, [ModifiedBy] =@ModifiedBy
                         ,[ModifiedDate] = @ModifiedDate WHERE (Id = @Id)">

                        <SelectParameters>
                            <asp:ControlParameter ControlID="drpStrategicObjective" Name="drpStrategicObjective" />
                        </SelectParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="IsActive" />
                            <asp:Parameter Name="Id" />
                            <asp:Parameter Name="Description" />
                             <asp:Parameter Name="ModifiedBy" />
                    <asp:Parameter Name="ModifiedDate" />
                        </UpdateParameters>
                    </asp:SqlDataSource>

                    <asp:ListView ID="ListView1" runat="server" DataSourceID="SqlDataSource1" DataKeyNames="Id" OnItemUpdating="ListView1_ItemUpdating">
                        <AlternatingItemTemplate>
                            <tr style="background-color: #FFF8DC;">
                                <td>
                                    <asp:LinkButton ID="EditButton" runat="server" class="btn btn-xs btn-primary" CausesValidation="false" CommandName="Edit" Text="Edit">
												<i class="ace-icon fa fa-pencil bigger-110"></i>	Edit
												
                                    </asp:LinkButton>
                                </td>
                                <%--<td>
                                    <asp:DropDownList ID="fk_StrategicObjective_IDDropDownList" Enabled="false" runat="server" SelectedValue='<%# Eval("fk_StrategicObjective_ID") %>' DataSource="<%# dsObjectives %>" DataTextField="Name" DataValueField="Id" />
                                </td>--%>
                                <td>

                                    <asp:CheckBox ID="IsActiveCheckBox" runat="server" Checked='<%# Eval("IsActive") %>' Enabled="false" />
                                </td>
                                <td>
                                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                                </td>
                                <td>
                                    <asp:Image ImageUrl='<%# GetImage(Eval("Name")) %>' runat="server" />
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <EditItemTemplate>
                            <tr style="background-color: #008A8C; color: #FFFFFF;">
                                <td>
                                    <asp:Button ID="UpdateButton" runat="server"  class="btn btn-xs btn-primary"  CausesValidation="false" CommandName="Update" Text="Update" />
                                    <asp:Button ID="CancelButton" runat="server"  class="btn btn-xs btn-primary"  CausesValidation="false" CommandName="Cancel" Text="Cancel" />
                                </td>
                                <%--<td>
                                    <asp:TextBox ID="fk_StrategicObjective_IDTextBox" runat="server" Text='<%# Bind("fk_StrategicObjective_ID") %>' />
                                </td>--%>
                                <td>
                                    <asp:CheckBox ID="IsActiveCheckBox" runat="server" Checked='<%# Bind("IsActive") %>' />
                                </td>
                                <td>

                                    <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                                </td>
                                <td>
                                    <asp:Image ImageUrl='<%# GetImage(Eval("Name")) %>' runat="server" />
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
                                    <asp:Button ID="InsertButton" runat="server"  class="btn btn-xs btn-primary"  CommandName="Insert" Text="Insert" />
                                    <asp:Button ID="CancelButton" runat="server"  class="btn btn-xs btn-primary"  CommandName="Cancel" Text="Clear" />

                                </td>

                               <%-- <td>

                                    <asp:TextBox ID="fk_StrategicObjective_IDTextBox" runat="server" Text='<%# Bind("fk_StrategicObjective_ID") %>' />
                                </td>--%>
                                <td>
                                    <asp:CheckBox ID="IsActiveCheckBox" runat="server" Checked='<%# Bind("IsActive") %>' />


                                </td>
                                <td>

                                    <asp:TextBox ID="DescriptionTextBox" runat="server" Text='<%# Bind("Description") %>' />
                                </td>

                                <td>
                                    <asp:Image ImageUrl='<%# GetImage(Eval("Name")) %>' runat="server" />
                                </td>

                            </tr>
                        </InsertItemTemplate>
                        <ItemTemplate>
                            <tr style="background-color: #DCDCDC; color: #000000;">
                                <td>
                                    <asp:LinkButton ID="EditButton" runat="server" class="btn btn-xs btn-primary" CausesValidation="false" CommandName="Edit" Text="Edit">
												<i class="ace-icon fa fa-pencil bigger-110"></i>	Edit
												
                                    </asp:LinkButton>
                                </td>
                                <%--<td>
                                    <asp:DropDownList ID="fk_StrategicObjective_IDDropDownList" Enabled="false" runat="server" SelectedValue='<%# Eval("fk_StrategicObjective_ID") %>' DataSource="<%# dsObjectives %>" DataTextField="Name" DataValueField="Id" />
                                </td>--%>
                                <td>
                                    <asp:CheckBox ID="IsActiveCheckBox" runat="server" Checked='<%# Eval("IsActive") %>' Enabled="false" />
                                </td>
                                <td>
                                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                                </td>

                                <td>
                                    <asp:Image ImageUrl='<%# GetImage(Eval("Name")) %>' runat="server" />
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
                                                <%--<th runat="server">Strategi cObjective</th>--%>
                                                <th runat="server" style="min-width:10%">Is Active</th>
                                                <th runat="server" style="min-width:10%">TID Description</th>
                                                <th runat="server">TID</th>


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
                                    <asp:DropDownList ID="fk_StrategicObjective_IDDropDownList" Enabled="false" runat="server" SelectedValue='<%# Eval("fk_StrategicObjective_ID") %>' DataSource="<%# dsObjectives %>" DataTextField="Name" DataValueField="Id" />
                                </td>
                                <td>
                                    <asp:CheckBox ID="IsActiveCheckBox" runat="server" Checked='<%# Eval("IsActive") %>' Enabled="false" />
                                </td>
                                <td>
                                    <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Eval("Description") %>' />
                                </td>

                                <td>
                                    <asp:Image ImageUrl='<%# GetImage(Eval("Name")) %>' runat="server" />

                                </td>

                            </tr>
                        </SelectedItemTemplate>
                    </asp:ListView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:db %>"
                        DeleteCommand="DELETE FROM [NTQ_Lookup_Q1] WHERE [Id] = @original_Id"
                        InsertCommand="INSERT INTO [NTQ_Lookup_Q1] ([fk_StrategicObjective_ID], [Name], [timestamp], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate],[IsActive]) VALUES (@fk_StrategicObjective_ID, @Name, @timestamp, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate,@IsActive)" OldValuesParameterFormatString="original_{0}"
                        SelectCommand="SELECT * FROM [NTQ_Lookup_Q1]"
                        UpdateCommand="UPDATE [NTQ_Lookup_Q1] SET [fk_StrategicObjective_ID] = @fk_StrategicObjective_ID, [Name] = @Name, [timestamp] = @timestamp, [CreatedBy] = @CreatedBy, [CreatedDate] = @CreatedDate, [ModifiedBy] = @ModifiedBy, [ModifiedDate] = @ModifiedDate,[IsActive]=@IsActive WHERE [Id] = @original_Id">
                        <DeleteParameters>
                            <asp:Parameter Name="original_Id" Type="Int32" />
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
                            <asp:Parameter Name="Name" Type="String" />
                            <asp:Parameter Name="timestamp" Type="DateTime" />
                            <asp:Parameter Name="CreatedBy" Type="Int32" />
                            <asp:Parameter Name="CreatedDate" Type="DateTime" />
                            <asp:Parameter Name="ModifiedBy" Type="Int32" />
                            <asp:Parameter Name="ModifiedDate" Type="DateTime" />
                            <asp:Parameter Name="original_Id" Type="Int32" />
                            <asp:Parameter Name="IsActive" Type="Boolean" />
                        </UpdateParameters>
                    </asp:SqlDataSource>

                    <asp:SqlDataSource ID="dsObjectives" runat="server" ConnectionString="<%$ ConnectionStrings:db %>" SelectCommand="SELECT [Id], [Name] FROM [NTQ_Lookup_StrategicObjective]"></asp:SqlDataSource>


                </div>

            </div>
        </div>
    </div>

</asp:Content>

