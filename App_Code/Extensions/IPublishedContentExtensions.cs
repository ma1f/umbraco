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

public static class IPublishedContentExtensions {

  /***** Json Extensions *****/
  public static T As<T>(this IPublishedContent content, string alias) {
    if (!content.HasProperty(alias) || !content.HasValue(alias))
      return default(T);
    var serial = new JavaScriptSerializer();
    var str = content.GetPropertyValue<string>(alias);
    return string.IsNullOrEmpty(str) ? default(T) : serial.Deserialize<T>(str);
  }

  public static IEnumerable<T> AsList<T>(this IPublishedContent content, string alias) {
    if (!content.HasProperty(alias) || !content.HasValue(alias))
      return new T[0];
    var serial = new JavaScriptSerializer();
    var str = content.GetPropertyValue<string>(alias);
    return string.IsNullOrEmpty(str) ? new T[0] : serial.Deserialize<IEnumerable<T>>(str);
  }

}
