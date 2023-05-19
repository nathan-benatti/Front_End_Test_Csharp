using TestProject1.Resources.PageObjects;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace FullStack.Tests.Scenario_Front
{
    public class SwagLabFeature
    {
        #region TestInitialization
        private SwagLabsPage Page;
        private bool stopTests;

        [SetUp]
        public void Setup()
        {
            Page = new SwagLabsPage();

            if (stopTests)
            {
                NUnit.Framework.Assert.Inconclusive("Test Suite is inconclusive because \"FrontEnd\" Failed.");
            }
        }

        [TearDown]
        public void TearDown()
        {
            Page.CloseWebDriver();
            if (NUnit.Framework.TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed && (NUnit.Framework.TestContext.CurrentContext.Test.Name == "FrontEnd"))
            {
                stopTests = true;
            }
        }
        #endregion

        [Test, Order(0), Category("Front-End"),Timeout(60000)]
        public void Standard_User_Buy()
        {
            #region Arrange
            Page.UserOpenSwagLabsPage(Page.driver);
            Page.UserLoginStandard();
            Page.UserSelectAllItems(); // MUST ASSERT
            #endregion

            #region Act
            Page.UserSelectsCart();
            Page.UserCheckOut();
            Page.UserFillsInfo();
            Page.UserContinues();
            Page.UserFinish(); // MUST ASSERT
            #endregion

            #region Assert
            Page.IsUserOnChekoutComplete();
            #endregion 
        }
        [Test, Order(1), Category("Front-End"), Timeout(60000)]
        public void Products_Selected()
        {
            #region Arrange
            Page.UserOpenSwagLabsPage(Page.driver);
            Page.UserLoginStandard();
            #endregion

            #region Act
            Page.UserSelectAllItems();
            #endregion

            #region Assert
            Page.AssertBackPack();
            Page.AssertBikeLight();
            Page.AssertBoltTShirt();
            Page.AssertFleeceJacket();
            Page.AssertOnesie();
            Page.AssertAllTheThings();
            #endregion

        }

        [Test, Order(2), Category("Front-End"), Timeout(60000)]
        public void Standard_User_login()
        {
            #region Act
            Page.UserOpenSwagLabsPage(Page.driver);
            Page.UserLoginStandard();
            #endregion

            #region Assert
            Assert.IsTrue(Page.IsUserOnInventory());
            #endregion
        }

        [Test, Order(3), Category("Front-End"), Timeout(60000)]
        public void Locked_Out_User_login()
        {
            #region Act
            Page.UserOpenSwagLabsPage(Page.driver);
            Page.UserLockedOut();
            #endregion

            #region Assert
            Page.LockedOutError();
            #endregion
        }

        [Test, Order(4), Category("Front-End"), Timeout(60000)]
        public void Problem_User_login()
        {
            #region Act
            Page.UserOpenSwagLabsPage(Page.driver);
            Page.UserProblem();
            #endregion
            #region Assert
            Page.IsUserOnInventory();
            #endregion

        }

        [Test, Order(5), Category("Front-End"), Timeout(60000)]
        public void Performance_Glitch_User_login()
        {
            #region Act
            Page.UserOpenSwagLabsPage(Page.driver);
            Page.UserPerformanceGlicht();
            #endregion

            #region Assert
            Page.IsUserOnInventory();
            #endregion
        }

        [Test, Order(6), Category("Front-End"), Timeout(60000)]
        public void PriceTest()
        {
            #region Act
            Page.UserOpenSwagLabsPage(Page.driver);
            Page.UserLoginStandard();
            #endregion

            Page.SauceLabsBackPackPriceTest();
        }

        [Test, Order(7), Category("Front-End"), Timeout(60000)]
        public void BackPack()
        {
            Page.UserOpenSwagLabsPage(Page.driver);
            Page.UserLoginStandard();
            Page.UserSelectsBackPack();

            Page.AssertBackPack();
        }
    }
}
