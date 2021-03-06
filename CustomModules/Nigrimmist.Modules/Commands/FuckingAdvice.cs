﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

using HelloBotCommunication;
using HelloBotModuleHelper;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Nigrimmist.Modules.Helpers;

namespace Nigrimmist.Modules.Commands
{
    /// <summary>
    /// Fun advices from http://fucking-great-advice.ru/
    /// </summary>
    public class Advice : IActionHandler
    {
        public List<CallCommandInfo> CallCommandList
        {
            get
            {
                return new List<CallCommandInfo>()
                {
                    new CallCommandInfo("дай совет"),
                    new CallCommandInfo("advice")
                };
            }
        }
        public string CommandDescription { get { return @"Случайный совет с http://fucking-great-advice.ru/"; } }

        public void HandleMessage(string command, string args, object clientData, Action<string,AnswerBehaviourType> sendMessageFunc)
        {

            HtmlReaderManager hrm = new HtmlReaderManager();
            hrm.Encoding = Encoding.GetEncoding(1251);
            hrm.Get("http://fucking-great-advice.ru/api/random");
            var json = JsonConvert.DeserializeObject<dynamic>(hrm.Html);
            string advice = json.text.ToString();
            sendMessageFunc(HttpUtility.HtmlDecode(advice.RemoveAllTags()), AnswerBehaviourType.Text);
        }
    }
}
