using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace Correios.Automacao.Tests.Steps
{
    [Binding]
    public class BuscaCepSteps
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        private const string UrlBuscaCep = "https://buscacepinter.correios.com.br/app/cep/index.php";
        private const string UrlRastreamento = "https://rastreamento.correios.com.br/app/index.php";

        // ✅ CHECK por ID (vamos ajustar o ID real já já)
        private readonly By cepInputById = By.Id("endereco");

        // ✅ CHECK por XPATH
        private readonly By botaoBuscarByXpath = By.XPath("//*[@id='btn_pesquisar']");

        // ✅ CHECK por CSS (capturar a presença do CAPTCHA)
        private readonly By captchaByCss = By.CssSelector("img, input[name*='captcha'], input[id*='captcha']");

        private bool captchaDetected;

        [BeforeScenario]
        public void BeforeScenario()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");

            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(12));
            captchaDetected = false;
        }

        [AfterScenario]
        public void AfterScenario()
        {
            driver.Quit();
        }

        [Given(@"que estou na página de Busca CEP dos Correios")]
        public void GivenQueEstouNaPaginaDeBuscaCepDosCorreios()
        {
            driver.Navigate().GoToUrl(UrlBuscaCep);
        }

        [Given(@"que estou na página de Rastreamento dos Correios")]
        public void GivenQueEstouNaPaginaDeRastreamentoDosCorreios()
        {
            driver.Navigate().GoToUrl(UrlRastreamento);
        }

       [When(@"eu informo o CEP ""(.*)""")]
        public void WhenEuInformoOCep(string cep)
        {
            var inputCep = wait.Until(d =>
            {
                var el = d.FindElement(By.Id("cep"));
                return el.Displayed && el.Enabled ? el : null;
            });

            inputCep.Clear();

            foreach (var c in cep)
    {
        inputCep.SendKeys(c.ToString());
        System.Threading.Thread.Sleep(150); // simula digitação humana
    }

    inputCep.Clear();
    inputCep.SendKeys(cep);
}


        [When(@"eu informo o código de rastreio ""(.*)""")]
        public void WhenEuInformoOCodigoDeRastreio(string codigo)
        {
            var inputRastreio = wait.Until(d => d.FindElement(By.CssSelector("input, textarea")));
            inputRastreio.Clear();
            inputRastreio.SendKeys(codigo);
        }

        [When(@"eu tento buscar o CEP")]
        public void WhenEuTentoBuscarOCep()
        {
            wait.Until(d => d.FindElement(botaoBuscarByXpath)).Click();
            captchaDetected = ElementExists(captchaByCss);
}

        [When(@"eu tento buscar o rastreio")]
        public void WhenEuTentoBuscarORastreio()
        {
            wait.Until(d => d.FindElement(botaoBuscarByXpath)).Click();
            captchaDetected = ElementExists(captchaByCss);
        }

        [Then(@"devo identificar que existe CAPTCHA bloqueando a automação e registrar o impedimento")]
        public void ThenDevoIdentificarQueExisteCaptchaBloqueandoAAutomacaoERegistrarOImpedimento()
        {
            if (!captchaDetected)
                Assert.Fail("Não foi possível identificar o CAPTCHA na tela. Validar seletor CSS do CAPTCHA.");

            Assert.Inconclusive("Impedimento: o fluxo exige CAPTCHA obrigatório. A automação cobre navegação, preenchimento e tentativa de busca, mas não contorna mecanismos de segurança.");
        }

        private bool ElementExists(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
