using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTest : IDisposable
  {

    public void Dispose()
    {
      Client.ClearAll();
    }

    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=school;password=school;port=3306;database=kaya_jepson_test;";
    }

    [TestMethod]
    public void ClientConstructor_CreatesInstanceOfClient_Client()
    {
      Client newClient = new Client("test", 1);
      Assert.AreEqual(typeof(Client), newClient.GetType());
    }

    [TestMethod]
    public void GetNameClient_ReturnsNameClient_String()
    {
      //Arrange
      string nameClient = "Jessica";
      Client newClient = new Client(nameClient, 1);

      //Act
      string result = newClient.GetNameClient();

      //Assert
      Assert.AreEqual(nameClient, result);
    }

    [TestMethod]
    public void SetNameClient_SetNameClient_String()
    {
      //Arrange
      string nameClient = "Jessica";
      Client newClient = new Client(nameClient, 1);

      //Act
      string updatedNameClient = "Carmen";
      newClient.SetNameClient(updatedNameClient);
      string result = newClient.GetNameClient();

      //Assert
      Assert.AreEqual(updatedNameClient, result);
    }

    [TestMethod]
    public void GetAll_ReturnsEmptyList_ClientList()
    {
      //Arrange
      List<Client> newList = new List<Client> { };

      //Act
      List<Client> result = Client.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_ReturnsClients_ClientList()
    {
      //Arrange
      string nameClient01 = "Jessica";
      string nameClient02 = "Carmen";
      Client newClient1 = new Client(nameClient01, 1);
      newClient1.Save();
      Client newClient2 = new Client(nameClient02, 1);
      newClient2.Save();
      List<Client> newList = new List<Client> { newClient1, newClient2 };

      //Act
      List<Client> result = Client.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectClientFromDatabase_Client()
    {
      //Arrange
      Client testClient = new Client("Jessica", 1);
      testClient.Save();

      //Act
      Client foundClient = Client.Find(testClient.GetId());

      //Assert
      Assert.AreEqual(testClient, foundClient);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNameClientsAreTheSame_Client()
    {
      // Arrange, Act
      Client firstClient = new Client("Jessica", 1);
      Client secondClient = new Client("Jessica", 1);

      // Assert
      Assert.AreEqual(firstClient, secondClient);
    }

    [TestMethod]
    public void Save_SavesToDatabase_ClientList()
    {
      //Arrange
      Client testClient = new Client("Jessica", 1);

      //Act
      testClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{testClient};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Client testClient = new Client("Jessica", 1);

      //Act
      testClient.Save();
      Client savedClient = Client.GetAll()[0];

      int result = savedClient.GetId();
      int testId = testClient.GetId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Edit_UpdatesClientInDatabase_String()
    {
      //Arrange
      Client testClient = new Client("Carmen", 1);
      testClient.Save();
      string secondNameClient = "Jessica";

      //Act
      testClient.Edit(secondNameClient);
      string result = Client.Find(testClient.GetId()).GetNameClient();

      //Assert
      Assert.AreEqual(secondNameClient, result);
    }

  }
}
