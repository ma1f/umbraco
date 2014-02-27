using System.Collections.Generic;
using System.Globalization;
using System.Web;
using umbraco.cms.businesslogic.web;
using Umbraco.Core.Models;
using Umbraco.Web;
using Umbraco.Web.Models;

namespace Models {

  public class CallToAction {
    public Media Image { get; set; }
    public string Heading { get; set; }
    public string Synopsis { get; set; }
    public int InternalLink { get; set; }
    public string ExternalUrl { get; set; }
    public bool NewWindow { get; set; }
    public bool IsInternal { get; set; }

    public class Media {
      public string Id { get; set; }
      public string Src { get; set; }
      public string Name { get; set; }
    }
  }

}