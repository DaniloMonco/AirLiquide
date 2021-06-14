Nesse exemplo simples, fiz minha visão do que seria o nível de maturidade Rest (Level 2) conhecido como glory of Rest.
Obviamente, forcei situações já que não tinha muita regra para ser implementada e decidi lançar os erros de validação (FluentValidation)
via exceptions customizadas. Utilizei o MediatR para organizar e deixar a controller o mais "limpa" possível e apesar de "verboso" criando
classes "request" e "response" para cada "ação", prefiro desta forma para ter a separação entre
modelo x assinatura dos endpoints da API.
Fiz um middleware simples para tratar esses exceptions e devolver os httpstatus e não perder nenhum evento de logs (utilizando o Serilog).
Com relação ao ORM, prefiro utilizar o Dapper. Embora o linq tenha alguma vantagem para quem não conhece SQL, eu prefiro fazer meu
modelo e traduzir da melhor forma para o banco de dados (algo que nesse exemplo, também não faz sentido pois não existe complexidade)
HealthCheck para verificar se o SQLServer esta funcional, migrations estão utilizando o DbUp.
Por fim, alguns testes unitários.