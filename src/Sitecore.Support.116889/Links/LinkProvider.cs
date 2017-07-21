namespace Sitecore.Support.Links
{
  using Sitecore.Configuration;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.Globalization;
  using Sitecore.Links;
  using Sitecore.Sites;
  using Sitecore.Web;
  using System;

  public class LinkProvider : Sitecore.Links.LinkProvider
  {
    #region Modified code

    public override string GetItemUrl(Item item, UrlOptions options)
    {
      Assert.ArgumentNotNull(item, "item");
      Assert.ArgumentNotNull(options, "options");

      this.ResolveLanguage(item, options);

      var itemUrl = CreateLinkBuilder(options).GetItemUrl(item);
      if (options.LowercaseUrls)
      {
        itemUrl = itemUrl.ToLowerInvariant();
      }
      return itemUrl;
    }

    #endregion

    #region Added code

    private void ResolveLanguage(Item item, UrlOptions defaultUrlOptions)
    {
      Assert.ArgumentNotNull(item, "item");
      Assert.ArgumentNotNull(defaultUrlOptions, "defaultUrlOptions");

      if (!Settings.Rendering.SiteResolving)
      {
        return;
      }

      if (item != null)
      {
        if (MatchCurrentSite(item, new SiteContext(Context.Site.SiteInfo)))
        {
          defaultUrlOptions.Language = Context.Language;
        }
        else
        {
          var siteInfo = ResolveTargetSite(item, true);
          defaultUrlOptions.Language = Language.Parse(siteInfo.Language);
        }
      }
    }

    private SiteInfo ResolveTargetSite(Item item, bool resolveSite)
    {
      Assert.ArgumentNotNull(item, "item");

      var urlOptions = this.GetDefaultUrlOptions();
      urlOptions.SiteResolving = resolveSite;

      return this.CreateLinkBuilder(urlOptions).GetTargetSite(item);
    }

    private static bool MatchCurrentSite(Item item, SiteContext currentSite)
    {
      Assert.ArgumentNotNull(item, "item");
      Assert.ArgumentNotNull(currentSite, "currentSite");

      if (!Settings.Rendering.SiteResolvingMatchCurrentSite)
      {
        return false;
      }

      var fullPath = item.Paths.FullPath;
      var startPath = currentSite.StartPath;

      if (!fullPath.StartsWith(startPath, StringComparison.InvariantCultureIgnoreCase))
      {
        return false;
      }

      return (fullPath.Length <= startPath.Length) || (fullPath[startPath.Length] == '/');
    }

    #endregion
  }
}