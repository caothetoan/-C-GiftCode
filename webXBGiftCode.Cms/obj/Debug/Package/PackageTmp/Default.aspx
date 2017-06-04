<%@ Page Title="" Language="C#" MasterPageFile="~/CmsPage.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="webXBGiftCode.Cms.SurveyAccountList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content2" ContentPlaceHolderID="CMSContentPlaceHolder" runat="server">
    <h2>Danh sách tin bài
    </h2>

    <div class="filter_box">
     
        <div class="filter">
            <ul>
                <li class="label">Từ ngày:</li>
                <li class="data">
                    <telerik:RadDateTimePicker runat="server" ID="dtpBeginTime" Skin="Windows7" DateInput-DateFormat="dd/MM/yyyy HH:mm:ss"
                        DateInput-DisplayDateFormat="dd/MM/yyyy HH:mm:ss" MinDate="2013-01-01" MaxDate="2100-01-01"
                        Culture="vi-VN" ShowPopupOnFocus="True" Width="200px">
                        <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                            ViewSelectorText="x" Skin="Windows7">
                        </Calendar>
                        <DateInput runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm:ss" DateFormat="dd/MM/yyyy HH:mm:ss"
                            EnableSingleInputRendering="True" LabelWidth="64px">
                        </DateInput>
                        <DatePopupButton ToolTip="Chọn ngày"></DatePopupButton>
                        <TimeView runat="server" Culture="vi-VN" ShowFooter="False" ShowHeader="False">
                        </TimeView>
                        <TimePopupButton ToolTip="Chọn giờ"></TimePopupButton>
                    </telerik:RadDateTimePicker>
                </li>
                <li class="label">Đến ngày:</li>
                <li class="data">
                    <telerik:RadDateTimePicker runat="server" ID="dtpEndTime" Skin="Windows7" DateInput-DateFormat="dd/MM/yyyy HH:mm:ss"
                        DateInput-DisplayDateFormat="dd/MM/yyyy HH:mm:ss" MinDate="2013-01-01" MaxDate="2100-01-01"
                        Culture="vi-VN" ShowPopupOnFocus="True" Width="200px">
                        <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                            ViewSelectorText="x" Skin="Windows7">
                        </Calendar>
                        <DateInput runat="server" DisplayDateFormat="dd/MM/yyyy HH:mm:ss" DateFormat="dd/MM/yyyy HH:mm:ss"
                            EnableSingleInputRendering="True" LabelWidth="64px">
                        </DateInput>
                        <DatePopupButton ToolTip="Chọn ngày"></DatePopupButton>
                        <TimeView runat="server" Culture="vi-VN" ShowFooter="False" ShowHeader="False">
                        </TimeView>
                        <TimePopupButton ToolTip="Chọn giờ"></TimePopupButton>
                    </telerik:RadDateTimePicker>
                </li>
            </ul>
        </div>
        <div class="filter">
            <ul>
                <li class="label">Mã tài khoản:</li>
                <li class="data">
                    <telerik:RadTextBox ID="txtId" runat="server" Skin="Windows7"
                        EnabledStyle-HorizontalAlign="Right" Width="200px">
                    </telerik:RadTextBox>
                </li>
                <li class="label">Tên đăng nhập:</li>
                <li class="data">
                    <telerik:RadTextBox ID="txtKeyword" runat="server" Skin="Windows7" Width="100%" EmptyMessage="">
                    </telerik:RadTextBox>
                </li>
               
            </ul>
        </div>
           <div class="filter">
            <ul>
                <li class="label">Trạng thái:</li>
                <li class="data">
                    <telerik:RadComboBox ID="cbbStatus" runat="server" Skin="Windows7" EmptyMessage="Chọn trạng thái sử dụng"
                        Width="200px" OnSelectedIndexChanged="Filter" AutoPostBack="True">
                        <Items>
                            <telerik:RadComboBoxItem Text="----- Tất cả -----" Value="0" />
                            <telerik:RadComboBoxItem Text="Đang chờ" Value="1" Selected="True" />

                            <telerik:RadComboBoxItem Text="Chấp thuận" Value="2" />
                            <telerik:RadComboBoxItem Text="Từ chối" Value="3" />
                        </Items>
                    </telerik:RadComboBox>
                </li>
                 <li class="button">
                    <telerik:RadButton ID="btnFilter" runat="server" Text="Lọc dữ liệu" Skin="Windows7"
                        OnClick="Filter">
                        <Icon PrimaryIconUrl="Images/Filter.png" PrimaryIconLeft="4" PrimaryIconTop="4" />
                    </telerik:RadButton>
                </li>
                <li class="button">
                    <telerik:RadButton ID="btnClear" runat="server" Text="Xóa bộ lọc" Skin="Windows7"
                        OnClick="Clear">
                        <Icon PrimaryIconUrl="Images/Clear.png" PrimaryIconLeft="4" PrimaryIconTop="4" />
                    </telerik:RadButton>
                </li>
            </ul>
        </div>
    </div>
    <telerik:RadGrid ID="DataGrid" runat="server" Skin="Windows7" AllowPaging="True"
        AllowSorting="True" AllowCustomPaging="True" PageSize="15" OnNeedDataSource="DataGridNeedDataSource"
        OnItemDataBound="DataGridItemDataBound" CellSpacing="0" OnDeleteCommand="DataGridDeleteCommand"
        OnItemCommand="DataGridItemCommand">
        <PagerStyle Mode="NextPrevAndNumeric" FirstPageToolTip="Về trang đầu" LastPageToolTip="Tới trang cuối"
            PrevPageToolTip="Trang trước" NextPageToolTip="Trang sau" PageSizeLabelText="Số lượng bản ghi trên trang"
            PagerTextFormat="{4} Trang {0} / {1}, bản ghi từ {2} đến {3} - Tổng số trang: {5}"
            AlwaysVisible="True"></PagerStyle>
        <MasterTableView DataKeyNames="RowNumber" CommandItemDisplay="Top" NoMasterRecordsText="Không có dữ liệu">
            <HeaderStyle Font-Bold="true" />
            <CommandItemTemplate>
                <telerik:RadButton ID="btnAdd" runat="server" Text="Thêm mới" EnableEmbeddedSkins="False"
                    CssClass="RadLinkButton" HoveredCssClass="RadHoveredLinkButton">
                    <Icon PrimaryIconUrl="Images/Add.png" PrimaryIconLeft="4" PrimaryIconTop="4" />
                </telerik:RadButton>
            </CommandItemTemplate>
            <Columns>
                <telerik:GridBoundColumn UniqueName="Index" HeaderText="STT" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="AccountID" DataField="AccountID" HeaderText="Mã tài khoản" HeaderStyle-HorizontalAlign="Center"
                    ItemStyle-HorizontalAlign="Right">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="UserName" DataField="UserName" HeaderText="Tên đăng nhập">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="QuestionID" DataField="QuestionID" HeaderText="Mã câu hỏi">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="Content" DataField="Content" HeaderText="Nội dung">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="StatusAnswer" DataField="StatusAnswer" HeaderText="Trạng thái">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="GiftCode" DataField="GiftCode" HeaderText="GiftCode">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn UniqueName="GiftCodeID" DataField="GiftCodeID" HeaderText="GiftCodeID">
                </telerik:GridBoundColumn>

                <telerik:GridImageColumn UniqueName="UrlImage" DataType="System.String" DataImageUrlFields="UrlImage"
                    AlternateText="Không có ảnh" DataAlternateTextField="Content"
                    ImageAlign="Middle" ImageHeight="120px" ImageWidth="160px" HeaderText="Image Column">
                </telerik:GridImageColumn>

                <telerik:GridHyperLinkColumn UniqueName="LinkUrlImage" DataNavigateUrlFields="UrlImage" Text="Xem ảnh" Target="_blank"
                    HeaderText="Link ảnh">
                </telerik:GridHyperLinkColumn>

                <telerik:GridBoundColumn UniqueName="CreatedDateTime" DataField="CreatedDateTime" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}"
                    HeaderText="Ngày tạo">
                </telerik:GridBoundColumn>

                <telerik:GridButtonColumn UniqueName="ActiveColumn" ButtonType="PushButton" HeaderText="Duyệt"
                    CommandName="Active" Text="Chấp thuận" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                </telerik:GridButtonColumn>
                <telerik:GridButtonColumn UniqueName="DeActiveColumn" ButtonType="PushButton" HeaderText="Không Duyệt"
                    CommandName="DeActive" Text="Từ chối" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                </telerik:GridButtonColumn>
                <%--<telerik:GridButtonColumn UniqueName="ActiveColumn" ButtonType="ImageButton" HeaderText="Hiển thị"
                    CommandName="Active" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                    ImageUrl="Images/Check.png">
                </telerik:GridButtonColumn>
                <telerik:GridEditCommandColumn UniqueName="EditColumn" ButtonType="ImageButton" HeaderText="Sửa"
                    ItemStyle-HorizontalAlign="Center" EditImageUrl="Images/Edit.png">
                </telerik:GridEditCommandColumn>
                <telerik:GridButtonColumn UniqueName="DeleteColumn" ButtonType="ImageButton" ConfirmText="Bạn có chắc chắn muốn xóa không?"
                    ConfirmTitle="Xóa thông tin bài viết" ConfirmDialogType="RadWindow" HeaderText="Xóa"
                    ImageUrl="Images/Delete.png" CommandName="Delete" ItemStyle-HorizontalAlign="Center">
                </telerik:GridButtonColumn>--%>
            </Columns>
        </MasterTableView>

    </telerik:RadGrid>

   <%-- <script>
      

        $('.RadGrid').magnificPopup({
            delegate: '.gallery_item',
            type: 'image',
            closeOnContentClick: false,
            closeBtnInside: false,
            mainClass: 'mfp-with-zoom mfp-img-mobile',

            gallery: {
                enabled: true,
                tPrev: 'Trước (Left arrow key)', // title for left button
                tNext: 'Sau (Right arrow key)', // title for right button
                tCounter: '<span class="mfp-counter">%curr% / %total%</span>', // markup of counter
            },


            /* zoom: {
                 enabled: true,
                 duration: 300
                 , // don't foget to change the duration also in CSS
                 opener: function (element) {
                     return element.find('img');
                 }
             }*/

        });
    </script>--%>

</asp:Content>
