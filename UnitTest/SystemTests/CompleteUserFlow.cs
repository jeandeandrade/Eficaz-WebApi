using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;
using Infrastructure.Repositories.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTest.SystemTests
{
    public class CompleteUserFlowTests
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _wait;

        public CompleteUserFlowTests()
        {
            _driver = new ChromeDriver();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20)); // Aumentando o tempo de espera para 20 segundos
        }

        [Fact]
        public async Task CompleteUserFlow_Test()
        {
            try
            {
                // 1. Registro de Novo Usuário
                _driver.Navigate().GoToUrl("http://localhost:5173/registration");

                await Task.Delay(2000);

                _wait.Until(driver => driver.FindElement(By.Id("nomeCompleto"))).SendKeys("Olaf");
                _driver.FindElement(By.Id("apelido")).SendKeys("OlafUser");
                _driver.FindElement(By.Id("cpf")).SendKeys("12345678909");
                _driver.FindElement(By.Id("data")).SendKeys("1990-01-01");
                _driver.FindElement(By.Id("genero")).SendKeys("Masculino");
                _driver.FindElement(By.Id("telefone")).SendKeys("123456789");
                _driver.FindElement(By.Id("email")).SendKeys("olaf.user@example.com");
                _driver.FindElement(By.Id("senha")).SendKeys("issenha");

                // Clique no botão "Próximo Passo"
                await Task.Delay(2000);
                var nextStepButton = _wait.Until(driver => driver.FindElement(By.XPath("//button[contains(., 'Próximo Passo')]")));
                nextStepButton.Click();

                // 2. Registro de Endereço
                _wait.Until(driver => driver.FindElement(By.Name("rua"))).SendKeys("Nove de Julho");
                _driver.FindElement(By.Name("bairro")).SendKeys("Altaneira");
                _driver.FindElement(By.Name("cep")).SendKeys("00000-000");
                _driver.FindElement(By.Name("complemento")).SendKeys("Atrás do Tauste");
                _driver.FindElement(By.Name("cidade")).SendKeys("Ubatuba");
                _driver.FindElement(By.Name("numeroResidencia")).SendKeys("123");

                await Task.Delay(2000);
                var saveButton = _wait.Until(driver => driver.FindElement(By.XPath("//div[@class='text-right mt-14']//button[contains(., 'Salvar')]")));
                saveButton.Click();

                // 3. Login do Usuário
                _driver.Navigate().GoToUrl("http://localhost:5173/login");

                _wait.Until(driver => driver.FindElement(By.CssSelector("input[type='email']"))).SendKeys("olaf.user@example.com");
                _driver.FindElement(By.CssSelector("input[type='password']")).SendKeys("issenha");
                _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

                // Verificar se está na página inicial após login
                _wait.Until(driver => driver.FindElement(By.CssSelector("nav")));

                // 4. Acesso à página de perfil
                _driver.Navigate().GoToUrl("http://localhost:5173/profile");
                await Task.Delay(2000);

                // Verificar se os botões "Voltar à Página Inicial" e "Editar Perfil" estão visíveis
                var backToHomeButton = _wait.Until(driver => driver.FindElement(By.CssSelector("button.button")));
                Assert.NotNull(backToHomeButton);
                var editProfileButton = _wait.Until(driver => driver.FindElement(By.CssSelector("button.button:nth-child(2)")));
                Assert.NotNull(editProfileButton);

                // 5. Testar "Voltar à Página Inicial"
                backToHomeButton.Click();
                await Task.Delay(2000); // Aguardar navegação
                Assert.Contains("home", _driver.Url.ToLower()); // Certifique-se de que a URL seja a página inicial

                // 6. Testar "Editar Perfil" e garantir que o modal de edição seja exibido
                _driver.Navigate().GoToUrl("http://localhost:5173/profile");
                await Task.Delay(2000);
                editProfileButton.Click();

                // Verificar se o modal de edição de perfil apareceu
                var modal = _wait.Until(driver => driver.FindElement(By.CssSelector(".modal-content")));
                Assert.NotNull(modal);

                // 7. Preencher o formulário de edição e salvar
                var phoneField = _driver.FindElement(By.Name("telefone"));
                phoneField.Clear();
                phoneField.SendKeys("987654321");

                var saveButtonModal = _driver.FindElement(By.CssSelector(".button.bg-blue-500"));
                saveButtonModal.Click();

                // 8. Verificação no Banco de Dados
                await Task.Delay(2000);
                Console.WriteLine("Verificando o banco de dados");

                using (var context = new EficazDbContext(new DbContextOptionsBuilder<EficazDbContext>()
                .UseMySql(Environment.GetEnvironmentVariable("DEFAULT_CONNECTION"), ServerVersion.AutoDetect(Environment.GetEnvironmentVariable("DEFAULT_CONNECTION")))
                .Options))
                {
                    var user = context.Users.FirstOrDefault(u => u.Email == "olaf.user@example.com");
                    Assert.NotNull(user);
                    Assert.Equal("987654321", user.Telefone);
                }

                _wait.Until(driver => driver.FindElement(By.CssSelector("nav")));

                // 9. Logout do Usuário
                var logoutButton = _wait.Until(driver => driver.FindElement(By.XPath("//button[.//span[contains(text(), 'LOG OUT')]]")));
                logoutButton.Click();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro durante o teste: {ex.Message}");
                throw;
            }
            finally
            {
                _driver.Quit();
            }
        }
    }
}
