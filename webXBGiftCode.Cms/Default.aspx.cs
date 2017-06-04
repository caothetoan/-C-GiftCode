using System;
using System.Linq;
using System.Web.UI;
using Go.App.Utils;
using Libs;
using Telerik.Web.UI;
using webXBGiftCode.Cms.Common;

namespace webXBGiftCode.Cms
{
    public partial class SurveyAccountList : Page
    {
        string myscript =
                "<script>  $(document).ready(function () { $('.RadGrid').magnificPopup({ delegate: '.gallery_item', type: 'image',gallery: {enabled: true,tPrev: 'Trước (Left arrow key)', tNext: 'Sau (Right arrow key)',tCounter: '<span class=\"mfp-counter\">%curr% / %total%</span>'} }); });</script>";
        
        protected override void OnPreRender(EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "pageLoadHandler", myscript, false);

            base.OnPreRender(e);
        }

        private void Script()
        {
           /* if (!Page.IsPostBack)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "show",  myscript );
            }*/
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "myscript", myscript, true);
        }
         protected void radGrid_PreRender(object sender, EventArgs e)
         {
             Script();
         }
        #region Filter


        protected void Filter(object sender, EventArgs e)
        {
            DataGrid.CurrentPageIndex = 0;
            DataGrid.Rebind();

            //Script();
        }

        protected void Clear(object sender, EventArgs e)
        {
            cbbStatus.ClearSelection();
            cbbStatus.SelectedIndex = 1;

            dtpBeginTime.Clear();
            dtpEndTime.Clear();
            txtId.Text = "0";
            txtKeyword.Text = string.Empty;
            DataGrid.CurrentPageIndex = 0;
            DataGrid.Rebind();
        }

        #endregion Filter

        #region DataGrid events

        protected void DataGridNeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                var status =
                    Convert.ToInt32(!string.IsNullOrEmpty(cbbStatus.SelectedValue) ? cbbStatus.SelectedValue : "1");

                var accountId = 0;
                if (!string.IsNullOrEmpty(txtId.Text))
                    accountId = Convert.ToInt32(txtId.Text);

                int totalRecord;
                //int totalPage = 0;

                var userName = txtKeyword.Text;

                var fromDate = new DateTime(2000, 1, 1);
                var toDate = new DateTime(2000, 1, 1);
                var pageIndex = (DataGrid.CurrentPageIndex + 1);

                var objService = new DBSurveyAccount();
                var objList = objService.GetList(accountId, userName, status, fromDate, toDate,
                    pageIndex, DataGrid.PageSize, out totalRecord);

                DataGrid.VirtualItemCount = totalRecord;
                DataGrid.DataSource = objList;
            }
            catch (Exception ex)
            {
                Logger.WriteLog(Logger.LogType.Error, ex.Message);
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert",
                                                    string.Format(Setting.MessageBoxScriptFormat, ex.Message), true);
            }
        }

        protected void DataGridItemDataBound(object sender, GridItemEventArgs e)
        {
            #region Command item

            try
            {
                var commandItem = DataGrid.MasterTableView.GetItems(GridItemType.CommandItem)[0];
                var btnAdd = (RadButton)commandItem.FindControl("btnAdd");
                btnAdd.Attributes["onclick"] = "return ShowItemPopup();";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert",
                                                    string.Format(Setting.MessageBoxScriptFormat, ex.Message),
                                                    true);
            }

            #endregion Command item

            #region Data item

            if (!(e.Item is GridDataItem)) return;

            try
            {
                var currentItem = e.Item as GridDataItem;
                if (GridMode)
                {
                    currentItem["Index"].Text =
                        (DataGrid.CurrentPageIndex * DataGrid.PageSize + e.Item.ItemIndex + 1).ToString();

                    var status = int.Parse(DataBinder.Eval(currentItem.DataItem, "StatusAnswer").ToString());
                   
                    /*currentItem["StatusAnswer"].Text = status == 0
                        ? "Chờ duyệt"
                        : status == 1 ? "Từ chối" : "Chấp nhận";*/

                    switch (status)
                    {
                        case 3:
                            currentItem["StatusAnswer"].Text = "Từ chối";
                            break;
                        case 2:
                            currentItem["StatusAnswer"].Text = "Chấp nhận";
                            break;
                        default:
                            currentItem["StatusAnswer"].Text = "Chờ duyệt";
                            break;
                    }
                    //((WebImage)currentItem["HighlightColumn"].Controls[0]).ImageUrl = status == 2
                    //                                                                       ? "Images/Check.png"
                    //                                                                       : "Images/Uncheck.png";

                    //((WebImage)currentItem["ActiveColumn"].Controls[0]).ImageUrl = status >= 1
                    //                                                                    ? "Images/Check.png"
                    //                                                                    : "Images/Uncheck.png";

                    //var id = Convert.ToInt32(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["Id"]);
                    //((WebImage)currentItem["EditColumn"].Controls[0]).Attributes["onclick"] =
                    //    string.Format("return ShowItemPopup({0});", id);
                    currentItem["UrlImage"].CssClass = "gallery_item";
                    var src = DataBinder.Eval(currentItem.DataItem, "UrlImage").ToString();
                    currentItem["UrlImage"].Attributes["data-mfp-src"] = src;

                   
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Alert",
                                                    string.Format(Setting.MessageBoxScriptFormat, ex.Message), true);
            }

            #endregion Data item
        }

        protected void DataGridItemCommand(object sender, GridCommandEventArgs e)
        {
            if (!(e.Item is GridDataItem)) return;

            var item = (GridDataItem)e.Item;
            var id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["RowNumber"]);

            var accountId = Convert.ToInt32(item["AccountID"].Text);
            var questionId = Convert.ToInt32(item["QuestionID"].Text);
            var giftCode = string.Empty;
            //var giftCode = (DataBinder.Eval(e.Item.DataItem, "GiftCode").ToString());

            switch (e.CommandName)
            {
                case "Active":
                    try
                    {
                        var activeStatus = 2;
                        int responeStatus = 0;
                        var objService = new DBSurveyAccount();
                        objService.Update(accountId, questionId, activeStatus, out giftCode, out responeStatus);
                        DataGrid.Rebind();
                        /*DataGrid.DataBind();*/
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Alert",
                                                            string.Format(Setting.MessageBoxScriptFormat, ex.Message),
                                                            true);
                    }
                    break;
                case "DeActive":
                    try
                    {
                        // sửa lại từ chối = 3
                        var deActiveStatus = 3;
                        int responeStatus = 0;
                        var objService = new DBSurveyAccount();
                        objService.Update(accountId, questionId, deActiveStatus, out giftCode, out responeStatus);
                        DataGrid.Rebind();

                       /* DataGrid.DataBind();*/
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "Alert",
                                                            string.Format(Setting.MessageBoxScriptFormat, ex.Message),
                                                            true);
                    }
                    break;
            }
        }

        protected void DataGridDeleteCommand(object sender, GridCommandEventArgs e)
        {
            //try
            //{
            //    var item = (GridDataItem)e.Item;
            //    var id = Convert.ToInt32(item.OwnerTableView.DataKeyValues[item.ItemIndex]["Id"]);
            //    ArticleService.Remove(id);
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "Alert",
            //                                        string.Format(Setting.MessageBoxScriptFormat, ex.Message), true);
            //    e.Canceled = true;
            //}
        }

        #endregion DataGrid events

        private bool GridMode
        {
            get
            {
                try
                {
                    return !string.IsNullOrEmpty(Request.QueryString["grid"])
                               ? Convert.ToBoolean(Request.QueryString["grid"].Trim())
                               : Setting.GridThemeEnable;
                }
                catch
                {
                    return Setting.GridThemeEnable;
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            //InitFilter();

            DataGrid.MasterTableView.ShowHeader = GridMode;
            DataGrid.MasterTableView.TableLayout = GridMode ? GridTableLayout.Auto : GridTableLayout.Fixed;
            DataGrid.MasterTableView.AutoGenerateColumns = !GridMode;
            if (GridMode)
            {
                DataGrid.MasterTableView.ItemTemplate = null;
            }
            else
            {
                foreach (
                    var column in
                        DataGrid.MasterTableView.Columns.Cast<GridColumn>().Where(column => !(column is GridBoundColumn))
                    )
                    column.Visible = false;
            }
        }
    }
}