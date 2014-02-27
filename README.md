## Reusable umbraco functionality

### Property Editors
* [Open Graph Editor](/App_Plugins/OpenGraphTags)
* [Call To Action Editor](/App_Plugins/CTAEditor)
* [Url Picker](/App_Plugins/UrlPicker)

### Extension Methods
* [Umbraco extensions](/App_Code/Extensions/UmbracoExtensions.cs) - GetJson, GetJson<T>, GetJsonList<T>, GetMedia, GetMediaList, GetContent, GetContentList

#### Open graph editor
adding open graph tags to pages, handy if you want to stay up with the play in social media sharing of content.

**Usage**
```
@foreach (var tag in Model.Content.GetJsonList<OpenGraphTag>("openGraphTags")) {
  <meta property="@tag.Name" content="@tag.Content" />
}
```
**Preview**
![ScreenShot](/opengrapheditor.png)

#### Call to action editor
For adding multiple 'promo' or 'call to action' blocks to a page, consisting of a thumbnail, heading, description and url

**Usage**
```
@foreach (var cta in Model.Content.GetJsonList<CallToAction>("ctas")) {
  <a href="@(cta.IsInternal ? Umbraco.Url(cta.InternalLink) : cta.ExternalUrl)"@(cta.NewWindow ? "target=\"_blank\"" : "") class="cta">
    @if (cta.Image != null) { <img src="@cta.Image.Src?width=300" alt="@cta.Image.Name" /> }
    <h5>@cta.Heading</h5>
    <p>@cta.Synopsis</p>
  </a>
}
```
**Preview**
![ScreenShot](/cta-editor.png)

#### Url Picker - for all your single url picking needs
Sometimes you just need one url.

**Usage**
```
@{
 var cta = Model.Content.GetJson<UrlPicker>("cta");
}
<a href="@(cta.IsInternal ? Umbraco.Url(cta.InternalLink, UrlProviderMode.Relative) : cta.ExternalUrl)" class="btn">@cta.Title</a>
```
**Preview**
![ScreenShot](/url-picker.png)




