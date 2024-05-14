using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.WebRequestMethods;

namespace SummerSchool
{
    public class Information
    {
        public string[] Commands { get; set; }
        public string[] Answer { get; set; }
    }
}
