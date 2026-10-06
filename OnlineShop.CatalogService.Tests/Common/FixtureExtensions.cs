using AutoFixture;
using AutoFixture.AutoNSubstitute;

namespace OnlineShop.CatalogService.Tests.Common;
public static class FixtureExtensions
{
    /// <summary>
    /// Создаёт AutoFixture с интеграцией NSubstitute для автомокинга.
    /// </summary>
    public static IFixture CreateFixture()
    {
        return new Fixture().Customize(new AutoNSubstituteCustomization
        {
            ConfigureMembers = true
        });
    }
}