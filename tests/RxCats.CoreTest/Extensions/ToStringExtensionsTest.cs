using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RxCats.Core.Extensions;

namespace RxCats.CoreTest.Extensions;

[TestClass]
public class ToStringExtensionsTest
{
    struct SomeData
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return this.GetPropertiesAsString();
        }
    }

    [TestMethod]
    public void GetPropertiesAsString()
    {
        var data = new SomeData
        {
            Id = 0,
            Name = "Ann"
        };

        Assert.IsTrue(data.ToString().Contains('0'));
        Assert.IsTrue(data.ToString().Contains("Ann"));
    }
}