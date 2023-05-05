// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Standard.AI.OpenAI.Models.Services.Foundations.AIModels;
using Standard.AI.OpenAI.Models.Services.Foundations.ExternalAIModels;
using Xunit;

namespace Standard.AI.OpenAI.Tests.Unit.Services.Foundations.AIModels
{
    public partial class AIModelServiceTests
    {
        [Fact]
        public async Task ShouldRetrieveAIModelByNameAsync()
        {
            // given 
            string randomString = CreateRandomString();
            string randomAIModelName = randomString;
            string inputAIModelName = randomAIModelName;

            dynamic aiModelRandomProperties =
                CreateRandomAIModelProperties();

            var externalAIModel = new ExternalAIModel
            {
                Id = aiModelRandomProperties.Id,
                Created = aiModelRandomProperties.Created,
                Object = aiModelRandomProperties.Object,
                OwnedBy = aiModelRandomProperties.OwnedBy,
                Parent = aiModelRandomProperties.Parent,
                Root = aiModelRandomProperties.Root,

                Permissions = ((dynamic[])aiModelRandomProperties.Permissions).Select(
                    randomPermissionProperty =>
                    {
                        return new ExternalAIModelPermission
                        {
                            Id = randomPermissionProperty.Id,
                            Object = randomPermissionProperty.Object,
                            Created = randomPermissionProperty.Created,
                            AllowCreateEngine = randomPermissionProperty.AllowCreateEngine,
                            AllowSampling = randomPermissionProperty.AllowSampling,
                            AllowLogprobs = randomPermissionProperty.AllowLogprobs,
                            AllowSearchIndices = randomPermissionProperty.AllowSearchIndices,
                            AllowView = randomPermissionProperty.AllowView,
                            AllowFineTuning = randomPermissionProperty.AllowFineTuning,
                            Organization = randomPermissionProperty.Organization,
                            IsBlocking = randomPermissionProperty.IsBlocking
                        };
                    }).ToArray()
            };

            var expectedAIModel = new AIModel
            {
                Name = aiModelRandomProperties.Name,
                CreatedDate = aiModelRandomProperties.CreatedDate,
                Type = aiModelRandomProperties.Type,
                OwnedBy = aiModelRandomProperties.OwnedBy,
                Parent = aiModelRandomProperties.Parent,
                OriginModel = aiModelRandomProperties.OriginModel,

                Permissions = ((dynamic[])aiModelRandomProperties.Permissions).Select(
                    randomPermissionProperty =>
                    {
                        return new AIModelPermission
                        {
                            Id = randomPermissionProperty.Id,
                            Type = randomPermissionProperty.Type,
                            CreatedDate = randomPermissionProperty.CreatedDate,
                            AllowCreateEngine = randomPermissionProperty.AllowCreateEngine,
                            AllowSampling = randomPermissionProperty.AllowSampling,
                            AllowLogProbabilities = randomPermissionProperty.AllowLogProbabilities,
                            AllowSearchIndices = randomPermissionProperty.AllowSearchIndices,
                            AllowView = randomPermissionProperty.AllowView,
                            AllowFineTuning = randomPermissionProperty.AllowFineTuning,
                            Organization = randomPermissionProperty.Organization,
                            IsBlocking = randomPermissionProperty.IsBlocking
                        };
                    }).ToArray()
            };

            int created = aiModelRandomProperties.Created;

            this.dateTimeBrokerMock.Setup(broker =>
                broker.ConvertToDateTimeOffSet(created))
                    .Returns(aiModelRandomProperties.CreatedDate);

            ((dynamic[])aiModelRandomProperties.Permissions).ToList().ForEach(permission =>
            {
                int permissionCreated = permission.Created;

                this.dateTimeBrokerMock.Setup(broker =>
                    broker.ConvertToDateTimeOffSet(permissionCreated))
                        .Returns(permission.CreatedDate);
            });

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAIModelByIdAsync(inputAIModelName))
                    .ReturnsAsync(externalAIModel);

            // when
            AIModel actualAIModel =
                await this.aiModelService.RetrieveAIModelByNameAsync(inputAIModelName);

            // then
            actualAIModel.Should().BeEquivalentTo(expectedAIModel);

            this.dateTimeBrokerMock.Verify(broker =>
                broker.ConvertToDateTimeOffSet(created),
                    Times.Once);

            ((dynamic[])aiModelRandomProperties.Permissions).ToList().ForEach(permission =>
            {
                int permissionCreated = permission.Created;

                this.dateTimeBrokerMock.Verify(broker =>
                    broker.ConvertToDateTimeOffSet(permissionCreated),
                        Times.Once);
            });

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAIModelByIdAsync(inputAIModelName),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}