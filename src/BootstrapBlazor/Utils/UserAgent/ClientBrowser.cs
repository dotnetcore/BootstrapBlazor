// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text.RegularExpressions;

namespace BootstrapBlazor.Components
{
    /// <summary>
    ///
    /// </summary>
    internal class ClientBrowser
    {
        private static Dictionary<string, string> _versionMap = new()
        {
            { "/8", "1.0" },
            { "/1", "1.2" },
            { "/3", "1.3" },
            { "/412", "2.0" },
            { "/416", "2.0.2" },
            { "/417", "2.0.3" },
            { "/419", "2.0.4" },
            { "?", "/" }
        };

        /// <summary>
        ///
        /// </summary>
        /// <param name="userAgent"></param>
        public ClientBrowser(string userAgent)
        {
            foreach (var matchItem in _matchs)
            {
                foreach (var regexItem in matchItem.Regexes)
                {
                    try
                    {
                        if (regexItem.IsMatch(userAgent))
                        {
                            var match = regexItem.Match(userAgent);

                            matchItem.Action(match, this);

                            Major = new Regex(@"\d*").Match(Version).Value;

                            return;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public string? Major { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        [NotNull]
        public string? Version { get; set; }


        private static void NameVersionAction(Match match, object obj)
        {
            var current = obj as ClientBrowser;

            if (current != null)
            {
                current.Name = new Regex(@"^[a-zA-Z]+", RegexOptions.IgnoreCase).Match(match.Value).Value;
                if (match.Value.Length > current.Name.Length)
                {
                    current.Version = match.Value[(current.Name.Length + 1)..];
                }
            }
        }

        private static List<MatchExpression> _matchs = new()
        {
#nullable disable
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"(opera\smini)\/([\w\.-]+)",RegexOptions.IgnoreCase),// Opera Mini
                    new Regex(@"(opera\s[mobiletab]+).+version\/([\w\.-]+)",RegexOptions.IgnoreCase),// Opera Mobi/Tablet
                    new Regex(@"(opera).+version\/([\w\.]+)",RegexOptions.IgnoreCase),// Opera > 9.80
                    new Regex(@"(opera)[\/\s]+([\w\.]+)",RegexOptions.IgnoreCase)// Opera < 9.80
                },
                Action = NameVersionAction
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"(opios)[\/\s]+([\w\.]+)",RegexOptions.IgnoreCase)// Opera mini on iphone >= 8.0
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Opera Mini";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"\s(opr)\/([\w\.]+)",RegexOptions.IgnoreCase)// Opera Webkit
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Opera";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"(kindle)\/([\w\.]+)",RegexOptions.IgnoreCase),// Kindle
                    new Regex(@"(lunascape|maxthon|netfront|jasmine|blazer)[\/\s]?([\w\.]+)*",RegexOptions.IgnoreCase),// Lunascape/Maxthon/Netfront/Jasmine/Blazer

                    new Regex(@"(avant\s|iemobile|slim|baidu)(?:browser)?[\/\s]?([\w\.]*)",RegexOptions.IgnoreCase), // Avant/IEMobile/SlimBrowser/Baidu
                    new Regex(@"(?:ms|\()(ie)\s([\w\.]+)",RegexOptions.IgnoreCase),// Internet Explorer

                    new Regex(@"(rekonq)\/([\w\.]+)*",RegexOptions.IgnoreCase),// Rekonq
                    new Regex(@"(chromium|flock|rockmelt|midori|epiphany|silk|skyfire|ovibrowser|bolt|iron|vivaldi|iridium|phantomjs)\/([\w\.-]+)",RegexOptions.IgnoreCase), // Chromium/Flock/RockMelt/Midori/Epiphany/Silk/Skyfire/Bolt/Iron/Iridium/PhantomJS
                },
                Action = NameVersionAction
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"(trident).+rv[:\s]([\w\.]+).+like\sgecko",RegexOptions.IgnoreCase)// IE11
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    current.Name = "IE";
                    current.Version = "11";
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"(edge)\/((\d+)?[\w\.]+)",RegexOptions.IgnoreCase),// Microsoft Edge
                },
                Action = NameVersionAction
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"(yabrowser)\/([\w\.]+)",RegexOptions.IgnoreCase)// Yandex
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Yandex";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"(comodo_dragon)\/([\w\.]+)",RegexOptions.IgnoreCase)// Comodo Dragon
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = nameAndVersion[0].Replace('_', ' ');
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"(micromessenger)\/([\w\.]+)",RegexOptions.IgnoreCase)// WeChat
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "WeChat";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"xiaomi\/miuibrowser\/([\w\.]+)",RegexOptions.IgnoreCase)// MIUI Browser
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "MIUI Browser";
                    current.Version = nameAndVersion[0];
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"\swv\).+(chrome)\/([\w\.]+)",RegexOptions.IgnoreCase)// Chrome WebView
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = new Regex("(.+)").Replace(nameAndVersion[0], "$1 WebView");
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"android.+samsungbrowser\/([\w\.]+)",RegexOptions.IgnoreCase),
                    new Regex(@"android.+version\/([\w\.]+)\s+(?:mobile\s?safari|safari)*",RegexOptions.IgnoreCase)// Android Browser
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Android Browser";
                    current.Version = nameAndVersion[0];
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"(chrome|omniweb|arora|[tizenoka]{5}\s?browser)\/v?([\w\.]+)",RegexOptions.IgnoreCase),// Chrome/OmniWeb/Arora/Tizen/Nokia
                    new Regex(@"(qqbrowser)[\/\s]?([\w\.]+)",RegexOptions.IgnoreCase)// QQBrowser
                },
                Action = NameVersionAction
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"(uc\s?browser)[\/\s]?([\w\.]+)",RegexOptions.IgnoreCase),
                    new Regex(@"ucweb.+(ucbrowser)[\/\s]?([\w\.]+)",RegexOptions.IgnoreCase),
                    new Regex(@"juc.+(ucweb)[\/\s]?([\w\.]+)",RegexOptions.IgnoreCase),// UCBrowser
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Android Browser";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"(dolfin)\/([\w\.]+)",RegexOptions.IgnoreCase)// Dolphin
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Dolphin";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"((?:android.+)crmo|crios)\/([\w\.]+)",RegexOptions.IgnoreCase)// Chrome for Android/iOS
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Chrome";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@";fbav\/([\w\.]+);",RegexOptions.IgnoreCase)// Facebook App for iOS
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Facebook";
                    current.Version = nameAndVersion[0];
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"fxios\/([\w\.-]+)",RegexOptions.IgnoreCase)// Firefox for iOS
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Firefox";
                    current.Version = nameAndVersion[0];
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"version\/([\w\.]+).+?mobile\/\w+\s(safari)",RegexOptions.IgnoreCase)// Mobile Safari
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Mobile Safari";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"version\/([\w\.]+).+?(mobile\s?safari|safari)",RegexOptions.IgnoreCase)// Safari & Safari Mobile
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = nameAndVersion[1];
                    current.Version = nameAndVersion[0];
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"webkit.+?(mobile\s?safari|safari)(\/[\w\.]+)",RegexOptions.IgnoreCase)// Safari < 3.0
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = nameAndVersion[0];

                    var version = nameAndVersion[1];

                    current.Version = _versionMap.Keys.Any(m => m == version) ? _versionMap[version] : version;
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"(konqueror)\/([\w\.]+)",RegexOptions.IgnoreCase),// Konqueror
                    new Regex(@"(webkit|khtml)\/([\w\.]+)",RegexOptions.IgnoreCase)
                },
                Action = NameVersionAction
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"(navigator|netscape)\/([\w\.-]+)",RegexOptions.IgnoreCase)// Netscape
                },
                Action = (Match match, Object obj) =>
                {
                    var current = obj as ClientBrowser;

                    var nameAndVersion = match.Value.Split('/');

                    current.Name = "Netscape";
                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression
            {
                Regexes = new List<Regex> {
                    new Regex(@"(swiftfox)",RegexOptions.IgnoreCase),// Swiftfox
                    new Regex(@"(icedragon|iceweasel|camino|chimera|fennec|maemo\sbrowser|minimo|conkeror)[\/\s]?([\w\.\+]+)",RegexOptions.IgnoreCase),// IceDragon/Iceweasel/Camino/Chimera/Fennec/Maemo/Minimo/Conkeror
                    new Regex(@"(firefox|seamonkey|k-meleon|icecat|iceape|firebird|phoenix)\/([\w\.-]+)",RegexOptions.IgnoreCase),// Firefox/SeaMonkey/K-Meleon/IceCat/IceApe/Firebird/Phoenix
                    new Regex(@"(mozilla)\/([\w\.]+).+rv\:.+gecko\/\d+",RegexOptions.IgnoreCase),// Mozilla
                    new Regex(@"(polaris|lynx|dillo|icab|doris|amaya|w3m|netsurf|sleipnir)[\/\s]?([\w\.]+)",RegexOptions.IgnoreCase),// Polaris/Lynx/Dillo/iCab/Doris/Amaya/w3m/NetSurf/Sleipnir
                    new Regex(@"(links)\s\(([\w\.]+)",RegexOptions.IgnoreCase),// Links
                    new Regex(@"(gobrowser)\/?([\w\.]+)*",RegexOptions.IgnoreCase),// GoBrowser
                    new Regex(@"(ice\s?browser)\/v?([\w\._]+)",RegexOptions.IgnoreCase),// ICE Browser
                    new Regex(@"(mosaic)[\/\s]([\w\.]+)",RegexOptions.IgnoreCase)// Mosaic
                },
                Action = NameVersionAction
            },
#nullable restore
        };
    }
}
