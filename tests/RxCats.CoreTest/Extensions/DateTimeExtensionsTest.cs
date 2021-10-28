using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RxCats.Core.Extensions;

namespace RxCats.CoreTest.Extensions;

[TestClass]
public class DateTimeExtensionsTest
{
    [TestMethod]
    public void GetWeekOfYear()
    {
        Assert.AreEqual(202101, new DateTime(2021, 1, 1, 0, 0, 0).GetWeekOfYear());
        Assert.AreEqual(202153, new DateTime(2021, 12, 31, 0, 0, 0).GetWeekOfYear());
        Assert.AreEqual(202201, new DateTime(2022, 1, 1, 0, 0, 0).GetWeekOfYear());
    }
}