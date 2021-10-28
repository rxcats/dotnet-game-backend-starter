using Microsoft.VisualStudio.TestTools.UnitTesting;
using RxCats.Core.Extensions;

namespace RxCats.CoreTest.Extensions;

[TestClass]
public class StringExtensionsTest
{
    enum Color
    {
        Red, Yellow, Green
    }

    [TestMethod]
    public void ParseEnum()
    {
        Assert.AreEqual(Color.Red, "Red".ParseEnum<Color>());
        Assert.AreEqual(Color.Red, "red".ParseEnum<Color>());

        Assert.AreEqual(Color.Yellow, "Yellow".ParseEnum<Color>());
        Assert.AreEqual(Color.Yellow, "yellow".ParseEnum<Color>());

        Assert.AreEqual(Color.Green, "Green".ParseEnum<Color>());
        Assert.AreEqual(Color.Green, "green".ParseEnum<Color>());
    }
}