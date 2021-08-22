// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace BootstrapBlazor.Components
{
#nullable disable
    /// <summary>
    ///
    /// </summary>
    internal class ClientOS
    {
        private static Dictionary<string, string> _versionMap = new Dictionary<string, string>{
            {"4.90","ME" },
            { "NT3.51","NT 3.11"},
            { "NT4.0","NT 4.0"},
            { "NT 5.0","2000"},
            { "NT 5.1","XP"},
            { "NT 5.2","XP"},
            { "NT 6.0","Vista"},
            { "NT 6.1","7"},
            { "NT 6.2","8"},
            { "NT 6.3","8.1"},
            { "NT 6.4","10"},
            { "NT 10.0","10"},
            { "ARM","RT"}
        };

        /// <summary>
        ///
        /// </summary>
        /// <param name="userAgent"></param>
        public ClientOS(string userAgent)
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

                            return;
                        }
                    }
                    catch (Exception) { }
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Version { get; set; }

        private static void NameVersionAction(Match match, Object obj)
        {
            var current = obj as ClientOS;

            current.Name = new Regex(@"^[a-zA-Z]+", RegexOptions.IgnoreCase).Match(match.Value).Value;
            if (match.Value.Length > current.Name.Length)
            {
                current.Version = match.Value.Substring(current.Name.Length + 1);
            }
        }

        private static List<MatchExpression> _matchs = new List<MatchExpression> {
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"microsoft\s(windows)\s(vista|xp)",RegexOptions.IgnoreCase),// Windows (iTunes)

                },
                Action = NameVersionAction
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(windows)\snt\s6\.2;\s(arm)",RegexOptions.IgnoreCase),// Windows RT
                    new Regex(@"(windows\sphone(?:\sos)*)[\s\/]?([\d\.\s]+\w)*",RegexOptions.IgnoreCase),// Windows Phone
                },
                Action = (Match match, Object obj)=>{
                    var current = obj as ClientOS;

                    current.Name = new Regex(@"(^[a-zA-Z]+\s[a-zA-Z]+)",RegexOptions.IgnoreCase).Match(match.Value).Value;

                    if(current.Name.Length<match.Value.Length)
                    {
                        var version = match.Value.Substring(current.Name.Length+1);

                        current.Version = _versionMap.Keys.Any(m=>m==version)? _versionMap[version]:version;
                    }
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(windows\smobile|windows)[\s\/]?([ntce\d\.\s]+\w)",RegexOptions.IgnoreCase)
                },
                Action = (Match match, Object obj)=>{
                    var current = obj as ClientOS;

                    current.Name = new Regex(@"(^[a-zA-Z]+)",RegexOptions.IgnoreCase).Match(match.Value).Value;

                    if(current.Name.Length<match.Value.Length)
                    {
                        var version = match.Value.Substring(current.Name.Length + 1);

                        current.Version = _versionMap.Keys.Any(m=>m==version)? _versionMap[version]:version;
                    }
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(win(?=3|9|n)|win\s9x\s)([nt\d\.]+)",RegexOptions.IgnoreCase)
                },
                Action = (Match match, Object obj)=>{
                    var current = obj as ClientOS;

                    var nameAndVersion = new string[]{match.Value.Substring(0,match.Value.IndexOf(" ")),match.Value.Substring(match.Value.IndexOf(" ")+1) };

                    current.Name = "Windows";

                    var version = nameAndVersion[1];

                    current.Version = _versionMap.Keys.Any(m=>m==version)? _versionMap[version]:version;
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"\((bb)(10);",RegexOptions.IgnoreCase)// BlackBerry 10
                },
                Action = (Match match, Object obj)=>{
                    var current = obj as ClientOS;

                    current.Name = "BlackBerry";

                    current.Version = "BB10";
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(blackberry)\w*\/?([\w\.]+)*",RegexOptions.IgnoreCase),// Blackberry
                    new Regex(@"(tizen)[\/\s]([\w\.]+)",RegexOptions.IgnoreCase),// Tizen
                    new Regex(@"(android|webos|palm\sos|qnx|bada|rim\stablet\sos|meego|contiki)[\/\s-]?([\w\.]+)*",RegexOptions.IgnoreCase),// Android/WebOS/Palm/QNX/Bada/RIM/MeeGo/Contiki
                    new Regex(@"linux;.+(sailfish);",RegexOptions.IgnoreCase)// Sailfish OS
                },
                Action = NameVersionAction
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(symbian\s?os|symbos|s60(?=;))[\/\s-]?([\w\.]+)*",RegexOptions.IgnoreCase)// Symbian
                },
                Action = (Match match, Object obj)=>{
                    var current = obj as ClientOS;

                    var nameAndVersion = new string[]{match.Value.Substring(0,match.Value.IndexOf(" ")),match.Value.Substring(match.Value.IndexOf(" ")+1) };

                    current.Name = "Symbian";

                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"\((series40);",RegexOptions.IgnoreCase)// Series 40
                },
                Action = (Match match, Object obj)=>{
                    var current = obj as ClientOS;

                    current.Name = match.Value;
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"mozilla.+\(mobile;.+gecko.+firefox",RegexOptions.IgnoreCase)// Firefox OS
                },
                Action = (Match match, Object obj)=>{
                    var current = obj as ClientOS;

                    var nameAndVersion = new string[]{match.Value.Substring(0,match.Value.IndexOf(" ")),match.Value.Substring(match.Value.IndexOf(" ")+1) };

                    current.Name = "Firefox OS";

                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    // Console
                    new Regex(@"(nintendo|playstation)\s([wids34portablevu]+)",RegexOptions.IgnoreCase),// Nintendo/Playstation

                    // GNU/Linux based
                    new Regex(@"(mint)[\/\s\(]?(\w+)*",RegexOptions.IgnoreCase),// Mint
                    new Regex(@"(mageia|vectorlinux)[;\s]",RegexOptions.IgnoreCase),// Mageia/VectorLinux
                    new Regex(@"(joli|[kxln]?ubuntu|debian|[open]*suse|gentoo|(?=\s)arch|slackware|fedora|mandriva|centos|pclinuxos|redhat|zenwalk|linpus)[\/\s-]?(?!chrom)([\w\.-]+)*",RegexOptions.IgnoreCase),// Joli/Ubuntu/Debian/SUSE/Gentoo/Arch/Slackware

                    // Joli/Ubuntu/Debian/SUSE/Gentoo/Arch/Slackware
                    // Fedora/Mandriva/CentOS/PCLinuxOS/RedHat/Zenwalk/Linpus
                    new Regex(@"(hurd|linux)\s?([\w\.]+)*",RegexOptions.IgnoreCase),// Hurd/Linux
                    new Regex(@"(gnu)\s?([\w\.]+)*",RegexOptions.IgnoreCase)// GNU
                },
                Action = NameVersionAction
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(cros)\s[\w]+\s([\w\.]+\w)",RegexOptions.IgnoreCase)// Chromium OS
                },
                Action = (Match match, Object obj)=>{
                    var current = obj as ClientOS;

                    var nameAndVersion = new string[]{match.Value.Substring(0,match.Value.IndexOf(" ")),match.Value.Substring(match.Value.IndexOf(" ")+1) };

                    current.Name = "Chromium OS";

                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(sunos)\s?([\w\.]+\d)*",RegexOptions.IgnoreCase)// Solaris
                },
                Action = (Match match, Object obj)=>{
                    var current = obj as ClientOS;

                    var nameAndVersion = new string[]{match.Value.Substring(0,match.Value.IndexOf(" ")),match.Value.Substring(match.Value.IndexOf(" ")+1) };

                    current.Name = "Solaris";

                    current.Version = nameAndVersion[1];
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"\s([frentopc-]{0,4}bsd|dragonfly)\s?([\w\.]+)*",RegexOptions.IgnoreCase),// FreeBSD/NetBSD/OpenBSD/PC-BSD/DragonFly
                    new Regex(@"(haiku)\s(\w+)",RegexOptions.IgnoreCase)
                },
                Action = NameVersionAction
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(ip[honead]+)(?:.*os\s([\w]+)*\slike\smac|;\sopera)",RegexOptions.IgnoreCase)// iOS
                },
                Action = (Match match, Object obj)=>{
                    var current = obj as ClientOS;

                    var nameAndVersion = new string[]{match.Value.Substring(0,match.Value.IndexOf(" ")),match.Value.Substring(match.Value.IndexOf(" ")+1) };

                    current.Name = "iOS";

                    current.Version = new Regex(@"\d+(?:\.\d+)*").Match(nameAndVersion[1].Replace("_",".")).Value;
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"(mac\sos\sx)\s?([\w\s\.]+\w)*",RegexOptions.IgnoreCase),
                    new Regex(@"(macintosh|mac(?=_powerpc)\s)",RegexOptions.IgnoreCase)// Mac OS
                },
                Action = (Match match, Object obj)=>{
                    var current = obj as ClientOS;

                    var nameAndVersion = new string[]{match.Value.Substring(0,match.Value.IndexOf(" ")),match.Value.Substring(match.Value.IndexOf(" ")+1) };

                    current.Name = "Mac OS";

                    current.Version = nameAndVersion[1].Replace('_','.');
                }
            },
            new MatchExpression{
                Regexes = new List<Regex>{
                    new Regex(@"((?:open)?solaris)[\/\s-]?([\w\.]+)*",RegexOptions.IgnoreCase),// Solaris
                    new Regex(@"(aix)\s((\d)(?=\.|\)|\s)[\w\.]*)*",RegexOptions.IgnoreCase),// AIX
                    new Regex(@"(plan\s9|minix|beos|os\/2|amigaos|morphos|risc\sos|openvms)",RegexOptions.IgnoreCase),// Plan9/Minix/BeOS/OS2/AmigaOS/MorphOS/RISCOS/OpenVMS
                    new Regex(@"(unix)\s?([\w\.]+)*",RegexOptions.IgnoreCase)// UNIX
                },
                Action = NameVersionAction
            }
        };
    }
#nullable restore
}
