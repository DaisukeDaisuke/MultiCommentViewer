using MultiCommentViewer.Test;
using NUnit.Framework;
using System;
using System.Linq;

namespace MultiCommentViewerTests
{
    [TestFixture]
    class DynamicOptionsPersistenceTests
    {
        [Test]
        public void LegacyConnectionDisplayIndexesMigrateToNamedColumnOrder()
        {
            var legacy = string.Join("\r\n", new[]
            {
                "ConnectionsViewSelectionDisplayIndex=0",
                "ConnectionsViewSiteDisplayIndex=1",
                "ConnectionsViewConnectionNameDisplayIndex=2",
                "ConnectionsViewInputDisplayIndex=3",
                "ConnectionsViewBrowserDisplayIndex=4",
                "ConnectionsViewConnectionDisplayIndex=5",
                "ConnectionsViewDisconnectionDisplayIndex=6",
                "ConnectionsViewSaveDisplayIndex=8",
                "ConnectionsViewLoggedinUsernameDisplayIndex=9",
                "ConnectionsViewConnectionBackgroundDisplayIndex=10",
                "ConnectionsViewConnectionForegroundDisplayIndex=11",
                "FontSize=22",
            });
            var options = new DynamicOptionsTest();

            options.Deserialize(legacy);

            Assert.AreEqual(7, options.ConnectionsViewSaveDisplayIndex);
            Assert.AreEqual(8, options.ConnectionsViewLoggedinUsernameDisplayIndex);
            Assert.AreEqual(9, options.ConnectionsViewConnectionBackgroundDisplayIndex);
            Assert.AreEqual(10, options.ConnectionsViewConnectionForegroundDisplayIndex);
            Assert.AreEqual(22, options.FontSize);

            var serialized = options.Serialize();
            var lines = serialized.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            Assert.IsTrue(lines.Contains("MultiCommentViewer.FontSize=22"));
            Assert.IsTrue(lines.Any(x => x.StartsWith("MultiCommentViewer.ConnectionsViewColumnOrder=", StringComparison.Ordinal)));
            Assert.IsFalse(lines.Any(x => x.StartsWith("MultiCommentViewer.ConnectionsViewSaveDisplayIndex=", StringComparison.Ordinal)));
        }

        [Test]
        public void NamedColumnOrderRoundTripsWithoutAbsoluteIndexes()
        {
            var options = new DynamicOptionsTest
            {
                ConnectionsViewSelectionDisplayIndex = 1,
                ConnectionsViewSiteDisplayIndex = 2,
                ConnectionsViewConnectionNameDisplayIndex = 3,
                ConnectionsViewInputDisplayIndex = 4,
                ConnectionsViewBrowserDisplayIndex = 5,
                ConnectionsViewConnectionDisplayIndex = 6,
                ConnectionsViewDisconnectionDisplayIndex = 7,
                ConnectionsViewSaveDisplayIndex = 0,
                ConnectionsViewLoggedinUsernameDisplayIndex = 8,
                ConnectionsViewConnectionBackgroundDisplayIndex = 9,
                ConnectionsViewConnectionForegroundDisplayIndex = 10,
            };

            var serialized = options.Serialize();
            var reloaded = new DynamicOptionsTest();
            reloaded.Deserialize(serialized);

            Assert.AreEqual(0, reloaded.ConnectionsViewSaveDisplayIndex);
            Assert.AreEqual(1, reloaded.ConnectionsViewSelectionDisplayIndex);
            Assert.AreEqual(2, reloaded.ConnectionsViewSiteDisplayIndex);
            Assert.AreEqual(10, reloaded.ConnectionsViewConnectionForegroundDisplayIndex);
        }
    }
}