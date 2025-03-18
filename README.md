# MyFood API

A API MyFood é uma aplicação desenvolvida em .NET para ajudar os usuários a registrar e acompanhar suas refeições diárias, calorias consumidas e macronutrientes (proteínas, carboidratos e gorduras). A API também oferece funcionalidade para definir metas nutricionais personalizadas, calcular as necessidades diárias de calorias e sugerir metas de macronutrientes.

Esta API foi criada para ser consumida pelo [MyFood UI](https://github.com/guilhermevmandrade/myfood-ui), que é a interface de usuário do sistema. A interação entre o front-end e a API permite que os usuários registrem suas refeições e metas nutricionais, e acompanhem seu progresso.

## Funcionalidades

- **Criação de usuários**: A API permite a criação de novos usuários, com seus dados salvos no banco de dados para utilização futura.
- **Registro de refeições**: Permite que os usuários registrem as refeições que consomem ao longo do dia.
- **Cálculo de calorias e macronutrientes**: Calcula as calorias e os macronutrientes de cada refeição, levando em consideração os alimentos registrados.
- **Metas nutricionais**: Permite que os usuários definam suas metas de calorias diárias e a porcentagem de cada macronutriente.
- **Sugestão de meta de calorias**: A API sugere uma meta diária de calorias com base no perfil do usuário (peso, altura, idade, nível de atividade).

## Tecnologias Utilizadas

- **.NET Core 6+**
- **Entity Framework Core**
- **PostgreSQL** (Banco de dados relacional)
- **Dapper** (Para otimização de consultas)
- **Swagger** (Documentação da API)

## Instalação

### Pré-requisitos

- **.NET SDK** (versão 6 ou superior)
- **PostgreSQL**

### Passos para instalação

1. **Clonar o repositório**

   Clone este repositório para sua máquina local:

   ```bash
   git clone https://github.com/guilhermevmandrade/myfood-api.git
   ```

2. **Configurar o banco de dados**

   Configure o banco de dados, criando a estrutura necessária e adicionando as tabelas. Você pode configurar a conexão com o banco de dados no arquivo `appsettings.json`.

3. **Instalar as dependências**

   Navegue até o diretório do projeto e execute o comando para restaurar as dependências:

   ```bash
   dotnet restore
   ```

4. **Executar as migrações do Entity Framework**

   Para criar as tabelas no banco de dados, execute:

   ```bash
   dotnet ef database update
   ```

5. **Rodar a API**

   Para rodar o projeto, use o seguinte comando:

   ```bash
   dotnet run
   ```

   A API estará disponível em `http://localhost:5000` ou conforme configurado no `launchSettings.json`.

## Swagger

A API está documentada e pode ser testada através do Swagger. Para acessar a documentação, basta iniciar o servidor da API e navegar até a seguinte URL:

```
http://localhost:5000/swagger
```

No Swagger, você poderá visualizar todos os endpoints disponíveis, testar as requisições diretamente e explorar a estrutura da API.   
