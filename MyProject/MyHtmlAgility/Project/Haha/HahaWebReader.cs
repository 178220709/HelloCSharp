﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Fizzler.Systems.HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyProject.MyHtmlAgility.Core;
using MyProject.MyHtmlAgility.SpiderBase;
using Omu.ValueInjecter;
using Suijing.Utils;

namespace JsonSong.Spider.Project.Haha
{
    [TestClass]
    public class HahaWebReader : WebTaskReader
    {
        public override ReadResult GetHtmlContent(string url)
        {
            var re = new ReadResult(url);
            var doc = NormalHtmlHelper.GetDocumentNode(url);
            var divContent = doc.DocumentNode.QuerySelector(".list.joke.joke-item");
            re.Content = divContent.QuerySelector(".clearfix.mt-15").OuterHtml;
            var divFooterA = divContent.QuerySelectorAll(".clearfix.mt-20.joke-item-footer .fl a").ToArray();
            var zan = ConvertHelper.ConvertStrToInt(divFooterA[0].InnerText);
            var bishi = ConvertHelper.ConvertStrToInt(divFooterA[1].InnerText);
            re.Weight = ((zan + bishi) / 100) * (zan - bishi * 3);
            return re;
        }

        public override void FireTaskCallBack(IList<ReadResult> res)
        {
            var manager = SpiderService.Instance;
            foreach (var re in res)
            {
                manager.AddNoRepeat(re, 1);
            }
        }



        [TestMethod]
        public static List<ReadResult> GetRecommand()
        {
            var urls = new List<string>();
            const string url = "http://www.haha.mx/joke/1660764";
            var doc = NormalHtmlHelper.GetDocumentNode(url);
            doc.DocumentNode.QuerySelector(".recommand-joke-main-list-thumbnail")
                .QuerySelectorAll(".joke-text.word-wrap")
                .Select(a => a.QuerySelector("a"))
                .Select(a => "http://www.haha.mx" + a.GetAttributeValue("href", ""))
                .ToList().ForEach(urls.Add);

            doc.DocumentNode.QuerySelector(".recommand-joke-main-list-text")
               .QuerySelectorAll("a").Select(a => "http://www.haha.mx" + a.GetAttributeValue("href", ""))
               .ToList().ForEach(urls.Add);
            var reader = new HahaWebReader();
            var factory = new WebTaskFactory(reader);
            return factory.StartAndCallBack(urls.Distinct().ToList());

        }

        [TestMethod]
        public void Test1()
        {
            // 测试文字笑话
            const string url = "http://www.haha.mx/joke/1660764";
            var doc = NormalHtmlHelper.GetDocumentNode(url);
            var divContent = doc.DocumentNode.QuerySelector(".list.joke.joke-item");

            var content = divContent.QuerySelector(".clearfix.mt-15");
            var divFooterA = divContent.QuerySelectorAll(".clearfix.mt-20.joke-item-footer .fl a").ToArray();
            var zan = ConvertHelper.ConvertStrToInt(divFooterA[0].InnerText);
            var bishi = ConvertHelper.ConvertStrToInt(divFooterA[1].InnerText);
            var weight = ((zan + bishi) / 100) * (zan - bishi * 3);
        }
        [TestMethod]
        public void Test2()
        {
            // 测试图片笑话
            const string url = "http://www.haha.mx/joke/1661700";
            var doc = NormalHtmlHelper.GetDocumentNode(url);
            var divContent = doc.DocumentNode.QuerySelector(".list.joke.joke-item");

            var content = divContent.QuerySelector(".clearfix.mt-15");
            var divFooterA = divContent.QuerySelectorAll(".clearfix.mt-20.joke-item-footer .fl a").ToArray();
            var zan = ConvertHelper.ConvertStrToInt(divFooterA[0].InnerText);
            var bishi = ConvertHelper.ConvertStrToInt(divFooterA[1].InnerText);
            var weight = ((zan + bishi) / 100) * (zan - bishi * 3);
        }
    }
}



/*将Flag 转换为string类型
    db.sp_haha.find().forEach(function(x){
x.Flag=x.Flag+"";
db.HahaJoke.save(x)})
*/