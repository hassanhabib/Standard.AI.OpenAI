// --------------------------------------------------------------- 
// Copyright (c) Coalition of the Good-Hearted Engineers 
// ---------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Force.DeepCloner;
using Moq;
using OpenAI.NET.Models.Completions;
using OpenAI.NET.Models.Completions.Exceptions;
using OpenAI.NET.Models.ExternalCompletions;
using Xunit;

namespace OpenAI.NET.Tests.Unit.Foundations.Completions
{
    public partial class CompletionServiceTests
    {
        [Fact]
        public async Task ShouldThrowValidationExceptionOnPromptIfCompletionIsNullAsync()
        {
            // given
            Completion nullCompletion = null;
            var nullCompletionException = new NullCompletionException();

            var exceptedCompletionValidationException = 
                new CompletionValidationException(nullCompletionException);

            // when
            ValueTask<Completion> promptCompletionTask = 
                this.completionService.PromptCompletionAsync(nullCompletion);
            
            CompletionValidationException actualCompletionValidationException = 
                await Assert.ThrowsAsync<CompletionValidationException>(
                    promptCompletionTask.AsTask);

            // then
            actualCompletionValidationException.Should()
                .BeEquivalentTo(exceptedCompletionValidationException);

            this.openAiBrokerMock.Verify(broker =>
                broker.PostCompletionRequestAsync(
                    It.IsAny<ExternalCompletionRequest>()),
                        Times.Never);

            this.openAiBrokerMock.VerifyNoOtherCalls();
        }
    }
}
