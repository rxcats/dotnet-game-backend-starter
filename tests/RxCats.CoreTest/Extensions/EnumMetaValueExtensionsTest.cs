using Microsoft.VisualStudio.TestTools.UnitTesting;
using RxCats.Core.Extensions;

namespace RxCats.CoreTest.Extensions;

[TestClass]
public class EnumMetaValueExtensionsTest
{
    enum Season
    {
        [EnumMetaValue("spring")] Spring,

        [EnumMetaValue("summer")] Summer,

        [EnumMetaValue("autumn")] Autumn,

        [EnumMetaValue("winter")] Winter
    }

    [TestMethod]
    public void GetMetaValue()
    {
        Assert.AreEqual("spring", Season.Spring.GetMetaValue());
        Assert.AreEqual("summer", Season.Summer.GetMetaValue());
        Assert.AreEqual("autumn", Season.Autumn.GetMetaValue());
        Assert.AreEqual("winter", Season.Winter.GetMetaValue());
    }

    [TestMethod]
    public void EnumToString()
    {
        Assert.AreEqual("Spring", Season.Spring.ToString());
        Assert.AreEqual("Summer", Season.Summer.ToString());
        Assert.AreEqual("Autumn", Season.Autumn.ToString());
        Assert.AreEqual("Winter", Season.Winter.ToString());
    }
}