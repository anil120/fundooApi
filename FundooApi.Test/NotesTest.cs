using FundooAPI.DataContext;
using FundooAPI.Repository;
using FundooAPI.Services;
using FundooData.Models;
using FundooNotes.Controllers;
using GenFu;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace FundooApi.Test
{
    public class NotesControllerTest
    {
        private readonly NotesController controller;
        private readonly INotesService service;
        private readonly INotesRepository repository;
        private Mock<AuthenticationContext> authenticationContext;
        AuthenticationContext context;

        public NotesControllerTest(NotesController controller, INotesService service, AuthenticationContext authenticationContext,INotesRepository repository)
        {
            this.controller = controller;
            this.service = service;
            this.repository = repository;
            this.authenticationContext = new Mock<AuthenticationContext>();
        }
        private IEnumerable<NotesModel> GetFakeData()
        {
            var persons = A.ListOf<NotesModel>(26);
            persons.ForEach(x => x.Id = Guid.NewGuid());
            return persons.Select(_ => _);
        }
        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = controller.GetNotes();
            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }
        [Fact]
        public void GetNotesTest()
        {
            // Arrange
            var service = new Mock<INotesService>();
            var persons = GetFakeData();
            service.Setup(x => x.GetAllItems()).Returns(persons);
            
            // Act
            var results = controller.GetNotes();
            var count = results.Count();
            // Assert
            Assert.Equal(count, 26);
        }
        [Fact]
        public  void Task_GetPostById_Return_OkResult()
        {
            //Arrange  
            var controller = new NotesController(context, repository);
            //var postId = 2;

            //Act  
            var data =  controller.GetNotes();

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

    }
}
