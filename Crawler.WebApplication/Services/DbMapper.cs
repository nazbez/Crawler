﻿using Crawler.Services.Models.ResponseModels;
using Crawler.WebApplication.Models;
using System.Linq;
using System.Collections.Generic;

namespace Crawler.WebApplication.Services
{
    public class DbMapper
    {
        public IEnumerable<TestViewModel> MapTests(TestsModel testsModel)
        {
            var tests = testsModel.Tests.Select(x => new TestViewModel()
            {
                Id = x.Id,
                Url = x.Url,
                SaveTime = x.SaveTime
            });

            return tests;
        }

        public TestResultsViewModel MapTestResults(TestResultsModel testResultsModel)
        {
            var url = testResultsModel.Url;

            var testResults = testResultsModel.Results
                .Select(x => new TestResultModel()
                {
                    Url = x.Url,
                    ResponseTime = x.ResponseTime,
                    InHtml = x.InHtml,
                    InSitemap = x.InSitemap
                });

            return new TestResultsViewModel()
            {
                Url = url,
                TestResults = testResults
            };
        }
    }
}
