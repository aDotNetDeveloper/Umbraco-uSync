using Our.Umbraco.Vorto.Extensions;
using Umbraco.Web;
using Umbraco.Core.Models;

namespace uSync.Site.Models
{
    public class BannerRepository
    {
        public const string DataItemProperty = "bannerData";
        private string _alt;
        private IPublishedContent _dataItem;
        private string _url;

        public BannerRepository(IPublishedContent model)
        {
            _dataItem = model.GetVortoValue<IPublishedContent>(DataItemProperty);
            var umb = new UmbracoHelper(UmbracoContext.Current);
            if (BannerImageExists)
            {
                // this parse will fail if image id has not been remapped from GUID
                var image = umb.TypedMedia(int.Parse(GetBannerId));
                _url = image.GetCropUrl("1200px");
                _alt = image.Name;
            }
        }
        public bool BannerImageExists => _dataItem != null && !string.IsNullOrWhiteSpace(_dataItem.GetPropertyValue<string>("bannerImage"));

        public string GetBannerId => BannerImageExists ? _dataItem.GetPropertyValue<string>("bannerImage") : null;

        public string Url => _url;
        public string AltText => _alt;
    }
}