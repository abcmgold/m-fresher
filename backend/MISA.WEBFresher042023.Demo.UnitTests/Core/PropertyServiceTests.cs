using AutoMapper;
using MISA.WebFresher042023.Demo.Core.Dto.Property;
using MISA.WebFresher042023.Demo.Core.Entities;
using MISA.WebFresher042023.Demo.Core.HandleException;
using MISA.WebFresher042023.Demo.Core.Interface.Repository;
using MISA.WebFresher042023.Demo.Core.Service;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEBFresher042023.Demo.UnitTests.Core
{
    [TestFixture]
    public class PropertyServiceTests
    {
        /// <summary>
        /// Unit test cho hàm GetByIdAsync
        /// </summary>
        /// <returns>NotFoundException</returns>
        /// CreatedBy: BATUAN(24/06/2023)
        [Test]
        public async Task GetByIdAsync_NullEntity_NotFoundException()
        {
            // Arrange
            var id = Guid.Parse("4c70d504-706a-4b0d-b66c-8a807571ce07");

            var propertyNullRepository = new FakeNullPropertyRepository();

            var mapper = new FakeMapper();

            var propertyService = new PropertyService(propertyNullRepository, mapper);
            // Act && Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await propertyService.GetByIdAsync(id));
        }

        /// <summary>
        /// Unit test cho hàm GetByIdAsync
        /// </summary>
        /// <returns>PorpertyDto</returns>
        /// CreatedBy: BATUAN(24/06/2023)
        [Test]
        public async Task GetByIdAsync_ValidEntity_ValidDto()
        {
            // Arrange
            var id = Guid.Parse("4c70d504-706a-4b0d-b66c-8a807571ce07");

            var propertyRepository = new FakePropertyRepository();

            var mapper = new FakeMapper();

            var propertyService = new PropertyService(propertyRepository, mapper);

            var actualResult = await propertyService.GetByIdAsync(id);
            // Act && Assert
            Assert.That(propertyRepository.ActualId, Is.EqualTo(id));
        }

        /// <summary>
        /// Unit test cho hàm GetByIdAsync ver2
        /// </summary>
        /// <returns>NotFoundException</returns>
        /// CreatedBy: BATUAN(24/06/2023)
        [Test]
        public async Task GetByIdAsync_NullEntity_NotFoundExceptionVer2()
        {
            // Arrange
            var id = Guid.Parse("4c70d504-706a-4b0d-b66c-8a807571ce07");
             
            var propertyRepository = Substitute.For<IPropertyRepository>();

            propertyRepository.GetByIdAsync(id).ReturnsNull();

            var mapper = Substitute.For<IMapper>();

            var propertyService = new PropertyService(propertyRepository, mapper);

            // Act && Assert    
            Assert.ThrowsAsync<NotFoundException>(async () => await propertyService.GetByIdAsync(id));
        }

        /// <summary>
        /// Unit test cho hàm GetLastestCodeAsync
        /// </summary>
        /// <returns>ProeprtyCode được thêm vào mới nhất</returns>
        /// CreatedBy: BATUAN(24/06/2023)
        [Test]
        public async Task GetLastTestCodeAsync_Empty_ValidCode()
        {
            // Arrange

            var lastTestCode = "TS00001";

            var propertyRepository = Substitute.For<IPropertyRepository>();

            propertyRepository.GetLastestCodeAsync().Returns("TS00001");

            var mapper = Substitute.For<IMapper>();

            var propertyService = new PropertyService(propertyRepository, mapper);

            // Act

            var code = await propertyService.GetLastestCodeAsync();

            // Assert
            Assert.That(lastTestCode, Is.EqualTo(code));

            await propertyRepository.Received(1).GetLastestCodeAsync();
        }

        /// <summary>
        /// Unit test cho hàm GetAsync
        /// </summary>
        /// <returns>List propertyDto</returns>
        /// CreatedBy: BATUAN(24/06/2023)
        [Test]
        public async Task GetAsync_Empty_ListPropertyDto()
        {
            // Arrange

            var propertyRepository = Substitute.For<IPropertyRepository>();

            List<PropertyDto?> listPropertyDto = new();

            var propertyDto = new PropertyDto()
            {
                PropertyCode = "TS00001"
            };
            listPropertyDto.Add(propertyDto);

            var property = new Property()
            {
                PropertyCode = "TS00001"
            };

            List<Property> listProperty = new();

            listProperty.Add(property);

            propertyRepository.GetAsync().Returns(listProperty);

            var mapper = Substitute.For<IMapper>();

            mapper.Map<PropertyDto>(Arg.Any<Property>()).Returns(propertyDto);

            var propertyService = new PropertyService(propertyRepository, mapper);

            // Act
            var properties = await propertyService.GetAsync();

            bool isEqual = properties.SequenceEqual(listPropertyDto);
            // Assert
            Assert.That(isEqual, Is.EqualTo(true));
        }

        /// <summary>
        /// Unit test cho hàm updateAsync
        /// </summary>
        /// <returns>NotFoundException</returns>
        /// CreatedBy: BATUAN(24/06/2023)
        [Test]
        public async Task UpdateAsync_InvalidId_NotFoundException()
        {
            // Arrange
            var id = Guid.Parse("4c70d504-706a-4b0d-b66c-8a807571ce07");

            var propertyUpdateDto = new PropertyUpdateDto();

            var property = new Property();

            var propertyRepository = Substitute.For<IPropertyRepository>();

            propertyRepository.GetByIdAsync(id).ReturnsNull();

            var mapper = Substitute.For<IMapper>();

            var propertyService = new PropertyService(propertyRepository, mapper);

            // Act && Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await propertyService.UpdatePropertyAsync(id,propertyUpdateDto));
        }

        /// <summary>
        /// Unit test cho hàm GetByIdAsync
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: BATUAN(24/06/2023)
        [Test]
        public async Task UpdateAsync_ValidId_NumberRecordUpdated()
        {
            // Arrange
            var id = Guid.Parse("4c70d504-706a-4b0d-b66c-8a807571ce07");

            var propertyUpdateDto = new PropertyUpdateDto() { 
                PropertyId = id,
            };

            var property = new Property()
            {
                PropertyId = id,
            };

            var propertyRepository = Substitute.For<IPropertyRepository>();

            propertyRepository.GetByIdAsync(id).Returns(property);

            propertyRepository.UpdatePropertyAsync(id, property).Returns(1);

            var mapper = Substitute.For<IMapper>();

            mapper.Map<Property>(Arg.Any<PropertyUpdateDto>()).Returns(property);

            var propertyService = new PropertyService(propertyRepository, mapper);

            // Act
            var res = await propertyService.UpdatePropertyAsync(id, propertyUpdateDto);
            // Assert
            Assert.That(res, Is.EqualTo(1));

            await propertyRepository.Received(1).UpdatePropertyAsync(id, property);
        }

        /// <summary>
        /// Unit test cho hàm InsertAsync với đầu vào là mã code bị trùng
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: BATUAN(24/06/2023)
        [Test]
        public async Task InsertAsync_DuplicateCode_ExistedPropertyCodeException()
        {
            // Arrange
            var propertyCode = "TS00001";

            var property = new Property { PropertyCode = propertyCode };

            var propertyCreateDto = new PropertyCreateDto { PropertyCode = propertyCode };


            var propertyRepository = Substitute.For<IPropertyRepository>();

            propertyRepository.checkDuplicateProeprtyCode("TS00001").Returns(property);

            var mapper = Substitute.For<IMapper>();

            var propertyService = new PropertyService(propertyRepository, mapper);

            // Act && Assert
            Assert.ThrowsAsync<ExistedPropertyCodeException>(async () => await propertyService.InsertPropertyAsync(propertyCreateDto));

            await propertyRepository.Received(1).InsertPropertyAsync(property);
        }

        /// <summary>
        /// Unit test cho hàm InsertAsync với đầu vào là mã code không trùng
        /// </summary>
        /// <returns></returns>
        /// CreatedBy: BATUAN(24/06/2023)
        [Test]
        public async Task InsertAsync_ValidCode_NumberRecordInserted()
        {
            // Arrange
            var code = "TS00001";
            var property = new Property
            {
                PropertyCode = code,
            };

            var propertyCreateDto = new PropertyCreateDto
            {
                PropertyCode = code,
            };

            var propertyRepository = Substitute.For<IPropertyRepository>();

            propertyRepository.checkDuplicateProeprtyCode(code).ReturnsNull();

            propertyRepository.InsertPropertyAsync(property).Returns(1);

            var mapper = Substitute.For<IMapper>();

            mapper.Map<Property>(Arg.Any<PropertyCreateDto>()).Returns(property);

            var propertyService = new PropertyService(propertyRepository, mapper);

            // Act
            var res = await propertyService.InsertPropertyAsync(propertyCreateDto);
            // Assert
            Assert.That(res, Is.EqualTo(1));

            await propertyRepository.Received(1).InsertPropertyAsync(property);
        }
    }
}
