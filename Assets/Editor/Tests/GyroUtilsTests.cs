using Controls;
using NUnit.Framework;

public class GyroUtilsTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void GyroUtilsTestAvailable()
    {
        Assert.AreEqual(GyroUtils.IsGyroAvailable(), false);
    }
}