Feature: Avaliação - Busca CEP Correios (com impedimento por CAPTCHA)

Scenario: Validar fluxo de busca CEP e rastreamento até o limite permitido (CAPTCHA)
Given que estou na página de Busca CEP dos Correios
When eu informo o CEP "80700000"
And eu tento buscar o CEP
Then devo identificar que existe CAPTCHA bloqueando a automação e registrar o impedimento

When eu informo o CEP "01013-001"
And eu tento buscar o CEP
Then devo identificar que existe CAPTCHA bloqueando a automação e registrar o impedimento

Given que estou na página de Rastreamento dos Correios
When eu informo o código de rastreio "SS987654321BR"
And eu tento buscar o rastreio
Then devo identificar que existe CAPTCHA bloqueando a automação e registrar o impedimento