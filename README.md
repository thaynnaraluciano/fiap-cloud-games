# FIAP Cloud Games
### Uma plataforma de venda de jogos digitais e gestão de servidores para partidas online.

#### Projeto desenvolvido para o Tech Challenge FIAP, da turma 9NETT. Realizado pelo grupo 14, composto pelos alunos: 
 - Antonio Renato Pastoriza Ferreira De Souza
 - Bruno Luiz de Souza
 - Patrick Mondaini das Neves
 - Thaynnara Luciano dos Santos e
 - Vitor de Oliveira Rennó Ribeiro

## Instruções 
Para executar a aplicação em ambiente local, siga os passos a seguir: 
* Faça o clone do repositório para um diretório da sua máquina
    ```
    git clone https://github.com/thaynnaraluciano/fiap-cloud-games
    ```
* Acesse o diretório *fiap-cloud-games*
    ```
    cd fiap-cloud-games
    ```
* Abra a solution *FiapCloudGames.sln* (disponível em: *fiap-cloud-games*) com o Visual Studio e preencha as credenciais do servidor SMTP no arquivo *appsettings.json*, localizado na camada de *Presentation*, no projeto *Api.csproj*. Para ambiente de desenvolvimento, sugiro a utilização da ferramenta [mailTrap](https://mailtrap.io/). O mailTrap é um serviço para teste de envio de e-mails e a próxima seção contém as instruções para sua utilização.

### Configurando o MailTrap
* Acesse o [MailTrap](https://mailtrap.io/)
* Crie uma conta na ferramenta e faça login
* Na tela inicial da ferramenta clique em "Start testing" na seção "Sandbox EmailTesting"
* Serão exibidas na tela as credenciais do servidor SFTP. Estas são as informações que devem ser informadas no arquivo *appsettings.json*
* Nesta mesma tela, serão exibidos os emails que forem enviados durante os testes da aplicação.

## Escolhas de desenvolvimento

A aplicação desenvolvida é um monorepo, para facilitar a execução em ambiente local e por se tratar de um MVP. 
A aplicação foi feita em camadas, sendo elas: Presentation, Domain, Infrastructure, CrossCutting e Tests. Todos os projetos foram desevolvidos seguindo as boas práticas de programação e foram baseadas no DDD.

Os testes criados nas APIs foram unitários, utilizando as bibliotecas xUnit e Bogus. Os testes foram focados em garantir o funcionamento das validações e o tratamento de exceções.

Para logs foi utilizada a interface ILogger da biblioteca Microsoft.Extensions.Logging. Utilizando-a é possível gerar logs de information, warning, error, entre outros e os logs podem ser consultados no ambiente onde for realizado o deploy.

O tratamento de exceções é realizado pela captura de exceções e tratamento conforme statusCodes do Http via exceptions personalizadas. A captura e retorno padronizado das exceções foi configurado via Middleware.

Por questões de segurança da aplicação, o usuário deve criar uma senha forte durante o cadastro. A definição de senha forte considerada foi ter no mínimo 8 dígitos, contendo caracteres maiúsculos, minúsculos, números e caracteres especiais.