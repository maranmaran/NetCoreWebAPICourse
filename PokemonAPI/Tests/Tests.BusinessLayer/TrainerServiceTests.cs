using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using PokemonAPI.BusinessLayer.Implementations.DomainServices;
using PokemonAPI.DomainLayer.Entities;
using PokemonAPI.PersistenceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Tests.BusinessLayer
{
    public class TrainerServiceTests
    {

        [Fact]
        public async Task GetAll_TwoTrainerInDb_GetsBoth()
        {
            var trainers = new List<Trainer>()
            {
                new Trainer()
                {
                    Id = Guid.Parse("f86b3510-3466-47a1-9a6d-8325b8b305c5"),
                    FirstName = "Ivan",
                    LastName = "Zenja"
                },
                new Trainer()
                {
                    Id = Guid.Parse("c6b8d529-6b99-4b99-a7b6-ccf7a213467a"),
                    FirstName = "Darko",
                    LastName = "Marijanovic"
                }
            };

            // ARRANGE
            var repository = new Mock<IRepository<Trainer>>();
            repository.Setup(x =>
                x.GetAll(
                    null,
                    null,
                    It.IsAny<Func<IQueryable<Trainer>, IIncludableQueryable<Trainer, object>>>(),
                    true,
                    CancellationToken.None)).ReturnsAsync(trainers);

            var mapper = TestHelper.GetMapper();

            var logger = new NullLogger<TrainerService>();

            var service = new TrainerService(repository.Object, mapper, logger);

            // ACT
            var result = (await service.GetAll(CancellationToken.None)).ToList();

            // ASSERT
            Assert.Equal(trainers[0].Id, result[0].Id);
            Assert.Equal(trainers[1].Id, result[1].Id);

            repository.Verify(x => x.GetAll(null,
                null,
                It.IsAny<Func<IQueryable<Trainer>, IIncludableQueryable<Trainer, object>>>(),
                true,
                CancellationToken.None), Times.Once);

            repository.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task Create_AddTrainerToDb_InsertsTrainer()
        {
            var trainer = new Trainer()
            {
                Id = Guid.Parse("f86b3510-3466-47a1-9a6d-8325b8b305c5"),
                FirstName = "Ivan",
                LastName = "Zenja"
            };

            //arrange 
            var repository = new Mock<IRepository<Trainer>>();
            repository.Setup(x =>
                x.Insert(
                    It.IsAny<Trainer>(), CancellationToken.None)).ReturnsAsync(trainer.Id);

            var mapper = TestHelper.GetMapper();

            var logger = new NullLogger<TrainerService>();

            var service = new TrainerService(repository.Object, mapper, logger);

            //act
            var trainerDTO = mapper.Map<TrainerDTO>(trainer);
            var result = (await service.Create(trainerDTO, CancellationToken.None));

            //assert
            repository.Verify(x => x.Insert(
                    It.IsAny<Trainer>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();

        }

        [Fact]
        public async Task Delete_DeleteTrainerFromDb_DeleteTrainer()
        {
            var trainerId = new Guid("f86b3510-3466-47a1-9a6d-8325b8b305c5");
            var request = new Trainer();
            request.Id = trainerId;
            var inputGuidToAssert = Guid.Empty;

            //arrange 
            var repository = new Mock<IRepository<Trainer, TrainerDTO>>();
            var insertSetup = repository.Setup(x =>
                x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None));

            insertSetup.Callback((Guid guid, CancellationToken token) => inputGuidToAssert = guid);

            var mapper = TestHelper.GetMapper();

            var logger = new NullLogger<TrainerService>();

            var service = new TrainerService(repository.Object, mapper, logger);

            //act
            await service.Delete(trainerId, CancellationToken.None);

            //assert
            Assert.Equal(trainerId, inputGuidToAssert);
            repository.Verify(x => x.Delete(
                    It.IsAny<Guid>(),
                    CancellationToken.None)
            , Times.Once);

            repository.VerifyNoOtherCalls();
        }
    }
}
