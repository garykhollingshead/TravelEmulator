using NUnit.Framework.Constraints;
using TravelEmulator.Data;
using TravelEmulator.Generators;

namespace UnitTests;

[TestFixture]
public class SettingsTests
{
    [SetUp]
    public void SetUp()
    {
        RandomGenerator.Seed = 5;
    }

    [Test]
    public void GenerateFromFile_GivenGoodCarSettingsFile_ReturnsNewSettings()
    {
        var settings = Settings.GenerateFromFile("./TestData/GoodCarSettings.json");
        
        Assert.That(settings, Is.Not.Null);
        Assert.That(settings.VehicleType, Is.EqualTo(VehicleTypes.Car));
    }

    [Test]
    public void GenerateFromFile_GivenGoodBoatSettingsFile_ReturnsNewSettings()
    {
        var settings = Settings.GenerateFromFile("./TestData/GoodBoatSettings.json");
        
        Assert.That(settings, Is.Not.Null);
        Assert.That(settings.VehicleType, Is.EqualTo(VehicleTypes.Boat));
    }

    [Test]
    public void GenerateFromFile_GivenBadSettingsFile_ThrowsException()
    {
        var exception = Assert.Throws<Exception>(() => Settings.GenerateFromFile("./TestData/BadSettings.json"));
        Assert.That(exception?.Message, Does.StartWith("Unable to parse Settings file."));
    }

    [Test]
    public void GenerateFromFile_GivenNonexistentSettingsFile_ThrowsException()
    {
        var exception = Assert.Throws<Exception>(() => Settings.GenerateFromFile("./ShouldNotExist.json"));
        Assert.That(exception?.Message, Does.StartWith("Unable to read settings file from"));
    }
}