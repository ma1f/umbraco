using Examine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Serialization;
using System.Xml.XPath;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Web;
using Umbraco.Web.Routing;

public static class UmbracoExtensions {

  /***** Json Extensions *****/
  public static object GetJson(this IPublishedContent content, string alias) {
    var serial = new JavaScriptSerializer();
    var str = content.GetPropertyValue<string>(alias);
    var json = serial.DeserializeObject(str);
    return json;
  }
  public static T GetJson<T>(this IPublishedContent content, string alias) {
    if (!content.HasProperty(alias) || !content.HasValue(alias))
      return default(T);
    var serial = new JavaScriptSerializer();
    var str = content.GetPropertyValue<string>(alias);
    return string.IsNullOrEmpty(str) ? default(T) : serial.Deserialize<T>(str);
  }
  public static IEnumerable<T> GetJsonList<T>(this IPublishedContent content, string alias) {
    if (!content.HasProperty(alias) || !content.HasValue(alias))
      return new T[0];
    var serial = new JavaScriptSerializer();
    var str = content.GetPropertyValue<string>(alias);
    return string.IsNullOrEmpty(str) ? new T[0] : serial.Deserialize<IEnumerable<T>>(str);
  }

  /***** IPublishedContent Extensions *****/
  public static IPublishedContent GetMedia(this IPublishedContent page, string alias) {
    if (!page.HasProperty(alias)) return null; //throw new Exception("Doctype " + page.DocumentTypeAlias + " is lacking the following alias: " + alias);
    if (!page.HasValue(alias)) return null;
    return UmbracoContext.Current.MediaCache.GetById(page.GetPropertyValue<int>(alias));
  }
  public static IEnumerable<IPublishedContent> GetMediaList(this IPublishedContent page, string alias) {
    if (!page.HasProperty(alias)) return new List<IPublishedContent>(); //throw new Exception("Doctype " + page.DocumentTypeAlias + " is lacking the following alias: " + alias);
    if (!page.HasValue(alias)) return new List<IPublishedContent>();
    var mediacache = UmbracoContext.Current.MediaCache;
    return page.GetPropertyValue<string>(alias)
      .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
      .Select(x => mediacache.GetById(int.Parse(x)))
      .Where(x => x != null);
  }

  // just cache - no point getting via examine as lose strongly typed aspect
  public static IPublishedContent GetContent(this IPublishedContent page, string alias) {
    //return cache.Get<IPublishedContent>("GetContent[" + page.Id + "," + alias + "]", () => {
    if (!page.HasProperty(alias)) return null;
    if (!page.HasValue(alias)) return null;
    return UmbracoContext.Current.ContentCache.GetById(page.GetPropertyValue<int>(alias));
    //}, file: UMBRACO_CONFIG);
  }
  public static IEnumerable<IPublishedContent> GetContentList(this IPublishedContent page, string alias) {
    //return cache.Get<IEnumerable<IPublishedContent>>("GetContentList[" + page.Id + "," + alias + "]", () => {
    if (!page.HasProperty(alias)) return new List<IPublishedContent>(); //throw new Exception("Doctype " + page.DocumentTypeAlias + " is lacking the following alias: " + alias);
    if (!page.HasValue(alias)) return new List<IPublishedContent>();
    var contentcache = UmbracoContext.Current.ContentCache;
    return page.GetPropertyValue<string>(alias)
        .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(x => contentcache.GetById(int.Parse(x)))
        .Where(x => x != null);
    //}, file: UMBRACO_CONFIG);
  }

  /// <summary>
  /// Get DataType prevalues
  /// </summary>
  /// <param name="dataTypeId"></param>
  /// <returns></returns>
  public static IEnumerable<string> GetPrevalues(this UmbracoHelper umbraco, string datatypename) {
    //return cache.Get<IEnumerable<string>>("GetPrevalues[" + datatypename + "]", () => {
      var dataservice = UmbracoContext.Current.Application.Services.DataTypeService;
      var datatype = dataservice.GetAllDataTypeDefinitions().FirstOrDefault(x => x.Name == datatypename);
      return dataservice.GetPreValuesByDataTypeId(datatype.Id);
    //}, file: UMBRACO_CONFIG);
  }

}
