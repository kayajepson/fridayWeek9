using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=school;password=school;port=3306;database=kaya_jepson_test;";
    }

    public void Dispose()
    {
      Stylist.ClearAll();
      // Client.ClearAll();
    }

    [TestMethod]
    public void StylistConstructor_CreatesInstanceOfStylist_Stylist()
    {
      Stylist newStylist = new Stylist("test stylist");
      Assert.AreEqual(typeof(Stylist), newStylist.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      //Arrange
      string name = "test Stylist";
      Stylist newStylist = new Stylist(name);

      //Act
      string result = newStylist.GetName();

      //Assert
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void GetAll_ReturnsAllStylistObjects_StylistList()
    {
      //Arrange
      string name01 = "suzie";
      string name02 = "tammy";
      Stylist newStylist1 = new Stylist(name01);
      newStylist1.Save();
      Stylist newStylist2 = new Stylist(name02);
      newStylist2.Save();
      List<Stylist> newList = new List<Stylist> { newStylist1, newStylist2 };

      //Act
      List<Stylist> result = Stylist.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectStylist_Stylist()
    {
      //Arrange
      Stylist testStylist = new Stylist("suzie");
      testStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      //Assert
      Assert.AreEqual(testStylist, foundStylist);
    }


    [TestMethod]
    public void GetAll_StylistsEmptyAtFirst_List()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Stylist()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("suzie");
      Stylist secondStylist = new Stylist("suzie");

      //Assert
      Assert.AreEqual(firstStylist, secondStylist);
    }
    [TestMethod]
    public void Save_SavesStylistToDatabase_StylistList()
    {
      //Arrange
      Stylist testStylist = new Stylist("suzie");
      testStylist.Save();

      //Act
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }


  }
}
