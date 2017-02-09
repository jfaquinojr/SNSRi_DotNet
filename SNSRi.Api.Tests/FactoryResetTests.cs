using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SNSRi.Business;
using SNSRi.Entities;
using SNSRi.Entities.HomeSeer;

namespace SNSRi.Api.Tests
{
    /// <summary>
    /// Summary description for FactoryResetTests
    /// </summary>
    [TestClass]
    public class FactoryResetTests
    {
        public FactoryResetTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Convert_HSDeviceToDevice_MustHaveNoError()
        {
            // Arrange
            var hsDevice = new HSDevice();

            // Act
            var device = ObjectConverter.ConvertToDevice(hsDevice);

            // Assert
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Convert_WhenDeviceNameIncludesRoot_MustBeHidden()
        {
            Assert.IsTrue(ObjectConverter.ConvertToDevice(createDevice("DeviceWithRootInName", "Location1", "Location2", "1", "On", false, 3)).HideFromView);
        }

        [TestMethod]
        public void Convert_WhenDeviceNameIncludesBattery_MustBeHidden()
        {
            Assert.IsTrue(ObjectConverter.ConvertToDevice(createDevice("DeviceWithBatteryInName", "Location1", "Location2", "1", "On", false, 3)).HideFromView);
        }

        [TestMethod]
        public void Convert_WhenDeviceNameDoesntIncludesBatteryOrRoot_MustBeVisible()
        {
            Assert.IsFalse(ObjectConverter.ConvertToDevice(createDevice("NormalEverydayDevice", "Location1", "Location2", "1", "On", false, 3)).HideFromView);
        }


        [TestMethod]
        public void Convert_HSRoomToUIRoom_MustHaveNoError()
        {
            // Arrange

            // Act
            var room = ObjectConverter.ConvertToRoom("My Room", 1);

            // Assert
            Assert.IsTrue(true);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Convert_HSRoomNameIsAll_MustThrowException()
        {
            // Arrange
            
            // Act
            var room = ObjectConverter.ConvertToRoom("All", 1);

            // Assert
            Assert.IsTrue(false);
        }


        private HSDevice createDevice(string name, string location1, string location2, string value, string status,
            bool hide, int refId)
        {
            return new HSDevice
            {
                Name = name,
                Location = location1,
                Location2 = location2,
                Value = value,
                Status = status,
                HideFromView = hide,
                Ref = refId
            };
        }

        [TestMethod]
        public void FactoryReset_CompareDevices_NoChanges()
        {
            // Arrange


            // get the list of existing homeseer devices
            var devices1 = new List<HSDevice>
            {
                createDevice("Device One", "Location1", "Location2", "1", "On", false, 1),
                createDevice("Device Two", "Location1", "Location2", "1", "On", false, 2)
            };


            // get the list of fresh homeseer devices
            var devices2 = new List<HSDevice>()
            {
                createDevice("Device One", "Location1", "Location2", "1", "On", false, 1),
                createDevice("Device Two", "Location1", "Location2", "1", "On", false, 2)
            };

            // check if list has changed
            var result = FactoryReset.Instance.CompareDevices(devices1, devices2);

            // Assert
            Assert.IsFalse(result.HasChanges);
        }

        [TestMethod]
        public void FactoryReset_CompareDevices_OneDeviceAdded()
        {
            // Arrange


            // get the list of existing homeseer devices
            var devices1 = new List<HSDevice>
            {
                createDevice("Device One", "Location1", "Location2", "1", "On", false, 1),
                createDevice("Device Two", "Location1", "Location2", "1", "On", false, 2)
            };


            // get the list of fresh homeseer devices
            var devices2 = new List<HSDevice>()
            {
                createDevice("Device One", "Location1", "Location2", "1", "On", false, 1),
                createDevice("Device Two", "Location1", "Location2", "1", "On", false, 2),
                createDevice("Device Three", "Location1", "Location2", "1", "On", false, 2)
            };

            // check if list has changed
            var result = FactoryReset.Instance.CompareDevices(devices1, devices2);

            // Assert
            Assert.IsTrue(result.AddedDevices.Count == 1);
        }


        [TestMethod]
        public void FactoryReset_CompareRandomSortedDevices_MustStillMatch()
        {
            // Arrange


            // get the list of existing homeseer devices
            var devices1 = new List<HSDevice>
            {
                createDevice("Device One", "Location1", "Location2", "1", "On", false, 1),
                createDevice("Device Two", "Location1", "Location2", "1", "On", false, 2),
                createDevice("Device Three", "Location1", "Location2", "1", "On", false, 3),
                createDevice("Device Four", "Location1", "Location2", "1", "On", false, 4),
                createDevice("Device Five", "Location1", "Location2", "1", "On", false, 5),
            };


            // get the list of fresh homeseer devices
            var devices2 = new List<HSDevice>()
            {
                createDevice("Device Five", "Location1", "Location2", "1", "On", false, 5),
                createDevice("Device One", "Location1", "Location2", "1", "On", false, 1),
                createDevice("Device Four", "Location1", "Location2", "1", "On", false, 4),
                createDevice("Device Three", "Location1", "Location2", "1", "On", false, 3),
                createDevice("Device Two", "Location1", "Location2", "1", "On", false, 2),
            };

            // check if list has changed
            var result = FactoryReset.Instance.CompareDevices(devices1, devices2);

            // Assert
            Assert.IsFalse(result.HasChanges);
        }

        [TestMethod]
        public void FactoryReset_CompareDevices_ThreeDevicseAdded()
        {
            // Arrange


            // get the list of existing homeseer devices
            var devices1 = new List<HSDevice>
            {
                createDevice("Device One", "Location1", "Location2", "1", "On", false, 1),
                createDevice("Device Two", "Location1", "Location2", "1", "On", false, 2)
            };


            // get the list of fresh homeseer devices
            var devices2 = new List<HSDevice>()
            {
                createDevice("Device One", "Location1", "Location2", "1", "On", false, 1),
                createDevice("Device Two", "Location1", "Location2", "1", "On", false, 2),
                createDevice("Device Three", "Location1", "Location2", "1", "On", false, 3),
                createDevice("Device Four", "Location1", "Location2", "1", "On", false, 4),
                createDevice("Device Five", "Location1", "Location2", "1", "On", false, 5)
            };

            // check if list has changed
            var result = FactoryReset.Instance.CompareDevices(devices1, devices2);

            // Assert
            Assert.IsTrue(result.AddedDevices.Count == 3);
        }

        [TestMethod]
        public void FactoryReset_CompareDevices_DeviceDeleted()
        {
            // get the list of existing homeseer devices
            var devices1 = new List<HSDevice>
            {
                createDevice("Device One", "Location1", "Location2", "1", "On", false, 1),
                createDevice("Device Two", "Location1", "Location2", "1", "On", false, 2),
                createDevice("Device Three", "Location1", "Location2", "1", "On", false, 2)
            };


            // get the list of fresh homeseer devices
            var devices2 = new List<HSDevice>()
            {
                createDevice("Device One", "Location1", "Location2", "1", "On", false, 1),
                createDevice("Device Two", "Location1", "Location2", "1", "On", false, 2),
            };

            // check if list has changed
            var result = FactoryReset.Instance.CompareDevices(devices1, devices2);

            // Assert
            Assert.IsTrue(result.AddedDevices.Count == 0 && result.DeletedDevices.Count == 1);
        }

        [TestMethod]
        public void FactoryReset_CompareDevices_TwoDevicesDeleted()
        {
            // get the list of existing homeseer devices
            var devices1 = new List<HSDevice>
            {
                createDevice("Device One", "Location1", "Location2", "1", "On", false, 1),
                createDevice("Device Two", "Location1", "Location2", "1", "On", false, 2),
                createDevice("Device Three", "Location1", "Location2", "1", "On", false, 2)
            };


            // get the list of fresh homeseer devices
            var devices2 = new List<HSDevice>()
            {
                createDevice("Device One", "Location1", "Location2", "1", "On", false, 1),
            };

            // check if list has changed
            var result = FactoryReset.Instance.CompareDevices(devices1, devices2);

            // Assert
            Assert.IsTrue(result.AddedDevices.Count == 0 && result.DeletedDevices.Count == 2);
        }


        [TestMethod]
        public void FactoryReset_CompareDevices_DeviceRenamed()
        {
            // get the list of existing homeseer devices
            var devices1 = new List<HSDevice>
            {
                createDevice("Device One", "Location1", "Location2", "1", "On", false, 1),
                createDevice("Device Two", "Location1", "Location2", "1", "On", false, 2)
            };


            // get the list of fresh homeseer devices
            var devices2 = new List<HSDevice>()
            {
                createDevice("Device One", "Location1", "Location2", "1", "On", false, 1),
                createDevice("Device Renamed", "Location1", "Location2", "1", "On", false, 2),
            };

            // check if list has changed
            var result = FactoryReset.Instance.CompareDevices(devices1, devices2);

            // Assert
            Assert.IsTrue(result.AddedDevices.Count == 1 && result.DeletedDevices.Count == 1);
        }


        [TestMethod]
        public void FactoryReset_DeviceNameThatIncludesRoot_MustBeHidden()
        {
            var devices1 = new List<HSDevice>
            {
                createDevice("Device One", "Location1", "Location2", "1", "On", false, 1),
                createDevice("Device Two", "Location1", "Location2", "1", "On", false, 2),
                createDevice("Device Root", "Location1", "Location2", "1", "On", false, 3)
            };

            //FactoryReset.Instance
            
        }
    }
}
