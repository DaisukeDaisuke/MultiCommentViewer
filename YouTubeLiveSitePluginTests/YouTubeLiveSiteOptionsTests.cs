using NUnit.Framework;
using ryu_s.YouTubeLive.Message.Action;
using System.Collections.Generic;
using System.Linq;
using YouTubeLiveSitePlugin;
using YouTubeLiveSitePlugin.Test2;

namespace YouTubeLiveSitePluginTests
{
    [TestFixture]
    class YouTubeLiveSiteOptionsTests
    {
        [Test]
        public void IsHideMembershipIconIsSavedAndReloaded()
        {
            var options = new YouTubeLiveSiteOptions();

            Assert.IsFalse(options.IsHideMembershipIcon);

            options.IsHideMembershipIcon = true;
            var serialized = options.Serialize();
            var reloaded = new YouTubeLiveSiteOptions();
            reloaded.Deserialize(serialized);

            Assert.IsTrue(reloaded.IsHideMembershipIcon);
        }

        [Test]
        public void HideMembershipIconOmitsOnlyCustomThumbnailBadgeParts()
        {
            var badges = new List<IAuthorBadge>
            {
                new AuthorBadgeCustomThumb(
                    new List<Thumbnail1> { new Thumbnail1("small"), new Thumbnail1("large") },
                    "member"),
                new AuthorBadgeIcon("MODERATOR", "moderator"),
            };

            var visibleParts = MessageBase.Convert("author", badges).ToList();
            var hiddenParts = MessageBase.Convert("author", badges, true).ToList();

            Assert.AreEqual(3, visibleParts.Count);
            Assert.AreEqual(2, hiddenParts.Count);
        }
    }
}
