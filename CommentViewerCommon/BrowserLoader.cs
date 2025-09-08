﻿using System;
using System.Collections.Generic;
using ryu_s.BrowserCookie;
using Common;
namespace CommentViewerCommon
{
    public class BrowserLoader : IBrowserLoader
    {
        private readonly ILogger _logger;

        public BrowserLoader(ILogger logger)
        {
            _logger = logger;
        }
        public IEnumerable<IBrowserProfile> LoadBrowsers()
        {
            var list = new List<IBrowserProfile>();
            var managers = new List<IBrowserManager>
            {
                new ChromeManager(),
                new ChromeBetaManager(),
                new FirefoxManager(),
                new EdgeManager(),
                new OperaManager(),
                new OperaGxManager(),
                new BuildinManager(),
            };
            foreach (var manager in managers)
            {
                try
                {
                    list.AddRange(manager.GetProfiles());
                }
                catch (Exception ex)
                {
                    _logger.LogException(ex);
                }
            }
            return list;
        }
    }
}
