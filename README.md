# Automação Correios – Busca CEP e Rastreamento

## 📌 Objetivo
Este projeto tem como objetivo demonstrar a criação de uma automação de testes E2E utilizando C#, SpecFlow, NUnit e Selenium WebDriver, aplicada aos fluxos de **Busca de CEP** e **Rastreamento de objetos** no site dos Correios.

A automação foi desenvolvida até o limite permitido pela aplicação, respeitando os mecanismos de segurança implementados.

## 🎥 Vídeo de Demonstração

A execução real da automação (incluindo validação manual de CAPTCHA e finalização automática dos fluxos) pode ser visualizada no vídeo abaixo:

▶️ **Assistir / baixar o vídeo:**  

https://github.com/user-attachments/assets/a5a837c2-b66c-46f8-8383-38b2b3a62a92

---

## 🧪 Cenários Automatizados
- Acesso à página de Busca de CEP dos Correios  
- Preenchimento do campo de CEP com valores válidos e inválidos  
- Tentativa de execução da busca  
- Acesso à página de Rastreamento  
- Preenchimento do código de rastreio  
- Tentativa de execução do rastreamento  

Os cenários estão descritos em BDD (Gherkin) no arquivo `BuscaCep.feature`.

---

## 🚫 Impedimento Técnico – CAPTCHA
Durante a execução dos fluxos, foi identificado que tanto a **Busca de CEP** quanto o **Rastreamento** exigem a resolução de um **CAPTCHA obrigatório** para prosseguir.

Por se tratar de um mecanismo de segurança:
- A automação não tenta burlar ou contornar o CAPTCHA  
- O teste valida a navegação, o preenchimento dos campos e a tentativa de consulta  
- O cenário aguarda o usuário resolver o CAPTCHA manualmente  
- O fluxo continua automaticamente somente após o CAPTCHA ser validado corretamente  

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

### 1. Clonar o repositório
```
git clone https://github.com/josimachado-qa/correios-automacao-specflow.git
```

### 2. Abrir a solução
Abra o arquivo `Correios.Automacao.sln` no Visual Studio.

### 3. Restaurar os pacotes
Restaure os pacotes NuGet pelo Visual Studio ou terminal.

### 4. Executar os testes
```
dotnet test
```

---

## 🧭 Durante a execução
- O navegador será aberto automaticamente  
- O usuário deverá preencher o CAPTCHA quando solicitado  
- Caso o CAPTCHA seja digitado incorretamente, o teste continuará aguardando  
- O teste só prossegue quando o CAPTCHA for resolvido corretamente  

---

## 📎 Observações Finais
Este projeto foi desenvolvido com foco em **qualidade, realismo e boas práticas de automação**, refletindo os desafios reais encontrados em aplicações que utilizam mecanismos de segurança como CAPTCHA.

O objetivo não é burlar o sistema, mas demonstrar **capacidade técnica, estrutura de testes, automação de fluxos reais e tomada de decisão madura em QA**.
