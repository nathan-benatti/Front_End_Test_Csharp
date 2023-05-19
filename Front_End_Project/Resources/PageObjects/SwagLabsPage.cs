using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace TestProject1.Resources.PageObjects
{
    public class SwagLabsPage
    {
        public readonly IWebDriver driver;
        public readonly WebDriverWait wait;

        public SwagLabsPage()
        {
            var browserOption = Environment.GetEnvironmentVariable("Opcao");

            switch (browserOption)
            {
                case "Chrome":
                case null:
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    ChromeOptions chromeOptions = new ChromeOptions();
                    //options.AddArguments("--start-maximized");  Ativar quando quiser rodar modo usuário
                    chromeOptions.AcceptInsecureCertificates = true;
                    chromeOptions.AddArguments("--remote-allow-origins=*");
                    chromeOptions.AddArguments("--headless=new");
                    chromeOptions.AddArguments("--disable-gpu");
                    chromeOptions.AddArguments("--no-sandbox");
                    chromeOptions.AddArguments("--disable-extensions");
                    chromeOptions.AddArguments("--disable-infobars");
                    driver = new ChromeDriver(chromeOptions);
                    wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    break;
                case "Edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    EdgeOptions edgeOptions = new EdgeOptions();
                    //options.AddArguments("--start-maximized");  Ativar quando quiser rodar modo usuário
                    edgeOptions.AcceptInsecureCertificates = true;
                    edgeOptions.AddArguments("--remote-allow-origins=*");
                    edgeOptions.AddArguments("--headless");
                    edgeOptions.AddArguments("--disable-gpu");
                    edgeOptions.AddArguments("--no-sandbox");
                    edgeOptions.AddArguments("--disable-extensions");
                    edgeOptions.AddArguments("--disable-infobars");
                    driver = new EdgeDriver(edgeOptions);
                    wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                    break;
            }
        }

        // WebElements 
        // Login Elements
        public IWebElement InputUsername => driver.FindElement(By.Id("user-name"));
        public IWebElement InputPassword => driver.FindElement(By.Id("password"));
        public IWebElement LoginButton => driver.FindElement(By.Id("login-button"));

        // Products Elements
        public IWebElement SauceLabsBackpack => driver.FindElement(By.Id("add-to-cart-sauce-labs-backpack"));
        public IWebElement SauceLabsBackpackSelected => driver.FindElement(By.Id("remove-sauce-labs-backpack"));
        public IWebElement SauceLabsBackPackPrice => driver.FindElement(By.XPath("//*[@id=\"inventory_container\"]/div/div[1]/div[2]/div[2]/div"));

        public IWebElement SauceLabsBikeLight => driver.FindElement(By.Id("add-to-cart-sauce-labs-bike-light"));
        public IWebElement SauceLabsBikeLightSelected => driver.FindElement(By.Id("remove-sauce-labs-bike-light"));
        public IWebElement SauceLabsBikeLightPrice => driver.FindElement(By.XPath("//*[@id=\"inventory_container\"]/div/div[2]/div[2]/div[2]/div"));
       
        public IWebElement SauceLabsBoltTShirt => driver.FindElement(By.Id("add-to-cart-sauce-labs-bolt-t-shirt"));
        public IWebElement SauceLabsBoltTShirtSelected => driver.FindElement(By.Id("remove-sauce-labs-bolt-t-shirt"));
        public IWebElement SauceLabsBoltTShirtPrice => driver.FindElement(By.XPath("//*[@id=\"inventory_container\"]/div/div[3]/div[2]/div[2]/div"));
       
        public IWebElement SauceLabsFleeceJacket => driver.FindElement(By.Id("add-to-cart-sauce-labs-fleece-jacket"));
        public IWebElement SauceLabsFleeceJacketSelected => driver.FindElement(By.Id("remove-sauce-labs-fleece-jacket"));
        public IWebElement SauceLabsFleeceJacketPrice => driver.FindElement(By.XPath("//*[@id=\"inventory_container\"]/div/div[4]/div[2]/div[2]/div"));
       
        public IWebElement SauceLabsOnesie => driver.FindElement(By.Id("add-to-cart-sauce-labs-onesie"));
        public IWebElement SauceLabsOnesieSelected => driver.FindElement(By.Id("remove-sauce-labs-onesie"));
        public IWebElement SauceLabsOnesiePrice => driver.FindElement(By.XPath("//*[@id=\"inventory_container\"]/div/div[5]/div[2]/div[2]/div"));
        
        public IWebElement SauceLabsTestAlltheThings => driver.FindElement(By.Id("add-to-cart-test.allthethings()-t-shirt-(red)"));
        public IWebElement SauceLabsTestAlltheThingsSelected => driver.FindElement(By.Id("remove-test.allthethings()-t-shirt-(red)"));
        public IWebElement SauceLabsTestAlltheThingsPrice => driver.FindElement(By.XPath("//*[@id=\"inventory_container\"]/div/div[6]/div[2]/div[2]/div"));
       
        // Buying Elements
        public IWebElement CartIconButton => driver.FindElement(By.Id("shopping_cart_container"));
        public IWebElement CheckOutButton => driver.FindElement(By.Id("checkout"));
        public IWebElement FirstName => driver.FindElement(By.Id("first-name"));
        public IWebElement LastName => driver.FindElement(By.Id("last-name"));
        public IWebElement PostalCode => driver.FindElement(By.Id("postal-code"));
        public IWebElement ContinueButton => driver.FindElement(By.Id("continue"));
        public IWebElement FinishButton => driver.FindElement(By.Id("finish"));
        public IWebElement BackHomeButton => driver.FindElement(By.Id("back-to-products"));
        public IWebElement ErrorMessage => driver.FindElement(By.CssSelector("h3[data-test='error']"));
        

        public void UserOpenSwagLabsPage(IWebDriver driver)
        {
            driver.Navigate().GoToUrl("https://www.saucedemo.com/");
        }
        public void UserLoginStandard()
        {
            InputUsername.SendKeys("standard_user");
            InputPassword.SendKeys("secret_sauce");
            LoginButton.Click();
        }
        public void UserLockedOut()
        {
            InputUsername.SendKeys("locked_out_user");
            InputPassword.SendKeys("secret_sauce");
            LoginButton.Click();
        }
        public bool LockedOutError()
        {
            string ExpectedErrorText = "Epic sadface: Sorry, this user has been locked out.";

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            try
            {
                string ErrorText = ErrorMessage.Text; // Make sure ErrorMessage is defined and accessible
                Console.WriteLine("Locked out user error received");
                Assert.That(ErrorText, Is.EqualTo(ExpectedErrorText));
                return true;
            }
            catch (AssertionException)
            {
                Console.WriteLine("Locked out user error message not received");
                return false;
            }
        }
        public void UserProblem()
        {
            InputUsername.SendKeys("problem_user");
            InputPassword.SendKeys("secret_sauce");
            LoginButton.Click();
        }
        public void UserPerformanceGlicht()
        {
            InputUsername.SendKeys("performance_glitch_user");
            InputPassword.SendKeys("secret_sauce");
            LoginButton.Click();
        }
        public void UserSelectsBackPack()
        {
            SauceLabsBackpack.Click();
        }

        // Products Assert Methods 
        public bool AssertBackPack()
        {
            try
            {
                string elementText = SauceLabsBackpackSelected.Text;
                if (elementText.Equals("Remove", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("It looks like the backpack is selected");
                    return true;
                }
                else
                {
                    Console.WriteLine("It looks like the backpack is not selected");
                    return false;
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Backpack element not found");
                return false;
            }
        }
        public bool AssertBikeLight()
        {
            try
            {
                string elementText = SauceLabsBikeLightSelected.Text;
                if (elementText.Equals("Remove", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("It looks like the bikelight is selected");
                    return true;
                }
                else
                {
                    Console.WriteLine("It looks like the bikelight is not selected");
                    return false;
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("bikelight element not found");
                return false;
            }
        }
        public bool AssertBoltTShirt()
        {
            try
            {
                string elementText = SauceLabsBoltTShirtSelected.Text;
                if (elementText.Equals("Remove", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("It looks like the boltTshirt is selected");
                    return true;
                }
                else
                {
                    Console.WriteLine("It looks like the boltTshirt is not selected");
                    return false;
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("boltTshirt element not found");
                return false;
            }
        }
        public bool AssertFleeceJacket()
        {
            try
            {
                string elementText = SauceLabsFleeceJacketSelected.Text;
                if (elementText.Equals("Remove", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("It looks like the fleecejacket is selected");
                    return true;
                }
                else
                {
                    Console.WriteLine("It looks like the fleecejacket is not selected");
                    return false;
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("fleecejacket element not found");
                return false;
            }
        }
        public bool AssertOnesie()
        {
            try
            {
                string elementText = SauceLabsOnesieSelected.Text;
                if (elementText.Equals("Remove", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("It looks like the onesie is selected");
                    return true;
                }
                else
                {
                    Console.WriteLine("It looks like the onesie is not selected");
                    return false;
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("onesie element not found");
                return false;
            }
        }
        public bool AssertAllTheThings()
        {
            try
            {
                string elementText = SauceLabsTestAlltheThingsSelected.Text;
                if (elementText.Equals("Remove", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("It looks like the allthethings is selected");
                    return true;
                }
                else
                {
                    Console.WriteLine("It looks like the allthethings is not selected");
                    return false;
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("allthethings element not found");
                return false;
            }
        }

        public void UserSelectAllItems()
        {
            SauceLabsBackpack.Click();
            SauceLabsBikeLight.Click();
            SauceLabsBoltTShirt.Click();
            SauceLabsFleeceJacket.Click();
            SauceLabsOnesie.Click();
            SauceLabsTestAlltheThings.Click();
        }
        public void UserSelectsCart()
        {
            CartIconButton.Click();
        }
        public void UserCheckOut()
        {
            CheckOutButton.Click();
        }
        public void UserFillsInfo()
        {
            FirstName.SendKeys("Name");
            LastName.SendKeys("Last Name");
            PostalCode.SendKeys("123456");
        }
        public void UserContinues()
        {
            ContinueButton.Click();
        }
        public void UserFinish()
        {
            FinishButton.Click();
        }
        public void UserGoBackHome()
        {
            BackHomeButton.Click();
        }
        public bool IsUserOnInventory()
        {
            try
            {
                wait.Until(ExpectedConditions.UrlContains("inventory.html"));
                Console.WriteLine("You are in the Inventory Page");
                return true;
            }catch(WebDriverTimeoutException)
            {
                Console.WriteLine("You are NOT in the Inventory Page");
                return false; 
            }
        }

        public bool IsUserOnChekoutComplete()
        {
            try
            {
                wait.Until(ExpectedConditions.UrlContains("checkout-complete.html"));
                Console.WriteLine("You are in the CheckOut Page");
                return true;
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("You are NOT in the CheckOut Page");
                return false;
            }
        }
        public void CloseWebDriver()
        {
            driver.Quit();
        }

        public void  SauceLabsBackPackPriceTest()
        {
            string PriceTest = SauceLabsBackPackPrice.Text;
            Console.WriteLine(PriceTest);
        }
    }
}
//te