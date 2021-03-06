﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.WebTesting;
using MongoDB.Driver.Linq;
using JsonSong.Spider.Project.Haha;
using JsonSong.Spider.Project.Youmin;
using MyProject.MyHtmlAgility.SpiderCommon;
using MyProject.TestHelper;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using Suijing.Utils;

namespace  MyProject.MyHtmlAgility.ProjectTest
{
    /// <summary>
    /// 将lteAdmin原html文件 去掉头尾 只取内容 转换成cshtml
    /// </summary>
    [TestClass]
    public class SyncTest
    {

        private Dictionary<string, string> paras = new Dictionary<string, string>()
        {
            {"pageIndex", "1"},
            {"pageSize", "10"},
            {"cnName", "spider"},
        };


        [TestMethod]
        public void SyncTest1()
        {
            paras["cnName"] = "sp_haha";
            SyncSpiderHelper.StartSync(paras,1);

            paras["cnName"] = "sp_youmin";
            SyncSpiderHelper.StartSync(paras,2);
        }


        [TestMethod]
        public void UniqTest1()
        {
            //去重
            var instance = SyncSpiderHelper.GetSpiderCn("spider");
            var tarUrl =
                instance.Entities.ToList().GroupBy(a => a.Url)
                    .Where(g => g.Count() > 1)
                    .SelectMany(g => g.ToList().Skip(1).Select(i => i.Id)).ToList();
            tarUrl.ForEach(a => instance.Delete(a));
        }

        [TestMethod]
        public void CleanTest1()
        {
            //删除权重太低的数据
            var instance = SyncSpiderHelper.GetSpiderCn("spider");
            var tarList = instance.Entities.Where(a => a.Weight < 0.1).ToList();

            tarList.ForEach(a => instance.Delete(a.Id));
        }

  
    }
}
