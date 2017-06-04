namespace webXBGiftCode.Cms.Common
{
    public static class ImageDataProvider
    {
        //public static object GetPage(int id, int categoryId, int groupId, int status, string keyword,
        //                             DateTime? beginTime,
        //                             DateTime? endTime, int pageIndex, int pageSize,
        //                             ref int totalRecord, ref int totalPage)
        //{
        //    try
        //    {
        //        var imageList = ImageService.GetPage(id, categoryId, groupId, status, keyword, beginTime, endTime,
        //                                             pageIndex, pageSize, ref totalRecord, ref totalPage);
        //        //Add cache here

        //        var objectList = from item in imageList
        //                         let image =
        //                             !string.IsNullOrEmpty(item.ImageUrl)
        //                                 ? item.ImageUrl + Setting.ImageThumbnailSize
        //                                 : Setting.NoImageAvailable
        //                         select new
        //                                    {
        //                                        item.Id,
        //                                        Owner = item.OwnerName,
        //                                        item.Title,
        //                                        item.Description,
        //                                        Info = item.Tags,
        //                                        Image = image,
        //                                        item.Status,
        //                                        CreatedTime = item.CreatedTime.ToString(Setting.LongDateTimeFormat)
        //                                    };
        //        return objectList;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.WriteLog(Logger.LogType.Error, ex.Message);
        //        throw new Exception(ex.Message);
        //    }
        //}
    }
}