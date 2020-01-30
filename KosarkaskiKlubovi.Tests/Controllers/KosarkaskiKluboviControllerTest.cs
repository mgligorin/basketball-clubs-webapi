using KosarkaskiKlubovi.Controllers;
using KosarkaskiKlubovi.Interfaces;
using KosarkaskiKlubovi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;

namespace KosarkaskiKlubovi.Tests.Controllers
{
    [TestClass]
    public class KosarkaskiKluboviControllerTest
    {
        [TestMethod]
        public void GetReturnsKosarkaskiKlubWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<IKosarkaskiKlubRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(new KosarkaskiKlub { Id = 1, Naziv = "Sacramento Kings", Liga = "NBA", GodinaOsnivanjaKluba = 1985, BrojOsvojenihTrofeja = 5 });

            var controller = new KosarkaskiKluboviController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<KosarkaskiKlub>;

            // Assert
            Assert.IsNotNull(contentResult);
            Assert.IsNotNull(contentResult.Content);
            Assert.AreEqual(1, contentResult.Content.Id);
        }

        // --------------------------------------------------------------------------------------

        [TestMethod]
        public void GetReturnsNotFound()
        {
            // Arrange
            var mockRepository = new Mock<IKosarkaskiKlubRepository>();
            var controller = new KosarkaskiKluboviController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteReturnsNotFound()
        {
            // Arrange 
            var mockRepository = new Mock<IKosarkaskiKlubRepository>();
            var controller = new KosarkaskiKluboviController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        // --------------------------------------------------------------------------------------

        [TestMethod]
        public void DeleteReturnsOk()
        {
            // Arrange
            var mockRepository = new Mock<IKosarkaskiKlubRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(new KosarkaskiKlub { Id = 1, Naziv = "Sacramento Kings", Liga = "NBA", GodinaOsnivanjaKluba = 1985, BrojOsvojenihTrofeja = 5 });
            var controller = new KosarkaskiKluboviController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Delete(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(OkResult));
        }

        // --------------------------------------------------------------------------------------

        [TestMethod]
        public void PutReturnsBadRequest()
        {
            // Arrange
            var mockRepository = new Mock<IKosarkaskiKlubRepository>();
            var controller = new KosarkaskiKluboviController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(2, new KosarkaskiKlub { Id = 1, Naziv = "Sacramento Kings", Liga = "NBA", GodinaOsnivanjaKluba = 1985, BrojOsvojenihTrofeja = 5 });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        // -------------------------------------------------------------------------------------

        [TestMethod]
        public void PostMethodSetsLocationHeader()
        {
            // Arrange
            var mockRepository = new Mock<IKosarkaskiKlubRepository>();
            var controller = new KosarkaskiKluboviController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Post(new KosarkaskiKlub { Id = 1, Naziv = "Sacramento Kings", Liga = "NBA", GodinaOsnivanjaKluba = 1985, BrojOsvojenihTrofeja = 5 });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<KosarkaskiKlub>;

            // Assert
            Assert.IsNotNull(createdResult);
            Assert.AreEqual("DefaultApi", createdResult.RouteName);
            Assert.AreEqual(1, createdResult.RouteValues["id"]);
        }

        // ------------------------------------------------------------------------------------------

        [TestMethod]
        public void GetReturnsMultipleObjects()
        {
            // Arrange
            List<KosarkaskiKlub> kosarkaskiKlubovi = new List<KosarkaskiKlub>();
            kosarkaskiKlubovi.Add(new KosarkaskiKlub { Id = 1, Naziv = "Sacramento Kings", Liga = "NBA", GodinaOsnivanjaKluba = 1985, BrojOsvojenihTrofeja = 5 });
            kosarkaskiKlubovi.Add(new KosarkaskiKlub { Id = 2, Naziv = "Dallas Mavericks", Liga = "NBA", GodinaOsnivanjaKluba = 1980, BrojOsvojenihTrofeja = 6 });

            var mockRepository = new Mock<IKosarkaskiKlubRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(kosarkaskiKlubovi.AsEnumerable());
            var controller = new KosarkaskiKluboviController(mockRepository.Object);

            // Act
            IEnumerable<KosarkaskiKlub> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(kosarkaskiKlubovi.Count, result.ToList().Count);
            Assert.AreEqual(kosarkaskiKlubovi.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(kosarkaskiKlubovi.ElementAt(1), result.ElementAt(1));
        }
    }
}
