using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using HairSalon;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{

  [TestClass]

  public class StylistsTest : IDisposable
  {
    public StylistsTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=hairsalon_test;";
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyFirst_0()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Equals_OverrideTrueIfNamesAreSame_Stylist()
    {
      //Arrange, Act
      Stylist firstStylist = new Stylist("Jen");
      Stylist secondStylist = new Stylist("Jen");

      //Assert
      Assert.AreEqual(firstStylist, secondStylist);
    }

    [TestMethod]
    public void Save_SavesToDatabase_StylistList()
    {
      //Arrange
      Stylist testStylist = new Stylist("Jen");

      //Act
      testStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};

      //Assert
      Assert.AreEqual(result, testList);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Assert
      Stylist testStylist = new Stylist("Jen");

      //Act
      testStylist.Save();
      Stylist savedStylist = Stylist.GetAll()[0];

      int result = savedStylist.GetId();
      int testId = testStylist.GetId();

      //Assert
      Assert.AreEqual(result, testId);
    }

    [TestMethod]
    public void Find_FindStylistInDatabase_Stylist()
    {
      // Arrange
      Stylist testStylist = new Stylist("Jen");
      testStylist.Save();

      // Act
      Stylist foundStylist = Stylist.Find(testStylist.GetId());

      // Assert
      Assert.AreEqual(testStylist, foundStylist);
    }

    [TestMethod]
    public void Update_UpdatesStylistInDatabase_String()
    {
      // Arrange
      string name = "Jen";
      Stylist testStylist = new Stylist(name);
      testStylist.Save();
      string newName = "Helen";

      // Act
      testStylist.UpdateName(newName);

      string result = Stylist.Find(testStylist.GetId()).GetName();

      // Assert
      Assert.AreEqual(newName, result);
    }
  }
}
