// ---------------------------------------------------------------------------------- 
// Copyright (c) The Standard Organization, a coalition of the Good-Hearted Engineers 
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
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
        public async Task ShouldRetrieveAllAIModelsAsync()
        {
            // given
            List<dynamic> aiModelRandomPropertiesList =
                CreateRandomAIModelsPropertiesList();

            var externalAIModelResult = new ExternalAIModelsResult
            {
                AIModels = aiModelRandomPropertiesList.Select(item =>
                {
                    return new ExternalAIModel
                    {
                        Id = item.Id,
                        Created = item.Created,
                        Object = item.Object,
                        OwnedBy = item.OwnedBy,
                        Parent = item.Parent,
                        Root = item.Root,

                        Permissions = ((dynamic[])item.Permissions).Select(
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
                }).ToArray()
            };

            IEnumerable<AIModel> randomAIModels = aiModelRandomPropertiesList.Select(item =>
            {
                return new AIModel
                {
                    Name = item.Name,
                    CreatedDate = item.CreatedDate,
                    Type = item.Type,
                    OwnedBy = item.OwnedBy,
                    Parent = item.Parent,
                    OriginModel = item.OriginModel,

                    Permissions = ((dynamic[])item.Permissions).Select(
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
            }).ToArray();

            IEnumerable<AIModel> expectedAIModels = randomAIModels;

            aiModelRandomPropertiesList.ForEach(item =>
            {
                int created = item.Created;

                this.dateTimeBrokerMock.Setup(broker =>
                    broker.ConvertToDateTimeOffSet(created))
                        .Returns(item.CreatedDate);

                ((dynamic[])item.Permissions).ToList().ForEach(permission =>
                {
                    int permissionCreated = permission.Created;

                    this.dateTimeBrokerMock.Setup(broker =>
                        broker.ConvertToDateTimeOffSet(permissionCreated))
                            .Returns(permission.CreatedDate);
                });
            });

            this.openAIBrokerMock.Setup(broker =>
                broker.GetAllAIModelsAsync())
                    .ReturnsAsync(externalAIModelResult);

            // when
            IEnumerable<AIModel> actualAIModels =
                await this.aiModelService.RetrieveAllAIModelsAsync();

            // then
            actualAIModels.Should().BeEquivalentTo(expectedAIModels);

            aiModelRandomPropertiesList.ForEach(item =>
            {
                int created = item.Created;

                this.dateTimeBrokerMock.Verify(broker =>
                    broker.ConvertToDateTimeOffSet(created),
                        Times.Once);

                ((dynamic[])item.Permissions).ToList().ForEach(permission =>
                {
                    int permissionCreated = permission.Created;

                    this.dateTimeBrokerMock.Verify(broker =>
                        broker.ConvertToDateTimeOffSet(permissionCreated),
                            Times.Once);
                });
            });

            this.openAIBrokerMock.Verify(broker =>
                broker.GetAllAIModelsAsync(),
                    Times.Once);

            this.dateTimeBrokerMock.VerifyNoOtherCalls();
            this.openAIBrokerMock.VerifyNoOtherCalls();
        }
    }
}
