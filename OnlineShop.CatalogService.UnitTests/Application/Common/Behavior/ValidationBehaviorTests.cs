using FluentAssertions;
using FluentValidation;
using MediatR;
using OnlineShop.CatalogService.Application.Common.Behaviors;

namespace OnlineShop.CatalogService.Tests.Application.Common.Behavior;
public class ValidationBehaviorTests
{
    public record TestRequest(string Value) : IRequest<string>;

    public class AlwaysValidValidator : AbstractValidator<TestRequest>
    {
        public AlwaysValidValidator() { }
    }

    public class AlwaysInvalidValidator : AbstractValidator<TestRequest>
    {
        public AlwaysInvalidValidator()
        {
            RuleFor(x => x.Value)
                .Must(_ => false)
                .WithMessage("Always invalid");
        }

        [Fact]
        public async Task Handle_ShouldCallNext_WhenValidationPasses()
        {
            var validators = new IValidator<TestRequest>[] { new AlwaysValidValidator() };
            var behavior = new ValidationBehavior<TestRequest, string>(validators);

            var nextCalled = false;
            Task<string> Next(CancellationToken ct)
            {
                nextCalled = true;
                return Task.FromResult("next result");
            }

            var request = new TestRequest("anything");

            var result = await behavior.Handle(request, Next, CancellationToken.None);

            nextCalled.Should().BeTrue();
            result.Should().Be("next result");
        }
    }

    [Fact]
    public async Task Handle_ShouldThrowValidationException_WhenValidationFails()
    {
        var validators = new IValidator<TestRequest>[] { new AlwaysInvalidValidator() };
        var behavior = new ValidationBehavior<TestRequest, string>(validators);

        var nextCalled = false;
        Task<string> Next(CancellationToken ct)
        {
            nextCalled = true;
            return Task.FromResult("next result");
        }

        var request = new TestRequest("anything");

        var act = () => behavior.Handle(request, Next, CancellationToken.None);

        await act.Should().ThrowAsync<ValidationException>()
            .Where(e => e.Errors.Any(f => f.ErrorMessage == "Always invalid"));

        nextCalled.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_ShouldCallNext_WhenNoValidators()
    {
        var validators = new IValidator<TestRequest>[] {};
        var behavior = new ValidationBehavior<TestRequest, string>(validators);

        var nextCalled = false;
        Task<string> Next(CancellationToken ct)
        {
            nextCalled = true;
            return Task.FromResult("next result");
        }

        var request = new TestRequest("anything");

        var result = await behavior.Handle(request, Next, CancellationToken.None);

        nextCalled.Should().Be(true);
        result.Should().Be("next result");
    }

    [Fact]
    public async Task Handle_ShouldAggregateErrors_FromMultipleValidators()
    {
        var validators = new IValidator<TestRequest>[]
        {
            new AlwaysInvalidValidator(),
            new AlwaysInvalidValidator()
        };
        var behavior = new ValidationBehavior<TestRequest, string>(validators);

        Task<string> Next(CancellationToken ct) => Task.FromResult("next");

        var request = new TestRequest("anything");

        var act = () => behavior.Handle(request, Next, CancellationToken.None);

        var exception = await act.Should().ThrowAsync<ValidationException>();
        exception.Which.Errors.Should().HaveCountGreaterThanOrEqualTo(2);
    }
}