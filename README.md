# Cashback API v1
Projeto _back-end_ de exemplo de venda de albums com cálculo de _cashback_.

Para ver a aplicação funcionando [clique aqui](https://cashback-api.azurewebsites.net)

## Código-fonte
Para ter acesso ao código-fonte do projeto, clone o repositório

    git clone https://github.com/chpsousa/cashback.git
    
ou [clique aqui](https://github.com/chpsousa/cashback/archive/master.zip) para baixar o código-fonte em um arquivo .zip.

## Tecnologias e ferramentas utilizadas
- .NET Core 2.2
- .NET Standard 2.0
- Entity Framework Core 2.2.6
- Visual Studio 2019
- EFCore InMemory
- XUnit (units tests)
- Swagger
- Azure Web App

## Estrutura do projeto
A _solution_ Cashback.sln é composta por três projetos:

- Cashback.API: 
Projeto com uma API Rest para execução das operações
- Cashback.Domain: 
Projeto com todo o domínio da aplicação, criado sobre a arquitetura CQRS.
- Cashback.Tests: 
Testes unitários do domínio e dos comandos da aplicação

## Execução
Para executar a API localmente:
- Clone o repositório;
- Acesse o diretório do projeto Cashback.API, utilizando seu prompt de comando.
- Execute o comando `dotnet run`

ou 
- Abra a Solution Cashback.sln
- Selecione o projeto Cashback.API
- Pressione a tecla F5.

O projeto utiliza banco de dados em memória, facilitando a execução na máquina local.

## Cashback.API
A API foi criada no padrão RESTful e conta com as seguintes _controllers_ e _endpoints_

ValuesController
- [GET] (api_url/values) - Uma controller simples com um health check do serviço

AlbumsController
- [GET] (api_url/api/albums) - Retorna uma lista dos albums.
- [GET] (api_url/api/albums/id) - Retorna o album do id mencionado.

GenresController
- [GET] (api_url/api/genres) - Retorna uma lista dos gêneros musicais.
- [GET] (api_url/api/genres/id) - Retorna o gênero musical do id mencionado.

SalesController
- [GET] (api_url/api/sales) - Retorna uma lista das vendas.
- [GET] (api_url/api/sales/id) - Retorna a venda do id mencionado.
- [POST] (api_url/api/sales) - Registra uma venda. Para registrar uma venda, utilize o corpo da requisição conforme modelo abaixo:

`{
  "id": "string",
  "customerName": "string",
  "items": [
    {
      "albumId": "string"
    }
  ]
}`

Uma documentação com maiores detalhes sobre a API pode ser acessada [localmente](https://localhost:44362/) ou no [live-demo](https://cashback-api.azurewebsites.net).

## Cashback.Domain
Projeto .NET Standard com toda a regra da aplicação.
Está implementada com modelo CQRS e utiliza uma integração com o Spotify para popular os dados.

## Cashback.Tests
O projeto conta com testes de unidade automatizados para o domínio e para os comandos.
Para executar os testes:

- Clone o repositório
- Acesse o diretório do projeto de testes, Cashback.Tests que está dentro de src/Cashback
- Execute o comando `dotnet test` com seu prompt de comando.

ou

- Abra a Solution Cashback.sln com o Visual Studio.
- Clique com o botão direito sobre o projeto Cashback.Tests
- Clique sobre a opção Run tests.

## Autor
Carlos Henrique Prado Sousa - [Linkedin](https://www.linkedin.com/in/chpsousa/)




