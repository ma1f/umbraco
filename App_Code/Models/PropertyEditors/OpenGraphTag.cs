using System.Collections.Generic;
using System.Globalization;
using System.Web;
using umbraco.cms.businesslogic.web;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Models {
  
  public class OpenGraphTag {
    public string Name { get; set; }
    public string Content { get; set; }
  }

}