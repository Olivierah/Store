# App Store
------------------------------------------------
### Framework:

_Versão do Framework necessária para rodar o projeto:_

- [.NET 6 (DotNet6)] 
    
    
------------------------------------------------ 
### NuGet - Solution

_Pacotes e versões necessárias para executar o projeto_


| Pacote | Versão | Autor |
| :--- | ------ | :---: |
| Microsoft.AspNetCore.Authentication.JwtBearer | **6.0.8** | Microsoft |
| Microsoft.EntityFrameworkCore.SqlServer | **6.0.8** | Microsoft |
| Microsoft.EntityFrameworkCore.Design | **6.0.8** | Microsoft |
| Microsoft.AspNetCore.Authentication | **2.2.0** | Microsoft |
| Microsoft.EntityFrameworkCore.Tools | **6.0.8** | Microsoft |
| Microsoft.EntityFrameworkCore | **6.0.8** | Microsoft |
| Swashbuckle.AspNetCore | **6.4.0** | Swashbuckle.AspNetCore |
| Newtonsoft.Json | **13.0.1** | James Newton-King |
| RabbitMQ.Client | **6.4.0** | VMware |
| SecureIdentity | **1.0.4** | André Baltieri |

------------------------------------------------
### Docker - RabbitMQ

_O **DockerFile** apresenta os dados de acesso da pataforma web do RabbitMQ e a porta_

------------------------------------------------
### Execução
_Para executar o projeto, siga os passos abaixo:_

**1 - CONECTE A APLICAÇÃO EM UMA BASE DE DADOS Microsoft SQL**
- Crie um banco vazio com o nome "StoreDB"
- Utilize o ususário "sa"
- Senha padrão "Senha12345"

**2 - EXECUTANDO A MIGRATION.**
 - certifique-se de que o caminho esteja apontando para o projeto **"Store.Repository"**
 - **Utilizando o CLI do .NET**
    - dotnet ef migrations add InitialCreate
    - dotnet ef database update 
 - **Utilizando o PowerShell no Visual Studio**
    - Add-Migration InitialCreate
    - Update-Database

**3 - JSON WEB TOKEN**
_Para gerar o token, será necessário criar um cadastro._
 - Acesse o endpoint **"v1/account"** e preencha os dados solicitados.
 - Acesso o endpoint **"v1/login"** e insira os dados solicitados. Um token será devolvido no Response body.
 - Utilize o token para acessar os outros endpoints.



[.NET 6 (DotNet6)]: <https://dotnet.microsoft.com/en-us/download/dotnet/6.0>
