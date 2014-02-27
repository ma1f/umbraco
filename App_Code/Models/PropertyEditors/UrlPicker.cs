using System.Collections.Generic;
using System.Globalization;
using System.Web;
using umbraco.cms.businesslogic.web;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Models {

  public class UrlPicker {
    public string Title{get;set;}
    public int InternalLink{get;set;}
    public string InternalLinkName{get;set;}
    public string ExternalUrl{get;set;}
    public bool IsInternal{get;set;}
    public bool NewWindow{get;set;}
  }

}