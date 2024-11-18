# PGApi

## Resumo

O **PGApi** é uma API desenvolvida em C# utilizando ASP.NET Core, projetada para ser uma solução prática e escalável para gerenciar operações em um laboratório. Esta aplicação foi criada como um ambiente de aprendizado para explorar e testar conceitos de desenvolvimento de software, incluindo a implementação do padrão Saga para gerenciamento de transações em sistemas distribuídos.

## Objetivo

O objetivo principal da **PGApi** é fornecer uma interface para gerenciar dados de laboratório, incluindo operações como criação de pedidos, gerenciamento de produtos e acompanhamento de resultados de testes. A API foi estruturada em uma arquitetura em camadas, separando as responsabilidades entre as diferentes partes do sistema, promovendo uma manutenção mais fácil e escalabilidade.

### Funcionalidades

- Gerenciamento de pedidos de laboratório.
- Registro e atualização de produtos.
- Implementação do padrão Saga para coordenar transações entre diferentes serviços.
- Suporte a testes unitários e de integração.

## Estrutura do Projeto

A estrutura do projeto é organizada da seguinte forma, seguindo os princípios de **Domain-Driven Design (DDD)** e **SOLID**:

- **PGApi.Api**: 
  - Contém as **Controllers**, que são responsáveis por receber e processar as requisições HTTP, delegando a lógica para as camadas inferiores.
  - **Middlewares** são utilizados para adicionar funcionalidades ao pipeline de processamento de requisições, como autenticação e tratamento de erros.
  - **Configurations** inclui a configuração do aplicativo, como serviços e dependências.

- **PGApi.Application**: 
  - Inclui os **Services**, que orquestram a lógica da aplicação, realizando operações de negócio e gerenciando a comunicação entre a camada de domínio e a infraestrutura.
  - **Interfaces** definem contratos que os serviços e repositórios devem implementar, promovendo a inversão de dependências.
  - **DTOs** (Data Transfer Objects) são utilizados para transportar dados entre as camadas, garantindo que a estrutura interna das entidades não seja exposta.
  - **Commands** e **Handlers** implementam o padrão CQRS (Command Query Responsibility Segregation), onde comandos alteram o estado do sistema e handlers processam esses comandos.

- **PGApi.Domain**: 
  - Contém as **Entities**, que representam as regras de negócio e o estado do sistema.
  - **ValueObjects** são utilizados para encapsular valores sem identidade própria, promovendo um modelo de domínio mais rico.
  - **Enums** definem conjuntos de constantes nomeadas, utilizados em diversas partes do domínio.
  - **Interfaces** que definem os repositórios e serviços que serão utilizados pela camada de aplicação.

- **PGApi.Infrastructure**: 
  - A seção **Data** lida com o acesso a dados e a configuração do banco de dados.
  - **Repositories** implementam a lógica de acesso a dados, permitindo que a camada de domínio permaneça desacoplada da infraestrutura.
  - **Messaging** inclui implementações para comunicação assíncrona, como filas e eventos.
  - **Migrations** contém as migrações do banco de dados, facilitando a evolução do esquema ao longo do tempo.

- **PGApi.Shared (Opcional)**:
  - **Utils** contém utilitários e funções auxiliares que podem ser utilizadas em diferentes partes do projeto.
  - **Extensions** inclui métodos de extensão que aumentam a funcionalidade de classes existentes, promovendo um código mais limpo e reutilizável.

## Fluxo do Sistema

O fluxo do sistema ocorre da seguinte forma:

1. **Requisição do Cliente**:
   - O cliente faz uma requisição HTTP para a API (por exemplo, para criar um pedido ou obter uma lista de pedidos).
   
2. **Controller**:
   - A requisição chega até o **Controller** correspondente na camada **Api**. O controller é responsável por receber a requisição e validar os dados recebidos.
   - Exemplo: O método `CreateOrder` recebe os dados do pedido, que são mapeados para um comando.

3. **Mediator**:
   - Após a validação, o controller usa o **MediatR** para enviar o comando (no caso, um **CreateOrderCommand**) ou a consulta (no caso de uma **GetAllOrdersQuery**) para o **Handler** correspondente.
   - O **MediatR** é responsável por fazer a comunicação entre o controller e o handler, delegando a execução da lógica de negócio.

4. **Handler**:
   - O **Handler** processa o comando ou consulta. Ele contém a lógica da aplicação, como validações, cálculos e manipulação de dados.
   - Exemplo: O **CreateOrderHandler** cria um pedido com os dados do DTO, valida se a quantidade é válida e persiste a informação no banco de dados.

5. **Repositório**:
   - O handler comunica-se com a camada de **Infraestrutura**, que contém a implementação do **Repositório**. O repositório é responsável por interagir com o banco de dados (via **Entity Framework** ou outro ORM) e realizar as operações de CRUD.
   - Exemplo: O repositório **SqlServerOrderRepository** é chamado para adicionar um novo pedido ao banco de dados.

6. **Resposta**:
   - Após a execução do comando ou consulta, o **Handler** retorna um **Result**. O **Result** pode ser um sucesso ou uma falha, contendo os dados ou mensagens de erro.
   - O controller recebe essa resposta e, dependendo do resultado, retorna um código HTTP correspondente (200 OK, 400 Bad Request, etc.).

7. **Cliente Recebe a Resposta**:
   - O cliente recebe a resposta da API, com os dados solicitados ou o status da operação (sucesso ou erro).

## Testes

Os testes para a aplicação são desenvolvidos para garantir que a lógica do sistema funcione conforme o esperado. São utilizados testes unitários e de integração para verificar a funcionalidade de cada componente da API.

### Como Executar os Testes

1. Certifique-se de ter o [.NET SDK](https://dotnet.microsoft.com/download) instalado.
2. Navegue até a pasta do projeto no terminal.
3. Execute o comando:

   ```bash
   dotnet test
