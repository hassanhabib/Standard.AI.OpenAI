// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using KellermanSoftware.CompareNetObjects;
using KellermanSoftware.CompareNetObjects.TypeComparers;
using Moq;
using RESTFulSense.Exceptions;
using Standard.AI.OpenAI.Brokers.DateTimes;
using Standard.AI.OpenAI.Brokers.OpenAIs;
using Standard.AI.OpenAI.Models.Services.Foundations.AIFiles;
using Standard.AI.OpenAI.Services.Foundations.AIFiles;
using Tynamix.ObjectFiller;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIFiles
{
    public partial class AIFileServiceTests
    {
        private readonly Mock<IOpenAIBroker> openAiBrokerMock;
        private readonly Mock<IDateTimeBroker> dateTimeBrokerMock;
        private readonly ICompareLogic compareLogic;
        private readonly IAIFileService aiFileService;

        public AIFileServiceTests()
        {
            this.openAiBrokerMock = new Mock<IOpenAIBroker>();
            this.dateTimeBrokerMock = new Mock<IDateTimeBroker>();

            var comparisonConfig = new ComparisonConfig();

            // CompareLogic doesn't support comparing collections with a non integer indexer
            comparisonConfig.CustomPropertyComparer<HttpContentHeaders>(
                httpContentHeaders => httpContentHeaders.NonValidated,
                new HttpContentHeadersTypeComparer());

            this.compareLogic = new CompareLogic(comparisonConfig);

            this.aiFileService = new AIFileService(
                openAIBroker: this.openAiBrokerMock.Object,
                dateTimeBroker: this.dateTimeBrokerMock.Object);
        }

        public static TheoryData UnauthorizedExceptions()
        {
            return new TheoryData<HttpResponseException>
            {
                new HttpResponseUnauthorizedException(),
                new HttpResponseForbiddenException()
            };
        }

        public dynamic CreateRandomFileProperties()
        {
            int randomCreated = GetRandomNumber();
            DateTimeOffset randomCreatedDate = CreateRandomDateTimeOffset();

            dynamic randomFileProperties = CreateRandomFileProperties(
                created: randomCreated,
                createdDate: randomCreatedDate);

            return randomFileProperties;
        }

        public dynamic CreateRandomFileProperties(int created, DateTimeOffset createdDate)
        {
            Stream randomStream = CreateRandomStream();
            string randomFileName = CreateRandomString();
            string randomFileType = CreateRandomString();
            int randomBytesSize = GetRandomNumber();

            return new
            {
                ExternalFile = new StreamContent(randomStream),
                Content = randomStream,
                FileName = randomFileName,
                Name = randomFileName,
                Type = randomFileType,
                Object = randomFileType,
                Purpose = CreateRandomString(),
                Id = CreateRandomString(),
                Size = randomBytesSize,
                Bytes = randomBytesSize,
                Created = created,
                CreatedDate = createdDate,
                Deleted = GetRandomBoolean()
            };
        }

        private Expression<Func<ExternalAIFileRequest, bool>> SameExternalAIFileRequestAs(
            ExternalAIFileRequest expectedExternalAIFileRequest)
        {
            return actualExternalAIFileRequest =>
                this.compareLogic.Compare(
                    expectedExternalAIFileRequest,
                    actualExternalAIFileRequest)
                        .AreEqual;
        }

        private static bool GetRandomBoolean() =>
            Randomizer<bool>.Create();

        private static DateTimeOffset CreateRandomDateTimeOffset() =>
            new DateTimeRange(earliestDate: new DateTime()).GetValue();

        private static string CreateRandomString() =>
            new MnemonicString().GetValue();

        private static int GetRandomNumber() =>
            new IntRange(min: 2, max: 10).GetValue();

        private Stream CreateRandomStream()
        {
            int randomWordCount = GetRandomNumber();

            string randomContent =
                new MnemonicString(randomWordCount)
                    .GetValue();

            byte[] buffer = Encoding.UTF8.GetBytes(randomContent);
            var memoryStream = new TestableStream(buffer);

            return memoryStream;
        }
    }

    internal class TestableStream : MemoryStream
    {
        public TestableStream(byte[] buffer)
            : base(buffer)
        {
        }

        /// <summary>
        /// We have to override ReadTimeout because the base implementation
        /// throws an exception which causes FluentAssertions to fail.
        /// </summary>
        public override int ReadTimeout => 0;

        /// <summary>
        /// We have to override WriteTimeout because the base implementation
        /// throws an exception which causes FluentAssertions to fail.
        /// </summary>
        public override int WriteTimeout => 0;
    }

    internal class HttpContentHeadersTypeComparer : DictionaryComparer
    {
        public HttpContentHeadersTypeComparer()
            : base(RootComparerFactory.GetRootComparer())
        {
        }

        public override void CompareType(CompareParms parms)
        {
            try
            {
                parms.Result.AddParent(parms.Object1);
                parms.Result.AddParent(parms.Object2);

                // objects must be the same length
                bool countsDifferent = DictionaryCountsDifferent(parms);

                if (countsDifferent && parms.Result.ExceededDifferences)
                    return;

                CompareByKeys(parms);
            }
            finally
            {
                parms.Result.RemoveParent(parms.Object1);
                parms.Result.RemoveParent(parms.Object2);
            }
        }

        private void CompareByKeys(CompareParms parms)
        {
            var dict1 = (IReadOnlyDictionary<string, HeaderStringValues>)parms.Object1;
            var dict2 = (IReadOnlyDictionary<string, HeaderStringValues>)parms.Object2;

            if (dict1 != null)
            {
                foreach (var key in dict1.Keys)
                {
                    string currentBreadCrumb = AddBreadCrumb(parms.Config, parms.BreadCrumb, "[" + key.ToString() + "].Value");

                    CompareParms childParms = new CompareParms
                    {
                        Result = parms.Result,
                        Config = parms.Config,
                        ParentObject1 = parms.Object1,
                        ParentObject2 = parms.Object2,
                        Object1 = dict1[key],
                        Object2 = (dict2 != null) && dict2.ContainsKey(key) ? dict2[key] : null,
                        BreadCrumb = currentBreadCrumb
                    };

                    RootComparer.Compare(childParms);

                    if (parms.Result.ExceededDifferences)
                        return;
                }
            }

            if (dict2 != null)
            {
                foreach (var key in dict2.Keys)
                {
                    if (dict1 != null && dict1.ContainsKey(key))
                        continue;

                    var currentBreadCrumb = AddBreadCrumb(parms.Config, parms.BreadCrumb,
                        "[" + key.ToString() + "].Value");

                    var childParms = new CompareParms
                    {
                        Result = parms.Result,
                        Config = parms.Config,
                        ParentObject1 = parms.Object1,
                        ParentObject2 = parms.Object2,
                        Object1 = null,
                        Object2 = dict2[key],
                        BreadCrumb = currentBreadCrumb
                    };

                    RootComparer.Compare(childParms);

                    if (parms.Result.ExceededDifferences)
                        return;

                }
            }
        }

        private bool DictionaryCountsDifferent(CompareParms parms)
        {
            IDictionary iDict1 = parms.Object1 as IDictionary;
            IDictionary iDict2 = parms.Object2 as IDictionary;

            int iDict1Count = (iDict1 == null) ? 0 : iDict1.Count;
            int iDict2Count = (iDict2 == null) ? 0 : iDict2.Count;

            if (iDict1Count == iDict2Count)
                return false;

            Difference difference = new Difference
            {
                ParentObject1 = parms.ParentObject1,
                ParentObject2 = parms.ParentObject2,
                PropertyName = parms.BreadCrumb,
                Object1Value = iDict1Count.ToString(CultureInfo.InvariantCulture),
                Object2Value = iDict2Count.ToString(CultureInfo.InvariantCulture),
                ChildPropertyName = "Count",
                Object1 = iDict1,
                Object2 = iDict2
            };

            AddDifference(parms.Result, difference);

            return true;
        }
    }
}
