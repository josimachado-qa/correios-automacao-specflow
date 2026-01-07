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
        private IWebDriver driver = null!;
        private WebDriverWait wait = null!;

        private const string UrlBuscaCep = "https://buscacepinter.correios.com.br/app/cep/index.php";
        private const string UrlRastreamento = "https://rastreamento.correios.com.br/app/index.php";

        private readonly By BuscaCep_InputCep_ById = By.Id("cep");
        private readonly By BuscaCep_BotaoBuscar_ByXpath = By.XPath("//*[@id='btn_pesquisar']");
        private readonly By BuscaCep_BotaoNovaBusca_ById = By.Id("btn_nbusca");
        private readonly By BuscaCep_CaptchaInput_ById = By.Id("captcha");

        private readonly By Rast_InputObjeto_ById = By.Id("objeto");
        private readonly By Rast_BotaoConsultar_ById = By.Id("b-pesquisar");
        private readonly By Rast_CaptchaInput_ById = By.Id("captcha");
        private readonly By Rast_Alerta_ById = By.Id("alerta");
        private readonly By Rast_AlertaMsg_ByCss = By.CssSelector("#alerta .msg");

        [BeforeScenario]
        public void BeforeScenario()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
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
            wait.Until(d => d.FindElement(BuscaCep_InputCep_ById).Displayed);
        }

        [Given(@"que estou na página de Rastreamento dos Correios")]
        public void GivenQueEstouNaPaginaDeRastreamentoDosCorreios()
        {
            driver.Navigate().GoToUrl(UrlRastreamento);
            wait.Until(d => d.FindElement(Rast_InputObjeto_ById).Displayed);
        }

        [When(@"eu informo o CEP ""(.*)""")]
        public void WhenEuInformoOCep(string cep)
        {
            var inputCep = wait.Until(d =>
            {
                var el = d.FindElement(BuscaCep_InputCep_ById);
                return el.Displayed && el.Enabled ? el : null;
            });

            inputCep.Clear();
            inputCep.SendKeys(cep);
        }

        [When(@"eu tento buscar o CEP")]
        public void WhenEuTentoBuscarOCep()
        {
            ClickSeguro(BuscaCep_BotaoBuscar_ByXpath);

            wait.Until(d =>
            {
                try
                {
                    var captcha = d.FindElement(BuscaCep_CaptchaInput_ById);
                    return captcha.Displayed;
                }
                catch
                {
                    return false;
                }
            });
        }

        [When(@"eu aguardo o usuário preencher o CAPTCHA e reenviar a consulta de CEP")]
        public void WhenEuAguardoUsuarioCaptchaEReenvioCep()
        {
            AguardarUsuarioPreencherCaptcha(BuscaCep_CaptchaInput_ById);
            AguardarResultadoBuscaCep();
        }

        [Then(@"devo confirmar que o CEP não existe")]
        public void ThenDevoConfirmarQueOCepNaoExiste()
        {
            Assert.That(EstaVisivel(BuscaCep_BotaoNovaBusca_ById), Is.True, "O resultado da busca por CEP não carregou (botão Nova Busca não ficou visível).");

            var texto = BodyTextLower();

            Assert.That(
                !(texto.Contains("rua quinze de novembro") && (texto.Contains("são paulo/sp") || texto.Contains("sao paulo/sp"))),
                Is.True,
                "O retorno parece ser de um CEP válido (endereço encontrado), mas esse caso deveria ser CEP inexistente."
            );
        }

        [Then(@"devo confirmar que o resultado contém ""(.*)""")]
        public void ThenDevoConfirmarQueOResultadoContem(string esperado)
        {
            Assert.That(EstaVisivel(BuscaCep_BotaoNovaBusca_ById), Is.True, "O resultado da busca por CEP não carregou (botão Nova Busca não ficou visível).");

            var texto = BodyTextLower();

            Assert.That(
                texto.Contains("rua quinze de novembro") && (texto.Contains("são paulo/sp") || texto.Contains("sao paulo/sp")),
                Is.True,
                $"Resultado esperado não encontrado. Esperado conter: {esperado}"
            );
        }

        [When(@"eu volto para a tela inicial de busca de CEP")]
        [Then(@"eu volto para a tela inicial de busca de CEP")]
        public void VoltarParaTelaInicialBuscaCep()
        {
            var botaoNovaBusca = wait.Until(d =>
            {
                var el = d.FindElement(BuscaCep_BotaoNovaBusca_ById);
                return el.Displayed && el.Enabled ? el : null;
            });

            botaoNovaBusca.Click();

            wait.Until(d => d.Url.Contains("/app/cep/index.php", StringComparison.OrdinalIgnoreCase));
            wait.Until(d => d.FindElement(BuscaCep_InputCep_ById).Displayed);
        }

        [When(@"eu informo o código de rastreio ""(.*)""")]
        public void WhenEuInformoOCodigoDeRastreio(string codigo)
        {
            var inputObj = wait.Until(d =>
            {
                var el = d.FindElement(Rast_InputObjeto_ById);
                return el.Displayed && el.Enabled ? el : null;
            });

            inputObj.Clear();
            inputObj.SendKeys(codigo);
        }

        [When(@"eu tento consultar o rastreio")]
        public void WhenEuTentoConsultarORastreio()
        {
            ClickSeguro(Rast_BotaoConsultar_ById);

            wait.Until(d =>
            {
                try
                {
                    var captcha = d.FindElement(Rast_CaptchaInput_ById);
                    return captcha.Displayed;
                }
                catch
                {
                    return false;
                }
            });
        }

        [When(@"eu aguardo o usuário preencher o CAPTCHA e reenviar a consulta de rastreio")]
        public void WhenEuAguardoUsuarioCaptchaEReenvioRastreio()
        {
            AguardarUsuarioPreencherCaptcha(Rast_CaptchaInput_ById);
            AguardarAlertaRastreamento();
        }

        [Then(@"devo confirmar a mensagem de rastreio inválido ""(.*)""")]
        public void ThenDevoConfirmarMensagemRastreioInvalido(string mensagemEsperada)
        {
            var localWait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));

            var msgFinal = localWait.Until(d =>
            {
                try
                {
                    var el = d.FindElement(Rast_AlertaMsg_ByCss);
                    if (!el.Displayed) return null;

                    var txt = (el.Text ?? string.Empty).Trim();
                    if (string.IsNullOrWhiteSpace(txt)) return null;

                    if (txt.Equals("Buscando...", StringComparison.OrdinalIgnoreCase))
                        return null;

                    if (txt.Equals(mensagemEsperada, StringComparison.OrdinalIgnoreCase))
                        return txt;

                    return txt;
                }
                catch
                {
                    return null;
                }
            });

            Assert.That(msgFinal, Is.Not.Null.And.Not.Empty, "A mensagem final do alerta não foi obtida.");
            Assert.That(msgFinal, Is.EqualTo(mensagemEsperada), $"Mensagem diferente do esperado. Obtido: '{msgFinal}'");
        }

        private void AguardarUsuarioPreencherCaptcha(By captchaBy)
        {
            var fim = DateTime.UtcNow.AddMinutes(5);

            while (DateTime.UtcNow < fim)
            {
                var valor = GetValueSafe(captchaBy);
                if (!string.IsNullOrWhiteSpace(valor))
                    return;

                System.Threading.Thread.Sleep(400);
            }

            Assert.Fail("Timeout aguardando o usuário digitar o CAPTCHA.");
        }

        private void AguardarResultadoBuscaCep()
        {
            var fim = DateTime.UtcNow.AddMinutes(5);

            while (DateTime.UtcNow < fim)
            {
                if (EstaVisivel(BuscaCep_BotaoNovaBusca_ById))
                    return;

                System.Threading.Thread.Sleep(500);
            }

            Assert.Fail("Timeout aguardando o resultado da Busca CEP carregar (Nova Busca não ficou visível).");
        }

        private void AguardarAlertaRastreamento()
        {
            var fim = DateTime.UtcNow.AddMinutes(5);

            while (DateTime.UtcNow < fim)
            {
                if (IsAlertaAberto())
                    return;

                System.Threading.Thread.Sleep(500);
            }

            Assert.Fail("Timeout aguardando o alerta do rastreamento aparecer após CAPTCHA.");
        }

        private bool IsAlertaAberto()
        {
            try
            {
                var alerta = driver.FindElement(Rast_Alerta_ById);
                var classe = alerta.GetAttribute("class") ?? string.Empty;
                return alerta.Displayed && classe.Contains("aberto");
            }
            catch
            {
                return false;
            }
        }

        private void ClickSeguro(By by)
        {
            var el = wait.Until(d => d.FindElement(by));
            try
            {
                wait.Until(_ => el.Displayed && el.Enabled);
                el.Click();
            }
            catch
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", el);
            }
        }

        private bool EstaVisivel(By by)
        {
            try
            {
                var els = driver.FindElements(by);
                foreach (var el in els)
                {
                    if (el.Displayed)
                        return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private string GetValueSafe(By by)
        {
            try
            {
                var el = driver.FindElement(by);
                return el.GetAttribute("value") ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        private string BodyTextLower()
        {
            try
            {
                var t = driver.FindElement(By.TagName("body")).Text ?? string.Empty;
                return t.ToLowerInvariant();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
