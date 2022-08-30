# App Store
------------------------------------------------
### Framework:

_Versão do Framework necessária para rodar o projeto:_

- [.NET 6 (DotNet6)] 
    
    
------------------------------------------------ 
### NuGet

_Pacotes e versões necessárias para executar o projeto_


| Pacote | Versão |
| ------ | ------ |
| Microsoft.AspNetCore.Authentication | [2.2.0][PlDb] |
| GitHub | [plugins/github/README.md][PlGh] |
| Google Drive | [plugins/googledrive/README.md][PlGd] |
| OneDrive | [plugins/onedrive/README.md][PlOd] |
| Medium | [plugins/medium/README.md][PlMe] |
| Google Analytics | [plugins/googleanalytics/README.md][PlGa] |
------------------------------------------------
### Execução
_Para executar o projeto, siga os passos abaixo:_
**1 - CONECTE A APLICAÇÃO EM UMA BASE DE DADOS Microsoft SQL**
- Crie um banco vazio com o nome "StoreDB"
- Utilize o ususário "sa"
- Senha padrão "Senha12345"
- Caso queira personalisar os dados de acesso, vá até 			**"Store/Store.Repository/Context/StoreDataContext"** e altere os dados de conexão.

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
