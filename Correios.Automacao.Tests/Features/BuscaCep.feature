Feature: Avaliação – Busca CEP e Rastreamento Correios (CAPTCHA manual)

Scenario: Validar fluxo de busca CEP e rastreamento com CAPTCHA manual
  Given que estou na página de Busca CEP dos Correios
  When eu informo o CEP "80700000"
  And eu tento buscar o CEP
  And eu aguardo o usuário preencher o CAPTCHA e reenviar a consulta de CEP
  Then devo confirmar que o CEP não existe
  And eu volto para a tela inicial de busca de CEP

  Given que estou na página de Busca CEP dos Correios
  When eu informo o CEP "01013-001"
  And eu tento buscar o CEP
  And eu aguardo o usuário preencher o CAPTCHA e reenviar a consulta de CEP
  Then devo confirmar que o resultado contém "Rua Quinze de Novembro, São Paulo/SP"
  And eu volto para a tela inicial de busca de CEP

  Given que estou na página de Rastreamento dos Correios
  When eu informo o código de rastreio "SS987654321BR"
  And eu tento consultar o rastreio
  And eu aguardo o usuário preencher o CAPTCHA e reenviar a consulta de rastreio
  Then devo confirmar a mensagem de rastreio inválido "Objeto não encontrado na base de dados dos Correios."
