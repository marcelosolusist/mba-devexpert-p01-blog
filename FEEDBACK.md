# Feedback do Instrutor

#### 15/10/24 - Revisão Inicial - Eduardo Pires

## Pontos Positivos:

- Boa separação de responsabilidades
- Demonstrou conhecimento em Identity e JWT.
- Mapeou bem entidades no EF e suas ViewModels com AutoMapper
- Mostrou entendimento do ecossistema de desenvolvimento em .NET
- Usou bem as extension methods para configs das aplicações e DI
- Uso correto do SQLite para evitar setup de infra
- Usou conceitos de DDD sem criar complexidade demais
- Os serviços de domínio estão adequados ao propósito
- Tratamento de erros de negócio adequados
- Documentou bem o repositório.

## Pontos Negativos:

- O Identity e Autor deveriam ser a mesma coisa, porém deve existir a entidade Autor e estar ligada a um registro do Identity.
- Não achei necessário criar um login depois um autor, isto deveria estar tudo dentro do mesmo processo.
- A navegação está precária, um anonimo poderia ler um post em seus detalhes. Apesar do layout não ser um ponto forte é importante ter uma usabilidade.

## Sugestões:

- Unificar a criação do user + autor no mesmo processo. Utilize o ID do registro do Identity como o ID da PK do Autor, assim você mantém um link lógico entre os elementos.
