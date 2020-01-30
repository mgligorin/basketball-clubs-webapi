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
    public class KosarkasiControllerTest
    {
        [TestMethod]
        public void GetReturnsKosarkasWithSameId()
        {
            // Arrange
            var mockRepository = new Mock<IKosarkasRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(new Kosarkas { Id = 1, ImeIPrezime = "Bogdan Bogdanovic", GodinaRodjenja = 1992, BrojUtakmicaZaKlub = 96, ProsecanBrojPoena = 12.3m });

            var controller = new KosarkasiController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(1);
            var contentResult = actionResult as OkNegotiatedContentResult<Kosarkas>;

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
            var mockRepository = new Mock<IKosarkasRepository>();
            var controller = new KosarkasiController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Get(1);

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DeleteReturnsNotFound()
        {
            // Arrange 
            var mockRepository = new Mock<IKosarkasRepository>();
            var controller = new KosarkasiController(mockRepository.Object);

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
            var mockRepository = new Mock<IKosarkasRepository>();
            mockRepository.Setup(x => x.GetById(1)).Returns(new Kosarkas { Id = 1, ImeIPrezime = "Bogdan Bogdanovic", GodinaRodjenja = 1992, BrojUtakmicaZaKlub = 96, ProsecanBrojPoena = 12.3m });
            var controller = new KosarkasiController(mockRepository.Object);

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
            var mockRepository = new Mock<IKosarkasRepository>();
            var controller = new KosarkasiController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Put(2, new Kosarkas { Id = 1, ImeIPrezime = "Bogdan Bogdanovic", GodinaRodjenja = 1992, BrojUtakmicaZaKlub = 96, ProsecanBrojPoena = 12.3m });

            // Assert
            Assert.IsInstanceOfType(actionResult, typeof(BadRequestResult));
        }

        // -------------------------------------------------------------------------------------

        [TestMethod]
        public void PostMethodSetsLocationHeader()
        {
            // Arrange
            var mockRepository = new Mock<IKosarkasRepository>();
            var controller = new KosarkasiController(mockRepository.Object);

            // Act
            IHttpActionResult actionResult = controller.Post(new Kosarkas { Id = 1, ImeIPrezime = "Bogdan Bogdanovic", GodinaRodjenja = 1992, BrojUtakmicaZaKlub = 96, ProsecanBrojPoena = 12.3m });
            var createdResult = actionResult as CreatedAtRouteNegotiatedContentResult<Kosarkas>;

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
            List<Kosarkas> kosarkasi = new List<Kosarkas>();
            kosarkasi.Add(new Kosarkas { Id = 1, ImeIPrezime = "Luka Doncic", GodinaRodjenja = 1999, BrojUtakmicaZaKlub = 26, ProsecanBrojPoena = 18.2m });
            kosarkasi.Add(new Kosarkas { Id = 2, ImeIPrezime = "Bogdan Bogdanovic", GodinaRodjenja = 1992, BrojUtakmicaZaKlub = 96, ProsecanBrojPoena = 12.3m });

            var mockRepository = new Mock<IKosarkasRepository>();
            mockRepository.Setup(x => x.GetAll()).Returns(kosarkasi.AsEnumerable());
            var controller = new KosarkasiController(mockRepository.Object);

            // Act
            IEnumerable<Kosarkas> result = controller.Get();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(kosarkasi.Count, result.ToList().Count);
            Assert.AreEqual(kosarkasi.ElementAt(0), result.ElementAt(0));
            Assert.AreEqual(kosarkasi.ElementAt(1), result.ElementAt(1));
        }
    }
}