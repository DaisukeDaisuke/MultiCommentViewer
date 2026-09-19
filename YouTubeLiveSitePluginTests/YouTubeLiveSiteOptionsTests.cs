using NUnit.Framework;
using ryu_s.YouTubeLive.Message.Action;
using System;
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
        public void LegacyYouTubeSettingsMigrateToNamespacedKeysWithoutDroppingOtherSettings()
        {
            var options = new YouTubeLiveSiteOptions();
            options.Deserialize("IsAllChat=False\r\nIsHideMembershipIcon=True\r\nOther.Namespace.Value=keep");

            Assert.IsFalse(options.IsAllChat);
            Assert.IsTrue(options.IsHideMembershipIcon);

            var lines = options.Serialize().Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            CollectionAssert.Contains(lines, "YouTubeLiveSitePlugin.IsAllChat=False");
            CollectionAssert.Contains(lines, "YouTubeLiveSitePlugin.IsHideMembershipIcon=True");
            CollectionAssert.Contains(lines, "Other.Namespace.Value=keep");
            CollectionAssert.DoesNotContain(lines, "IsHideMembershipIcon=True");
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
