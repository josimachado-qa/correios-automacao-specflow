# Automação Correios – Busca CEP e Rastreamento

## 📌 Objetivo
Este projeto tem como objetivo demonstrar a criação de uma automação de testes E2E utilizando C#, SpecFlow, NUnit e Selenium WebDriver, aplicada aos fluxos de Busca de CEP e Rastreamento de objetos no site dos Correios.

A automação foi desenvolvida até o limite permitido pela aplicação, respeitando os mecanismos de segurança implementados.

---

## 🧪 Cenários Automatizados
- Acesso à página de Busca de CEP dos Correios
- Preenchimento do campo de CEP com valores válidos e inválidos
- Tentativa de execução da busca
- Acesso à página de Rastreamento
- Preenchimento do código de rastreio
- Tentativa de execução do rastreamento

Os cenários estão descritos em BDD (Gherkin) no arquivo BuscaCep.feature.

---

## 🚫 Impedimento Técnico – CAPTCHA
Durante a execução dos fluxos, foi identificado que tanto a Busca de CEP quanto o Rastreamento exigem a resolução de um CAPTCHA obrigatório para prosseguir.

Por se tratar de um mecanismo de segurança:
- A automação não tenta burlar ou contornar o CAPTCHA
- O teste valida a navegação, o preenchimento dos campos e a tentativa de busca
- O cenário registra formalmente o impedimento técnico, encerrando o fluxo de forma controlada

Esse comportamento está documentado nos cenários como uma limitação intencional, seguindo boas práticas de automação e ética profissional.

---

## 🛠️ Tecnologias Utilizadas
- C#
- .NET 8
- SpecFlow
- NUnit
- Selenium WebDriver
- Git / GitHub

---

## ▶️ Como Executar o Projeto
1. Clonar o repositório:
   git clone https://github.com/josimachado-qa/correios-automacao-specflow.git

2. Abrir a solução no Visual Studio:
   Correios.Automacao.sln

3. Restaurar os pacotes NuGet

4. Executar os testes pelo Test Explorer ou via terminal:
   dotnet test

---

## 📎 Observações Finais
Este projeto tem caráter técnico e demonstrativo, com foco em estrutura, organização, escrita de cenários BDD e tratamento correto de impedimentos reais encontrados em aplicações com mecanismos de segurança.
