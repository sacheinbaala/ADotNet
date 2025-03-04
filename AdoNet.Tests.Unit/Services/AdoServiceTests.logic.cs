﻿// ---------------------------------------------------------------
// Copyright (c) Hassan Habib All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// ---------------------------------------------------------------

using ADotNet.Models.Pipelines.AspNets;
using Moq;
using Xunit;

namespace AdoNet.Tests.Unit.Services
{
    public partial class AdoServiceTests
    {
        [Fact]
        public void ShouldSerializeAndWriteAdoPipelineModel()
        {
            // given
            AspNetPipeline randomAspNetPipeline = 
                CreateRandomAspNetPipeline();
            
            AspNetPipeline inputAspNetPipeline = 
                randomAspNetPipeline;
            
            string randomPath = GetRandomFilePath();
            string inputPath = randomPath;
            string serialziedPipeline = GetRandomString();

            this.yamlBrokerMock.Setup(broker =>
                broker.SerializeToYaml(inputAspNetPipeline))
                    .Returns(serialziedPipeline);

            // when
            this.adoService.SerializeAndWriteToFile(
                inputPath, 
                inputAspNetPipeline);

            // then
            this.yamlBrokerMock.Verify(broker =>
                broker.SerializeToYaml(inputAspNetPipeline),
                    Times.Once);

            this.filesBrokerMock.Verify(broker =>
                broker.WriteToFile(inputPath, serialziedPipeline),
                    Times.Once);

            this.yamlBrokerMock.VerifyNoOtherCalls();
            this.filesBrokerMock.VerifyNoOtherCalls();
        }
    }
}
