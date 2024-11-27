/*using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace UnitTest.SystemTests
{
    public class UserProfileTests : IDisposable
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public UserProfileTests()
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20)); // Aumentando o tempo de espera para 30 segundos
        }

        [Fact]
        public void UserProfile_CRUD_Test()
        {
            // Navegar até a página de login
            _driver.Navigate().GoToUrl("http://localhost:5173/login");

            Thread.Sleep(2000);

            // Login
            var emailElement = _wait.Until(driver => driver.FindElement(By.CssSelector("input[type='email']")));
            emailElement.SendKeys("olaf.user@example.com");
            _driver.FindElement(By.CssSelector("input[type='password']")).SendKeys("issenha");
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Thread.Sleep(2000);

            // Verificar se está na página inicial após login
            _wait.Until(driver => driver.FindElement(By.CssSelector("nav")));

            Thread.Sleep(2000);

            // Logout
            var logoutButton = _wait.Until(driver => driver.FindElement(By.XPath("//button[.//span[contains(text(), 'LOG OUT')]]")));
            logoutButton.Click();

            Thread.Sleep(2000);

            // Navegar para a página de registro
            _driver.Navigate().GoToUrl("http://localhost:5173/registration");

            Thread.Sleep(2000);

            // Registro de Usuário
            _wait.Until(driver => driver.FindElement(By.Id("nomeCompleto"))).SendKeys("Tobias");
            _driver.FindElement(By.Id("apelido")).SendKeys("Tobi");
            _driver.FindElement(By.Id("email")).SendKeys("tobias@gmail.com");
            _driver.FindElement(By.Id("data")).SendKeys("1992-02-02");
            _driver.FindElement(By.Id("cpf")).SendKeys("12345678909");
            _driver.FindElement(By.Id("genero")).SendKeys("Masculino");
            _driver.FindElement(By.Id("senha")).SendKeys("password123");
            _driver.FindElement(By.Id("telefone")).SendKeys("1199999-9999");

            Thread.Sleep(2000);

            // Clique no botão "Próximo Passo"
            var nextStepButton = _wait.Until(driver => driver.FindElement(By.XPath("//button[contains(., 'Próximo Passo')]")));
            nextStepButton.Click();

            Thread.Sleep(2000);

            // Registro de Endereço
            _wait.Until(driver => driver.FindElement(By.Name("rua"))).SendKeys("Nove de Julho");
            _driver.FindElement(By.Name("bairro")).SendKeys("Altaneira");
            _driver.FindElement(By.Name("cep")).SendKeys("00000-000");
            _driver.FindElement(By.Name("complemento")).SendKeys("Atrás do Tauste");
            _driver.FindElement(By.Name("cidade")).SendKeys("Ubatuba");
            _driver.FindElement(By.Name("numeroResidencia")).SendKeys("123");

            var saveButton = _wait.Until(driver => driver.FindElement(By.XPath("//div[@class='text-right mt-14']//button[contains(., 'Salvar')]"))); saveButton.Click();

            Thread.Sleep(2000);

            // Verificar se o usuário e o endereço foram criados
            var createdUserName = _wait.Until(driver => driver.FindElement(By.Id("nomeCompleto"))).GetAttribute("value");
            Assert.Equal("Tobias", createdUserName);

            var createdStreetName = _wait.Until(driver => driver.FindElement(By.Name("rua"))).GetAttribute("value");
            Assert.Equal("Nove de Julho", createdStreetName);

            // Verificar se estamos na página inicial
            _wait.Until(driver => driver.FindElement(By.CssSelector("nav")));
        }

        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
*/